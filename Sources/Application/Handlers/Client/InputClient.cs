using System;
using System.Collections.Generic;
using DragonBoyZ.Application.Menu;
using DragonBoyZ.Application.Constants;
using DragonBoyZ.Application.IO;
using DragonBoyZ.Application.Main;
using DragonBoyZ.Application.Threading;
using DragonBoyZ.Application.Manager;
using DragonBoyZ.DatabaseManager;
using DragonBoyZ.DatabaseManager.Player;
using DragonBoyZ.Model.Template;
using DragonBoyZ.Model.Option;
using DragonBoyZ.Model.Character;
using DragonBoyZ.Application.Map;
using System.Runtime.CompilerServices;
using System.Linq;
using DragonBoyZ.Application.Extension;
using DragonBoyZ.Application.Handlers.Menu;
using Sources.Database;
using DragonBoyZ.Application.Handlers.Item;
using DragonBoyZ.Application.Extension.Namecball;
using DragonBoyZ.Sources.Application.Extension;
using Google.Protobuf.WellKnownTypes;
using DragonBoyZ.Sources.Application.Activity.CauCa;

namespace DragonBoyZ.Application.Handlers.Client
{
    public static class InputClient
    {
        public static void JoinHome(Model.Character.Character character,bool isDefaul = true, bool isTeleport = false, int typeTeleport = 0)
        {
            var home = new Home(character.InfoChar.Gender);
            home.Maps[0].JoinZone(character, 0, isDefaul, isTeleport, typeTeleport);
        }


        public static  void JoinKarin(Model.Character.Character character,int mapId, bool isDefaul = false, bool isTeleport = false, int typeTeleport = 0)
        {
            var karin = new Karin();
            karin.GetMapById(mapId)
                .JoinZone(character, 0, isDefaul, isTeleport, typeTeleport);
        }
        public static void HanleInputClient(Model.Character.Character character, Message message)
        {
            if(message == null) return;
            try
            {
                var lengthInput = message.Reader.ReadByte();
                var listInput = new List<string>();
                for (var i = 0; i < lengthInput; i++)
                {
                    listInput.Add(message.Reader.ReadUTF());
                }
                if(listInput.Count <= 0) return;
                switch (character.TypeInput)
                {
                    case 0://Nạp thẻ
                        {
                            var soSeriText = listInput[0];
                            var maPinText = listInput[1];

                            Console.WriteLine("Loai the " + character.NapTheTemp.LoaiThe + " menh gia " + character.NapTheTemp.MenhGia + " So Seri " + soSeriText + " ma pin " + maPinText);
                            GachThe.SendCard(character, character.NapTheTemp.LoaiThe, character.NapTheTemp.MenhGia, soSeriText, maPinText);
                            break;
                        }
                    case 1://Gift code 
                        {
                            var codeInput = listInput[0];
                            GiftcodeDataBase.RewardGiftcode(character, codeInput);
                            break;
                        }
                    #region Menu Buff Admin
                    case 2://MenuAdmin_Đổi mật khẩu
                        {
                            var timeServer = ServerUtils.CurrentTimeMillis();
                            var oldPass = listInput[0];
                            var newPass = listInput[1];
                            // var sdt = listInput[2];
                            var checkData = UserDB.CheckBeforeChangePass(character.Player.Id, oldPass);
                            if (!checkData)
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Thông tin tài khoản không chính xác, vui lòng nhập lại."));
                                return;
                            }
                            UserDB.DoiMatKhau(character.Player.Id, newPass);
                            character.CharacterHandler.SendMessage(Service.OpenUiSay((short)character.ShopId, "Đổi mật khẩu thành công, vui lòng thoát game và đăng nhập lại"));
                            break;
                        }
                    case 3://MenuAdmin_Ban Player
                        {
                            var namePlayer = listInput[0];
                            var banReason = listInput[1];
                            var @char = ClientManager.Gi().GetCharacter(namePlayer);
                            if (@char != null)
                            {
                                UserDB.BanUser(@char.Player.Id);
                                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("Nhân vật " + namePlayer + " đã bị khóa tài khoản với lý do: " + banReason));
                                ClientManager.Gi().KickSession(@char.Player.Session);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy tên nhân vật này đang online"));
                            }
                            Console.WriteLine("ten TK: " + namePlayer + " ly do ban: " + banReason);
                            ServerUtils.WriteLog("AdminBan/" + @char.Player.Id, $"Lý do: " + banReason);
                            break;
                        }
                    case 4: //MenuAdmin-BuffItem
                        {
                            string namePlayer = listInput[0];
                            string itemId = listInput[1];
                            string opItem = listInput[2];
                            int soLuong = Int32.Parse(listInput[3]);
                            int itemIdInt = Int32.Parse(itemId);
                            int maxQuantity = 99;

                            int[] optionIds = opItem.Split('.').Select(s => Int32.Parse(s.Split('-')[0])).ToArray();
                            int[] optionParams = opItem.Split('.').Select(s => Int32.Parse(s.Split('-')[1])).ToArray();

                            bool checkZero = Array.Exists(optionIds, element => element == 0) && Array.Exists(optionParams, element => element == 0);
                            var @nhanvat = ClientManager.Gi().GetCharacter(namePlayer);
                            var itemToAdd = ItemCache.GetItemDefault((short)itemIdInt);
                            ItemTemplate itemTemplate = ItemCache.ItemTemplate(itemToAdd.Id);
                            if (itemTemplate.IsUpToUp)
                            {
                                if (soLuong <= 0)
                                {
                                    soLuong = 1;
                                }
                                if (soLuong > maxQuantity)
                                {
                                    soLuong = maxQuantity;
                                }
                            }

                            itemToAdd.Options.Add(new OptionItem()
                            {
                                Id = 73,
                                Param = 0
                            });

                            if (!checkZero)
                            {
                                for (int i = 0; i < optionIds.Length && i < optionParams.Length; i++)
                                {
                                    itemToAdd.Options.Add(new OptionItem()
                                    {
                                        Id = optionIds[i],
                                        Param = optionParams[i]
                                    });
                                }
                            }
                            itemToAdd.Quantity = soLuong;

                            if (namePlayer == "0")
                            {
                                character.CharacterHandler.AddItemToBag(true, itemToAdd, "Admin Buff");
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().ADD_ITEM, $"x{soLuong} {itemTemplate.Name}")));
                                break;
                            }
                            else if (@nhanvat != null)
                            {
                                @nhanvat.CharacterHandler.AddItemToBag(true, itemToAdd, "Admin Buff");
                                @nhanvat.CharacterHandler.SendMessage(Service.SendBag(@nhanvat));
                                @nhanvat.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().ADD_ITEM, $"x{soLuong} {itemTemplate.Name}")));
                                break;
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy tên nhân vật này đang online"));
                            }
                            break;
                        }
                    case 5://MenuAdmin_Spawn Boss
                        {
                            var idBoss = Int32.Parse(listInput[0]);
                            var newBoss = new Boss();
                            newBoss.CreateBoss(idBoss, character.InfoChar.X, character.InfoChar.Y);
                            newBoss.CharacterHandler.SetUpInfo();
                            character.Zone.ZoneHandler.AddBoss(newBoss);
                            break;
                        }
                    case 6://MenuAdmin_Kill
                        {
                            var namePlayer = listInput[0];
                            var @char = (Model.Character.Character)ClientManager.Gi().GetCharacter(namePlayer);
                            bool checkNamePlayer = (namePlayer == "0");
                            if (@char != null)
                            {
                                @char.CharacterHandler.MineHp(10000000000 * 100000);
                                @char.CharacterHandler.SendDie();
                                @char.CharacterHandler.SetUpInfo();
                                @char.CharacterHandler.SendMessage(Service.MeLoadPoint(@char));
                                @char.CharacterHandler.SendMessage(Service.ServerMessage("Nghiệp quật chetme mày."));
                                break;
                            }
                            else if (checkNamePlayer)
                            {
                                character.CharacterHandler.MineHp(10000000000 * 100000);
                                character.CharacterHandler.SendDie();
                                character.CharacterHandler.SetUpInfo();
                                character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Nghiệp quật chetme mày."));
                                break;
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy tên nhân vật này đang online"));
                            }
                            break;
                        }
                    case 7://MenuAdmin_Buff TNSM
                        {
                            var name = listInput[0];
                            var type = Int32.Parse(listInput[1]);
                            var count = Int32.Parse(listInput[2]);
                            var @char = ClientManager.Gi().GetCharacter(name);

                            //Kiểm tra namePlayer
                            bool checkNamePlayer = (name == "0");

                            switch (type)
                            {
                                case 0: // Sức mạnh
                                    {
                                        if (@char != null)
                                        {
                                            @char.CharacterHandler.PlusPower(count);
                                            @char.CharacterHandler.SendMessage(Service.UpdateExp(0, count));
                                            @char.CharacterHandler.SendMessage(Service.ServerMessage($"Buff {0} Sức mạnh thành công "));
                                        }
                                        else if (checkNamePlayer)
                                        {
                                            character.CharacterHandler.PlusPower(count);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(0, count));
                                            character.CharacterHandler.SendMessage(Service.ServerMessage($"Buff {0} Sức mạnh thành công "));
                                        }
                                        break;
                                    }
                                case 1: //Tiềm năng
                                    {
                                        if (@char != null)
                                        {
                                            @char.CharacterHandler.PlusPotential(count);
                                            @char.CharacterHandler.SendMessage(Service.UpdateExp(1, count));
                                            @char.CharacterHandler.SendMessage(Service.ServerMessage($"Buff {0} Tiềm năng thành công "));
                                        }
                                        else if (checkNamePlayer)
                                        {
                                            character.CharacterHandler.PlusPotential(count);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(1, count));
                                            character.CharacterHandler.SendMessage(Service.ServerMessage($"Buff {0} Tiềm năng thành công "));
                                        }
                                        break;
                                    }
                                case 2: //Ca hai
                                    {
                                        if (@char != null)
                                        {
                                            @char.CharacterHandler.PlusPotential(count);
                                            @char.CharacterHandler.SendMessage(Service.UpdateExp(1, count));
                                            @char.CharacterHandler.PlusPower(count);
                                            @char.CharacterHandler.SendMessage(Service.UpdateExp(0, count));
                                            @char.CharacterHandler.SendMessage(Service.ServerMessage($"Buff {count} Sức mạnh và tiềm năng thành công "));
                                        }
                                        else if (checkNamePlayer)
                                        {
                                            character.CharacterHandler.PlusPotential(count);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(1, count));
                                            character.CharacterHandler.PlusPower(count);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(0, count));
                                            character.CharacterHandler.SendMessage(Service.ServerMessage($"Buff {count} Sức mạnh và tiềm năng thành công "));
                                        }
                                        break;
                                    }
                            }

                            break;
                        }
                    case 8://MenuAdmin_Buff Chỉ Số
                        {
                            var namePlayer = listInput[0];
                            var type = Int32.Parse(listInput[1]);
                            var countHp = Int32.Parse(listInput[2]);
                            var countKi = Int32.Parse(listInput[3]);
                            var countSd = Int32.Parse(listInput[4]);
                            var countCrit = Int32.Parse(listInput[5]);
                            var countAmor = Int32.Parse(listInput[6]);
                            var @char = (Model.Character.Character)ClientManager.Gi().GetCharacter(namePlayer);
                            var info1 = $"{ServerUtils.Color("red")}Đã Buff Chỉ Số: ";
                            info1 += $"{ServerUtils.Color("blue")}HP: {countHp}\nKI: {countKi}\nSức đánh: {countSd}\nCrit: {countCrit}\nGiáp: {countAmor}";
                            var info2 = $"{ServerUtils.Color("red")}Đã Set Chỉ Số: ";
                            info2 += $"{ServerUtils.Color("blue")}HP: {countHp}\nKI: {countKi}\nSức đánh: {countSd}\nCrit: {countCrit}\nGiáp: {countAmor}";

                            //Kiểm tra namePlayer
                            bool checkNamePlayer = (namePlayer == "0");

                            switch (type)
                            {
                                case 0: //Buff
                                    {
                                        if (@char != null)
                                        {
                                            @char.InfoChar.OriginalHp += countHp;
                                            @char.InfoChar.OriginalMp += countKi;
                                            @char.InfoChar.OriginalDamage += countSd;
                                            @char.InfoChar.OriginalCrit += countCrit;
                                            @char.InfoChar.OriginalDefence += countAmor;
                                            @char.CharacterHandler.SetUpInfo();
                                            @char.CharacterHandler.SendMessage(Service.MeLoadPoint(@char));
                                            @char.CharacterHandler.SendMessage(Service.BigMessage(info1));
                                            break;
                                        }
                                        else if (checkNamePlayer)
                                        {
                                            character.InfoChar.OriginalHp += countHp;
                                            character.InfoChar.OriginalMp += countKi;
                                            character.InfoChar.OriginalDamage += countSd;
                                            character.InfoChar.OriginalCrit += countCrit;
                                            character.InfoChar.OriginalDefence += countAmor;
                                            character.CharacterHandler.SetUpInfo();
                                            character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                                            character.CharacterHandler.SendMessage(Service.BigMessage(info1));
                                            break;
                                        }
                                        else
                                        {
                                            character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy tên nhân vật này đang online"));
                                        }
                                        break;
                                    }
                                case 1: //Set
                                    {
                                        if (@char != null)
                                        {
                                            @char.InfoChar.OriginalHp = countHp;
                                            @char.InfoChar.OriginalMp = countKi;
                                            @char.InfoChar.OriginalDamage = countSd;
                                            @char.InfoChar.OriginalCrit = countCrit;
                                            @char.InfoChar.OriginalDefence = countAmor;
                                            @char.CharacterHandler.SetUpInfo();
                                            @char.CharacterHandler.SendMessage(Service.MeLoadPoint(@char));
                                            @char.CharacterHandler.SendMessage(Service.BigMessage(info2));
                                            break;
                                        }
                                        else if (checkNamePlayer)
                                        {
                                            character.InfoChar.OriginalHp = countHp;
                                            character.InfoChar.OriginalMp = countKi;
                                            character.InfoChar.OriginalDamage = countSd;
                                            character.InfoChar.OriginalCrit = countCrit;
                                            character.InfoChar.OriginalDefence = countAmor;
                                            character.CharacterHandler.SetUpInfo();
                                            character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                                            character.CharacterHandler.SendMessage(Service.BigMessage(info2));
                                            break;
                                        }
                                        else
                                        {
                                            character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy tên nhân vật này đang online"));
                                        }
                                        break;
                                    }
                            }
                            break;                         
                        }
                    case 9://MenuAdmin_Buff Task
                        {
                            var namePlayer = listInput[0];
                            var idTask = Int16.Parse(listInput[1]);
                            var indexTask = Int16.Parse(listInput[2]);
                            var countTask = Int16.Parse(listInput[3]);
                            var @char = (Model.Character.Character)ClientManager.Gi().GetCharacter(namePlayer);

                            //Check namePlayer
                            bool checkNamePlayer = (namePlayer == "0");

                            if (@char != null)
                            {
                                @char.InfoTask.Id = (short)idTask;
                                @char.InfoTask.Index = (sbyte)indexTask;
                                @char.InfoTask.Count = (short)countTask;
                                @char.CharacterHandler.SendMessage(Service.SendTask(@char));
                                @char.CharacterHandler.SendMessage(Service.BigMessage("Bạn đã được Buff nhiệm vụ."));
                                break;
                            }
                            else if (checkNamePlayer)
                            {
                                character.InfoTask.Id = (short)idTask;
                                character.InfoTask.Index = (sbyte)indexTask;
                                character.InfoTask.Count = (short)countTask;
                                character.CharacterHandler.SendMessage(Service.SendTask(character));
                                character.CharacterHandler.SendMessage(Service.BigMessage("Bạn đã được Buff nhiệm vụ."));
                                break;
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy tên nhân vật này đang online"));
                            }
                            break;
                        }
                    case 10://MenuAdmin_Kích Player
                        {
                            var namePlayer = ClientManager.Gi().GetCharacter(listInput[0]);
                            ClientManager.Gi().KickSession(namePlayer.Player.Session);
                            var temp = ClientManager.Gi().GetPlayer(namePlayer.Player.Id);
                            if (temp != null)
                            {
                                ClientManager.Gi().KickSession(temp.Session);
                            }
                            character.CharacterHandler.SendMessage(Service.BigMessage($"Đã kick {namePlayer}."));
                            break;
                        }
                    case 11://MenuAdmin_Teleport
                        {
                            var namePlayer = listInput[0];
                            var map111 = MapManager.Get(character.InfoChar.MapId);
                            var @char = ClientManager.Gi().GetCharacter(namePlayer);
                            var zone = @char.Zone;

                            if (@char != null)
                            {
                                var mapId = @char.InfoChar.MapId;
                                var mapTeleport = MapManager.Get(mapId);
                                if (zone == null) return;
                                if (zone.Characters.Count >= mapTeleport.TileMap.MaxPlayers)
                                {
                                    character.CharacterHandler.SendMessage(
                                        Service.ServerMessage(TextServer.gI().MAX_NUMCHARS));
                                    return;
                                }
                                character.InfoChar.X = (short)ServerUtils.RandomNumber(@char.InfoChar.X - 30, @char.InfoChar.X + 30);
                                character.InfoChar.Y = @char.InfoChar.Y;
                                map111.OutZone(character, mapTeleport.Id);
                                zone.ZoneHandler.JoinZone(character, false, false, 0);
                                character.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, ServerUtils.FilterWords($"Admin tới chơi, Bro!!!")));
                                @char.CharacterHandler.SendZoneMessage(Service.PublicChat(@char.Id, ServerUtils.FilterWords($"Hú hồn hà")));
                            }
                            break;
                        }
                    case 12: //MenuAdmin_Check Giftcode
                        {
                            var giftcode = listInput[0];
                            if (!GiftcodeDataBase.GetCode(giftcode))
                            {
                                character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy !"));
                                return;
                            }
                            var checkCount = GiftcodeDataBase.GetCount(giftcode);
                            var checkTimeExpire = GiftcodeDataBase.GetTimeExpire(giftcode);
                            var item = GiftcodeDataBase.GetItem(giftcode);
                            var gold = GiftcodeDataBase.GetThoiVang(giftcode);
                            var gem = GiftcodeDataBase.GetGem(giftcode);
                            var ruby = GiftcodeDataBase.GetRuby(giftcode);
                            var text = "";
                            text += "|0|Info Giftcode: " + giftcode + "\n";
                            text += "|7|Lượt nhập: " + checkCount + "\n";
                            text += "|7|HSD: " + checkTimeExpire + "\n";
                            for (int i = 0; i < item.Count; i++)
                            {
                                var ItemDefault = ItemCache.GetItemDefault(item[i].Id, item[i].Quantity);
                                var template = ItemCache.ItemTemplate(ItemDefault.Id);
                                var isTypeBody = template.IsTypeBody();
                                text += $"|2|{(isTypeBody ? "" : item[i].Quantity + " ")}{template.Name}\n";

                            }
                            if (gold != -1 || gold != 0)
                            {

                                text += $"|1|{gold} thỏi vàng\n";
                            }
                            if (gem != -1 || gem != 0)
                            {
                                text += $"|1|{gem} Ngọc xanh\n";
                            }
                            if (ruby != -1 || ruby != 0)
                            {
                                text += $"|1|{ruby} Hồng ngọc\n";
                            }
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(64, text, new List<string> { "OK" }, 3));
                            character.TypeMenu = 11;
                            break;
                        }
                    case 13: //MenuAdmin_Buff VND
                        {
                            var namePlayer = listInput[0];
                            var countVND = Int32.Parse(listInput[1]);
                            var @char = ClientManager.Gi().GetCharacter(namePlayer);

                            //Kiểm tra namePlayer
                            bool checkNamePlayer = (namePlayer == "0");

                            if (@char != null)
                            {
                                UserDB.NapVND(@char.Player.Id, countVND);
                                @char.CharacterHandler.SendMessage(Service.ServerMessage(string.Format($"Bạn đã nhận được {countVND}VND")));
                                break;
                            }
                            else if (checkNamePlayer)
                            {
                                UserDB.NapVND(character.Player.Id, countVND);
                                character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format($"Bạn đã nhận được {countVND}VND")));
                                break;
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy tên nhân vật này đang online"));
                            }
                            break;
                        }
                    case 23://MenuAdmin_Kill
                        {
                            var namePlayer = listInput[0];
                            var @char = (Model.Character.Character)ClientManager.Gi().GetCharacter(namePlayer);
                            bool checkNamePlayer = (namePlayer == "0");
                            if (@char != null)
                            {
                                @char.CharacterHandler.MineHp(10000000000 * 100000);
                                @char.CharacterHandler.SendDie();
                                @char.CharacterHandler.SetUpInfo();
                                @char.CharacterHandler.SendMessage(Service.MeLoadPoint(@char));
                                @char.CharacterHandler.SendMessage(Service.ServerMessage("Nghiệp quật chetme mày."));
                                break;
                            }
                            else if (checkNamePlayer)
                            {
                                character.CharacterHandler.MineHp(10000000000 * 100000);
                                character.CharacterHandler.SendDie();
                                character.CharacterHandler.SetUpInfo();
                                character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Nghiệp quật chetme mày."));
                                break;
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy tên nhân vật này đang online"));
                            }
                            break;
                        }
                    case 24://MenuAdmin_Thông báo toàn server
                        {
                            var inputNoiDung = listInput[0];
                            var info = $"Admin thông báo: " + inputNoiDung;
                            ClientManager.Gi().SendMessageCharacter(Service.WorldChat(null, info, 0));
                            break;
                        }
                    case 25://MenuAdmin_Thông báo player
                        {
                            var namPlayer = listInput[0];
                            var inputNoiDung = listInput[1];
                            var @char = (Model.Character.Character)ClientManager.Gi().GetCharacter(namPlayer);
                            var info = $"Admin thông báo: " + inputNoiDung;
                            if (@char != null)
                            {
                                @char.CharacterHandler.SendMessage(Service.WorldChat(null, info, 0));
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy tên nhân vật này đang online"));
                            }
                            break;
                        }
                    case 27: //MenuAdmin_Kéo player
                        {
                            var namePlayer = listInput[0];
                            var getMapMe = character.InfoChar.MapId;
                            var @char = ClientManager.Gi().GetCharacter(namePlayer);
                            if (@char != null)
                            {
                                @char.InfoChar.X = character.InfoChar.X;
                                @char.InfoChar.Y = character.InfoChar.Y;
                                MapManager.JoinMap((Model.Character.Character)@char, getMapMe, character.Zone.Id, false, false, 0);
                                @char.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã đến chỗ Admin!"));
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.DialogMessage("Không tìm thấy tên nhân vật này đang online"));
                            }
                            break;
                        }
                    #endregion
                                        
                    case 14: //Input chọn level BDKB CDRD
                        // ingored input level dungeon
                        var levele = Int32.Parse(listInput[0]);
                        int l2;
                        var isNumberr = Int32.TryParse(listInput[0], out l2);
                     
                        if (levele > 110 && !isNumberr && levele <= 0)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerChat("Level không hợp lệ, vui lòng chọn lại !"));
                            return;
                        }
                        var clan = ClanManager.Get(character.ClanId);

                        if (character.InfoChar.MapId == 48)
                        {
                            if (clan.cdrd.Count <= 0 && !clan.cdrd.Open){
                            character.CharacterHandler.SendMessage(Service.ServerChat("Bạn đã hết số lần tham gia trong ngày, vui lòng quay lại vào ngày mai !"));
                                return;
                            }
                            clan.cdrd.Init(levele);
                            MapManager.OutMap(character, clan.cdrd.MapCDRD[0].Id);
                            character.InfoChar.X = 1103;
                            character.InfoChar.Y = 336;
                            clan.cdrd.MapCDRD[0].JoinZone(character, 0);
                        }
                        else
                        {
                            if (clan.bdkb.Count <= 0){
                            character.CharacterHandler.SendMessage(Service.ServerChat("Bạn đã hết số lần tham gia trong ngày, vui lòng quay lại vào ngày mai !"));
                                return;
                            }
                            clan.bdkb.Init(levele);
                            MapManager.OutMap(character, clan.bdkb.MapBDKB[0].Id);
                            character.InfoChar.X = 78;
                            character.InfoChar.Y = 336;
                            clan.bdkb.MapBDKB[0].JoinZone(character, 0, false, false);
                        }
                        break;
                    case 15: // Đổi đệ tử
                        var ok = listInput[0];
                        if (ok == "ok" || ok == "OK")
                        {
                            var disciple = character.Disciple;
                            if (disciple == null)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().DONT_FIND_DISCIPLE));
                                return;
                            }

                            var itemDiscipleBody = disciple.ItemBody.FirstOrDefault(item => item != null);

                            if (itemDiscipleBody != null)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().PLEASE_EMPTY_DISCIPLE_BODY));
                                return;
                            }

                            var oldStatus = disciple.Status;

                            if (oldStatus < 3)
                            {
                                character.Zone.ZoneHandler.RemoveDisciple(character.Disciple);
                            }

                            disciple = new Disciple();
                            if (character.Disciple.InfoChar.Gender == 0)
                            {
                                disciple.CreateNewDisciple(character, 1);
                            }else if (character.Disciple.InfoChar.Gender == 1)
                            {
                                disciple.CreateNewDisciple(character, 2);
                            }
                            else
                            {
                                disciple.CreateNewDisciple(character, 0);
                            }
                            disciple.Player = character.Player;
                            disciple.Zone = character.Zone;
                            disciple.CharacterHandler.SetUpInfo();
                            character.Disciple = disciple;

                            if (!character.InfoChar.Fusion.IsFusion && oldStatus < 3)
                            {
                                character.Zone.ZoneHandler.AddDisciple(disciple);
                            }
                            else
                            {
                                character.CharacterHandler.SetUpInfo();
                                character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                                character.CharacterHandler.SendMessage(Service.SendHp((int)character.InfoChar.Hp));
                                character.CharacterHandler.SendMessage(Service.SendMp((int)character.InfoChar.Mp));
                                character.CharacterHandler.SendZoneMessage(Service.PlayerLevel(character));
                            }
                            character.CharacterHandler.RemoveItemBagById(401, 1, reason: "Dùng đổi đệ tử");
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            DiscipleDB.Update(disciple);
                        }
                        break;
                    case 16:
                        {
                            int n;
                            bool isNumeric = int.TryParse(listInput[0], out n);
                            if (!isNumeric)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            var inputValue = Int32.Parse(listInput[0]);

                            if (inputValue < 0)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            if (inputValue > UserDB.GetVND(character.Player))
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            UserDB.MineVND(character.Player, inputValue);
                            
                            character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn vừa quy đổi {inputValue} hồng ngọc"));
                        }
                        break;
                    case 111:
                        {
                            int n;
                            bool isNumeric = int.TryParse(listInput[0], out n);
                            if (!isNumeric)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            var inputValue = Int32.Parse(listInput[0]);
                            var thoivan = UserDB.GetThoiVang(character.Player);
                            if (inputValue < 0 || inputValue > thoivan)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            UserDB.MineThoiVang(character.Player, inputValue);
                            var item2 = ItemCache.GetItemDefault(457);
                            item2.Quantity = thoivan;
                            character.CharacterHandler.AddItemToBag(true, item2);
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage($"Bạn vừa quy đổi {inputValue} thỏi vàng"));
                            return;
                        }
                    case 17:
                        int c;
                        bool nb = int.TryParse(listInput[0], out c);
                        
                        if (!nb)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                            return;
                        }
                       
                        int thoivang = int.Parse(listInput[0]);

                        if (thoivang > character.CharacterHandler.GetThoiVangInBag() || character.CharacterHandler.GetItemBagById(457) == null || character.CharacterHandler.GetItemBagById(457).Quantity <= 0)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn không có đủ thỏi vàng"));
                            return;
                        }
                        character.DataMiniGame.thoivang += thoivang;
                        character.DataMiniGame.pickChan = true;
                        character.CharacterHandler.RemoveItemBagById(457, thoivang);
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Tổng số thỏi vàng đã đập vào Chẵn: " + thoivang));
                        break;
                    case 18:
                        int d;
                        bool nc = int.TryParse(listInput[0], out d);
                        if (!nc)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                            return;
                        }
                        thoivang = int.Parse(listInput[0]);
                        if (thoivang > character.CharacterHandler.GetThoiVangInBag() || character.CharacterHandler.GetItemBagById(457) == null || character.CharacterHandler.GetItemBagById(457).Quantity <= 0)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn không có đủ thỏi vàng"));
                            return;
                        }
                        character.DataMiniGame.thoivang += thoivang;
                        character.DataMiniGame.pickLe = true;
                        character.CharacterHandler.RemoveItemBagById(457, thoivang);
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Tổng số thỏi vàng đã đập vào Lẻ: " + thoivang));
                        break;
                    case 19:
                        string Name = listInput[0];
                        character.Name = Name;
                        character.CharacterHandler.SendMessage(Service.MeLoadAll(character));
                        break;
                    case 20:
                        Name = listInput[0];
                        character.Disciple.Name = Name;
                        character.Disciple.CharacterHandler.SendMessage(Service.MeLoadAll(character.Disciple));
               
                        break;
                    case 21: //Đổi mật khẩu
                        var passNow = listInput[0];
                        var passChange = listInput[1];
                        var confirmPassChange = listInput[2];
                        if (UserDB.GetPassword(character.Player).Contains(passNow))
                        {
                            // dung pass thuc hien change
                            if (confirmPassChange == passChange)
                            {

                                UserDB.ChangePassword(character.Player, passChange);
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(5,"Bạn đã đổi mật khẩu thành: " + passChange));
                            }
                            else
                            {
                                // ko confirm pass doi dung
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Nhập lại mật khẩu cần thay đổi phải đúng !"));
                            }
                        }
                        else
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Sai mật khẩu hiện tại!"));
                            // sai mk hien tai
                        }
                        break;
                    case 22: // Khí ga hủy diệt
                        var clan2 = ClanManager.Get(character.ClanId);
                        int m;
                        bool isNumer = int.TryParse(listInput[0], out m);
                        if (!isNumer)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                            return;
                        }
                        if (clan2 == null)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn không có bang hội?"));
                            return;
                        }
                        var level = Int32.Parse(listInput[0]);
                        if (level <= 0 || level > 110)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Chỉ có thể nhập từ cấp 0 -> cấp 110"));
                            return;
                        }
                        if (clan2.Gas.Count <= 0){
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã hết số lần tham gia trong ngày, vui lòng quay lại vào ngày mai !"));
                                return;
                            }
                        clan2.Gas.Level = level;
                        clan2.Gas.initMapKhiGas();
                        clan2.Gas.InitMob(level);
                        var mapOld = MapManager.Get(character.InfoChar.MapId);
                        mapOld.OutZone(character, 149);
                        character.InfoChar.X = 121;
                        character.InfoChar.Y = 336;
                        clan2.Gas.GasMaps[0].JoinZone(character, 0);
                        //foreach (var CharacterSameClan in character.Zone.Characters.Values.ToList().Where(c => c.ClanId == character.ClanId))
                        //{
                        //    mapOld.OutZone(character, 149);
                        //    clan2.Gas.GasMaps[0].JoinZone(CharacterSameClan, 0);
                        //  //  character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage("Doanh trại độc nhãn", 0, (int)(ServerUtils.CurrentTimeMillis() - clan.Reddot.timeDoanhTrai)));
                        //}
                        break;
                    case 26: //Bán thỏi vàng sll
                        {
                            int n;
                            bool isNumeric = int.TryParse(listInput[0], out n);
                            if (!isNumeric)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            var inputValue = Int32.Parse(listInput[0]);

                            if (inputValue < 0)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            // Kiểm tra có đủ VNĐ không
                            if (character.CharacterHandler.GetItemBagById(457).Quantity < inputValue || character.CharacterHandler.GetItemBagById(457).Quantity < 0 || character.CharacterHandler.GetItemBagById(457) == null)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ thỏi vàng !"));
                                return;
                            }
                            character.CharacterHandler.RemoveItemBagById(457, inputValue);
                            long GoldGet = (long)((long)inputValue * 500000000);
                            if (GoldGet + character.InfoChar.Gold >= character.InfoChar.LimitGold)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Đã Max giới hạn vàng, vui lòng mở rộng giới hạn vàng để bán !"));
                                return;
                            }
                            character.InfoChar.Gold += GoldGet;
                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ServerUtils.GetMoneys(GoldGet) + " vàng"));
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            break;
                        }
                    #region Event Bán Cá
                    case 28: //Bán cá nóc sll
                        {
                            int n;
                            bool isNumeric = int.TryParse(listInput[0], out n);
                            if (!isNumeric)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            var inputValue = Int32.Parse(listInput[0]);

                            if (inputValue < 0)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            // Kiểm tra có đủ cá không
                            if (character.CharacterHandler.GetItemBagById(1002).Quantity < inputValue
                                || character.CharacterHandler.GetItemBagById(1002).Quantity < 0
                                || character.CharacterHandler.GetItemBagById(1002) == null)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ cá để bán!"));
                                return;
                            }
                            character.CharacterHandler.RemoveItemBagById(1002, inputValue, "Bán cá");
                            long GoldGet = (long)((long)inputValue * HandlerCauCa.GetGiaCa(1002));
                            if (GoldGet + character.InfoChar.Gold >= character.InfoChar.LimitGold)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Đã Max giới hạn vàng, vui lòng mở rộng giới hạn vàng để bán !"));
                                return;
                            }
                            character.InfoChar.Gold += GoldGet;
                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ServerUtils.GetMoneys(GoldGet) + " vàng"));
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            break;
                        }
                    case 29: //Bán cá 7 màu sll
                        {
                            int n;
                            bool isNumeric = int.TryParse(listInput[0], out n);
                            if (!isNumeric)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            var inputValue = Int32.Parse(listInput[0]);

                            if (inputValue < 0)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            // Kiểm tra có đủ cá không
                            if (character.CharacterHandler.GetItemBagById(1003).Quantity < inputValue
                                || character.CharacterHandler.GetItemBagById(1003).Quantity < 0
                                || character.CharacterHandler.GetItemBagById(1003) == null)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ cá để bán!"));
                                return;
                            }
                            character.CharacterHandler.RemoveItemBagById(1003, inputValue, "Bán cá");
                            long GoldGet = (long)((long)inputValue * HandlerCauCa.GetGiaCa(1003));
                            if (GoldGet + character.InfoChar.Gold >= character.InfoChar.LimitGold)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Đã Max giới hạn vàng, vui lòng mở rộng giới hạn vàng để bán !"));
                                return;
                            }
                            character.InfoChar.Gold += GoldGet;
                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ServerUtils.GetMoneys(GoldGet) + " vàng"));
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            break;
                        }
                    case 30: //Bán cá diêu hồng sll
                        {
                            int n;
                            bool isNumeric = int.TryParse(listInput[0], out n);
                            if (!isNumeric)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            var inputValue = Int32.Parse(listInput[0]);

                            if (inputValue < 0)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                                return;
                            }
                            // Kiểm tra có đủ cá không
                            if (character.CharacterHandler.GetItemBagById(1004).Quantity < inputValue
                                || character.CharacterHandler.GetItemBagById(1004).Quantity < 0
                                || character.CharacterHandler.GetItemBagById(1004) == null)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ cá để bán!"));
                                return;
                            }
                            character.CharacterHandler.RemoveItemBagById(1004, inputValue, "Bán cá");
                            long GoldGet = (long)((long)inputValue * HandlerCauCa.GetGiaCa(1004));
                            if (GoldGet + character.InfoChar.Gold >= character.InfoChar.LimitGold)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Đã Max giới hạn vàng, vui lòng mở rộng giới hạn vàng để bán !"));
                                return;
                            }
                            character.InfoChar.Gold += GoldGet;
                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ServerUtils.GetMoneys(GoldGet) + " vàng"));
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            break;
                        }
                    #endregion

                    case 31: //Đổi tên viết tắt của bang
                        if (listInput[0].Length > 10)
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Tối đa 10 kí tự"));
                            return;
                        }
                        ClanManager.Get(character.ClanId).shortName = listInput[0];
                        break;
                    
                    case 40:
                        {
                            var @char = ClientManager.Gi().GetPlayerByUserName(listInput[0]);
                            var item = ItemCache.GetItemDefault((short)(int.Parse(listInput[1])), int.Parse(listInput[2]));
                            @char.Character.CharacterHandler.AddItemToBag(true, item);
                            @char.Character.CharacterHandler.SendMessage(Service.SendBag(character));
                            @char.Character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được x" + listInput[2] + " " + ItemCache.ItemTemplate(item.Id).Name + " từ Admin"));
                        }
                        break;
                    case 1999: //đổi vnd sang vàng
                    {
                        // kiểm tra có phải là số không
                        int n;
                        bool isNumeric = int.TryParse(listInput[0], out n);
                        if (!isNumeric) 
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                            return;
                        }
                        var inputValue = Int32.Parse(listInput[0]);

                        if (inputValue < 0)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().INPUT_CORRECT_NUMBER));
                            return;
                        }
                        // Kiểm tra có đủ VNĐ không
                        int vnd = UserDB.GetVND(character.Player);
                        if (vnd < inputValue)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_VND));
                            return;
                        }
                        // Kiểm tra giới hạn vàng trên người
                        long quyDoi = inputValue*550;
                        if (character.InfoChar.Gold + quyDoi > character.InfoChar.LimitGold)
                        {
                            var quyDoiToiDa = (character.InfoChar.LimitGold - character.InfoChar.Gold)/550;
                            character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().VND_TO_GOLD_LIMIT, ServerUtils.GetMoneys(quyDoiToiDa))));
                            return;
                        }
                        // Oke hết thì trừ VNĐ và cộng vàng
                        if (UserDB.MineVND(character.Player, inputValue))
                        {
                            character.PlusGold(quyDoi);
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));

                            if (inputValue >= 20000 && !character.InfoChar.IsPremium)
                            {
                                character.InfoChar.IsPremium = true;
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().UPGRADE_TO_PREMIUM));
                            }
                        }
                        character.TypeInput = 0;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error HanleInputClient in Service.cs: {e.Message} \n {e.StackTrace}", e);
            }
            finally
            {
                message?.CleanUp();
            }
        }
        
        public static void HandleNapThe(Model.Character.Character character, Message message)
        {
            var gender = character.InfoChar.Gender;
            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, string.Format("Hãy đến gặp {0} để nạp thẻ bạn nhé.", TextTask.NameNpc[gender]), false, gender));
        }
    }
}