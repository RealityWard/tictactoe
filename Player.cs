using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace tictactoe
{
  class Player
  {
    private String _Name = "";
    public String Name
    { get { return _Name; } }
    private Image[] _Tokens = [];
    private int _TurnNumber = 0;
    public int TurnNumber
    { get { return _TurnNumber; } }

    public Player(String name, int turnNumber, String tokenSource, Stretch stretch, int numberOfTokens, Grid parentElement)
    {
      _Name = name;
      _TurnNumber = turnNumber;
      _Tokens = new Image[numberOfTokens];
      InitializeTokens(parentElement);
      SetTokens(tokenSource);
    }

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

    public void HideTokens()
    {
      for (int i = 0; i < _Tokens.Length; i++)
      {
        _Tokens[i].Visibility = Visibility.Hidden;
      }
    }

    private void InitializeTokens(Grid parentElement)
    {
      for (int i = 0; i < _Tokens.Length; i++)
      {
        _Tokens[i] = new Image();
        parentElement.Children.Add(_Tokens[i]);
      }
    }

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
