using Riptide;
using TankoholicLibrary;

namespace TankoholicServer;

internal class BulletMessageManager
{
    [MessageHandler((ushort)MessageIds.BulletSpawn)]
    public static void SendSpawnMessage(ushort id, Message message)
    {
        ServerNetworkManager.Instance.Server!.SendToAll(message);
    }

    [MessageHandler((ushort)MessageIds.BulletPosition)]
    private static void Position(ushort id, Message message)
    {
        var position = message.GetFloats();
        ServerNetworkManager.Instance.Server!.SendToAll(ServerNetworkManager.CreatePositionMessage(id, position, MessageIds.BulletPosition));

        if (!ServerDebug.DebugPosition) return;
        ServerDebug.Warn($"Bullet({id}) new position: X:{ position[0]} Y:{position[1] }");
    }
}