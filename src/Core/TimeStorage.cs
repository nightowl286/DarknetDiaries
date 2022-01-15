using DarknetDiaries.Standard;
using System;
using System.IO;

namespace DarknetDiaries.Core
{
   public class TimeStorage : ITimeStorage
   {
      #region Private
      private const string PATH = "time.dat";
      private double[] _Data;
      #endregion
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
      public TimeStorage()
      {
         if (File.Exists(PATH))
            Load();
         else
            _Data = Array.Empty<double>();
      }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

      #region Methods
      public double Get(int episodeNumber)
      {
         double time = 0;
         if (_Data.Length >= episodeNumber && episodeNumber >= 1)
            time = _Data[episodeNumber - 1];

         return time;
      }
      public void Save(int episodeNumber, double timePercent)
      {
         EnsureSize(episodeNumber);
         _Data[episodeNumber - 1] = timePercent;
         Save();
      }
      #endregion

      #region Helpers
      private void EnsureSize(int episodeNumber)
      {
         if (_Data.Length < episodeNumber)
            Array.Resize(ref _Data, episodeNumber);
      }
      private void Load()
      {
         using FileStream fs = new FileStream(PATH, FileMode.Open);
         using BinaryReader br = new BinaryReader(fs);
         int items = (int)(fs.Length / sizeof(int));

         _Data = new double[items];
         for (int i = 0; i < _Data.Length; i++)
         {
            double percent = br.ReadDouble();
            _Data[i] = percent;
         }
      }
      private void Save()
      {
         using FileStream fs = new FileStream(PATH, FileMode.Create);
         using BinaryWriter bw = new BinaryWriter(fs);

         foreach (double timePercent in _Data)
            bw.Write(timePercent);
      }
      #endregion
   }
}
