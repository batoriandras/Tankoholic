using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Riptide;
using TankoholicClassLibrary;

namespace TankoholicClient;

public class EntityManager
{
    public static Tank Tank;
    public static List<Tank> OtherTanks = new();
    public List<Bullet> Bullets = new();

    public static void SpawnPlayerTank()
    {
        Tank = new Tank(new Vector2(100, 100), ClientNetworkManager.Instance.Client.Id);
        GameManager.Instance.player.SetTank(Tank);
        MessageSender.SendSpawn(GameManager.Instance.player);
    }

    [MessageHandler((ushort)MessageIds.PLAYER_POSITION)]
    private static void HandlePosition(Message message)
    {
        var id = message.GetUShort();
        if (id == ClientNetworkManager.Instance.Client.Id) return;

        var pos = message.GetFloats();

        Tank tank = OtherTanks.FirstOrDefault(tank => tank.PlayerId == id);
        if (tank is not null)
        {
            tank.Position = new Vector2(pos[0], pos[1]);
        }
    }

    [MessageHandler((ushort)MessageIds.PLAYER_SPAWN)]
    private static void HandleSpawn(Message message)
    {
        var ids = message.GetUShorts();
        var username = message.GetString();

        /* Ha valakinek van erre valami jobb megoldása akkor ne legyen rest átírni */
        if (ids.Length == 1)
        {
            Tank = new Tank(new Vector2(100, 100), ids[0]);
        }
        else
        {
            if(OtherTanks.Count == 1)
            {
                var id = ids[0] == ClientNetworkManager.Instance.Client.Id ? ids[1] : ids[0];
                OtherTanks.Add(new Tank(new Vector2(100, 100), id));
            }
            else
            {
                foreach (var id in ids)
                    OtherTanks.Add(new Tank(new Vector2(100, 100), id));
            }
        }
    }
}