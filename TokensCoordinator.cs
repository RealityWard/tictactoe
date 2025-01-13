namespace tictactoe
{
  /// <summary>
  /// Indicates the letters available for tokens. 
  /// Must be kept in sync with _TokenFileNames manually.
  /// </summary>
  public enum TokenTypes
  {
    x,
    o
  }

  /// <summary>
  /// Indicates the colors available for tokens. 
  /// Must be kept in sync with _TokenFileNames manually.
  /// </summary>
  public enum TokenColors
  {
    red,
    blue
  }
  class TokensCoordinator
  {
    /// <summary>
    /// File paths for the images of the tokens. 
    /// Must be kept in sync with TokenTypes and TokenColors manually.
    /// </summary>
    private static String[][] _TokenFileNames = [
        [
         "assets/x_red.png",
         "assets/x_blue.png",
        ],
        [
         "assets/o_red.png",
         "assets/o_blue.png",
        ]
      ];

    /// <summary>
    /// Uses the Enums to return the desired token image file path.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static String GetTokenFileName(TokenTypes type, TokenColors color)
    {
      return _TokenFileNames[(int)type][(int)color];
    }
  }
}
