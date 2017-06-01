using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSolver
{
    public class Tile
    {
        /// <summary>
        /// The value of the tile, refer to the MSConstant class for possible values.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// The x-coorinate of the tile on the board.
        /// </summary>
        public int xCoord { get; set; }

        /// <summary>
        /// The y-coorinate of the tile on the board.
        /// </summary>
        public int yCoord { get; set; }
    }
}
