using Caliburn.Micro;
using DarknetDiaries.Core;
using DarknetDiaries.Standard;
using DarknetDiaries.WinUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace DarknetDiaries.WinUI
{
   internal class Bootstrapper : BootstrapperBase
   {
      #region Private
      private SimpleContainer _Container = new SimpleContainer();
      #endregion

      #region Bootstrapper basics
      public Bootstrapper()
      {
         Initialize();
      }
      protected override void OnStartup(object sender, StartupEventArgs e)
      {
         DisplayRootViewFor<ShellViewModel>();
      }
      protected override void Configure()
      {
         _Container.Singleton<IWindowManager, WindowManager>()
            .Singleton<IEventAggregator, EventAggregator>()
            .Singleton<SimpleContainer>()
            .Singleton<ITimeStorage, TimeStorage>()
            .Singleton<ShellViewModel>();

         _Container.PerRequest<PlayerViewModel>()
            .PerRequest<IEpisodeFeed, EpisodeFeed>()
            .PerRequest<IAudioPlayer, AudioPlayer>();
      }
      protected override IEnumerable<Assembly> SelectAssemblies() => new[] { Assembly.GetExecutingAssembly() };
      #endregion

      #region Overrides for container
      protected override object GetInstance(Type service, string key) => _Container.GetInstance(service, key);
      protected override IEnumerable<object> GetAllInstances(Type service) => _Container.GetAllInstances(service);
      protected override void BuildUp(object instance) => _Container.BuildUp(instance);
      #endregion
   }
}
