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
    private TicTacToeGrid _Grid = new(1);
    private int _NumberOfTokensPerPlayer = 5;
    private Player[] _Player = new Player[2];

    public MainWindow()
    {
      InitializeComponent();
      foreach (TokenColors color in Enum.GetValues(typeof(TokenColors)))
      {
        MenuSetup_ColorComboBox.Items.Add(color);
        MenuSetup_ColorComboBox1.Items.Add(color);
      }
      MenuSetup_ColorComboBox.SelectedItem = TokenColors.red;
      MenuSetup_ColorComboBox1.SelectedItem = TokenColors.blue;
    }

    private void HidePlayerTokens()
    {
      foreach (Player player in _Player)
      {
        player.HideTokens();
      }
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

    private String GetCurrentPlayerName()
    {
      int i;
      for (i = 0; i < _Player.Length; i++)
      {
        if (_Grid.Turn == _Player[i].TurnNumber)
        {
          return _Player[i].Name;
        }
      }
      return "";
    }

    private void TttBtn_OnClick(object sender, RoutedEventArgs e)
    {
      Console.WriteLine($"TttBtnClick: {Grid.GetRow((UIElement)sender)}, {Grid.GetColumn((UIElement)sender)}");
      Debug.WriteLine($"TttBtnClick: {Grid.GetRow((UIElement)sender)}, {Grid.GetColumn((UIElement)sender)}");
      SetPlayerMove(Grid.GetRow((UIElement)sender), Grid.GetColumn((UIElement)sender));
      int result = _Grid.ChangeEntry(Grid.GetRow((UIElement)sender), Grid.GetColumn((UIElement)sender));
      if (result == 0)
      {
        // Indicate turn swap
        PlayerTurnLabel.Content = GetCurrentPlayerName();
      }
      else if (result > 0)
      {
        // There was a win
        if (EndGameMessageBox.Show($"{GetCurrentPlayerName()} won.") == false)
        {
          MenuStart.Visibility = Visibility.Visible;
          gridBorder.Visibility = Visibility.Collapsed;
        }
        else
        {
          StartGame();
        }
      }
      else if (result == -1)
      {
        // Cat's Eye
        if (EndGameMessageBox.Show("Cat's Eye! :(") == false)
        {
          MenuStart.Visibility = Visibility.Visible;
          gridBorder.Visibility = Visibility.Collapsed;
        }
        else
        {
          StartGame();
        }
      }
    }

    private void StartSetupBtn_OnClick(object sender, RoutedEventArgs e)
    {
      MenuStart.Visibility = Visibility.Collapsed;
      MenuSetup.Visibility = Visibility.Visible;
    }

    private void UncheckAlternateXRadio_OnClick(object sender, RoutedEventArgs e)
    {
      if (MenuSetup_TokenORadioButton1 != null)
      {
        MenuSetup_TokenORadioButton1.IsChecked = true;
      }
    }

    private void UncheckAlternateX1Radio_OnClick(object sender, RoutedEventArgs e)
    {
      if (MenuSetup_TokenORadioButton != null)
      {
        MenuSetup_TokenORadioButton.IsChecked = true;
      }
    }

    private void UncheckAlternateORadio_OnClick(object sender, RoutedEventArgs e)
    {
      if (MenuSetup_TokenXRadioButton1 != null)
      {
        MenuSetup_TokenXRadioButton1.IsChecked = true;
      }
    }

    private void UncheckAlternateO1Radio_OnClick(object sender, RoutedEventArgs e)
    {
      if (MenuSetup_TokenXRadioButton != null)
      {
        MenuSetup_TokenXRadioButton.IsChecked = true;
      }
    }

    private void StartGameBtn_OnClick(object sender, RoutedEventArgs e)
    {
      StartGame();
    }

    private void StartGame()
    {
      TokenTypes type = TokenTypes.x;
      TokenTypes type1 = TokenTypes.o;
      if (MenuSetup_TokenORadioButton.IsChecked != null && (bool)MenuSetup_TokenORadioButton.IsChecked)
      {
        type = TokenTypes.o;
        type1 = TokenTypes.x;
      }
      foreach (Player player in _Player)
      {
        if (player != null)
        {
          player.HideTokens();
        }
      }
      _Player[0] = new Player(MenuSetup_NameTextBox.Text, 2,
        TokensCoordinator.GetTokenFileName(type, (TokenColors)MenuSetup_ColorComboBox.SelectionBoxItem),
        Stretch.Fill, _NumberOfTokensPerPlayer, ticTacToeGrid);
      _Player[1] = new Player(MenuSetup_NameTextBox1.Text, 10,
        TokensCoordinator.GetTokenFileName(type1, (TokenColors)MenuSetup_ColorComboBox1.SelectionBoxItem),
        Stretch.Fill, _NumberOfTokensPerPlayer, ticTacToeGrid);
      if (MenuSetup_RadioButton.IsChecked != null && (bool)MenuSetup_RadioButton.IsChecked)
      {
        _Grid = new(_Player[0].TurnNumber);
        PlayerTurnLabel.Content = _Player[0].Name;
      }
      else
      {
        _Grid = new(_Player[1].TurnNumber);
        PlayerTurnLabel.Content = _Player[1].Name;
      }
      MenuSetup.Visibility = Visibility.Collapsed;
      gridBorder.Visibility = Visibility.Visible;
    }
  }
}