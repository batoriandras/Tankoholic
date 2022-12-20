using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TankoholicLibrary;
using Riptide;
using TankoholicClient.Graphics.Tiles;
using TankoholicClient.Powerups;

namespace TankoholicClient;

public static class EntityManager
{
    public static Tank Tank;
    public static readonly List<Tank> OtherTanks = new();
    public static readonly List<Bullet> Bullets = new();
    public static readonly List<Entity> EntityTrashcan = new();
    public static readonly List<UnpassableTile> UnpassableTiles = new();

    private static void SpawnPlayerTank()
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
            if (entity is Bullet bullet)
            {
                Bullets.Remove(bullet);
            }

            if (entity is UnpassableTile tile)
            {
                UnpassableTiles.Remove(tile);
                MapManager.Instance.RemoveDrawnTile((DrawnTile)tile);
            }
            if (entity is PowerupEntity powerup)
            {
                PowerupManager.Instance.Powerups.Remove(powerup);
            }
        }
        EntityTrashcan.Clear();
    }
    
    public static void RemoveTank(ushort id)
    {
        Tank tank = OtherTanks.FirstOrDefault(tank => tank.PlayerId == id);
        OtherTanks.Remove(tank);
    }

    [MessageHandler((ushort)MessageIds.PlayerPosition)]
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

    [MessageHandler((ushort)MessageIds.PlayerSpawn)]
    private static void HandlePlayerSpawn(Message message)
    {
        var ids = message.GetUShorts();
        var username = message.GetString();
        var tilePositions = message.GetString();

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

                MapManager.Instance.SetTile(x, y, DrawnTile.FromPencil(GameManager.Instance.Player.Pencil, new Vector2(x * GameConstants.CELL_SIZE, y * GameConstants.CELL_SIZE)));
            }
        }
    }
    [MessageHandler((ushort)MessageIds.BulletSpawn)]
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

    [MessageHandler((ushort)MessageIds.UnpassableTileSpawn)]
    private static void HandleUnpassableTileSpawn(Message message)
    {
        var position = message.GetInts();
        int x = position[0];
        int y = position[1];
        MapManager.Instance.SetTile(x, y, DrawnTile.FromPencil(GameManager.Instance.Player.Pencil, new Vector2(x * GameConstants.CELL_SIZE, y * GameConstants.CELL_SIZE)));
    }

    [MessageHandler((ushort)MessageIds.UnpassableTileDespawn)]
    private static void HandleUnpassableTileDespawn(Message message)
    {
        var position = message.GetInts();
        int x = position[0];
        int y = position[1];
        MapManager.Instance.SetTile(x, y, new GrassTile(new Vector2(x * GameConstants.CELL_SIZE, y * GameConstants.CELL_SIZE)));
    }
}