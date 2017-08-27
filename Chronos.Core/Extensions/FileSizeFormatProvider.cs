using System;

namespace Chronos.Core.Extensions
{
    public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        private const string FileSizeFormat = "fs";
        private const decimal OneKiloByte = 1024m;
        private const decimal OneMegaByte = 1048576m;
        private const decimal OneGigaByte = 1073741824m;

        public object GetFormat(Type formatType)
        {
            object result;
            if (formatType == typeof(ICustomFormatter))
            {
                result = this;
            }
            else
            {
                result = null;
            }
            return result;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string result;
            if (format == null || !format.StartsWith("fs"))
            {
                result = FileSizeFormatProvider.DefaultFormat(format, arg, formatProvider);
            }
            else
            {
                if (arg is string)
                {
                    result = FileSizeFormatProvider.DefaultFormat(format, arg, formatProvider);
                }
                else
                {
                    decimal num;
                    try
                    {
                        num = Convert.ToDecimal(arg);
                    }
                    catch
                    {
                        result = FileSizeFormatProvider.DefaultFormat(format, arg, formatProvider);
                        return result;
                    }
                    string arg2;
                    if (num > 1073741824m)
                    {
                        num /= 1073741824m;
                        arg2 = "GB";
                    }
                    else
                    {
                        if (num > 1048576m)
                        {
                            num /= 1048576m;
                            arg2 = "MB";
                        }
                        else
                        {
                            if (num > 1024m)
                            {
                                num /= 1024m;
                                arg2 = "kB";
                            }
                            else
                            {
                                arg2 = " B";
                            }
                        }
                    }
                    string text = format.Substring(2);
                    if (string.IsNullOrEmpty(text))
                    {
                        text = "2";
                    }
                    result = string.Format("{0:N" + text + "}{1}", num, arg2);
                }
            }
            return result;
        }

        private static string DefaultFormat(string format, object arg, IFormatProvider formatProvider)
        {
            IFormattable formattable = arg as IFormattable;
            string result;
            if (formattable != null)
            {
                result = formattable.ToString(format, formatProvider);
            }
            else
            {
                result = arg.ToString();
            }
            return result;
        }
    }
}