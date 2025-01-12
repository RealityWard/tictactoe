
using Accessibility;

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
    { get { return _Turn; } }

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

    public int ChangeEntry(int row, int col)
    {
      if (row < 0 || col < 0 || row > 2 || col > 2) return -1;
      _Grid[row][col] = _Turn;
      int win = CheckForWin();
      if (win == 0)
      {
        ChangeTurn();
        return 0;
      }
      else
      {
        return win;
      }
    }

    /// <summary>
    /// Using 2 and 10 to distinguish turns. Reason for 2 and 10
    /// is each product of 1, 2, 10 in sets of three of any combination
    /// is unique thus creates a finite amount of unique values that
    /// indicate whether there is a win.
    /// </summary>
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

    /// <summary>
    /// Checks the 8 win states. Checks in this order:
    /// rows from top to bottom,
    /// cols from left to right,
    /// diagonal negative slope,
    /// diagonal positive slope.
    /// </summary>
    /// <returns>
    /// -1 for a cat's eye,
    /// 0 for no win,
    /// 1 to 8 for a win state in the order read.
    /// </returns>
    private int CheckForWin()
    {
      int result = 0;
      int check = 1;
      int i;

      // Row checks
      for (i = 0; i < 3; i++)
      {
        _WinStates[result] = _Grid[i][0] * _Grid[i][1] * _Grid[i][2];
        check = CheckRow(_WinStates[result], check);
        result += 1;
        if (check < 1) { return result; }
      }

      // Col checks
      i = 0;
      for (i = 0; i < 3; i++)
      {
        _WinStates[result] = _Grid[0][i] * _Grid[1][i] * _Grid[2][i];
        check = CheckRow(_WinStates[result], check);
        result += 1;
        if (check < 1) { return result; }
      }

      // diagonal negative slope
      _WinStates[result] = _Grid[0][0] * _Grid[1][1] * _Grid[2][2];
      check = CheckRow(_WinStates[result], check);
      result += 1;
      if (check < 1) { return result; }

      // diagonal positive slope
      _WinStates[result] = _Grid[2][0] * _Grid[1][1] * _Grid[0][2];
      check = CheckRow(_WinStates[result], check);
      result += 1;
      if (check < 1) { return result; }

      // Done with no victory found.
      if (check > 1)
      { return 0; }
      else { return -1; }
    }

    /// <summary>
    /// Using the product of the three in the row to determine
    /// if it is a victory, not victory, or unknown.
    /// </summary>
    /// <param name="input">Product of the row.</param>
    /// <param name="check">Meant to be used to return the result.
    /// Should start with 1 for proper ongoing results.</param>
    /// <returns>Possibly modified check.
    /// If check is -1 then victory.
    /// If check increases then victory is still possible.
    /// If check is unchanged then it is unknown if victory is still possible.</returns>
    private static int CheckRow(int input, int check)
    {
      // Victory
      if (input == 8 || input == 1000)
      { return -1; }
      // Victory is still possible
      if (input == 1 || input == 100 || input == 4 || input == 2 || input == 10)
      { return check + 1; }
      // Unknown
      return check;
    }
  }
}
