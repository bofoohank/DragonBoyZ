using DragonBoyZ.Application.IO;
using DragonBoyZ.Application.Main;
using DragonBoyZ.Application.Manager;
using DragonBoyZ.DatabaseManager;
using System.Data.Common;
using System;
using System.Collections.Generic;
using DragonBoyZ.Application.Helper;

namespace DragonBoyZ.Sources.Application.Extension
{
    public class NpcAutoChat
    {
        public NpcAutoChat()
        {
        }
        public void StartNpcAutoChat()
        {
            var timer = new System.Timers.Timer(5000);
            timer.Elapsed += (sender, e) =>
            {
                var data = NpcAutoChat.GetAllDataByTypeOne();
                foreach (var item in data)
                {
                    ClientManager.Gi().SendMessage(Service.NpcChat((short)item.npcId, item.text));
                }
            };

            timer.Start();
        }
        public static int Create(short npcId = 0, string text = "", short type = 1)
        {
            lock (DragonBoyZ.Application.Threading.Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToData();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command != null)
                    {
                        var createDate = ServerUtils.TimeNow();
                        command.CommandText =
                            $"INSERT INTO `npc` (`id`, `text`, `type`) VALUES ('{npcId}', '{text}', '{type}') ON DUPLICATE KEY UPDATE `text`='{text}', `type`='{type}'; SELECT LAST_INSERT_ID();";
                        var reader = int.Parse(command.ExecuteScalar()?.ToString() ?? "0");
                        return reader;
                    }
                }
                catch (Exception e)
                {
                    DragonBoyZ.Application.Threading.Server.Gi().Logger.Error($"Create new character error: {e.Message}\n{e.StackTrace}");
                    return 0;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
                return 0;
            }
        }
        public static List<(int npcId, string text)> GetAllDataByTypeOne()
        {
            var result = new List<(int npcId, string text)>();
            lock (DragonBoyZ.Application.Threading.Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToData();
                    using (DbCommand command = DbContext.gI()?.Connection.CreateCommand())
                    {
                        command.CommandText = "SELECT id, text FROM npc WHERE type = 1;";
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int npcId = reader.GetInt32(0);
                                string text = reader.GetString(1);
                                result.Add((npcId, text));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    DragonBoyZ.Application.Threading.Server.Gi().Logger.Error($"Error getting data: {e.Message}\n{e.StackTrace}");
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
            return result;
        }
    }
}