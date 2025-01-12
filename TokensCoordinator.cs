using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
  public enum TokenTypes
  {
    x,
    o
  }

  public enum TokenColors
  {
    red,
    blue,
    black
  }
  class TokensCoordinator
  {
    private static String[][] _TokenFileNames = [
        [
         "assets/x_red.png",
         "assets/x_blue.png",
         "assets/x_black.png"
        ],
        [
         "assets/o_red.png",
         "assets/o_blue.png",
         "assets/o_black.png"
        ]
      ];

    public static String GetTokenFileName(TokenTypes type, TokenColors color)
    {
      return _TokenFileNames[(int)type][(int)color];
    }
  }
}
