using Riptide;
using TankoholicClassLibrary;

namespace TankoholicServer;

internal class BulletMessageManager
{
    [MessageHandler((ushort)MessageIds.BULLET_SPAWN)]
    public static void SendSpawnMessage(ushort id, Message message)
    {
        Program.Server!.SendToAll(message);
    }

    [MessageHandler((ushort)MessageIds.BULLET_POSITION)]
    private static void Position(ushort id, Message message)
    {
        var position = message.GetFloats();
        Program.Server!.SendToAll(ServerNetworkManager.CreatePositionMessage(id, position, MessageIds.BULLET_POSITION));

        if (!ServerDebug.DebugPosition) return;
        ServerDebug.Warn($"Bullet({id}) new position: X:{position[0]} Y:{position[1]}");
    }
}