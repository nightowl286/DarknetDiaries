using Caliburn.Micro;
using DarknetDiaries.WinUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DarknetDiaries.WinUI
{
   internal class Bootstrapper : BootstrapperBase
   {
      #region Bootstrapper basics
      public Bootstrapper()
      {
         Initialize();
      }
      protected override void OnStartup(object sender, StartupEventArgs e)
      {
         DisplayRootViewFor<ShellViewModel>();
      }
      #endregion
   }
}
