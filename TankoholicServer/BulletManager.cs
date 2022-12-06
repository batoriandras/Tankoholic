using Riptide;
using TankoholicClassLibrary;

namespace TankoholicServer;

public class BulletMessageManager
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
        Program.Server!.SendToAll(CreatePositionMessage(id, position));

        if (!Program.DebugPosition) return;
        ServerDebug.Warn($"Bullet({id}) new position: X:{position[0]} Y:{position[1]}");
    }
    private static Message CreatePositionMessage(ushort id, float[] position)
    {
        Message message = Message.Create(MessageSendMode.Reliable, MessageIds.BULLET_POSITION);
        message.AddUShort(id);
        message.AddFloats((float[])position);
        return message;
    }
}