﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWize.Utils.Converters
{
    public class TransactionNameColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Colors.White;
            }
            var random = new Random();
            var transactionNameColor = String.Format("#FF{0:X6}", random.Next(0x1000000));
            return Color.FromArgb(transactionNameColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
