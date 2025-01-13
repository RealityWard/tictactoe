using System.Windows;

namespace tictactoe
{
  /// <summary>
  /// Dialogue Box popup for end of game to give the option to restart with current settings or return to start.
  /// </summary>
  public partial class EndGame : Window
  {
    public EndGame()
    {
      InitializeComponent();

    }

    /// <summary>
    /// CS side to the Restart btn. As soon as DialogResult is assigned it ends as if returned.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RestartBtnOnClick(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }

    /// <summary>
    /// CS side to the Return to main... As soon as DialogResult is assigned it ends as if returned.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ReturnToMainMenuOnClick(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
  }

  /// <summary>
  /// Interface to EndGame object.
  /// </summary>
  public class EndGameMessageBox
  {
    /// <summary>
    /// Interface to EndGame object. 
    /// </summary>
    /// <param name="text">Main text in center of dialogue box.</param>
    /// <returns>false on return to menu, true on restart, or null if closed.</returns>
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
