using Caliburn.Micro;
using DarknetDiaries.Standard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarknetDiaries.WinUI.ViewModels
{
   internal class EpisodeViewModel : PropertyChangedBase
   {
      #region Private
      private readonly ITimeStorage _TimeStorage;
      private readonly IWindowManager _WindowManager;
      private IEpisode _Episode;
      private TimeSpan _WatchedTime;
      #endregion

      #region Properties
      public IEpisode Episode { get => _Episode; private set => Set(ref _Episode, value); }
      public TimeSpan WatchedTime
      {
         get => _WatchedTime;
         set
         {
            Set(ref _WatchedTime, value);
            NotifyOfPropertyChange(() => IsFinished);
            NotifyOfPropertyChange(() => HasStarted);
            if (IsFinished)
               _TimeStorage.SaveAsFinished(Episode.Number);
            else
               _TimeStorage.Save(Episode.Number, value);
         }
      }
      public bool IsFinished => WatchedTime == Episode.Duration || WatchedTime.TotalSeconds == -1;
      public bool HasStarted => WatchedTime.TotalSeconds > 0;
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
         _WatchedTime = _TimeStorage.Get(episode.Number, out _);
         NotifyOfPropertyChange(() => WatchedTime);
         NotifyOfPropertyChange(() => HasStarted);
         NotifyOfPropertyChange(() => IsFinished);
      }
      public void Play()
      {
         // show window
      }
      #endregion
   }
}
