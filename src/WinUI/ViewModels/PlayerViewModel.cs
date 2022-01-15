using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;

namespace DarknetDiaries.WinUI.ViewModels
{
   internal class PlayerViewModel : Screen
   {
      #region Private
      private readonly IWindowManager _WindowManager;
      private EpisodeViewModel? _Episode;
      #endregion

      #region Properties
      public EpisodeViewModel? Episode { get => _Episode; private set => Set(ref _Episode, value); }
      #endregion
      public PlayerViewModel(IWindowManager windowManager)
      {
         _WindowManager = windowManager;
      }

      #region Methods
      public void SetEpisode(EpisodeViewModel episode)
      {
         Episode = episode;
      }
      public async override Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
      {
         ShellViewModel shell = IoC.Get<ShellViewModel>();

         await _WindowManager.ShowWindowAsync(shell);
         return await base.CanCloseAsync(cancellationToken);
      }
      #endregion
   }
}
