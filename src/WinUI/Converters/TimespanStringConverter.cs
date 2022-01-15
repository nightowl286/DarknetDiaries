using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace DarknetDiaries.WinUI.Converters
{
   internal class TimespanStringConverter : IValueConverter
   {
      #region Methods
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value is not TimeSpan ts)
            throw new ArgumentException("The value type must be a TimeSpan.", nameof(value));

         if (targetType != typeof(string))
            throw new ArgumentException("The target type must be a string.", nameof(targetType));

         List<string> parts = new List<string>();
         if (parts.Count > 0 || (ts.Days > 0 || ts.Hours > 0))
         {
            int hours = (ts.Days * 24) + ts.Hours;
            parts.Add($"{hours}h");
         }
         if (parts.Count > 0 || ts.Minutes > 0)
            parts.Add($"{ts.Minutes}m");

         parts.Add($"{ts.Seconds}s");

         return string.Join(" ", parts);
      }
      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new InvalidOperationException();
      }
      #endregion
   }
}
