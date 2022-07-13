using System;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CPD.Admin
{
    class Base
    {
    }

    [ValueConversion(typeof(DateTime), typeof(String))]
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Type lType = value.GetType();
            if (lType.Name == "DBNull")
            {
                return "";
            }
            else
            {
                DateTime lDate;
                lDate = (DateTime)value;
                return lDate.ToString("dd MMM yyyy HH mm");
            }
          
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(string), typeof(int))]
    public class IntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int lint;
            if (String.IsNullOrWhiteSpace(value.ToString()))
            {
                lint = 0;
            }
            else
            {
                lint = Int32.Parse(value.ToString());
            }

            return lint;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string lResult = value.ToString();
            lResult = lResult.ToString();
            return lResult;
        }
    }




    [ValueConversion(typeof(Decimal), typeof(String))]
    public class RandConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Decimal lRand;
            if(String.IsNullOrWhiteSpace(value.ToString()))
            {
                lRand = 0;
            }
            else
            {
            lRand = (Decimal)value;
            }

            return lRand.ToString("########0.00");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
