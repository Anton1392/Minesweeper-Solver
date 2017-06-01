using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MSSolver
{
    // Declares an enum for the Minesweeper Difficulty.
    public enum Difficulty { Beginner, Intermediate, Expert }

    /// <summary>
    /// This class is used to handle a representation of the Minesweeper board.
    /// There are functions that extract information about the board.
    /// </summary>
    public class MSData
    {
        // The representation of the Minesweeper board. This is the heart of the program.
        public Tile[,] Board { get; }

        // Storage for the current difficulty.
        public Difficulty currentDifficulty;

        // The constructor.
        public MSData(Difficulty diff)
        {
            // Stores the current difficulty.
            currentDifficulty = diff;

            // Creates the board with different sizes depending on the difficulty.
            if (diff == Difficulty.Beginner)
            {
                Board = new Tile[9, 9];
            }
            else if (diff == Difficulty.Intermediate)
            {
                Board = new Tile[16, 16];
            }
            else if (diff == Difficulty.Expert)
            {
                Board = new Tile[30, 16];
            }

            // Instantiates and sets the coordinate values of the tiles within the array
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    Board[i, j] = new Tile();
                    Board[i, j].xCoord = i;
                    Board[i, j].yCoord = j;
                }
            }
        }

        /// <summary>
        /// Refreshes the board state.
        /// </summary>
        public void RefreshBoard()
        {
            ImageToBoard();
            ReduceBoard();
        }

        /// <summary>
        /// Returns a list of tiles with the same type as specified.
        /// </summary>
        /// <param name="x">The x-coordinate of the tile of which the surrounding gets checked.</param>
        /// <param name="y">The y-coordinate of the tile of which the surrounding gets checked.</param>
        /// <param name="type">The type to check for surrounding the center tile. Refer to MSConstants for values</param>
        /// <returns></returns>
        public List<Tile> TilesSurrounding(int x, int y, int type)
        {
            List<Tile> tilesSurrounding = new List<Tile>();

            // Loops through all squares surrounding the center tile. i and j are the offsets from the center.
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // This if-statement prevents the center square being checked.
                    if (i != 0 || j != 0)
                    {
                        try
                        {
                            // If the current square matches the wanted type, add it to the list.
                            if (Board[x + i, y + j].Value == type)
                            {
                                tilesSurrounding.Add(Board[x + i, y + j]);
                            }
                        }
                        // Catches non-fatal out of range exceptions.
                        catch (IndexOutOfRangeException) { }
                    }
                }
            }
          
            // Returns the list of tiles matching the type.
            return tilesSurrounding;
        }

        /// <summary>
        /// Takes a screenshot, and translates it into board state.
        /// </summary>
        private void ImageToBoard()
        {
            // Declares integers holding the dimensions of the Minesweeper window.
            int frameX = 0;
            int frameY = 0;

            // Sets the frame of the screenshot depending on the difficulty. This is the width and height of the Minesweeper window.
            switch (currentDifficulty)
            {
                case Difficulty.Beginner:
                    frameX = 166;
                    frameY = 254;
                    break;

                case Difficulty.Intermediate:
                    frameX = 278;
                    frameY = 366;
                    break;

                case Difficulty.Expert:
                    frameX = 502;
                    frameY = 366;
                    break;
            }

            // Takes a screenshot of the entire Minesweeper window.
            Bitmap screenshot = MSIO.Screenshot(0, 0, frameX, frameY);

            // Loops through the length of the board, and extracts tiles which get color-scanned.
            {
                // Loops through the X dimension of the board array.
                for (int i = 0; i < Board.GetLength(0); i++)
                {
                    // Loops through the Y dimension of the board array.
                    for (int j = 0; j < Board.GetLength(1); j++)
                    {
                        // Extracts the tiles, one by one. Tiles are 16x16 pixels.
                        Bitmap tile = ImageSection(screenshot, new Rectangle(0 + 16 * i + MSConstants.FirstMineOffsetX, 0 + 16 * j + MSConstants.FirstMineOffsetY, 16, 16));

                        // Detects what tile it is, and assigns the value to the appropriate tile in Board.
                        Board[i, j].Value = TileDetection(tile);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a section of a Bitmap as another Bitmap.
        /// </summary>
        /// <param name="src">The source image.</param>
        /// <param name="section">The section to grab in the image.</param>
        /// <returns></returns>
        private Bitmap ImageSection (Bitmap src, Rectangle section)
        {
            // Generates an empty Bitmap with the specified Width and Height and an associated Graphics object.
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);

            // Draws the section of the source Bitmap to bmp.
            g.DrawImage(src, 0, 0, section, GraphicsUnit.Pixel);

            // Cleans up resources.
            g.Dispose();

            // Returns the section.
            return bmp;
        }

        /// <summary>
        /// Function detects what a particular tile contains.
        /// Returns an integer that represents the tile content.
        /// </summary>
        /// <param name="tile">Tile to detect.</param>
        /// <returns></returns>
        private int TileDetection(Bitmap tile)
        {
            // Color detection here.
            // Checks certain pixels for certain colors which decide which value to return.
            #region Color Detection
          
            // Checks for a flag.
            if (tile.GetPixel(7, 7) == Color.FromArgb(255, 0, 0) && tile.GetPixel(7, 10) == Color.FromArgb(0, 0, 0))
            {
                return MSConstants.Flag;
            }

            // Checks for a regular clickable tile.
            else if (tile.GetPixel(0,0) == Color.FromArgb(255, 255, 255))
            {
                return MSConstants.Tile;
            }

            // Checks for a one.
            else if (tile.GetPixel(8,4) == Color.FromArgb(0, 0, 255))
            {
                return MSConstants.One;
            }

            // Checks for a two.
            else if (tile.GetPixel(4, 3) == Color.FromArgb(0, 128, 0))
            {
                return MSConstants.Two;
            }

            // Checks for a three.
            else if (tile.GetPixel(7, 7) == Color.FromArgb(255, 0, 0) && tile.GetPixel(7, 10) == Color.FromArgb(192, 192, 192))
            {
                return MSConstants.Three;
            }

            // Checks for a four.
            else if (tile.GetPixel(5, 4) == Color.FromArgb(0, 0, 128))
            {
                return MSConstants.Four;
            }

            // Checks for a five.
            else if (tile.GetPixel(4, 4) == Color.FromArgb(128, 0, 0))
            {
                return MSConstants.Five;
            }

            // Checks for a six.
            else if (tile.GetPixel(4, 4) == Color.FromArgb(0, 128, 128))
            {
                return MSConstants.Six;
            }

            // Checks for a seven.
            else if (tile.GetPixel(4, 3) == Color.FromArgb(0, 0, 0))
            {
                return MSConstants.Seven;
            }

            // Checks for an eight.
            else if (tile.GetPixel(4, 4) == Color.FromArgb(128, 128, 128))
            {
                return MSConstants.Eight;
            }

            // If no matches, return an empty tile.
            else
            {
                return MSConstants.Empty;
            }

            #endregion
        }

        /// <summary>
        /// Reduces the board state to basic patterns.
        /// Finds all flags and sets the flags to empty tiles.
        /// Reduces all numbered tiles surrounding the flag by one point each.
        /// </summary>
        private void ReduceBoard()
        {
            // Loops through the entire board.
            for (int x = 0; x < Board.GetLength(0); x++)
            {
                for (int y = 0; y < Board.GetLength(1); y++)
                {
                    // If the current square is a Flag
                    if (Board[x,y].Value == MSConstants.Flag)
                    {
                        // Set the flag to empty, and reduce all surrounding tiles by one.                      
                        Board[x, y].Value = MSConstants.Empty;
                        // i and j are the offsets around the center square that is being checked.
                        for (int i = -1; i <= +1; i++)
                        {
                            for (int j = -1; j <= +1; j++)
                            {
                                try
                                {
                                    // If the current surrounding square is between 1 and 8, reduce it by one.
                                    if (Board[x + i, y + j].Value >= MSConstants.One && Board[x + i, y + j].Value <= MSConstants.Eight)
                                    {
                                        Board[x + i, y + j].Value--;
                                    }
                                }
                                // Catches IndexOutOfRangeExceptions. This is a non-fatal error and is therefore ignored.
                                catch (IndexOutOfRangeException) { }
                            }
                        }
                    }
                }
            }
        }
    }
}
