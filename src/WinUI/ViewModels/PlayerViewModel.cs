using Caliburn.Micro;
using DarknetDiaries.Standard;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DarknetDiaries.WinUI.ViewModels
{
   internal class PlayerViewModel : Screen
   {
      #region Private
      private readonly IWindowManager _WindowManager;
      private readonly IAudioPlayer _Player;
      private readonly ITimeStorage _TimeStorage;
      private EpisodeViewModel? _WatchedEpisode;
      private IEpisode? _Episode;
      private readonly TimeSpan _SkipAmount = TimeSpan.FromSeconds(15);
      #endregion

      #region Properties
      public IEpisode? Episode { get => _Episode; private set => Set(ref _Episode, value); }
      public IAudioPlayer Player => _Player;
      #endregion
      public PlayerViewModel(IWindowManager windowManager, IAudioPlayer player, ITimeStorage timeStorage)
      {
         _WindowManager = windowManager;
         _Player = player;
         _TimeStorage = timeStorage;
         player.PlaybackFinished += Player_PlaybackFinished;
      }

      #region Events
      private void Player_PlaybackFinished()
      {
         _TimeStorage.Save(_Episode!.Number, 1);
         _WatchedEpisode!.WatchedPercent = 1;
      }
      #endregion

      #region Methods
      public void SetEpisode(EpisodeViewModel episode)
      {
         _WatchedEpisode = episode;
         _Episode = episode.Episode;
      }
      protected override Task OnActivateAsync(CancellationToken cancellationToken)
      {
         _Player.Play(Episode!.Audio);
         _Player.Position = _WatchedEpisode!.WatchedPercent * _Player.Duration;

         return base.OnActivateAsync(cancellationToken);
      }
      public async override Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
      {
         if (_Player.IsPlaying)
         {
            SaveWatchedPercent();
            _Player.TogglePlay();
         }

         _Player.PlaybackFinished -= Player_PlaybackFinished;
         ShellViewModel shell = IoC.Get<ShellViewModel>();

         await _WindowManager.ShowWindowAsync(shell);
         return await base.CanCloseAsync(cancellationToken);
      }
      public void TogglePause()
      {
         if (_Player.IsPlaying)
            SaveWatchedPercent();
         _Player.TogglePlay();
      }
      public void GoBackward() 
      {
         TimeSpan newPosition = _Player.Position.Subtract(_SkipAmount);
         if (newPosition < TimeSpan.Zero)
            newPosition = TimeSpan.Zero;

         _Player.Position = newPosition;
         SaveWatchedPercent();
      }
      public void GoForward() 
      {
         TimeSpan newPosition = _Player.Position.Add(_SkipAmount);
         if (newPosition > _Player.Duration)
            newPosition = _Player.Duration;

         _Player.Position = newPosition;
         SaveWatchedPercent();
      }
      #endregion

      #region Helpers
      private void SaveWatchedPercent()
      {
         double percent = _Player.Position / _Player.Duration;
         _WatchedEpisode!.WatchedPercent = percent;

         _TimeStorage.Save(_Episode!.Number, percent);
      }
      #endregion
   }
}
