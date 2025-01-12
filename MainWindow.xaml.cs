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
    private Image[] _Player1Tokens;
    private Image[] _Player2Tokens;
    private int _Player1Number = 2;
    private int _Player2Number = 10;

    public MainWindow()
    {
      InitializeComponent();
      _Grid = new(_Player1Number);
      _Player1Tokens = new Image[_NumberOfTokensPerPlayer];
      _Player2Tokens = new Image[_NumberOfTokensPerPlayer];
      InitializeTokens();
      SetTokens("assets/o_blue.png", "assets/x_red.png");
    }

    private void InitializeTokens()
    {
      for (int i = 0; i < _NumberOfTokensPerPlayer; i++)
      {
        _Player1Tokens[i] = new Image();
        _Player2Tokens[i] = new Image();
        _Player1Tokens[i].Visibility = Visibility.Hidden;
        _Player2Tokens[i].Visibility = Visibility.Hidden;
        mainGrid.Children.Add(_Player1Tokens[i]);
        mainGrid.Children.Add(_Player2Tokens[i]);
      }
    }

    private void SetTokens(String token1, String token2)
    {
      BitmapImage token1BI = new();
      token1BI.BeginInit();
      token1BI.UriSource = new Uri(token1, UriKind.Relative);
      token1BI.EndInit();
      BitmapImage token2BI = new();
      token2BI.BeginInit();
      token2BI.UriSource = new Uri(token2, UriKind.Relative);
      token2BI.EndInit();
      for (int i = 0; i < _NumberOfTokensPerPlayer; i++)
      {
        _Player1Tokens[i].Visibility = Visibility.Hidden;
        _Player1Tokens[i].Stretch = Stretch.Fill;
        _Player1Tokens[i].Source = token1BI;
        _Player2Tokens[i].Visibility = Visibility.Hidden;
        _Player2Tokens[i].Stretch = Stretch.Fill;
        _Player2Tokens[i].Source = token2BI;
      }
    }

    private bool SetPlayerMove(int row, int col, int turn)
    {
      if (row < 0 || col < 0 || row > 2 || col > 2) return false;
      if (turn == _Player1Number)
      {
        int i;
        for (i = 0; i < _NumberOfTokensPerPlayer - 1 && _Player1Tokens[i].Visibility != Visibility.Hidden; i++)
        { }
        Grid.SetRow(_Player1Tokens[i], row);
        Grid.SetColumn(_Player1Tokens[i], col);
        _Player1Tokens[i].Visibility = Visibility.Visible;
      }
      else if (turn == _Player2Number)
      {
        int i;
        for (i = 0; i < _NumberOfTokensPerPlayer - 1 && _Player2Tokens[i].Visibility != Visibility.Hidden; i++)
        { }
        Grid.SetRow(_Player2Tokens[i], row);
        Grid.SetColumn(_Player2Tokens[i], col);
        _Player2Tokens[i].Visibility = Visibility.Visible;
      }
      else
      {
        return false;
      }
      return true;
    }

    private void TttBtnClick(object sender, RoutedEventArgs e)
    {
      Console.WriteLine($"TttBtnClick: {Grid.GetRow((UIElement)sender)}, {Grid.GetColumn((UIElement)sender)}");
      Debug.WriteLine($"TttBtnClick: {Grid.GetRow((UIElement)sender)}, {Grid.GetColumn((UIElement)sender)}");
      int result = _Grid.ChangeEntry(Grid.GetRow((UIElement)sender), Grid.GetColumn((UIElement)sender));
      SetPlayerMove(Grid.GetRow((UIElement)sender), Grid.GetColumn((UIElement)sender), _Grid.Turn);
      if (result == 0)
      {
        // Indicate turn swap
        MessageBox.Show("Need to Indicate turn swap", "Error", MessageBoxButton.OK);
      }
      if (result == 1)
      {
        //There was a win
        MessageBox.Show("You won.", "Winner", MessageBoxButton.OK);
      }
      else if (result == 2)
      {
        MessageBox.Show("Cat's Eye! :(", "Draw", MessageBoxButton.OK);
      }
      else if (result == -1)
      {
        MessageBox.Show("There was an issue?", "Error?", MessageBoxButton.OK);
      }
    }
  }
}