using AmazonChecker.CommonHelper;
using AmazonChecker.Properties;
using log4net;
using OfficeOpenXml;
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

        private ChromeHelper _chromeDriver;
        private ILog _log;
        private Thread _thread;
        private bool IsRunningProcess = false;
        public delegate void LogInfo(string info);
        private bool IsRunningExcel = false;

        public Main()
        {
            InitializeComponent();
            InitSetting();
        }

        private void InitSetting()
        {
            this._log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            this.tbLinkFileExcel.Text = (string)Settings.Default["PathFileExcel"];
            this.tbLinkGoogleSheets.Text = (string)Settings.Default["LinkGoogleSheet"];
            this.IsRunningExcel = (bool)Settings.Default["isRunningExcel"];
            this.tabRunning.SelectedTab = this.IsRunningExcel ? this.tabFileExcel : this.tabGoogleSheets;
            Console.Read();
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

        public void AmazonCheckProduct()
        {
            //Create a test file
            if (string.IsNullOrEmpty(this.tbLinkFileExcel.Text) || !File.Exists(this.tbLinkFileExcel.Text))
            {
                Notify.Warning("File không tồn tại hoặc không hợp lệ!");
                return;
            }

            FileInfo file = new FileInfo(this.tbLinkFileExcel.Text);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.First();

                var totalRecode = excelWorksheet.Dimension.End.Row;
                StartProcess(totalRecode - 1);
                var isChangeDeliver = false;

                for (int i = 2; i <= totalRecode; i++)
                {
                    try
                    {
                        // column 2: link product
                        var linkProduct = excelWorksheet.Cells[i, 2];
                        var asinCode = excelWorksheet.Cells[i, 1];

                        if (asinCode == null || string.IsNullOrEmpty(linkProduct.Text) || !(Uri.IsWellFormedUriString(linkProduct.Text, UriKind.Absolute)))
                        {
                            throw new Exception("\"Không có mã sản phẩm\" or \"không tồn tại link sản phẩm\" or \"Link sản phẩm không hơp lệ\"");
                        }
                        else
                        {
                            // check url exist 
                            _chromeDriver.Navigate().GoToUrl(linkProduct.Text);
                            var imgNotFoundPage = _chromeDriver.FindElement(XPATH_IMG_NOT_FOUND);
                            if (imgNotFoundPage != null)
                            {
                                var altImgNotFoundPage = imgNotFoundPage.GetAttribute("alt");
                                string messageNotFoundPage = "Sorry! We couldn't find that page";
                                if (altImgNotFoundPage.ToLower().Contains(messageNotFoundPage.ToLower()))
                                {
                                    throw new Exception("Link sản phẩm sai");
                                }
                            }

                            for (int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                            {
                                excelWorksheet.Cells[i, j].Style.Font.Color.SetColor(Color.Black);
                            }

                            // column 4: price
                            var price = excelWorksheet.Cells[i, 4].Value;
                            var price1 = _chromeDriver.FindElement(XPATH_PRICE1);
                            if (price1 != null && !string.IsNullOrEmpty(price1.Text))
                            {
                                price = price1.Text.Replace("$", "");
                            }
                            else
                            {
                                var price2 = _chromeDriver.FindElement(XPATH_PRICE2);
                                if (price2 != null && !string.IsNullOrEmpty(price2.Text))
                                {
                                    price = price2.Text.Replace("$", "");
                                }
                            }
                            if (excelWorksheet.Cells[i, 4].Value == null || price == null || !price.ToString().Equals(excelWorksheet.Cells[i, 4].Value.ToString()))
                            {
                                excelWorksheet.Cells[i, 4].Value = price;
                                excelWorksheet.Cells[i, 4].Style.Font.Color.SetColor(Color.Red);
                            }

                            if (!isChangeDeliver)
                            {
                                isChangeDeliver = ChangeDeliverVnToUs(asinCode.Text);
                            }

                            if (isChangeDeliver)
                            {
                                //column 6: out stock
                                var lstOutStock = _chromeDriver.FindElements(XPATH_OUT_STOCK);
                                var isInStock = false;
                                foreach (var outStock in lstOutStock)
                                {
                                    if (outStock != null && !string.IsNullOrEmpty(outStock.Text) && outStock.Text.ToLower().Contains("In Stock".ToLower()))
                                    {
                                        isInStock = true;
                                        break;
                                    }
                                }
                                if (excelWorksheet.Cells[i, 6].Value == null || isInStock != excelWorksheet.Cells[i, 6].Value.ToString().ToLower().Equals(YES.ToLower()))
                                {
                                    excelWorksheet.Cells[i, 6].Value = isInStock ? YES : NO;
                                    excelWorksheet.Cells[i, 6].Style.Font.Color.SetColor(Color.Red);
                                }

                                //// column 3: prime
                                _chromeDriver.Navigate().GoToUrl(string.Format(URL_PRIME, asinCode.Value));
                                var prime = _chromeDriver.FindElement(XPATH_PRIME);
                                var isPrime = (prime != null
                                                && !string.IsNullOrEmpty(prime.GetProperty("checked"))
                                                && prime.GetProperty("checked").ToLower().Equals(TRUE.ToLower())
                                                && isInStock
                                            )
                                            ? true : false;
                                if (prime != null)
                                {
                                    var primeNote = _chromeDriver.FindElement(XPATH_PRIME_NOTE);
                                    excelWorksheet.Cells[i, 9].Value = primeNote.Text;
                                }
                                if (excelWorksheet.Cells[i, 3].Value == null || isPrime != excelWorksheet.Cells[i, 3].Value.ToString().ToLower().Equals(YES.ToLower()))
                                {
                                    excelWorksheet.Cells[i, 3].Value = isPrime ? YES : NO;
                                    excelWorksheet.Cells[i, 3].Style.Font.Color.SetColor(Color.Red);
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

                    UpdateProcessStatus(i - 1, totalRecode - 1);
                }

                excelPackage.Save();
            }
        }

        private bool ChangeDeliverVnToUs(string productCode)
        {
            if (string.IsNullOrEmpty(productCode)) return false;
            return new WaitHelper(TimeSpan.FromSeconds(10)).Until(() =>
            {
                var result = false;

                try
                {
                    _chromeDriver.Navigate().GoToUrl(string.Format(URL_DELIVER, productCode));
                    var deliverLocation = _chromeDriver.FindElement(XPATH_DELIVER);
                    while (deliverLocation == null || string.IsNullOrEmpty(deliverLocation.Text))
                    {
                        deliverLocation = _chromeDriver.FindElement(XPATH_DELIVER);
                    }

                    if (deliverLocation.Text.ToLower().Contains("New York".ToLower()))
                    {
                        result = true;
                    }
                    else
                    {
                        deliverLocation.Click();
                        var inputZipCode = _chromeDriver.FindElement(XPATH_ZIPCODE);
                        while (inputZipCode == null)
                        {
                            inputZipCode = _chromeDriver.FindElement(XPATH_ZIPCODE);
                        }
                        inputZipCode.SendKeys("10004");

                        var submitZipCode = _chromeDriver.FindElement(XPATH_SUBMIT_ZIPCODE);
                        while (submitZipCode == null)
                        {
                            submitZipCode = _chromeDriver.FindElement(XPATH_SUBMIT_ZIPCODE);
                        }
                        submitZipCode.Click();

                        var btnCloseZipCode = _chromeDriver.FindElements(XPATH_CLOSE_ZIPCODE);
                        while (!btnCloseZipCode.Any())
                        {
                            btnCloseZipCode = _chromeDriver.FindElements(XPATH_CLOSE_ZIPCODE);
                        }

                        btnCloseZipCode.FirstOrDefault().Click();
                        deliverLocation = _chromeDriver.FindElement(XPATH_DELIVER);
                        while (deliverLocation == null || string.IsNullOrEmpty(deliverLocation.Text))
                        {
                            deliverLocation = _chromeDriver.FindElement(XPATH_DELIVER);
                        }
                        result = deliverLocation.Text.ToLower().Contains("New York".ToLower());
                    }
                }
                catch
                {
                }

                return result;
            });
        }

        private void StartProcess(int totalAmount)
        {
            _chromeDriver = new ChromeHelper();
            this.Invoke(new Action<int>((index) =>
            {
                this.lbStatus.Text = string.Format("0/{0}", totalAmount);
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
                this.lbStatus.Text = string.Format("{0}/{1}", i, j);
                this.progressStatus.Value = i;
            }), amount, totalAmount);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var isRunningExcel = this.tabRunning.SelectedTab == this.tabFileExcel;
            _thread = new Thread(() =>
            {
                if (isRunningExcel)
                {
                    AmazonCheckProduct();
                }
                else
                {

                }
                Notify.Info("Hoàn thành");
                StopProcess();
            });
            _thread.Start();
            this.btnRun.Enabled = false;
            this.btnRun.Text = "Running..";
            this.btnStop.Enabled = true;
            this.IsRunningProcess = true;
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
                this.btnRun.Enabled = true;
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
            this.IsRunningExcel = this.tabRunning.SelectedTab == this.tabFileExcel;
            Settings.Default["isRunningExcel"] = this.IsRunningExcel;
            Settings.Default.Save();
            this.lbEnvironmentRun.Text = "Chạy " + (this.IsRunningExcel ? this.tabFileExcel.Text : this.tabGoogleSheets.Text);
        }
    }
}
