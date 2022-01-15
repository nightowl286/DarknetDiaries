﻿using System;
using System.Collections.Generic;
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
   /// Interaction logic for PlayerView.xaml
   /// </summary>
   public partial class PlayerView : Window
   {
      public PlayerView()
      {
         InitializeComponent();
      }

      private void Window_MouseMove(object sender, MouseEventArgs e)
      {
         if (Mouse.LeftButton == MouseButtonState.Pressed && Mouse.Captured == null)
            this.DragMove();
      }
   }
}
