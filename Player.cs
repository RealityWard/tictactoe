using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace tictactoe
{
  /// <summary>
  /// To be used for information grabbed on the Menu Setup page.
  /// </summary>
  class Player
  {
    private String _Name = "";
    /// <summary>
    /// Player's human readable name.
    /// </summary>
    public String Name
    { get { return _Name; } }
    /// <summary>
    /// Tokens to be placed on the Tic-Tac-Toe grid visually.
    /// </summary>
    private Image[] _Tokens = [];
    /// <summary>
    /// Ideally a number good for unique products. 2 and 10 usually.
    /// </summary>
    private int _TurnNumber = 0;
    /// <summary>
    /// Player's turn number.
    /// </summary>
    public int TurnNumber
    { get { return _TurnNumber; } }

    /// <summary>
    /// Primary constructor which both creates the images UIElements 
    /// for the tokens, inserting them onto the parentGrid hidden 
    /// and sets the source for the images to tokenSource.
    /// </summary>
    /// <param name="name">Name the user enters.</param>
    /// <param name="turnNumber">Ideally a number good for unique products.</param>
    /// <param name="tokenSource">Path string to an image.</param>
    /// <param name="stretch">Image Stretch property.</param>
    /// <param name="numberOfTokens">Depends on the max number of tokens needed.</param>
    /// <param name="parentGrid">Grid element that the tokens will be placed in.</param>
    public Player(String name, int turnNumber, String tokenSource, Stretch stretch, int numberOfTokens, Grid parentGrid)
    {
      _Name = name;
      _TurnNumber = turnNumber;
      _Tokens = new Image[numberOfTokens];
      InitializeTokens(parentGrid);
      SetTokens(tokenSource);
    }

    /// <summary>
    /// Grabs the image from the path string argument.
    /// </summary>
    /// <param name="tokenSource">path string</param>
    private void SetTokens(String tokenSource)
    {
      BitmapImage tokenBI = new();
      tokenBI.BeginInit();
      tokenBI.UriSource = new Uri(tokenSource, UriKind.Relative);
      tokenBI.EndInit();
      for (int i = 0; i < _Tokens.Length; i++)
      {
        _Tokens[i].Visibility = Visibility.Hidden;
        _Tokens[i].Stretch = Stretch.Fill;
        _Tokens[i].Source = tokenBI;
      }
    }

    /// <summary>
    /// Changes the visibility of all this player's tokens to Hidden.
    /// </summary>
    public void HideTokens()
    {
      for (int i = 0; i < _Tokens.Length; i++)
      {
        _Tokens[i].Visibility = Visibility.Hidden;
      }
    }

    /// <summary>
    /// Adds Image UIElements to the parentGrid.
    /// </summary>
    /// <param name="parentGrid">Grid element that the tokens will be placed in.</param>
    private void InitializeTokens(Grid parentGrid)
    {
      for (int i = 0; i < _Tokens.Length; i++)
      {
        _Tokens[i] = new Image();
        parentGrid.Children.Add(_Tokens[i]);
      }
    }

    /// <summary>
    /// Finds the next still hidden token,
    /// Then changes the Grid.Row and Grid.Column to their respective inputs,
    /// Finally changes visibility to visible.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public void SetMove(int row, int col)
    {
      int i;
      for (i = 0; i < _Tokens.Length - 1 && _Tokens[i].Visibility != Visibility.Hidden; i++)
      { }
      Grid.SetRow(_Tokens[i], row);
      Grid.SetColumn(_Tokens[i], col);
      _Tokens[i].Visibility = Visibility.Visible;
    }

  }
}
