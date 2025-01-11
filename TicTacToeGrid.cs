namespace tictactoe
{
  class TicTacToeGrid
  {
    private int[][] _Grid = [
      [1, 1, 1],
      [1, 1, 1],
      [1, 1, 1]
    ];

    /// <summary>
    /// The product of each win state in order of
    /// top to bottom and left to right: rows, cols, diagonals.
    /// </summary>
    private int[] _WinStates = [1, 1, 1, 1, 1, 1, 1, 1];

    private int _Turn = 0;

    public int Turn
    {
      get { return _Turn; }
    }

    /// <summary>
    /// Base object that keeps info of the state of the game
    /// for checking for wins, losses, or cat's eyes.
    /// </summary>
    /// <param name="turn">Either a 2 or 10 to represent two players.</param>
    public TicTacToeGrid(int turn)
    {
      if (turn == 10 || turn == 2)
      {
        _Turn = turn;
      }
      else
      {
        _Turn = 2;
      }
    }

    public bool ChangeEntry(int row, int col)
    {
      if (row < 0 || col < 0 || row > 2 || col > 2) return false;
      _Grid[row][col] = _Turn;

      ChangeTurn();
      return true;
    }

    private void ChangeTurn()
    {
      if (_Turn == 2)
      {
        _Turn = 10;
      }
      else
      {
        _Turn = 2;
      }
    }
  }
}
