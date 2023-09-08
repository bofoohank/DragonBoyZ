using DragonBoyZ.Application.Constants;
using DragonBoyZ.Application.Handlers.Menu;
using DragonBoyZ.Application.IO;
using DragonBoyZ.Application.Main;
using DragonBoyZ.DatabaseManager;
using DragonBoyZ.Model.Character;
using DragonBoyZ.Model.Item;
using DragonBoyZ.Model.Option;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace DragonBoyZ.Sources.Application.Activity.CauCa
{
    public class HandlerCauCa
    {
        public static void UseCanCau(Character character, int type)
        {
            var checkGold = character.InfoChar.Gold;
            var checkBag = character.LengthBagNull();

            int indexTextThangCong = ServerUtils.RandomNumber(MenuNpc.TextTrangThai[0].Count);
            string thangCong = MenuNpc.TextTrangThai[0][indexTextThangCong];
            int indexTextThatBai = ServerUtils.RandomNumber(MenuNpc.TextTrangThai[1].Count);
            string thatBai = MenuNpc.TextTrangThai[1][indexTextThatBai];

            switch (type)
            {
                case 0: //Câu thường
                    {
                        character.InfoBuff.IsCauCaThuong = true;
                        if (checkGold >= CacheCauCa.GiaMoiCauThuong && checkBag >= 3)
                        {
                            character.MineGold(CacheCauCa.GiaMoiCauThuong);
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                            character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage("Đang chờ cá đớp: ", 100, (CacheCauCa.TimeCauCa / 1000)));
                            async void Action()
                            {
                                await Task.Delay(500);
                                character.InfoSkill.ThoiMien.IsThoiMien = true;
                                character.InfoSkill.ThoiMien.Time = CacheCauCa.TimeCauCa + ServerUtils.CurrentTimeMillis();
                                character.CharacterHandler.SendMessage(Service.SkillEffectPlayer(character.Id, character.Id, 1, 40));

                                await Task.Delay(CacheCauCa.TimeCauCa);
                                character.InfoBuff.IsCauCaThuong = false;

                                if (ServerUtils.RandomNumber(0, 100) < CacheCauCa.PercentNormal)
                                {
                                    character.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, thangCong));
                                    if (CacheCauCa.Exp_SM != 0 && CacheCauCa.Exp_TN != 0)
                                    {
                                        character.CharacterHandler.PlusPower(CacheCauCa.Exp_SM);
                                        character.CharacterHandler.PlusPotential(CacheCauCa.Exp_TN);
                                        character.CharacterHandler.SendMessage(Service.UpdateExp(0, CacheCauCa.Exp_SM));
                                        character.CharacterHandler.SendMessage(Service.UpdateExp(1, CacheCauCa.Exp_TN));
                                    }
                                    else
                                    {
                                        if (CacheCauCa.Exp_SM != 0)
                                        {
                                            character.CharacterHandler.PlusPower(CacheCauCa.Exp_SM);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(0, CacheCauCa.Exp_SM));
                                        }
                                        if (CacheCauCa.Exp_TN != 0)
                                        {
                                            character.CharacterHandler.PlusPotential(CacheCauCa.Exp_TN);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(1, CacheCauCa.Exp_TN));
                                        }    
                                    }    
                                    int idLoaiCaCauDuoc = RandomLoaiCaThuong();
                                    var item = ItemCache.GetItemDefault((short)idLoaiCaCauDuoc);
                                    item.Quantity = 1;
                                    var itemMap = new ItemMap(character.Id, item);
                                    itemMap.X = character.InfoChar.X;
                                    itemMap.Y = character.InfoChar.Y;
                                    character.Zone.ZoneHandler.LeaveItemMap(itemMap);
                                }
                                else
                                {
                                    character.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, thatBai));
                                    if (CacheCauCa.Exp_SM != 0 && CacheCauCa.Exp_TN != 0)
                                    {
                                        character.CharacterHandler.PlusPower(CacheCauCa.Exp_SM/2);
                                        character.CharacterHandler.PlusPotential(CacheCauCa.Exp_TN/2);
                                        character.CharacterHandler.SendMessage(Service.UpdateExp(0, CacheCauCa.Exp_SM/2));
                                        character.CharacterHandler.SendMessage(Service.UpdateExp(1, CacheCauCa.Exp_TN/2));
                                    }
                                    else
                                    {
                                        if (CacheCauCa.Exp_SM != 0)
                                        {
                                            character.CharacterHandler.PlusPower(CacheCauCa.Exp_SM/2);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(0, CacheCauCa.Exp_SM/2));
                                        }
                                        if (CacheCauCa.Exp_TN != 0)
                                        {
                                            character.CharacterHandler.PlusPotential(CacheCauCa.Exp_TN/2);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(1, CacheCauCa.Exp_TN/2));
                                        }
                                    }
                                }
                            };
                            var task = new Task(Action);
                            task.Start();
                        }
                        else
                        {
                            if (checkGold < CacheCauCa.GiaMoiCauThuong)
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Bạn không đủ tiền để mua mồi", false, character.InfoChar.Gender));
                            }
                            if (checkBag < 3)
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Cần ít nhất 3 ô hành trang trống", false, character.InfoChar.Gender));
                            }
                        }
                        break;
                    }
                case 1: //Câu đặc biệt
                    {
                        character.InfoBuff.IsCauCaThuong = true;
                        if (checkGold >= CacheCauCa.GiaMoiCauDacBiet && checkBag >= 3)
                        {
                            character.MineGold(CacheCauCa.GiaMoiCauDacBiet);
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                            character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage("Đang chờ cá đớp: ", 100, (CacheCauCa.TimeCauCa / 1000)));
                            async void Action()
                            {
                                await Task.Delay(500);
                                character.InfoSkill.ThoiMien.IsThoiMien = true;
                                character.InfoSkill.ThoiMien.Time = CacheCauCa.TimeCauCa + ServerUtils.CurrentTimeMillis();
                                character.CharacterHandler.SendMessage(Service.SkillEffectPlayer(character.Id, character.Id, 1, 40));

                                await Task.Delay(CacheCauCa.TimeCauCa);
                                character.InfoBuff.IsCauCaThuong = false;
                                if (ServerUtils.RandomNumber(0, 100) < CacheCauCa.PercentSpecial)//Tỷ lệ câu trúng cá
                                {
                                    character.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, thangCong));
                                    if (ServerUtils.RandomNumber(0, 100) < CacheCauCa.PercentDropItem)//Tỷ lệ ra cải trang, vpdl
                                    {
                                        character.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, thangCong));
                                        if (CacheCauCa.Exp_SM != 0 && CacheCauCa.Exp_TN != 0)
                                        {
                                            character.CharacterHandler.PlusPower(CacheCauCa.Exp_SM);
                                            character.CharacterHandler.PlusPotential(CacheCauCa.Exp_TN);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(0, CacheCauCa.Exp_SM));
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(1, CacheCauCa.Exp_TN));
                                        }
                                        else
                                        {
                                            if (CacheCauCa.Exp_SM != 0)
                                            {
                                                character.CharacterHandler.PlusPower(CacheCauCa.Exp_SM);
                                                character.CharacterHandler.SendMessage(Service.UpdateExp(0, CacheCauCa.Exp_SM));
                                            }
                                            if (CacheCauCa.Exp_TN != 0)
                                            {
                                                character.CharacterHandler.PlusPotential(CacheCauCa.Exp_TN);
                                                character.CharacterHandler.SendMessage(Service.UpdateExp(1, CacheCauCa.Exp_TN));
                                            }
                                        }
                                        int index = ServerUtils.RandomNumber(CacheCauCa.IdCaiTrangCauCa.Count);
                                        short idCaiTrangCauCa = (short)CacheCauCa.IdCaiTrangCauCa[index];
                                        var item = ItemCache.GetItemDefault(idCaiTrangCauCa);
                                        item.Options.Add(new OptionItem()
                                        {
                                            Id = 50,
                                            Param = ServerUtils.RandomNumber(10, 20)
                                        });
                                        item.Options.Add(new OptionItem()
                                        {
                                            Id = 77,
                                            Param = ServerUtils.RandomNumber(10, 20)
                                        });
                                        item.Options.Add(new OptionItem()
                                        {
                                            Id = 103,
                                            Param = ServerUtils.RandomNumber(10, 20)
                                        });
                                        if (ServerUtils.RandomNumber(100) >= CacheCauCa.PercentDropItemVV)
                                        {
                                            item.Options.Add(new OptionItem()
                                            {
                                                Id = 93,
                                                Param = ServerUtils.RandomNumber(1, 7)
                                            });
                                        }
                                        item.Options.Add(new OptionItem()
                                        {
                                            Id = 30,
                                            Param = 1
                                        });
                                        item.Quantity = 1;
                                        var itemMap = new ItemMap(character.Id, item);
                                        itemMap.X = character.InfoChar.X;
                                        itemMap.Y = character.InfoChar.Y;
                                        character.Zone.ZoneHandler.LeaveItemMap(itemMap);
                                    }
                                    else
                                    {
                                        int idLoaiCaCauDuoc = RandomLoaiCaDacBiet();
                                        var item = ItemCache.GetItemDefault((short)idLoaiCaCauDuoc);
                                        item.Quantity = 3;
                                        var itemMap = new ItemMap(character.Id, item);
                                        itemMap.X = character.InfoChar.X;
                                        itemMap.Y = character.InfoChar.Y;
                                        character.Zone.ZoneHandler.LeaveItemMap(itemMap);
                                    }
                                }
                                else
                                {
                                    character.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, thatBai));
                                    if (CacheCauCa.Exp_SM != 0 && CacheCauCa.Exp_TN != 0)
                                    {
                                        character.CharacterHandler.PlusPower(CacheCauCa.Exp_SM / 2);
                                        character.CharacterHandler.PlusPotential(CacheCauCa.Exp_TN / 2);
                                        character.CharacterHandler.SendMessage(Service.UpdateExp(0, CacheCauCa.Exp_SM / 2));
                                        character.CharacterHandler.SendMessage(Service.UpdateExp(1, CacheCauCa.Exp_TN / 2));
                                    }
                                    else
                                    {
                                        if (CacheCauCa.Exp_SM != 0)
                                        {
                                            character.CharacterHandler.PlusPower(CacheCauCa.Exp_SM / 2);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(0, CacheCauCa.Exp_SM / 2));
                                        }
                                        if (CacheCauCa.Exp_TN != 0)
                                        {
                                            character.CharacterHandler.PlusPotential(CacheCauCa.Exp_TN / 2);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(1, CacheCauCa.Exp_TN / 2));
                                        }
                                    }
                                }
                            };
                            var task = new Task(Action);
                            task.Start();
                        }
                        else
                        {
                            if (checkGold < CacheCauCa.GiaMoiCauThuong)
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Bạn không đủ tiền để mua mồi", false, character.InfoChar.Gender));
                            }
                            if (checkBag < 3)
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Cần ít nhất 3 ô hành trang trống", false, character.InfoChar.Gender));
                            }
                        }
                        break;
                    }
            }

        }

        static int RandomLoaiCaThuong()
        {
            int random = ServerUtils.RandomNumber(100); //< 40 

            if (random <= CacheCauCa.PercentNormal_CaBayMau)
            {
                if (random <= CacheCauCa.PercentNormal_CaDieuHong)
                {
                    return 1004;
                }
                else
                {
                    return 1003;
                }
            }
            else
            {
                return 1002;
            }
        }
        static int RandomLoaiCaDacBiet()
        {
            int random = ServerUtils.RandomNumber(100);
            if (random < (CacheCauCa.PercentSpecial_CaBayMau))
            {
                if (random < (CacheCauCa.PercentSpecial_CaDieuHong))
                {
                    return 1004;
                }
                else
                {
                    return 1003;
                }

            }
            else
            {
                return 1002;
            }
        }


        public static int GetGiaCa(int id)
        {
            int gia = 0;

            lock (DragonBoyZ.Application.Threading.Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToData();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return gia;
                    command.CommandText = $"SELECT `saleCoinLock` FROM `item` WHERE `id` = '{id}'";
                    using var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Đảm bảo chỉ có một giá trị duy nhất trong cột `count`
                        if (reader.Read())
                        {
                            gia = reader.GetInt32(0);
                        }
                    }

                    return gia;
                }
                catch (Exception e)
                {
                    string text = $"Error GetGiaCa: {e.Message}\n{e.StackTrace}";
                    DragonBoyZ.Application.Threading.Server.Gi().Logger.PrtColor("red", "Server", "darkred", text);
                    return gia;
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static void UpdateGiaCa(int id, int count = 0)
        {
            lock (DragonBoyZ.Application.Threading.Server.SQLLOCK)
            {
                try
                {
                    DbContext dbContext = DbContext.gI();

                    dbContext.ConnectToData();
                    using (DbCommand command = dbContext.Connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE `item` SET `saleCoinLock` = @count WHERE `id` = @id;";

                        DbParameter nameParam = command.CreateParameter();
                        nameParam.ParameterName = "@id";
                        nameParam.Value = id;
                        command.Parameters.Add(nameParam);

                        DbParameter countParam = command.CreateParameter();
                        countParam.ParameterName = "@count";
                        countParam.Value = count;
                        command.Parameters.Add(countParam);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    string text = $"Error UpdateGiaCa: {e.Message}\n{e.StackTrace}";
                    DragonBoyZ.Application.Threading.Server.Gi().Logger.PrtColor("red", "Server", "darkred", text);
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
        public static int RandomGiaCaNoc()
        {
            Random random = new Random();
            int minValue = CacheCauCa.Price_Canoc[0];
            int maxValue = CacheCauCa.Price_Canoc[1];
            int step = 1000000;

            int result = random.Next((maxValue - minValue) / step + 1) * step + minValue;
            return result;
        }
        public static int RandomGiaCa7Mau()
        {
            Random random = new Random();
            int minValue = CacheCauCa.Price_CaBayMau[0];
            int maxValue = CacheCauCa.Price_CaBayMau[1];
            int step = 1000000;

            int result = random.Next((maxValue - minValue) / step + 1) * step + minValue;
            return result;
        }
        public static int RandomGiaCaDieuHong()
        {
            Random random = new Random();
            int minValue = CacheCauCa.Price_CaDieuHong[0];
            int maxValue = CacheCauCa.Price_CaDieuHong[1];
            int step = 1000000;

            int result = random.Next((maxValue - minValue) / step + 1) * step + minValue;
            return result;
        }
    }
}
