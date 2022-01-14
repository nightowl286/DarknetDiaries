﻿using Caliburn.Micro;
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
            if (IsFinished)
               _TimeStorage.SaveAsFinished(Episode.Number);
            else
               _TimeStorage.Save(Episode.Number, value);
         }
      }
      public bool IsFinished => WatchedTime == Episode.Duration || WatchedTime.TotalSeconds == -1;
      #endregion
      public EpisodeViewModel(IEpisode episode, ITimeStorage storage)
      {
         _TimeStorage = storage;
         Change(episode);

      }

      #region Methods
      [MemberNotNull(nameof(_Episode))]
      public void Change(IEpisode episode)
      {
         Episode = episode;
         Debug.Assert(_Episode != null);
         _WatchedTime = _TimeStorage.Get(episode.Number, out _);
         NotifyOfPropertyChange(() => WatchedTime);
         NotifyOfPropertyChange(() => IsFinished);
      }
      #endregion
   }
}