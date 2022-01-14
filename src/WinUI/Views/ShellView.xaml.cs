using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DarknetDiaries.WinUI.Views
{
   /// <summary>
   /// Interaction logic for ShellView.xaml
   /// </summary>
   public partial class ShellView : Window
   {
      public ShellView()
      {
         InitializeComponent();
      }

      #region Events
      private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
      {
         OpenUrl(e.Uri.AbsoluteUri);
      }
      #endregion

      #region Methods
      private void OpenUrl(string url) => Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden });
      #endregion
   }
}
