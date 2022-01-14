using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DarknetDiaries.WinUI.ViewModels
{
   internal class PlayerViewModel : Screen
   {
      #region Private
      private readonly IWindowManager _WindowManager;
      private EpisodeViewModel _Episode;
      #endregion

      #region Properties
      public EpisodeViewModel Episode { get => _Episode; set => Set(ref _Episode, value); }
      #endregion
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
      public PlayerViewModel(IWindowManager windowManager)
      {
         _WindowManager = windowManager;
      }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

      #region Methods
      public async override Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
      {
         ShellViewModel shell = IoC.Get<ShellViewModel>();

         await _WindowManager.ShowWindowAsync(shell);
         return await base.CanCloseAsync(cancellationToken);
      }
      #endregion
   }
}
