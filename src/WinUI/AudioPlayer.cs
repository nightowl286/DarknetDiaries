using Caliburn.Micro;
using DarknetDiaries.Standard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace DarknetDiaries.WinUI
{
   internal class AudioPlayer : PropertyChangedBase, IAudioPlayer
   {
      #region Private
      private readonly MediaPlayer _Player;
      private readonly DispatcherTimer _Timer;
      private bool _IsPlaying;
      #endregion

      #region Properties
      public double Volume
      {
         get => _Player.Volume;
         set
         {
            _Player.Volume = value;
            NotifyOfPropertyChange();
         }
      }
      public TimeSpan Position
      {
         get => _Player.Position;
         set
         {
            _Player.Position = value;
            NotifyOfPropertyChange();
         }
      }
      public TimeSpan Duration => _Player.NaturalDuration.HasTimeSpan ? _Player.NaturalDuration.TimeSpan : TimeSpan.Zero;
      public bool IsPlaying { get => _IsPlaying; private set => Set(ref _IsPlaying, value); }
      public event System.Action? PlaybackFinished;
      #endregion
      public AudioPlayer()
      {
         _Player = new MediaPlayer();
         _Player.MediaEnded += Player_MediaEnded;

         _Timer = new DispatcherTimer()
         {
            Interval = TimeSpan.FromMilliseconds(15)
         };
         _Timer.Tick += Timer_Tick;
      }

      #region Events
      private void Timer_Tick(object? sender, EventArgs e)
      {
         NotifyOfPropertyChange(() => Position);
      }
      private void Player_MediaEnded(object? sender, EventArgs e)
      {
         _Timer.Stop();
         IsPlaying = false;
         NotifyOfPropertyChange(() => Position);

         if (PlaybackFinished != null)
            OnUIThread(PlaybackFinished);
      }
      #endregion

      #region Methods
      public void Play(Uri source)
      {
         _Player.Open(source);
         _Player.Play();
         EnsureDuration();
         IsPlaying = true;
         _Timer.Start();
      }
      public void TogglePlay()
      {
         if (IsPlaying)
         {
            _Player.Pause();
            _Timer.Stop();
            NotifyOfPropertyChange(() => Position);
         }
         else
         {
            _Player.Play();
            _Timer.Start();
         }

         IsPlaying = !IsPlaying;
      }
      public void Dispose()
      {
         // ensures there won't be a memory leak.
         _Player.MediaEnded -= Player_MediaEnded;
         _Timer.Stop();
         _Timer.Tick -= Timer_Tick;
      }
      #endregion

      #region Helpers
      private void EnsureDuration()
      {
         while (!_Player.NaturalDuration.HasTimeSpan)
            Thread.Sleep(10);
         NotifyOfPropertyChange(() => Duration);
      }
      #endregion
   }
}
