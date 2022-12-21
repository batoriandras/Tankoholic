namespace TankoholicServer
{
    internal class Program
    {
        private static void Main()
        {
            ServerNetworkManager.Instance.Initialize();

            while (true)
            {
                ServerNetworkManager.Instance.Update();
                Thread.Sleep(10);
            }
        }
    }
}