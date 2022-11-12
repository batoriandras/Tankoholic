namespace TankoholicClassLibrary
{
    public class Player
    {
        public string Username { get; set; }
        public int Id { get; set; }

        public Player(string username, int id)
        {
            Username = username;
            Id = id;
        }
    }
    
}