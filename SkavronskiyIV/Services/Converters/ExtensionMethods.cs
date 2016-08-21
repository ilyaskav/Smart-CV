using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Converters
{
    public static class ExtensionMethods
    {
        public static string Format(this DateTime? date)
        {
            if (date == null) return "наст. время";

            return string.Format("{0}/{1}", GetMonthName(date.Value.Month), date.Value.Year);
        }

        public static string Format(this DateTime date)
        {
            return string.Format("{0}/{1}", GetMonthName(date.Month), date.Year);
        }


        static string GetMonthName(int _month)
        {
            // переделать на return'ы
            var month = "";
            switch (_month)
            {
                case 1:
                    {
                        month = "Январь";
                        break;
                    }
                case 2:
                    {
                        month = "Февраль";
                        break;
                    }
                case 3:
                    {
                        month = "Март";
                        break;
                    }
                case 4:
                    {
                        month = "Февраль";
                        break;
                    }
                case 5:
                    {
                        month = "Апрель";
                        break;
                    }
                case 6:
                    {
                        month = "Июнь";
                        break;
                    }
                case 7:
                    {
                        month = "Июль";
                        break;
                    }
                case 8:
                    {
                        month = "Август";
                        break;
                    }
                case 9:
                    {
                        month = "Сентябрь";
                        break;
                    }
                case 10:
                    {
                        month = "Октябрь";
                        break;
                    }
                case 11:
                    {
                        month = "Ноябрь";
                        break;
                    }
                case 12:
                    {
                        month = "Декабрь";
                        break;
                    }
                default: break;
            }
            return month;
        }
    }
}
