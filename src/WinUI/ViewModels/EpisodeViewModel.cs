using Caliburn.Micro;
using DarknetDiaries.Standard;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace DarknetDiaries.WinUI.ViewModels
{
   internal class EpisodeViewModel : PropertyChangedBase
   {
      #region Private
      private readonly ITimeStorage _TimeStorage;
      private readonly IWindowManager _WindowManager;
      private IEpisode _Episode;
      private double _WatchedPercent;
      #endregion

      #region Properties
      public IEpisode Episode { get => _Episode; private set => Set(ref _Episode, value); }
      public double WatchedPercent
      {
         get => _WatchedPercent;
         set
         {
            Set(ref _WatchedPercent, value);
            NotifyOfPropertyChange(() => IsFinished);
            NotifyOfPropertyChange(() => HasStarted);
            _TimeStorage.Save(Episode.Number, value);
         }
      }
      public bool IsFinished => WatchedPercent == 1;
      public bool HasStarted => WatchedPercent > 0;
      #endregion
      public EpisodeViewModel(IEpisode episode, ITimeStorage storage, IWindowManager windowManager)
      {
         _TimeStorage = storage;
         Change(episode);

         _WindowManager = windowManager;
      }

      #region Methods
      [MemberNotNull(nameof(_Episode))]
      public void Change(IEpisode episode)
      {
         Episode = episode;
         Debug.Assert(_Episode != null);
         _WatchedPercent = _TimeStorage.Get(episode.Number);
         NotifyOfPropertyChange(() => WatchedPercent);
         NotifyOfPropertyChange(() => HasStarted);
         NotifyOfPropertyChange(() => IsFinished);
      }
      public void Play()
      {
         var model = IoC.Get<PlayerViewModel>();
         model.SetEpisode(this);

         _WindowManager.ShowWindowAsync(model);

         IoC.Get<ShellViewModel>().TryCloseAsync();
      }
      #endregion
   }
}
