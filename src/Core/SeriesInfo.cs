using DarknetDiaries.Standard;
using System;

namespace DarknetDiaries.Core
{
   internal class SeriesInfo : ISeriesInfo
   {
      #region Properties
      public string Title { get; set; }
      public Uri Link { get; set; }
      public Uri Image { get; set; }
      public string Description { get; set; }
      #endregion
      public SeriesInfo(string title, Uri link, Uri image, string description)
      {
         Title = title;
         Link = link;
         Image = image;
         Description = description;
      }
   }
}
