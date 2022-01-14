using DarknetDiaries.Standard;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace DarknetDiaries.Core
{
   public class TimeStorage : ITimeStorage
   {
      #region Private
      private const string PATH = "time.dat";
      private TimeSpan[] _Data;
      private static readonly TimeSpan _Finished = TimeSpan.FromSeconds(-1);
      #endregion
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
      public TimeStorage()
      {
         if (File.Exists(PATH))
            Load();
         else
            _Data = Array.Empty<TimeSpan>();
      }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

      #region Methods
      public TimeSpan Get(int episodeNumber, out bool isFinished)
      {
         TimeSpan time = TimeSpan.Zero;
         if (_Data.Length >= episodeNumber && episodeNumber >= 1)
            time = _Data[episodeNumber - 1];

         isFinished = time == _Finished;

         return time;
      }
      public void Save(int episodeNumber, TimeSpan time)
      {
         EnsureSize(episodeNumber);
         _Data[episodeNumber - 1] = time;
         Save();
      }
      public void SaveAsFinished(int episodeNumber) => Save(episodeNumber, _Finished);
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

         _Data = new TimeSpan[items];
         for (int i = 0; i < _Data.Length; i++)
         {
            int seconds = br.ReadInt32();
            _Data[i] = TimeSpan.FromSeconds(seconds);
         }
      }
      private void Save()
      {
         using FileStream fs = new FileStream(PATH, FileMode.Create);
         using BinaryWriter bw = new BinaryWriter(fs);

         foreach(TimeSpan time in _Data)
         {
            int seconds = (int)time.TotalSeconds;
            bw.Write(seconds);
         }

      }
      #endregion
   }
}
