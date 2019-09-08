using System;

namespace SmartCV.Service.Converters
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
            switch (_month)
            {
                case 1:
                    {
                        return "Январь";
                    }
                case 2:
                    {
                        return "Февраль";
                    }
                case 3:
                    {
                        return "Март";
                    }
                case 4:
                    {
                        return "Февраль";
                    }
                case 5:
                    {
                        return "Апрель";
                    }
                case 6:
                    {
                        return "Июнь";
                    }
                case 7:
                    {
                        return "Июль";
                    }
                case 8:
                    {
                        return "Август";
                    }
                case 9:
                    {
                        return "Сентябрь";
                    }
                case 10:
                    {
                        return "Октябрь";
                    }
                case 11:
                    {
                        return "Ноябрь";
                    }
                case 12:
                    {
                        return "Декабрь";
                    }
            }
            return null;
        }
    }
}
