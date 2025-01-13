using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static tictactoe.MainWindow;
using System.Windows.Controls;
using System.Windows.Media;

namespace tictactoe
{
  public partial class EndGame : Window
  {
    public EndGame()
    {
      InitializeComponent();

    }

    private void RestartBtnOnClick(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }

    private void ReturnToMainMenuOnClick(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }

    private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
    {
      this.MouseDown += delegate { DragMove(); };
    }
  }

  public class EndGameMessageBox
  {
    public static bool? Show(string text)
    {
      EndGame msg = new EndGame
      {
        labelText = { Content = text }
      };
      //msg.noBtn.Focus();
      return msg.ShowDialog();
    }
  }

}
