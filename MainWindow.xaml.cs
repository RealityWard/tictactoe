using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tictactoe
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private TicTacToeGrid _Grid;
    private int _NumberOfTokensPerPlayer = 5;
    private Player[] _Player = new Player[2];
    private String[] _FileNames = [
       "assets/x_red.png",
       "assets/x_blue.png",
       "assets/x_black.png",
       "assets/o_red.png",
       "assets/o_blue.png",
       "assets/o_black.png"
      ];
    public enum TokenNames
    {
      xRed,
      xBlue,
      xBlack,
      oRed,
      oBlue,
      oBlack
    }

    public MainWindow()
    {
      InitializeComponent();
      _Player[0] = new Player("Temp1", 2, _FileNames[(int)TokenNames.oBlue], Stretch.Fill, _NumberOfTokensPerPlayer, mainGrid);
      _Player[1] = new Player("Temp2", 10, _FileNames[(int)TokenNames.xBlue], Stretch.Fill, _NumberOfTokensPerPlayer, mainGrid);
      _Grid = new(_Player[0].TurnNumber);
    }

    private bool SetPlayerMove(int row, int col)
    {
      if (row < 0 || col < 0 || row > 2 || col > 2) return false;
      int i;
      for (i = 0; i < _Player.Length; i++)
      {
        if (_Grid.Turn == _Player[i].TurnNumber)
        {
          _Player[i].SetMove(row, col);
          return true;
        }
      }
      return false;
    }

    private void TttBtnClick(object sender, RoutedEventArgs e)
    {
      Console.WriteLine($"TttBtnClick: {Grid.GetRow((UIElement)sender)}, {Grid.GetColumn((UIElement)sender)}");
      Debug.WriteLine($"TttBtnClick: {Grid.GetRow((UIElement)sender)}, {Grid.GetColumn((UIElement)sender)}");
      int result = _Grid.ChangeEntry(Grid.GetRow((UIElement)sender), Grid.GetColumn((UIElement)sender));
      SetPlayerMove(Grid.GetRow((UIElement)sender), Grid.GetColumn((UIElement)sender));
      if (result == 0)
      {
        // Indicate turn swap
        MessageBox.Show("Need to Indicate turn swap", "Error", MessageBoxButton.OK);
      }
      else if (result > 0)
      {
        //There was a win
        MessageBox.Show("You won.", "Winner", MessageBoxButton.OK);
      }
      else if (result == -1)
      {
        MessageBox.Show("Cat's Eye! :(", "Draw", MessageBoxButton.OK);
      }
    }
  }
}