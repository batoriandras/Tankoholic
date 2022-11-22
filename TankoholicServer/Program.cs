namespace TankoholicServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerNetworkManager.Instance.Initialize();
            while (true)
            {
                ServerNetworkManager.Instance.Update();
            }
        }
    }
}