using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DragonBoyZ.Application.Constants;
using DragonBoyZ.Application.IO;
using DragonBoyZ.Application.Manager;
using DragonBoyZ.Application.Threading;
using DragonBoyZ.Model.Item;

namespace DragonBoyZ.Application.Extension.Namecball
{
    public class Init
    {
        public static int countNamecBall = 0;
        public static List<NamecBall> NamecBalls = new List<NamecBall>();
        public static List<int> MapInit = new List<int> { 7, 43, 8, 9, 25, 11, 12, 13, 10, 33, 34, 32, 31 };
        public static List<int> PosistionX = new List<int> { 854, 1052, 822, 551, 442, 711, 925, 1148, 698, 1334, 488, 433, 591 };
        public static List<int> PosistionY = new List<int> { 432, 432, 360, 384, 336, 336, 408, 384, 288, 360, 312, 384, 312 };
        public static long DelayInit = 3000 + ServerUtils.CurrentTimeMillis();
        public static void AutoInit(long timeserver)
        {
           
                    if (DelayInit < timeserver && countNamecBall <= 6)
                    {
                        RoiNgocRong();
                    }
                
        }
        public static void RoiNgocRong()
        {
            var itemDrop = ItemCache.GetItemDefault((short)(353+countNamecBall));
            var randomIndex = ServerUtils.RandomNumber(MapInit.Count);
            var Maps = MapManager.Get(MapInit[randomIndex]);
            var ToaDoX = PosistionX[randomIndex];
            var ToaDoY = PosistionY[randomIndex];
            var Zone = Maps.Zones[0];
            Zone.ItemMaps.TryAdd(0, new ItemMap(-1)
            {
                Id = 0,
                PlayerId = -1,
                Item = itemDrop,
                X = (short)ToaDoX,
                Y = (short)ToaDoY,
            });
            NamecBalls.Add(new NamecBall()
            {
                Id = itemDrop.Id,
                MapName = Maps.TileMap.Name,
                PlayerPick = -1,
                ZoneId = Zone.Id,
                MapId = Maps.Id,
                X = (short)ToaDoX,
                Y = (short)ToaDoY
            }) ;
			string text = "NRNM " + (itemDrop.Id - 352) + "S -- Map: " + Maps.Id + " -- Zone: " + Zone.Id + " -- Pos: " + ToaDoX + "," + ToaDoY;
			Server.Gi().Logger.PrtColor("green", "Spawns", "darkgreen", text);
            countNamecBall++;
            if (countNamecBall == 6)
            {
                DelayInit = 315000 + ServerUtils.CurrentTimeMillis();
            }
        }
    }
}
