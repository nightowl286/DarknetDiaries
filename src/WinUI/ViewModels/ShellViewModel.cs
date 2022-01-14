using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Caliburn.Micro;
using DarknetDiaries.Standard;

namespace DarknetDiaries.WinUI.ViewModels
{
   internal class ShellViewModel : PropertyChangedBase
   {
      #region Private
      private readonly IEpisodeFeed _Feed;
      private readonly ITimeStorage _TimeStorage;
      private readonly IWindowManager _WindowManager;
      private ISeriesInfo? _Info;
      private ObservableCollection<EpisodeViewModel> _Episodes = new ObservableCollection<EpisodeViewModel>();
      private bool _IsRefreshing;
      private EpisodeViewModel? _NextEpisode;
      #endregion

      #region Properties
      public ISeriesInfo? Info { get => _Info; private set => Set(ref _Info, value); }
      public ObservableCollection<EpisodeViewModel> Episodes => _Episodes;
      public EpisodeViewModel? NextEpisode { get => _NextEpisode; private set => Set(ref _NextEpisode, value); }
      #endregion
      public ShellViewModel(IEpisodeFeed feed, ITimeStorage timeStorage, IWindowManager windowManager)
      {
         _Feed = feed;
         _TimeStorage = timeStorage;
         _WindowManager = windowManager;
         RefreshFeed();
      }

      #region Methods
      public void Synchronise() => RefreshFeed();
      public bool CanSynchronise => !_IsRefreshing;
      private void RefreshFeed()
      {
         if (_IsRefreshing) return;

         _IsRefreshing = true;
         Task task = _Feed.RefreshData();

         task.ContinueWith(FeedRefreshed);
      }
      private void FeedRefreshed(Task task)
      {
         Info = _Feed.GetInfo();
         var episodes = _Feed.GetEpisodes();

         // replace or add episodes
         int epCounter = 0;
         foreach (IEpisode ep in episodes)
         {
            if (epCounter < _Episodes.Count)
               _Episodes[epCounter].Change(ep);
            else
            {
               EpisodeViewModel viewModel = new EpisodeViewModel(ep, _TimeStorage, _WindowManager);
               App.Current.Dispatcher.Invoke(
                  () => _Episodes.Add(viewModel));
            }
            epCounter++;
         }

         // remove later ones | should never actually be necessary but used as a precaution
         for (int i = _Episodes.Count - 1; i >= epCounter; i--)
            _Episodes.RemoveAt(i);

         // Select next episode
         EpisodeViewModel? nextEp = null;
         for(int i = _Episodes.Count - 1; i >= 0; i--)
         {
            if (nextEp?.IsFinished != true)
            {
               nextEp = _Episodes[i];
               break;
            }
         }
         NextEpisode = nextEp;

         _IsRefreshing = false;
         NotifyOfPropertyChange(() => CanSynchronise);
      }
      #endregion
   }
}
