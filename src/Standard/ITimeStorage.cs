namespace DarknetDiaries.Standard
{
   public interface ITimeStorage
   {
      #region Methods
      double Get(int episodeNumber);
      void Save(int episodeNumber, double timePercent);
      #endregion
   }
}
