using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.Xna.Framework;
using Riptide;
using TankoholicClassLibrary;

namespace TankoholicClient;

public class EntityManager
{
    public static Tank Tank;
    public static List<Tank> OtherTanks = new();
    public static List<Bullet> Bullets = new();
    public static List<Entity> EntityTrashcan = new();

    public static void SpawnPlayerTank()
    {
        Tank = new Tank(new Vector2(100, 100), ClientNetworkManager.Instance.Client.Id);
        GameManager.Instance.player.SetTank(Tank);
    }

    public static void SpawnBullet(Vector2 direction)
    {
        Bullet bullet = Tank.Shoot(direction);
        Bullets.Add(bullet);
        MessageSender.SendSpawn(bullet);
    }

    public static void ClearEntityTrashcan()
    {
        foreach (var entity in EntityTrashcan)
        {
            if (entity is Bullet)
            {
                Bullets.Remove((Bullet)entity);
            }
        }
        EntityTrashcan.Clear();
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
    private static void HandlePlayerSpawn(Message message)
    {
        var ids = message.GetUShorts();
        var username = message.GetString();

        /* Ha valakinek van erre valami jobb megoldása akkor ne legyen rest átírni */

        foreach (var id in ids)
        {
            if (id == ClientNetworkManager.Instance.Client.Id)
            {
                GameManager.Instance.player = new Player(id, username);
                SpawnPlayerTank();
            }
            else
            {
                OtherTanks.Add(new Tank(new Vector2(100, 100), id));
            }
        }
    }
    [MessageHandler((ushort)MessageIds.BULLET_SPAWN)]
    private static void HandleBulletSpawn(Message message)
    {
        var position = message.GetFloats();
        var direction = message.GetFloats();
        var playerId = message.GetUShort();

        Bullets.Add(new Bullet(
            new Vector2(position[0], position[1]), 
            new Vector2(direction[0], direction[1]), 
            playerId));
    }
}