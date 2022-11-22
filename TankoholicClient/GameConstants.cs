using System;

namespace TankoholicClient
{
    public static class GameConstants
    {
        public static readonly Random Random = new Random();

        public const int CELLS_HORIZONTALLY_COUNT = 360;
        public const int CELLS_VERTICALLY_COUNT = 210;
        public const int CELL_SIZE = 4;

        public const int WINDOW_WIDTH = CELLS_HORIZONTALLY_COUNT * CELL_SIZE;
        public const int WINDOW_HEIGHT = CELLS_VERTICALLY_COUNT * CELL_SIZE;

        public const string TITLE = "Tankoholic";
    }
}
