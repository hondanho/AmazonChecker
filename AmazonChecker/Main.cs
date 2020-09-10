using AmazonChecker.CommonHelper;
using AmazonChecker.Properties;
using AmazonChecker.Service;
using log4net;
using OfficeOpenXml;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace AmazonChecker
{
    public partial class Main : Form
    {
        private const string URL_DELIVER = @"https://www.amazon.com/dp/{0}";
        private const string XPATH_IMG_NOT_FOUND = "/html/body/div/div/a/img";

        private const string XPATH_DELIVER = "//*[@id='glow-ingress-line2']";

        private const string XPATH_PRICE1 = "//*[@id='priceblock_saleprice']";
        private const string XPATH_PRICE2 = "//*[@id='priceblock_ourprice']";

        private const string URL_PRIME = @"https://www.amazon.com/gp/offer-listing/{0}/ref=olp_f_primeEligible?ie=UTF8&f_primeEligible=true";
        private const string XPATH_PRIME = "//input[@type='checkbox'][@name='olpCheckbox_primeEligible']";
        private const string XPATH_PRIME_NOTE = "//*[@id='olpOfferList']//ul[@class='a-unordered-list a-vertical olpFastTrack']/li[1]";

        private const string XPATH_OUT_STOCK = "//*[@id='availability']/span";

        private const string XPATH_ZIPCODE = "//*[@id='GLUXZipUpdateInput']";
        private const string XPATH_SUBMIT_ZIPCODE = "//*[@id='GLUXZipUpdate']/span/input";
        private const string XPATH_CLOSE_ZIPCODE = "//*[@id='GLUXConfirmClose']";

        private const string YES = "Yes";
        private const string TRUE = "True";
        private const string NO = "No";

        private ChromeDriver _chromeDriver;
        private ILog _log;
        private Thread _thread;
        private bool IsRunningProcess = false;
        public delegate void LogInfo(string info);
        private bool IsRunningExcel = false;

        public Main()
        {
            InitializeComponent();
            InitSetting();
            this.lblStatus.Text = string.Empty;
            this.lblEnvironmentRun.Text = string.Empty;
        }

        private void InitSetting()
        {
            this._log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            this.tbLinkFileExcel.Text = (string)Settings.Default["PathFileExcel"];
            this.tbLinkGoogleSheets.Text = (string)Settings.Default["LinkGoogleSheet"];
            this.IsRunningExcel = (bool)Settings.Default["IsRunningExcel"];
            this.cbHideBrowser.Checked = (bool)Settings.Default["IsShowBrowser"];
            this.tabRunning.SelectedTab = this.IsRunningExcel ? this.tabFileExcel : this.tabGoogleSheets;
            this.lblEnvironmentRun.Text = "Chạy " + (this.IsRunningExcel ? this.tabFileExcel.Text : this.tabGoogleSheets.Text);
        }

        private void btnSelectFileExcel_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"; ;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    this.tbLinkFileExcel.Text = fbd.FileName;
                }
            }
        }

        private void tbLinkFileExcel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    this.tbLinkFileExcel.Text = fbd.FileName;
                }
            }
        }

        public bool AmazonCheckProduct(BaseChecker baseChecker)
        {
            if (baseChecker != null)
            {
                var totalRecord = baseChecker.FirstSheetDatas.Count - 1;
                StartProcess(totalRecord);
                var isChangeDeliver = false;

                for (int i = 1; i <= totalRecord; i++)
                {
                    var row = baseChecker.FirstSheetDatas[i];

                    try
                    {
                        // column 2: link product
                        var linkProduct = row[1]?.ToString();
                        var asinCode = row[0]?.ToString();

                        if (asinCode == null || string.IsNullOrEmpty(linkProduct) || !(Uri.IsWellFormedUriString(linkProduct, UriKind.Absolute)))
                        {
                            throw new Exception("\"Không có mã sản phẩm\" or \"không tồn tại link sản phẩm\" or \"Link sản phẩm không hơp lệ\"");
                        }
                        else
                        {
                            // check url exist 
                            _chromeDriver.Navigate().GoToUrl(linkProduct);
                            var imgNotFoundPage = _chromeDriver.FindWebElement(XPATH_IMG_NOT_FOUND);
                            if (imgNotFoundPage != null)
                            {
                                var altImgNotFoundPage = imgNotFoundPage.GetAttribute("alt");
                                string messageNotFoundPage = "Sorry! We couldn't find that page";
                                if (altImgNotFoundPage.ToLower().Contains(messageNotFoundPage.ToLower()))
                                {
                                    throw new Exception("Link sản phẩm sai");
                                }
                            }

                            //if (this.IsRunningExcel)
                            //{
                            //    foreach (var colum in row)
                            //    {

                            //    }
                            //    for (int j = data; j <= excelWorksheet.Dimension.End.Column; j++)
                            //    {
                            //        excelWorksheet.Cells[i, j].Style.Font.Color.SetColor(Color.Black);
                            //    }
                            //}

                            // column 4: price
                            var price = row[3]?.ToString();
                            var price1 = _chromeDriver.FindWebElement(XPATH_PRICE1);
                            if (price1 != null && !string.IsNullOrEmpty(price1.Text))
                            {
                                price = price1.Text.Replace("$", "");
                            }
                            else
                            {
                                var price2 = _chromeDriver.FindWebElement(XPATH_PRICE2);
                                if (price2 != null && !string.IsNullOrEmpty(price2.Text))
                                {
                                    price = price2.Text.Replace("$", "");
                                }
                            }
                            if (row[3] == null || price == null || !price.ToString().Equals(row[3]?.ToString()))
                            {
                                row[3] = price;
                                //excelWorksheet.Cells[i, 4].Style.Font.Color.SetColor(Color.Red);
                            }

                            if (!isChangeDeliver)
                            {
                                isChangeDeliver = ChangeDeliverVnToUs(asinCode);
                            }

                            if (isChangeDeliver)
                            {
                                //column 6: out stock
                                _chromeDriver.Navigate().GoToUrl(linkProduct);
                                var lstOutStock = _chromeDriver.FindWebElements(XPATH_OUT_STOCK);
                                var isInStock = false;
                                foreach (var outStock in lstOutStock)
                                {
                                    if (outStock != null && !string.IsNullOrEmpty(outStock.Text) && outStock.Text.ToLower().Contains("In Stock".ToLower()))
                                    {
                                        isInStock = true;
                                        break;
                                    }
                                }
                                if (row[5] == null || isInStock != row[5]?.ToString().ToLower().Equals(YES.ToLower()))
                                {
                                    row[5] = isInStock ? YES : NO;
                                    //excelWorksheet.Cells[i, 6].Style.Font.Color.SetColor(Color.Red);
                                }

                                //// column 3: prime
                                _chromeDriver.Navigate().GoToUrl(string.Format(URL_PRIME, asinCode));
                                var prime = _chromeDriver.FindWebElement(XPATH_PRIME);
                                var isPrime = (prime != null
                                                && !string.IsNullOrEmpty(prime.GetProperty("checked"))
                                                && prime.GetProperty("checked").ToLower().Equals(TRUE.ToLower())
                                                && isInStock
                                            )
                                            ? true : false;
                                if (prime != null)
                                {
                                    var primeNote = _chromeDriver.FindWebElement(XPATH_PRIME_NOTE);
                                    while(row.Count < 9)
                                    {
                                        row.Add(new object());
                                    }
                                    row[8] = primeNote.Text;
                                }
                                if (row[2] == null || isPrime != row[2].ToString().ToLower().Equals(YES.ToLower()))
                                {
                                    row[2] = isPrime ? YES : NO;
                                    //excelWorksheet.Cells[i, 3].Style.Font.Color.SetColor(Color.Red);
                                }
                            }
                            else
                            {
                                throw new Exception("Không đổi deliver sang us được");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex.Message);
                    }

                    UpdateProcessStatus(i, totalRecord);
                }

               return baseChecker.Save();
            }

            return false;
        }

        private bool ChangeDeliverVnToUs(string productCode)
        {
            if (string.IsNullOrEmpty(productCode)) return false;
            return new WaitHelper(TimeSpan.FromSeconds(20)).Until(() =>
            {
                var result = false;

                try
                {
                    _chromeDriver.Navigate().GoToUrl(string.Format(URL_DELIVER, productCode));
                    var deliverLocation = _chromeDriver.FindWebElement(XPATH_DELIVER);
                    while (deliverLocation == null || string.IsNullOrEmpty(deliverLocation.Text))
                    {
                        deliverLocation = _chromeDriver.FindWebElement(XPATH_DELIVER);
                    }

                    if (deliverLocation.Text.ToLower().Contains("New York".ToLower()))
                    {
                        result = true;
                    }
                    else
                    {
                        deliverLocation.Click();
                        var inputZipCode = _chromeDriver.FindWebElement(XPATH_ZIPCODE);
                        while (inputZipCode == null)
                        {
                            inputZipCode = _chromeDriver.FindWebElement(XPATH_ZIPCODE);
                        }
                        Thread.Sleep(200);
                        inputZipCode.SendKeys("10004");

                        var submitZipCode = _chromeDriver.FindWebElement(XPATH_SUBMIT_ZIPCODE);
                        while (submitZipCode == null)
                        {
                            submitZipCode = _chromeDriver.FindWebElement(XPATH_SUBMIT_ZIPCODE);
                        }
                        Thread.Sleep(200);
                        submitZipCode.Click();

                        var btnCloseZipCode = _chromeDriver.FindWebElements(XPATH_CLOSE_ZIPCODE);
                        while (!btnCloseZipCode.Any())
                        {
                            btnCloseZipCode = _chromeDriver.FindWebElements(XPATH_CLOSE_ZIPCODE);
                        }

                        Thread.Sleep(200);
                        btnCloseZipCode.FirstOrDefault().Click();
                        deliverLocation = _chromeDriver.FindWebElement(XPATH_DELIVER);
                        while (deliverLocation == null || string.IsNullOrEmpty(deliverLocation.Text))
                        {
                            deliverLocation = _chromeDriver.FindWebElement(XPATH_DELIVER);
                        }
                        result = deliverLocation.Text.ToLower().Contains("New York".ToLower());
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message);
                }

                return result;
            });
        }

        private void StartProcess(int totalAmount)
        {
            _chromeDriver = ChromeHelper.InitWebDriver(this.cbHideBrowser.Checked);
            this.Invoke(new Action<int>((index) =>
            {
                this.lblStatus.Text = string.Format("0/{0}", totalAmount);
                this.progressStatus.Maximum = index;
                this.progressStatus.Minimum = 0;
                this.progressStatus.Value = 0;
                this.progressStatus.Refresh();
            }), totalAmount);
        }

        private void UpdateProcessStatus(int amount, int totalAmount)
        {
            this.Invoke(new Action<int, int>((i, j) =>
            {
                this.lblStatus.Text = string.Format("{0}/{1}", i, j);
                this.progressStatus.Value = i;
            }), amount, totalAmount);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var isRunningExcel = this.tabRunning.SelectedTab == this.tabFileExcel;
            BaseChecker baseChecker;

            var messageError = isRunningExcel ? (string.IsNullOrEmpty(this.tbLinkFileExcel.Text) ? "File không hợp lệ hoặc không tồn tại":"")
                                               : (string.IsNullOrEmpty(this.tbLinkGoogleSheets.Text) ? "Link google sheets không được trống" : ""); 
            if (!string.IsNullOrEmpty(messageError))
            {
                Notify.Warning(messageError);
                return;
            }
            _thread = new Thread(() =>
            {
                try
                {
                    if (isRunningExcel)
                    {
                        baseChecker = new ExcelChecker(this.tbLinkFileExcel.Text);
                    }
                    else
                    {
                        baseChecker = new GoogleSheetsChecker(this.tbLinkGoogleSheets.Text);
                    }
                    if (AmazonCheckProduct(baseChecker))
                    {
                        Notify.Info("Hoàn thành");
                    }
                    else
                    {
                        Notify.Warning("Thất bại");
                    }
                }
                catch (ThreadAbortException ex)
                {
                    _log.Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message);
                    Notify.Warning(ex.Message);
                }
                finally
                {
                    StopProcess();
                }
            });
            _thread.Start();
            this.btnRun.Enabled = false;
            this.btnRun.Text = "Running..";
            this.btnStop.Enabled = true;
            this.IsRunningProcess = true;
            this.tabRunning.Enabled = true;
            this.cbHideBrowser.Enabled = true;
            this.lblEnvironmentRun.Text = "Chạy " + (this.IsRunningExcel ? this.tabFileExcel.Text : this.tabGoogleSheets.Text);
        }

        private void FormAmazonChecker_FormClosing(object sender, FormClosingEventArgs e)
        {
            AskStop();
        }

        private void StopProcess()
        {
            

            if (this._chromeDriver != null)
            {
                this._chromeDriver.Dispose();
            }

            this.Invoke(new Action(() =>
            {
                this.progressStatus.Value = 0;
                this.progressStatus.Maximum = 0;
                this.progressStatus.Minimum = 0;
                this.progressStatus.Refresh();
                this.btnRun.Enabled = true;
                this.lblEnvironmentRun.Text = string.Empty;
                this.lblStatus.Text = string.Empty;
                this.btnStop.Enabled = false;
                this.btnRun.Text = "Run";
                this.IsRunningProcess = false;
            }));

            if (_thread != null)
            {
                _thread.Abort();
            }
        }

        private void AskStop()
        {
            if (this.IsRunningProcess)
            {
                var confirm = MessageBox.Show(this, "Bạn có muốn dừng lại không?", "Xác nhận", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    StopProcess();
                }
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            AskStop();
        }

        private void tbLinkFileExcel_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["PathFileExcel"] = this.tbLinkFileExcel.Text;
            Settings.Default.Save();
        }

        private void tbLinktbGoogleSheets_TextChanged(object sender, EventArgs e)
        {
            Settings.Default["LinkGoogleSheet"] = this.tbLinkGoogleSheets.Text;
            Settings.Default.Save();
        }

        private void tabFileExccel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsRunningProcess)
            {
                this.IsRunningExcel = this.tabRunning.SelectedTab == this.tabFileExcel;
                Settings.Default["IsRunningExcel"] = this.IsRunningExcel;
                Settings.Default.Save();
                this.lblEnvironmentRun.Text = "Chạy " + (this.IsRunningExcel ? this.tabFileExcel.Text : this.tabGoogleSheets.Text);
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            Settings.Default["IsShowBrowser"] = this.cbHideBrowser.Checked;
            Settings.Default.Save();
        }
    }
}
