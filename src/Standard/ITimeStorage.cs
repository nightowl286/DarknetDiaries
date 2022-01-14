using System;

namespace DarknetDiaries.Standard
{
   public interface ITimeStorage
   {
      #region Methods
      TimeSpan Get(int episodeNumber, out bool isFinished);
      void Save(int episodeNumber, TimeSpan time);
      void SaveAsFinished(int episodeNumber);
      #endregion
   }
}
