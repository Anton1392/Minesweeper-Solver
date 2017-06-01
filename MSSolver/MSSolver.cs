using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSolver
{
    /// <summary>
    /// This class uses logic to solve a Minesweeper board.
    /// It sends outputs to the Minesweeper program.
    /// </summary>
    public class MSSolver
    {
        // Whether or not to use random clicks when the program can't find any solution through logic.
        public bool allowRandomClicks = true;

        // Creates an MSData class reference.
        private MSData data;

        // Flag for if the board is solved.
        private bool isSolved = false;

        // The constructor.
        public MSSolver(Difficulty diff)
        {
            // Instantiates the MSData class with the set difficulty.
            data = new MSData(diff);
        }

        /// <summary>
        /// Main flow of the solver.
        /// </summary>
        public void Solve()
        {
            MSLog.AddMessage("Started a new game.");

            // Resets the game position.
            MSIO.ResetMSWindowPos();
            MSLog.AddMessage("Reset the Minesweeper window position.");

            // Refreshes the board.
            data.RefreshBoard();

            // Loops until the board is solved.
            MSLog.AddMessage("Started solving.");
            while (!isSolved)
            {
                #region ComparisonSnapshot
                // Creates a snapshot of the board to compare with at the end, which determines if the board is solved.
                Tile[,] boardComparison = new Tile[data.Board.GetLength(0), data.Board.GetLength(1)];

                // Loops through and copies the board state. I couldn't find an easier method for this.
                for (int i = 0; i < data.Board.GetLength(0); i++)
                {
                    for (int j = 0; j < data.Board.GetLength(1); j++)
                    {
                        // Instantiates a new tile, and sets the proper values.
                        boardComparison[i, j] = new Tile();
                        boardComparison[i, j].Value = data.Board[i, j].Value;
                        boardComparison[i, j].xCoord = data.Board[i, j].xCoord;
                        boardComparison[i, j].yCoord = data.Board[i, j].yCoord;
                    }
                }
                #endregion

                #region The main logic loop.
                // Loops through the board.
                for (int x = 0; x < data.Board.GetLength(0); x++)
                {
                    for (int y = 0; y < data.Board.GetLength(1); y++)
                    {
                        // Flagging of surrounding squares using basic logic.
                        #region Basic flagging.
                        // If the checked tile is a numbered tile.
                        if (data.Board[x, y].Value >= MSConstants.One && data.Board[x, y].Value <= MSConstants.Eight)
                        {
                            // If the tile that is being checked has the same number of clickable tiles surrounding it,
                            // flag all clickable tiles around it.
                            if (data.TilesSurrounding(x, y, MSConstants.Tile).Count == data.Board[x, y].Value)
                            {
                                FlagTilesSurrounding(x, y);
                            }
                        }
                        #endregion

                        // Basic chord-ing.
                        #region Basic chord-ing.
                        // If the tile is a safe clear AND
                        // if the tile has clickable tiles around it,
                        // perform a Chord on it.
                        else if (data.Board[x,y].Value == MSConstants.SafeClear && data.TilesSurrounding(x, y, MSConstants.Tile).Count > 0)
                        {
                            Chord(x, y);
                        }
                        #endregion

                        // 1-2 pattern logic.
                        #region
                        // If a One has 2 or more tiles around it,
                        // and there is a Two around the 2 or more tiles.
                        // and the Two has (2 or more tiles+1) tiles around it, with (2 or more tiles) matching the first (2 or more) tiles.
                        // flag the +1 tile that does not match.
                        // Sorry if this is unclear. It works though.
                        if (data.Board[x,y].Value == MSConstants.One && data.TilesSurrounding(x,y,MSConstants.Tile).Count >= 2)
                        {
                            // Store the initial surrounding tiles preemptively. This improves performance by simply having an object rather than calling a method several times over.
                            List<Tile> initialTiles = data.TilesSurrounding(x, y, MSConstants.Tile);

                            // Finds all Twos around the surrounding tiles.
                            List<Tile> surroundingTwos = new List<Tile>();
                            foreach (Tile tileCheck in initialTiles)
                            {
                                surroundingTwos.AddRange(data.TilesSurrounding(tileCheck.xCoord, tileCheck.yCoord, MSConstants.Two));
                            }

                            // and if the Two has greater than initial tiles.count of tiles around it,
                            List<Tile> surrTilesToMatch = new List<Tile>();
                            foreach (Tile tileTwo in surroundingTwos)
                            {
                                surrTilesToMatch.Clear();
                                surrTilesToMatch = data.TilesSurrounding(tileTwo.xCoord, tileTwo.yCoord, MSConstants.Tile);

                                // and there are initial tile matches between the tiles surrounding the initial One, and the found Two.
                                // Loops through the list, looking for matches.
                                List<int> matchXcoords = new List<int>();
                                List<int> matchYcoords = new List<int>();
                                for (int i = 0; i < surrTilesToMatch.Count; i++)
                                {
                                    for (int j = 0; j < data.TilesSurrounding(x,y, MSConstants.Tile).Count; j++)
                                    {
                                        // if the x-coord matches
                                        if (surrTilesToMatch[i].xCoord == initialTiles[j].xCoord)
                                        {
                                            // if the y-coord matches
                                            if (surrTilesToMatch[i].yCoord == initialTiles[j].yCoord)
                                            {
                                                // Stores the coordinates of the matching squares.
                                                matchXcoords.Add(surrTilesToMatch[i].xCoord);
                                                matchYcoords.Add(surrTilesToMatch[i].yCoord);
                                            }
                                        }
                                    }
                                }
                                List<Tile> tilesToFlag = new List<Tile>();
                                // Loops through all squares to match
                                for (int i = 0; i < surrTilesToMatch.Count; i++)
                                {
                                    // Flag for if the current tile has a match.
                                    bool hasMatch = false;
                                    // Loops through all matching coordinates.
                                    for (int j = 0; j < matchXcoords.Count; j++)
                                    {
                                        // If the coordinates match, set the flag to true, and break.
                                        if (surrTilesToMatch[i].xCoord == matchXcoords[j] && surrTilesToMatch[i].yCoord == matchYcoords[j])
                                        {
                                            hasMatch = true;
                                            break;
                                        }
                                    }
                                    // If there is no match, store this tile for flagging.
                                    if (!hasMatch)
                                    {
                                        tilesToFlag.Add(surrTilesToMatch[i]);
                                    }
                                }

                                // Flags the tile within the List, as long as there's only one object in it.
                                if (tilesToFlag.Count == 1)
                                {
                                    MSLog.AddMessage("Recognized pattern 1-2.");
                                    // Flags the tile with a board refresh.
                                    Flag(tilesToFlag[0].xCoord, tilesToFlag[0].yCoord, true);

                                    // Breaks the loop, otherwise it can draw several conclusions from one pattern, and that loses us the game.
                                    break;
                                }
                            }
                        }
                        #endregion

                        // 1-1 pattern logic.
                        #region
                        // If a One has tiles surrounding it,
                        // and another One has surrounding tiles matching ALL of the first One's surrounding.
                        // Clear all unmatching tiles surrounding the second One.
                        if (data.Board[x,y].Value == MSConstants.One && data.TilesSurrounding(x,y,MSConstants.Tile).Count >= 1)
                        {
                            // Stores the initial tiles.
                            List<Tile> initialTiles = data.TilesSurrounding(x, y, MSConstants.Tile);

                            // Finds all ones surrounding the initial tiles.
                            List<Tile> surroundingOnes = new List<Tile>();
                            foreach (Tile tile in initialTiles)
                            {
                                surroundingOnes.AddRange(data.TilesSurrounding(tile.xCoord, tile.yCoord, MSConstants.One));
                            }

                            // Loops through all surrounding ones of the initial tiles.
                            foreach (Tile surrOne in surroundingOnes)
                            {
                                // Grabs all tiles surrounding the current one.
                                List<Tile> surrTilesOfOne = data.TilesSurrounding(surrOne.xCoord, surrOne.yCoord, MSConstants.Tile);

                                // Tries to find matches for the surrounding tiles.
                                // ALL Tiles in initialTiles should be matched with this to pass.    
                                 
                                // Count of matches and lists of the matching coordinates.
                                int matches = 0;
                                List<int> matchXCoords = new List<int>();
                                List<int> matchYCoords = new List<int>();
                                for (int i = 0; i < initialTiles.Count; i++)
                                {
                                    for (int j = 0; j < surrTilesOfOne.Count; j++)
                                    {
                                        if (initialTiles[i].xCoord == surrTilesOfOne[j].xCoord && initialTiles[i].yCoord == surrTilesOfOne[j].yCoord)
                                        {
                                            matches++;
                                            matchXCoords.Add(initialTiles[i].xCoord);
                                            matchYCoords.Add(initialTiles[i].yCoord);
                                        }
                                    }
                                }

                                // If there are exactly so many matches as the initial tile count was, clear the non-matched tiles.
                                if (matches == initialTiles.Count)
                                {
                                    // Loops through and compares the initial tiles, and the current surrounding tiles of the One.
                                    for (int i = 0; i < surrTilesOfOne.Count; i++)
                                    {
                                        // Loops through the initial tiles, checking for matches.
                                        bool hasMatch = false;
                                        for (int j = 0; j < initialTiles.Count; j++)
                                        {
                                            if (surrTilesOfOne[i].xCoord == initialTiles[j].xCoord && surrTilesOfOne[i].yCoord == initialTiles[j].yCoord)
                                            {
                                                hasMatch = true;
                                                break;
                                            }
                                        }

                                        // If the mine does not have a match, clear it.
                                        if (!hasMatch)
                                        {
                                            MSLog.AddMessage("Recognized pattern 1-1.");
                                            ClearMine(surrTilesOfOne[i].xCoord, surrTilesOfOne[i].yCoord);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region RandomClick
                if (allowRandomClicks)
                {
                    // Comparison of initial board state, vs current.
                    // Loops through the board snapshot.
                    bool _hasDifference = false;
                    for (int i = 0; i < boardComparison.GetLength(0); i++)
                    {
                        for (int j = 0; j < boardComparison.GetLength(1); j++)
                        {
                            // If some tile does NOT match, set _hasDifference to true.
                            if (data.Board[i, j].Value != boardComparison[i, j].Value)
                            {
                                _hasDifference = true;
                            }
                        }
                    }
                    // If there is no difference in board state, perform a randomized click.
                    if (!_hasDifference)
                    {
                        // Finds and stores all clickable tiles on the board.
                        List<Tile> possibleTiles = new List<Tile>();

                        for (int i = 0; i < data.Board.GetLength(0); i++)
                        {
                            for (int j = 0; j < data.Board.GetLength(1); j++)
                            {
                                if (data.Board[i, j].Value == MSConstants.Tile)
                                {
                                    possibleTiles.Add(data.Board[i, j]);
                                }
                            }
                        }

                        // Performs a click if there is atleast one clickable tile.
                        if (possibleTiles.Count >= 1)
                        {
                            Random rnd = new Random();
                            int index = rnd.Next(0, possibleTiles.Count);

                            // Performs a click on the randomly selected tile.
                            ClearMine(possibleTiles[index].xCoord, possibleTiles[index].yCoord);

                            MSLog.AddMessage("Randomly clicked tile " + possibleTiles[index].xCoord.ToString() + ", " + possibleTiles[index].yCoord.ToString() + ".");
                        }                        
                    }
                }                
                #endregion

                // This determines whether to keep on solving.
                #region Board comparison in the end.
                // Comparison of initial board state, vs current.
                // Loops through the board snapshot.
                bool hasDifference = false;
                for (int i = 0; i < boardComparison.GetLength(0); i++)
                {
                    for (int j = 0; j < boardComparison.GetLength(1); j++)
                    {
                        // If some tile does NOT match, set hasDifference to true.
                        if (data.Board[i, j].Value != boardComparison[i, j].Value)
                        {
                            hasDifference = true;
                        }
                    }
                }

                // Sets isSolved to false. This reverts if there is no difference in board state.
                isSolved = false;
                // If there is no difference in the board states, set isSolved to true and cancel the loop.
                if (!hasDifference)
                {
                    isSolved = true;
                }
                #endregion
            }

            // Adds log messages, saves the log, then clears the current log.
            MSLog.AddMessage("Game ended. Saving...");
            MSLog.SaveLog();
            MSLog.ClearLog();
        }

        /// <summary>
        /// Places a flag at a specific tile.
        /// </summary>
        /// <param name="x">X-coordinate of the tile on the board.</param>
        /// <param name="y">Y-coordinate of the tile on the board.</param>
        /// <param name="refreshBoard">Whether or not to refresh the board after the flagging.</param>
        public void Flag(int x, int y, bool refreshBoard)
        {
            // Moves the mouse to the target tile.
            // Takes into consideration the first mine offsets, and then adds 16 pixels per index of the mine.
            MSIO.MoveMouse(MSConstants.FirstMineOffsetX + 16 * (x), MSConstants.FirstMineOffsetY + 16 * (y));

            // Performs a rightclick
            MSIO.RightClick();

            // Moves the mouse away from the window as to not disturb the image recognition.
            // 500, 500 are arbitrary. It's simply out of the way of the Minesweeper window.
            MSIO.MoveMouse(500, 500);

            // This if-statement is for optimization, whether or not to refresh the board after the flag.
            if (refreshBoard)
            {
                // Refreshes the board state.
                data.RefreshBoard();
            }

            // Notes in the log file.
            MSLog.AddMessage("Flagged tile at " + x.ToString() + ", " + y.ToString() + ".");          
        }

        /// <summary>
        /// Clears a specific tile.
        /// </summary>
        /// <param name="x">X-coordinate of the tile on the board.</param>
        /// <param name="y">Y-coordinate of the tile on the board.</param>
        public void ClearMine(int x, int y)
        {
            // Moves the mouse to target tile.
            // Takes into consideration the first mine offsets, and then adds 16 pixels per index of the mine.
            MSIO.MoveMouse(MSConstants.FirstMineOffsetX + 16 * (x), MSConstants.FirstMineOffsetY + 16 * (y));

            // Performs a leftclick
            MSIO.Click();

            // Moves the mouse away from the window as to not disturb the image recognition.
            // 500, 500 are arbitrary. It's simply out of the way of the Minesweeper window.
            MSIO.MoveMouse(500, 500);

            // Refreshes the board state.
            data.RefreshBoard();

            // Logs this action.
            MSLog.AddMessage("Cleared tile at " + x.ToString() + ", " + y.ToString() + ".");
        }

        /// <summary>
        /// Clears all tiles surrounding a specific tile.
        /// </summary>
        /// <param name="x">X-coordinate of the tile on the board.</param>
        /// <param name="y">Y-coordinate of the tile on the board.</param>
        public void Chord(int x, int y)
        {
            // Moves the mouse.
            // Takes into consideration the first mine offset, and then adds 16 pixels per index of the mine.
            MSIO.MoveMouse(MSConstants.FirstMineOffsetX + 16 * (x), MSConstants.FirstMineOffsetY + 16 * (y));

            // Clicks both mouse buttons at the same time. This is called a chord.
            // This is called a "chord"
            MSIO.ClickBothMouseButtons();

            // Moves the mouse away from the window as to not disturb the image recognition.
            // NOTE: 500, 500 are (probably) temporary coordinates.
            MSIO.MoveMouse(500, 500);

            // Refreshes the board state.
            data.RefreshBoard();

            // Logs this action.
            MSLog.AddMessage("Chorded tile at " + x.ToString() + ", " + y.ToString() + ".");
        }

        public void FlagTilesSurrounding(int x, int y)
        {
            // Loops through all squares surrounding the center.
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    try
                    {
                        // If the checked tile is a clickable, flaggable tile.
                        if (data.Board[x + i, y + j].Value == MSConstants.Tile)
                        {
                            // Then flag the tile, without a board refresh.
                            Flag(x + i, y + j, false);
                        }
                    }
                    catch (IndexOutOfRangeException){/* Ignore non-fatal exception. */}                  
                }
            }

            // Refreshes the board.
            data.RefreshBoard();

            // Logs this action.
            MSLog.AddMessage("Flagged all tiles surrounding tile at " + x.ToString() + ", " + y.ToString() + ".");
        }
    }
}
