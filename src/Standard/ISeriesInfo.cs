using System;

namespace DarknetDiaries.Standard
{
   public interface ISeriesInfo
   {
      #region Properties
      string Title { get; }
      Uri Link { get; }
      Uri Image { get; }
      string Description { get; }
      #endregion
   }
}
