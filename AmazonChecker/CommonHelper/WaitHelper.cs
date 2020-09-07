using System;
using System.Threading.Tasks;


namespace AmazonChecker.CommonHelper
{
    public class WaitHelper
    {
        public TimeSpan TimeOut;
        public WaitHelper(TimeSpan timeOut)
        {
            this.TimeOut = timeOut;
        }
        public TResult Until<TResult>(Func<TResult> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition", "condition cannot be null");
            }
            Type typeFromHandle = typeof(TResult);
            if ((typeFromHandle.IsValueType && typeFromHandle != typeof(bool)) || !typeof(object).IsAssignableFrom(typeFromHandle))
            {
                throw new ArgumentException("Can only wait on an object or boolean response, tried to use type: " + typeFromHandle.ToString(), "condition");
            }

            TResult result = default;
            Task runCondition = new Task(() =>
            {
                while (true)
                {
                    TResult tresult = condition();
                    if (typeFromHandle == typeof(bool))
                    {
                        bool? flag = tresult as bool?;
                        if (flag != null && flag.Value)
                        {
                            result = tresult;
                            break;
                        }
                    }
                    else if (tresult != null)
                    {
                        result = tresult;
                        break;
                    }
                };

            });
            runCondition.Start();

            Task.WhenAny(runCondition, Task.Delay(this.TimeOut)).Wait();

            return result;
        }
    }

    public class WaitHelper<T>
    {
        public TimeSpan TimeOut;
        public T Input;

        public WaitHelper(T input, TimeSpan timeOut)
        {
            this.Input = input;
            this.TimeOut = timeOut;
        }

        public TResult Until<TResult>(Func<T, TResult> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition", "condition cannot be null");
            }
            Type typeFromHandle = typeof(TResult);
            if ((typeFromHandle.IsValueType && typeFromHandle != typeof(bool)) || !typeof(object).IsAssignableFrom(typeFromHandle))
            {
                throw new ArgumentException("Can only wait on an object or boolean response, tried to use type: " + typeFromHandle.ToString(), "condition");
            }

            TResult result = default;
            Task runCondition = new Task(() =>
            {

                while (true)
                {
                    TResult tresult = condition(this.Input);
                    if (typeFromHandle == typeof(bool))
                    {
                        bool? flag = tresult as bool?;
                        if (flag != null && flag.Value)
                        {
                            result = tresult;
                            break;
                        }
                    }
                    else if (tresult != null)
                    {
                        result = tresult;
                        break;
                    }
                };
            });
            runCondition.Start();

            Task.WhenAny(runCondition, Task.Delay(this.TimeOut)).Wait();

            return result;
        }
    }
}
