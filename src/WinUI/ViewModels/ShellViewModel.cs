using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DarknetDiaries.Standard;

namespace DarknetDiaries.WinUI.ViewModels
{
   internal class ShellViewModel : PropertyChangedBase
   {
      #region Private
      private readonly IEpisodeFeed _Feed;
      private ISeriesInfo? _Info;
      private ObservableCollection<IEpisode> _Episodes = new ObservableCollection<IEpisode>();
      #endregion

      #region Properties
      public ISeriesInfo? Info { get => _Info; private set => Set(ref _Info, value); }
      public ObservableCollection<IEpisode> Episodes => _Episodes;
      #endregion
      public ShellViewModel(IEpisodeFeed feed)
      {
         _Feed = feed;
         RefreshFeed();
      }

      #region Methods
      private void RefreshFeed()
      {
         Task task = _Feed.RefreshData();

         task.ContinueWith(FeedRefreshed);
      }
      private void FeedRefreshed(Task task)
      {
         Info = _Feed.GetInfo();
         var episodes = _Feed.GetEpisodes().ToList();

         // replace or add episodes
         for (int i = 0; i < episodes.Count; i++)
         {
            if (i < _Episodes.Count)
               _Episodes[i] = episodes[i];
            else
               _Episodes.Add(episodes[i]);
         }

         // remove later ones | should never actually be necessary but used as a precaution
         for (int i = _Episodes.Count - 1; i >= episodes.Count; i--)
            _Episodes.RemoveAt(i);
      }
      #endregion
   }
}
