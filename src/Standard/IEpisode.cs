using System;

namespace DarknetDiaries.Standard
{
   public interface IEpisode
   {
      #region Properties
      int Number { get; }
      string Title { get; }
      Uri? Image { get; }
      Uri Audio { get; }
      TimeSpan Duration { get; }
      #endregion
   }
}
