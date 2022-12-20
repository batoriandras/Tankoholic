using Microsoft.Xna.Framework;

namespace TankoholicClient.Pencil
{
    public class Pencil
    {
        public int PlayerId { get; }
        private const int PENCIL_SIZE = 5;
        public Color Color { get; set; }


        public Pencil(int playerId, Color color)
        {
            this.PlayerId = playerId;
            Color = color;
        }
    }
}
