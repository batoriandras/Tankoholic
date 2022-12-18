using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TankoholicClassLibrary;
using Riptide;
using System.Diagnostics;

namespace TankoholicClient;

public class EntityManager
{
    public static Tank Tank;
    public static List<Tank> OtherTanks = new();
    public static List<Bullet> Bullets = new();
    public static List<Entity> EntityTrashcan = new();
    public static List<UnpassableTile> UnpassableTiles = new();

    public static void SpawnPlayerTank()
    {
        Tank = new Tank(new Vector2(100, 100), ClientNetworkManager.Instance.Client.Id);
        GameManager.Instance.Player.SetTank(Tank);
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

            if (entity is UnpassableTile)
            {
                UnpassableTiles.Remove((UnpassableTile)entity);
                MapManager.Instance.RemoveDrawnTile((DrawnTile)entity);
            }
            if (entity is PowerupEntity powerup)
            {
                PowerupManager.Instance.Powerups.Remove(powerup);
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
        var tilePositions = message.GetString();

        /* Ha valakinek van erre valami jobb megoldása akkor ne legyen rest átírni */

        foreach (var id in ids)
        {
            if (id == ClientNetworkManager.Instance.Client.Id)
            {
                GameManager.Instance.Player = new Player(id, username);
                SpawnPlayerTank();
            }
            else
            {
                OtherTanks.Add(new Tank(new Vector2(100, 100), id));
            }
        }

        var positionStrings = tilePositions.Split(';');
        if (positionStrings.Length > 1)
        {
            foreach (var tilePos in positionStrings)
            {
                var split = tilePos.Split(',');
                int x = int.Parse(split[0]);
                int y = int.Parse(split[1]);

                MapManager.Instance.SetTile(x, y, DrawnTile.FromPencil(GameManager.Instance.Player.pencil, new Vector2(x * GameConstants.CELL_SIZE, y * GameConstants.CELL_SIZE)));
            }
        }
    }
    [MessageHandler((ushort)MessageIds.BULLET_SPAWN)]
    private static void HandleBulletSpawn(Message message)
    {
        var position = message.GetFloats();
        var direction = message.GetFloats();
        var playerId = message.GetUShort();

        if (playerId != GameManager.Instance.Player.Id)
        {
            Bullets.Add(new Bullet(
                new Vector2(position[0], position[1]), 
                new Vector2(direction[0], direction[1]), 
                playerId));
        }
    }

    [MessageHandler((ushort)MessageIds.UNPASSABLE_TILE_SPAWN)]
    private static void HandleUnpassableTileSpawn(Message message)
    {
        var position = message.GetInts();
        int x = position[0];
        int y = position[1];
        MapManager.Instance.SetTile(x, y, DrawnTile.FromPencil(GameManager.Instance.Player.pencil, new Vector2(x * GameConstants.CELL_SIZE, y * GameConstants.CELL_SIZE)));
    }

    [MessageHandler((ushort)MessageIds.UNPASSABLE_TILE_DESPAWN)]
    private static void HandleUnpassableTileDespawn(Message message)
    {
        var position = message.GetInts();
        int x = position[0];
        int y = position[1];
        MapManager.Instance.SetTile(x, y, new GrassTile(new Vector2(x * GameConstants.CELL_SIZE, y * GameConstants.CELL_SIZE)));
    }
}