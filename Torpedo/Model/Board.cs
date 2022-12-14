namespace NationalInstruments.Torpedo.Model
{
    /// <summary>
    /// Represents a Board for the game.
    /// </summary>
    internal class Board
    {
        public Coordinate[,] GameBoard { get; private set; }

        public Board(int boardSize)
        {
            GameBoard = new Coordinate[boardSize, boardSize];
        }
    }
}
