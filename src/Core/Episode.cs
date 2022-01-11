using DarknetDiaries.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace DarknetDiaries.Core
{
   internal class Episode : IEpisode
   {
      #region Properties
      public int Number { get; set; }
      public string Title { get; set; }
      public Uri? Image { get; set; }
      public Uri Audio { get; set; }
      public TimeSpan Duration { get; set; }
      #endregion
      public Episode(int number, string title, Uri audio)
      {
         Number = number;
         Title = title;
         Audio = audio;
      }
   }
}
