using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSolver
{
    /// <summary>
    /// Static class containing read-only constants for Minesweeper.
    /// This is an easy way to globally change constants if needed.
    /// </summary>
    public static class MSConstants
    {
        // Declares constants for all numerical tiles.
        #region Numerical tiles.

        public static int One { get; } = 1;
        public static int Two { get; } = 2;
        public static int Three { get; } = 3;
        public static int Four { get; } = 4;
        public static int Five { get; } = 5;
        public static int Six { get; } = 6;
        public static int Seven { get; } = 7;
        public static int Eight { get; } = 8;

        #endregion

        // Declares all special tiles.
        #region Special tiles.

        /// <summary>
        /// Constant for a tile that can be cleared safely.
        /// </summary>
        public static int SafeClear { get; } = 0;

        /// <summary>
        /// Constant for a red flag in Minesweeper.
        /// </summary>
        public static int Flag { get; } = -1;
        /// <summary>
        /// Constant for a clickable tile in Minesweeper.
        /// </summary>
        public static int Tile { get; } = -2;
        /// <summary>
        /// Constant for an unnumbered, unclickable, empty tile in Minesweeper.
        /// </summary>
        public static int Empty { get; } = -3;
        /// <summary>
        /// Constant for a mine tile in Minesweeper. These only appear after a loss, and should be used for loss-checking.
        /// </summary>
        public static int Mine { get; } = -4;

        #endregion

        // Declares other constants used for various purposes.
        #region Others

        // Offsets for where the first mine appears.
        /// <summary>
        /// X-Coordinate of where the first mines top-left corner appears.
        /// </summary>
        public static int FirstMineOffsetX { get; } = 15;
        /// <summary>
        /// Y-Coordinate of where the first mines top-left corner appears.
        /// </summary>
        public static int FirstMineOffsetY { get; } = 101;

        #endregion
    }
}
