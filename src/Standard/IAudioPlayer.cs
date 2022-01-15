using System;
using System.ComponentModel;

namespace DarknetDiaries.Standard
{
   public interface IAudioPlayer : INotifyPropertyChanged, IDisposable
   {
      #region Properties
      double Volume { get; set; }
      TimeSpan Position { get; set; }
      TimeSpan Duration { get; }
      bool IsPlaying { get; }
      event Action PlaybackFinished;
      #endregion

      #region Methods
      void TogglePlay();
      void Play(Uri source);
      #endregion
   }
}
