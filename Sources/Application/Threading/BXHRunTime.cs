using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonBoyZ.Application.IO;
using DragonBoyZ.DatabaseManager.Player;
using DragonBoyZ.Model.BangXepHang;

namespace DragonBoyZ.Application.Threading
{
    public class BxhRunTime
    {
        public List<BangXepHang> Players { get; set; }
        public List<BangXepHang> TopNap { get; set; }
        public Task RunTime { get; set; }


        public BxhRunTime()
        {
            Players = new List<BangXepHang>();
            TopNap = new List<BangXepHang>();
        }
        public void Start()
        {
            if (RunTime != null) return;
            RunTime = new Task(Action);
            RunTime.Start();
        }
        private async void Action()
        {
            while (Server.Gi().IsRunning)
            { 
                Players?.Clear();
                TopNap?.Clear();
                CharacterDB.SelectBXHSucManh(10);
                UserDB.SelectBXHTopNap(10);
                Server.Gi().Logger.PrtColor("yellow","Update","darkyellow","Bang Xep Hang");
                await Task.Delay(60000);
            }
        }

        public string GetList()
        {
            var text = $"{ServerUtils.Color("red")}Bảng Xếp Hạng Sức mạnh\b{ServerUtils.Color("blue")}";
            List<BangXepHang> list;
            lock (Players)
            {
                list = Players.ToList();
            }
            for (var i = 1; i < 11; i++)
            {
                string name = null;
                long diem = 0;
                list.ForEach(player =>
                {
                    if (player.I != i) return;
                    name = player.Name;
                    diem = player.Diem;
                });
                if(diem != 0)text += $"TOP {i}: {name}: {diem}\b";
            }
            return text;
        }

        public string GetListTopNap()
        {
            var text = $"{ServerUtils.Color("red")}Bảng Xếp Hạng TOP Nạp\b{ServerUtils.Color("blue")}";
            List<BangXepHang> list;
            lock (TopNap)
            {
                list = TopNap.ToList();
            }
            for (var i = 1; i < 11; i++)
            {
                string name = null;
                long diem = 0;
                list.ForEach(player =>
                {
                    if (player.I != i) return;
                    name = player.Name;
                    diem = player.Diem;
                });
                if(diem != 0)text += $"TOP {i}: {name}: {ServerUtils.GetMoneys((long)diem)} VNĐ\b";
            }
            return text;
        }
    }
}