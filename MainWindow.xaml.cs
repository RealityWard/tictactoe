using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
      // Filling in the options for the ComboBox due to limitations of data binding to Enums.
      foreach (TokenColors color in Enum.GetValues(typeof(TokenColors)))
      {
        MenuSetup_ColorComboBox.Items.Add(color);
        MenuSetup_ColorComboBox1.Items.Add(color);
      }
      // Setting default selection.
      MenuSetup_ColorComboBox.SelectedItem = TokenColors.red;
      MenuSetup_ColorComboBox1.SelectedItem = TokenColors.blue;
    }

    /// <summary>
    /// Hides all player tokens.
    /// </summary>
    private void HidePlayerTokens()
    {
      foreach (Player player in _Player)
      {
        if (player != null)
        {
          player.HideTokens();
        }
      }
    }

    /// <summary>
    /// Finds the current turn's player and sets their token to the specified coordinates.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns>False if the row or col are out of bounds.</returns>
    private bool SetPlayerMove(int row, int col)
    {
      // Validation for TTT grid.
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

    /// <summary>
    /// Finds the current turn's player and return their name.
    /// </summary>
    /// <returns>player.Name</returns>
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

    /// <summary>
    /// Main event for TicTacToe. Used on each square in the TicTacToe Grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TttBtn_OnClick(object sender, RoutedEventArgs e)
    {
      // Visual change on Grid for user.
      SetPlayerMove(Grid.GetRow((UIElement)sender), Grid.GetColumn((UIElement)sender));
      // Background change for tracking in TicTacToeGrid.
      int result = _Grid.ChangeEntry(Grid.GetRow((UIElement)sender), Grid.GetColumn((UIElement)sender));
      if (result == 0)
      {
        // Indicating turn swap. Could use to be more noticable in future update.
        PlayerTurnLabel.Content = GetCurrentPlayerName();
      }
      else if (result > 0)
      {
        // There was a win
        if (EndGameMessageBox.Show($"{GetCurrentPlayerName()} won.") == false)
        {
          // Menu
          MenuStart.Visibility = Visibility.Visible;
          gridBorder.Visibility = Visibility.Collapsed;
        }
        else
        {
          // Restart
          StartGame();
        }
      }
      else if (result == -1)
      {
        // Cat's Eye
        if (EndGameMessageBox.Show("Cat's Eye! :(") == false)
        {
          // Menu
          MenuStart.Visibility = Visibility.Visible;
          gridBorder.Visibility = Visibility.Collapsed;
        }
        else
        {
          // Restart
          StartGame();
        }
      }
    }

    /// <summary>
    /// StartSetup button on MenuStart.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StartSetupBtn_OnClick(object sender, RoutedEventArgs e)
    {
      MenuStart.Visibility = Visibility.Collapsed;
      MenuSetup.Visibility = Visibility.Visible;
    }

    // Start------Radio buttons alternating events------------------------------------------------------------
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
    // End------Radio buttons alternating events------------------------------------------------------------

    /// <summary>
    /// StartGame button on MenuSetup.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StartGameBtn_OnClick(object sender, RoutedEventArgs e)
    {
      StartGame();
    }

    /// <summary>
    /// Sets up the game board based on MenuSetup. Used for new game or restart.
    /// </summary>
    private void StartGame()
    {
      // Determining X and O
      TokenTypes type, type1;
      if (MenuSetup_TokenORadioButton.IsChecked != null && (bool)MenuSetup_TokenORadioButton.IsChecked)
      {
        type = TokenTypes.o;
        type1 = TokenTypes.x;
      }
      else
      {
        type = TokenTypes.x;
        type1 = TokenTypes.o;
      }

      // In case of restart.
      HidePlayerTokens();
      _Player[0] = new Player(MenuSetup_NameTextBox.Text, 2,
        TokensCoordinator.GetTokenFileName(type, (TokenColors)MenuSetup_ColorComboBox.SelectionBoxItem),
        Stretch.Fill, _NumberOfTokensPerPlayer, ticTacToeGrid);
      _Player[1] = new Player(MenuSetup_NameTextBox1.Text, 10,
        TokensCoordinator.GetTokenFileName(type1, (TokenColors)MenuSetup_ColorComboBox1.SelectionBoxItem),
        Stretch.Fill, _NumberOfTokensPerPlayer, ticTacToeGrid);

      // Determining player to go first.
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
      // Setting visibility in case of new game.

      MenuSetup.Visibility = Visibility.Collapsed;
      gridBorder.Visibility = Visibility.Visible;
    }
  }
}