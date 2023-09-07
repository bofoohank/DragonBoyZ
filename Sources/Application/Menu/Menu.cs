using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DragonBoyZ.Application.Constants;
using DragonBoyZ.Application.Handlers.Item;
using DragonBoyZ.Application.Handlers.Monster;
using DragonBoyZ.Application.IO;
using DragonBoyZ.Application.Manager;
using DragonBoyZ.Application.Map;
using DragonBoyZ.Application.Threading;
using DragonBoyZ.DatabaseManager;
using DragonBoyZ.DatabaseManager.Player;
using DragonBoyZ.Application.Menu;
using DragonBoyZ.Model.Character;
using DragonBoyZ.Model.Clan;
using DragonBoyZ.Model.Info;
using DragonBoyZ.Model.Option;
using DragonBoyZ.Model.SkillCharacter;
using DragonBoyZ.Model.Template;
using DragonBoyZ.Application.MainTasks;
using DragonBoyZ.Application.Extension;
using DragonBoyZ.Application.Extension.Chẵn_Lẻ_Momo;
using DragonBoyZ.Application.Extension.Bosses;
using DragonBoyZ.Application.Extension.ChampionShip;
using DragonBoyZ.Application.Extension.BlackballWar;
using DragonBoyZ.Application.Extension.Dragon;
using DragonBoyZ.Application.Extension.Event;
using DragonBoyZ.Application.Extension.Ký_gửi;
using DragonBoyZ.Application.Main;
using DragonBoyZ.Application.Handlers.Menu;
using DragonBoyZ.Application.Extension.Bosses.Mabu2Gio;
using DragonBoyZ.Application.Extension.Bosses.Mabu12Gio;
using DragonBoyZ.Application.Extension.Namecball;
using DragonBoyZ.Application.Train;
using DragonBoyZ.Application.Extension.Bosses.BigBoss;
using DragonBoyZ.Sources.Application.Activity.CauCa;

namespace DragonBoyZ.Application.Menu
{
    public static class Menu
    {
        public static void OpenUiMenu(short npcId, Character character)
        {
            Server.Gi().Logger.Debug($"Menu NpcId Case 33: ------------------------------------ {npcId}");
            try
            {
                switch (npcId)
                {
                    #region 3 ông già
                    case 0:
                    case 1:
                    case 2:
                        {
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBaOngGia[0], MenuNpc.Gi().MenuBaOngGia[0], character.InfoChar.Gender));
                                character.TypeMenu = 0;
                            }
                            break;
                        }
                    #endregion

                    #region 3-Rương đồ
                    case 3:
                        {
                            if (character.InfoChar.MapId == 153)
                            {
                                character.ShopId = 2222;
                                //character.CharacterHandler.SendMessage(Service.ClanBox(ClanManager.Get(character.ClanId).ClanBox));
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.SendBox(character, 1));
                            }
                            break;
                        }
                    #endregion

                    #region 4-Đậu thần
                    case 4:
                        {
                            var magicTree = MagicTreeManager.Get(character.Id);
                            if (magicTree == null) return;
                            var ngoc = magicTree.Diamond;
                            if (magicTree.IsUpdate)
                            {
                                character.CharacterHandler.SendMessage(Service.MagicTree1(new List<string>() { $"Nâng cấp\nnhanh\n{ngoc} ngọc", "Huỷ\nnâng cáp" }));
                            }
                            else
                            {
                                if (magicTree.Peas == magicTree.MaxPea)
                                {
                                    character.CharacterHandler.SendMessage(Service.MagicTree1(new List<string>()
                                    {"Thu hoạch", $"Nâng cấp\n{ServerUtils.ConvertMilisecond(DataCache.UpgradeDauThanTime[magicTree.Level - 1])}\n{ServerUtils.GetMoney(DataCache.UpgradeDauThanGold[magicTree.Level - 1])}\nvàng"}));
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.MagicTree1(new List<string>() { "Thu hoạch", $"Nâng cấp\n{ServerUtils.ConvertMilisecond(DataCache.UpgradeDauThanTime[magicTree.Level - 1])}\n300 Tr\nvàng", $"Kết hạt\nnhanh\n{ngoc} ngọc" }));
                                }
                            }
                            break;
                        }
                    #endregion

                    #region 7-Bumma
                    case 7:
                        {
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBumma[0], MenuNpc.Gi().MenuShopDistrict[0], character.InfoChar.Gender));
                                character.TypeMenu = 0;
                            }
                            break;
                        }
                    #endregion

                    #region 8-Dende
                    case 8:
                        {
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                if (character.DataNgocRongNamek.AlreadyPick(character))
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ồ, Ngọc Zồng Na Méc, bạn thật là may's mắn\nnếu tìm đủ 7 viên sẽ được [Zồng Thiên Na Méc] ban điều ước", new List<string> { "Hướng\ndẫn\nGọi Rồng", "Gọi Rồng", "Từ chối" }, character.InfoChar.Gender));
                                    character.TypeMenu = 2;
                                    break;
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextDende[0], MenuNpc.Gi().MenuShopDistrict[0], character.InfoChar.Gender));
                                    character.TypeMenu = 0;
                                    break;
                                }
                            }
                            break;
                        }
                    #endregion

                    #region 9-Appule
                    case 9:
                        {
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {

                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextAppule[0], MenuNpc.Gi().MenuShopDistrict[0], character.InfoChar.Gender));
                                character.TypeMenu = 0;
                            }
                            break;
                        }
                    #endregion

                    #region 10-Brief
                    case 10:
                        {
                            switch (character.InfoChar.MapId)
                            {

                                case 153:
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta có thể giúp gì cho bang hội của bạn?", new List<string> { "Chức năng\nbang hội", "Nhiệm vụ\nBang\n[5/5]", "Đảo Kame", "Đóng" }, character.InfoChar.Gender));
                                    character.TypeMenu = 1;
                                    break;
                                default:
                                    if (!TaskHandler.gI().ReportTask(character, npcId))
                                    {
                                        character.CharacterHandler.SendMessage(character.InfoChar.MapId == 84
                                        ? Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBrief[1],
                                            new List<string>()
                                            {
                                    character.InfoChar.Gender != 0
                                        ? character.InfoChar.Gender != 1 ? "Về Xayda" : "Về Namếc"
                                        : "Về\nTrái Đất"
                                            }, character.InfoChar.Gender)
                                        : Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBrief[0], MenuNpc.Gi().MenuBrief[0],
                                            character.InfoChar.Gender));
                                    }
                                    break;
                            }
                            break;
                        }
                    #endregion

                    #region 11-Cargo
                    case 11:
                        {
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextCargo[0], MenuNpc.Gi().MenuCargo[0], character.InfoChar.Gender));
                                break;
                            }
                            break;
                        }
                    #endregion

                    #region 12-Cui
                    case 12:
                        {
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                switch (character.InfoChar.MapId)
                                {
                                    case 19:
                                        if (character.InfoTask.Id >= 21)
                                        {
                                            //character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.gI().TextCui[1], MenuNpc.gI().MenuCui[2], character.InfoChar.Gender));
                                            HelpMission.openMenuCui(character);
                                        }
                                        else
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Bạn phải hoàn thành nhiệm vụ mới có thể mở khóa chức năng này"));

                                            //TaskHandler.DoUISay(character, "Thằng nhãi con, chưa xong nhiệm vụ đi đâu đây?");
                                        }
                                        break;
                                    case 68:
                                        if (character.InfoTask.Id >= 21)
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextCui[2], MenuNpc.Gi().MenuCui[3], character.InfoChar.Gender));
                                            // HelpMission.openMenuCui(character);
                                        }
                                        else
                                        {
                                            //TaskHandler.DoUISay(character, "Thằng nhãi con, chưa xong nhiệm vụ đi đâu đây?");
                                            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Bạn phải hoàn thành nhiệm vụ mới có thể mở khóa chức năng này"));
                                        }
                                        break;
                                    default:
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextCui[0], MenuNpc.Gi().MenuCui[0], character.InfoChar.Gender));
                                        break;
                                }
                            }
                            break;

                        }
                    #endregion

                    #region 13-Quy lão
                    case 13:
                        {
                            if (TaskHandler.CheckTask(character, 13, 0))
                            {
                                TaskHandler.gI().PlusSubTask(character, 1);
                                return;
                            }
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                if (character.InfoChar.LearnSkill != null)
                                {
                                    var itemAdd = character.InfoChar.LearnSkill.ItemSkill;
                                    var time = character.InfoChar.LearnSkill.Time - ServerUtils.CurrentTimeMillis();
                                    var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skill => skill.Id == character.InfoChar.LearnSkill.ItemTemplateSkillId);

                                    if (character.InfoChar.LearnSkill.Time <= ServerUtils.CurrentTimeMillis())
                                    {
                                        ItemHandler.AddLearnSkill(character, itemAdd, skillTemplate);
                                        character.InfoChar.LearnSkill = null;
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[0], MenuNpc.Gi().MenuQuyLao[0], character.InfoChar.Gender));
                                        character.TypeMenu = 0;
                                    }
                                    else
                                    {
                                        var itemTempalte = ItemCache.ItemTemplate(itemAdd.Id);
                                        var ngoc = 5;
                                        if (time / 600000 >= 2)
                                        {
                                            ngoc += (int)time / 600000;
                                        }

                                        var menu = string.Format(TextServer.gI().ADDING_SKILL, skillTemplate.Name,
                                            itemTempalte.Level, ServerUtils.GetTime(time));
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, menu, new List<string>() { $"Học\nCấp tốc\n{ngoc} ngọc", "Huỷ", "Bỏ qua" }, character.InfoChar.Gender));
                                        character.TypeMenu = 3;
                                    }
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[0], MenuNpc.Gi().MenuQuyLao[0], character.InfoChar.Gender));
                                    character.TypeMenu = 0;
                                }
                                break;
                            }
                            break;
                        }
                    #endregion

                    #region 14-Trưởng lão Guru
                    case 14:
                        {
                            if (TaskHandler.CheckTask(character, 13, 0))
                            {
                                TaskHandler.gI().PlusSubTask(character, 1);
                                return;
                            }
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                if (character.InfoChar.Gender != 1)
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Nơi đây chỉ dành cho những chiến binh Namếc, hãy về hành tinh của mình đi."));
                                    return;
                                }
                                if (character.InfoChar.LearnSkill != null)
                                {
                                    var itemAdd = character.InfoChar.LearnSkill.ItemSkill;
                                    var time = character.InfoChar.LearnSkill.Time - ServerUtils.CurrentTimeMillis();
                                    var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skill => skill.Id == character.InfoChar.LearnSkill.ItemTemplateSkillId);

                                    if (character.InfoChar.LearnSkill.Time <= ServerUtils.CurrentTimeMillis())
                                    {
                                        ItemHandler.AddLearnSkill(character, itemAdd, skillTemplate);
                                        character.InfoChar.LearnSkill = null;
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[1], MenuNpc.Gi().MenuQuyLao[1], character.InfoChar.Gender));
                                        character.TypeMenu = 0;
                                    }
                                    else
                                    {
                                        var itemTempalte = ItemCache.ItemTemplate(itemAdd.Id);
                                        var ngoc = 5;
                                        if (time / 600000 >= 2)
                                        {
                                            ngoc += (int)time / 600000;
                                        }

                                        var menu = string.Format(TextServer.gI().ADDING_SKILL, skillTemplate.Name,
                                            itemTempalte.Level, ServerUtils.GetTime(time));
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, menu, new List<string>() { $"Học\nCấp tốc\n{ngoc} ngọc", "Huỷ", "Bỏ qua" }, character.InfoChar.Gender));
                                        character.TypeMenu = 2;
                                    }
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[0], MenuNpc.Gi().MenuQuyLao[1], character.InfoChar.Gender));
                                    character.TypeMenu = 0;
                                }
                                break;
                            }
                            break;
                        }
                    #endregion

                    #region 15-Vua vegeta
                    case 15:
                        {
                            if (TaskHandler.CheckTask(character, 13, 0))
                            {
                                TaskHandler.gI().PlusSubTask(character, 1);
                                return;
                            }
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                if (character.InfoChar.Gender != 2)
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Nơi đây chỉ dành cho những chiến binh Xayda, hãy về hành tinh của mình đi."));
                                    return;
                                }
                                if (character.InfoChar.LearnSkill != null)
                                {
                                    var itemAdd = character.InfoChar.LearnSkill.ItemSkill;
                                    var time = character.InfoChar.LearnSkill.Time - ServerUtils.CurrentTimeMillis();
                                    var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skill => skill.Id == character.InfoChar.LearnSkill.ItemTemplateSkillId);

                                    if (character.InfoChar.LearnSkill.Time <= ServerUtils.CurrentTimeMillis())
                                    {
                                        ItemHandler.AddLearnSkill(character, itemAdd, skillTemplate);
                                        character.InfoChar.LearnSkill = null;
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[1], MenuNpc.Gi().MenuQuyLao[1], character.InfoChar.Gender));
                                        character.TypeMenu = 0;
                                    }
                                    else
                                    {
                                        var itemTempalte = ItemCache.ItemTemplate(itemAdd.Id);
                                        var ngoc = 5;
                                        if (time / 600000 >= 2)
                                        {
                                            ngoc += (int)time / 600000;
                                        }

                                        var menu = string.Format(TextServer.gI().ADDING_SKILL, skillTemplate.Name,
                                            itemTempalte.Level, ServerUtils.GetTime(time));
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, menu, new List<string>() { $"Học\nCấp tốc\n{ngoc} ngọc", "Huỷ", "Bỏ qua" }, character.InfoChar.Gender));
                                        character.TypeMenu = 2;
                                    }
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[0], MenuNpc.Gi().MenuQuyLao[1], character.InfoChar.Gender));
                                    character.TypeMenu = 0;
                                }
                                break;
                            }
                            break;
                        }
                    #endregion

                    #region 16-Uron
                    case 16:
                        {
                            var idShop = 15 + character.InfoChar.Gender;
                            character.CharacterHandler.SendMessage(Service.Shop(character, 0, idShop));
                            character.ShopId = idShop;
                            character.TypeShop = 0;
                            break;
                        }
                    #endregion

                    #region 17-Bò Mộng
                    case 17:
                        {
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBoMong[0], MenuNpc.Gi().MenuBoMong[0], character.InfoChar.Gender));
                                character.TypeMenu = 0;
                            }
                            break;
                        }
                    #endregion

                    #region 18-Thần mèo
                    case 18:
                        {
                            //if (TaskHandler.CheckTaskFinish(character, npcId) && character.InfoChar.OriginalDamage >= 10000)
                            //{
                            //    TaskHandler.DoClickNpcToNextTaskWithList(character, npcId);
                            //}
                            //else
                            //{
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                if (character.ConDuongRanDoc.isCDRD)
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Hãy cầm lấy hai hạt đậu cuối cùng của ta đây\nCố giữ mình nhé " + character.Name + "!", new List<string> { "Cảm ơn\nSư phụ" }, character.InfoChar.Gender));
                                    character.TypeMenu = 4;

                                }
                                else
                                {
                                    switch (character.DataTraining.Level)
                                    {
                                        case > 2:
                                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Con hãy bay theo cây Gậy Như Ý trên đỉnh tháp để đến Thần Điện gặp Thượng Đế\nCon rất xứng đáng làm đệ tử ông ấy.", new List<string> { !character.DataTraining.isTraining ? "Đăng ký\ntập\ntự động" : "Hủy đăng\nký tập\ntự động", "Tập luyện\nvới\nThần Mèo", "Tập luyện\nvới\nYajirô" }, character.InfoChar.Gender));
                                            character.TypeMenu = 3;
                                            break;
                                        case 1:

                                            break;
                                        case 0:
                                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId,
                                             "Muốn chiến thắng Tàu Pảy Pảy phải đánh bại được ta đã", new List<string> { !character.DataTraining.isTraining ? "Đăng ký\ntập\ntự động" : "Hủy đăng\nký tập\ntự động", "Nhiệm vụ", "Tập luyện\nvới\nThần Mèo", "Thách đấu\nThần Mèo" }, character.InfoChar.Gender));
                                            character.TypeMenu = 0;
                                            break;
                                    }
                                }
                            }
                            //}
                            break;
                        }
                    #endregion

                    #region 19-Thượng đế
                    case 19:
                        {
                            switch (character.InfoChar.MapId)
                            {
                                case 141:
                                    if (character.Zone.ZoneHandler.GetCountMob() <= 0)
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Hãy nắm lấy tay ta mau !", new List<string> { "Về\nthần điện" }, character.InfoChar.Gender));
                                        character.TypeMenu = 0;
                                    }
                                    else
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Đánh hết quái đi rồi tính tiếp !", new List<string> { "OK" }, character.InfoChar.Gender));
                                        character.TypeMenu = 10;
                                    }
                                    break;
                                default:
                                    {
                                        switch (character.DataTraining.Level)
                                        {

                                            case 2: // mr po po
                                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Pôpô là đệ tử của ta, luyện tập với Pôpô con sẽ có thêm nhiều\nkinh nghiệm\nđánh bại được Pôpô ta sẽ dạy võ công cho con", new List<string> { (character.DataTraining.isTraining) ? "Hủy Đăng\nký tập\ntự động" : "Đăng ký\ntập\ntự động", "Tập luyện\nvới\nMr.Pôpô", "Thách đấu\nvới\nMr.Pôpô" }, character.InfoChar.Gender));
                                                character.TypeMenu = 3;
                                                break;
                                            case 3: // thuong de
                                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Từ nay con sẽ là đệ tử của ta. Ta sẽ truyền cho con tất cả tuyệt kĩ", new List<string> { (character.DataTraining.isTraining) ? "Hủy Đăng\nký tập\ntự động" : "Đăng ký\ntập\ntự động", "Tập luyện\nvới\nThượng Đế", "Thách đấu\nvới\nThượng Đế" }, character.InfoChar.Gender));
                                                character.TypeMenu = 4;
                                                break;
                                            default:
                                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextThuongDe[1], new List<string>{(character.DataTraining.isTraining) ? "Hủy Đăng\nký tập\ntự động" : "Đăng ký\ntập\ntự động",
                "Tập luyện\nvới\nMr.Pôpô",
                "Tập luyện\nvới\nThuợng Đế",
                "Đến\nKaio",
                "Vòng quay\nMay mắn",
                                            "Top Vòng quay\nmay mắn"}, character.InfoChar.Gender));
                                                character.TypeMenu = 0;
                                                break;
                                        }

                                        break;
                                    }
                            }
                            break;
                        }
                    case 20:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextThanVuTru[0], MenuNpc.Gi().MenuThanVuTru[0], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 21-Bà hạt mít
                    case 21:
                        {
                            switch (character.InfoChar.MapId)
                            {
                                case 5:
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBaHatMit[0], MenuNpc.Gi().MenuBaHatMit[ConfigManager.gI().SuKienHe ? 26 : 3], character.InfoChar.Gender));
                                        character.TypeMenu = 1;
                                        break;
                                    }
                                case 42:
                                case 43:
                                case 44:
                                case 84:
                                    {
                                        List<string> menuBaHatMit = new List<string>();
                                        var bongTaiPorata2 = character.CharacterHandler.GetItemBagById(921);

                                        menuBaHatMit = MenuNpc.Gi().MenuBaHatMit[(character.InfoChar.IsNhanBua ? 0 : 1)];

                                        if (bongTaiPorata2 != null)
                                        {
                                            menuBaHatMit[(character.InfoChar.IsNhanBua ? 5 : 4)] = "Mở chỉ số\nBông tai\nPorata cấp 2";
                                        }

                                        character.CharacterHandler.SendMessage(
                                            Service
                                                .OpenUiConfirm(npcId, MenuNpc.Gi().TextBaHatMit[0], menuBaHatMit, character.InfoChar.Gender));
                                        character.TypeMenu = 0;
                                        break;
                                    }
                                case 46:
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBaHatMit[0], MenuNpc.Gi().MenuBaHatMit[15], character.InfoChar.Gender));
                                        character.TypeMenu = 14;
                                        break;
                                    }
                                case 112:
                                    if (character.DataVoDaiSinhTu.Round == 5 && !character.DataVoDaiSinhTu.Reward)
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId,
                                            "Đây là phần thưởng cho con!", new List<string> { "1 vệ tinh\nbất kì", "1 Bùa 1h\nbất kì" }, character.InfoChar.Gender));
                                        character.TypeMenu = 23;

                                    }
                                    else
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ngươi muốn đăng ký thi đấu võ đài?\nNhiều phần thưởng giá trị đang đợi ngươi đó.", new List<string> { "Top 100", "Đồng ý", "Từ chối", "Về\nđảo rùa" }, character.InfoChar.Gender));
                                        character.TypeMenu = 22;
                                    }


                                    break;
                            }

                            break;
                        }
                    #endregion

                    #region 22-Trọng tài
                    case 22:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Đại hội võ thuật Siêu Hạng\ndiễn ra 24/7 kể cả ngày lễ và chủ nhật\nHãy thi đấu ngay để khẳng định đẳng cấp của mình nhé",
                                   new List<string> { "Top 100\nCao Thủ", "Hướng\ndẫn\nthêm", $"Miễn phí\nCòn {character.DataSieuHang.Ticket} vé", "Ưu tiên\nđấu ngay", "Về\n\nĐại Hội\nVõ Thuật" }, character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 23-Ghi danh
                    case 23:
                        {
                            ChampionShip.gI().OpenMenuGhiDanh(character);
                            //if (TaskHandler.CheckTask(character, 19, 1))
                            //{
                            //    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Muốn bỏ qua nhiệm vụ Đại Hội Võ Thuật, bạn cần 10.000 Ngọc\nBạn có muốn bỏ qua không?", new List<String> { "Bỏ qua", "Từ chối" }, character.InfoChar.Gender));
                            //    character.TypeMenu = 6;
                            //}
                            //else
                            //{
                            //    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Nhót con miệng còn hôi sữa ra đây ăn cờ cờ", new List<String> { "CC" }, character.InfoChar.Gender));
                            //    character.TypeMenu = 7;   
                            //}
                            break;
                        }
                    #endregion

                    #region 25-Lính Canh
                    case 25:
                        if (character.ClanId != -1 && ClanManager.Get(character.ClanId) != null)
                        {
                            var clan = ClanManager.Get(character.Id);
                            //if (ServerUtils.TimeNow().Day - clan.TimeClanCreate.Day < 2)
                            //{
                            //    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Yêu cầu bang hội thành lập trên 2 ngày",
                            //      new List<string> { "Hướng\ndẫn\nthêm" }, character.InfoChar.Gender));
                            //    character.TypeMenu = 2;
                            //}
                            //else if (ServerUtils.TimeNow().Day - clan.Thành_viên.FirstOrDefault(i=>i.Id == character.Id).DateJoin.Day < 2)
                            //{
                            //    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ngươi phải tham gia bang hội trên 2 ngày thì mới được đi doanh trại",
                            //       new List<string> { "Hướng\ndẫn\nthêm" }, character.InfoChar.Gender));
                            //    character.TypeMenu = 2;
                            //}

                            //if (ServerUtils.TimeNow().Day - ClanManagerr.Get(character.ClanId).TimeClanCreate.Day  < 2)
                            //{
                            //    character.CharacterHandler.SendMessage(Service.ServerMessage("Yêu cầu bang hội được thành lập trên 2 ngày"));
                            //    return;
                            //}
                            if (character.Zone.ZoneHandler.GetCharacterClanInMap(character.ClanId).Count < 2 && !ClanManager.Get(character.ClanId).Reddot.Open)
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ít nhất phải có 1 đồng đội bên cạnh để vào",
                                  new List<string> { "Hướng\ndẫn\nthêm" }, character.InfoChar.Gender));
                                character.TypeMenu = 1;
                                return;
                            }
                            if (ClanManager.Get(character.ClanId).Reddot.Open)
                            {
                                var time = (ClanManager.Get(character.ClanId).Reddot.timeDoanhTrai - ServerUtils.CurrentTimeMillis()) / 60000;
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Đồng bang của ngươi đã vào trước rồi.Ngươi có muốn vào\n"
                                    + "không?\nCòn " + time + " phút nữa!",
                                    new List<string> { "Vào\n(miễn phí)", "Không", "Hướng\ndẫn\nthêm" }, character.InfoChar.Gender));
                                character.TypeMenu = 0;
                                return;
                            }

                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Hôm nay bang hội của ngươi chưa vào trại lần nào.Ngươi có muốn vào\n"
                                    + "không?\nĐể vào, ta khuyên ngươi nên có 3-4 người cùng bang đi cùng",
                                    new List<string> { "Vào\n(miễn phí)", "Không", "Hướng\ndẫn\nthêm" }, character.InfoChar.Gender));
                            character.TypeMenu = 0;



                        }
                        else
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Chỉ tiếp các bang hội, miễn tiếp khách vãng lai",
                                    new List<string> { "Hướng\ndẫn\nthêm" }, character.InfoChar.Gender));
                            character.TypeMenu = 2;
                        }
                        break;
                    #endregion

                    #region 26-Độc nhãn
                    case 26:
                        {
                            if (character.Zone.ZoneHandler.GetCountMob() <= 0)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Trại Độc Nhãn đã bị tiêu diệt, bạn có 5 phút để tìm kiếm ngọc 4 sao trước khi phi thuyền đến đón"));
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Ta chịu thua nhưng các ngươi đừng mong lấy được ngọc của ta!"));
                                character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                var clan = ClanManager.Get(character.ClanId);
                                if (clan != null)
                                {
                                    clan.Reddot.Win();
                                }
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Tên gà mờ,\nvẫn chưa thắng được ta thì đừng mơ đụng vào ngọc rồng\ncủa ta.", new List<string> { "Hủy" }, 3));
                                break;
                            }
                            break;
                        }
                    #endregion

                    #region 28-Kí gửi
                    case 28:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Cửa hàng chúng tôi chuyên mua bán hàng hiệu, hàng độc, cảm ơn bạn đã\nghé thăm", new List<string> { "Hướng\ndẫn\nthêm", "Mua bán\nKý gửi", "Từ chối" }, character.InfoChar.Gender));
                            break;
                        }
                    #endregion

                    #region 29-Rồng Omega
                    case 29:
                        {
                            BlackBallHandler.ForNpc.Omega_Dragon.OpenMenuOmega_Dragon(character, npcId);
                            break;
                        }
                    #endregion

                    #region 30->36 Rồng Sao đen
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                        {
                            BlackBallHandler.ForNpc.Rong_nSaoDen.OpenMenuRong_nSaoDen(character, npcId);
                            break;
                        }
                    #endregion

                    #region 37-Bumma TL
                    case 37:
                        {
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBumma[0], MenuNpc.Gi().MenuShopDistrict[1], character.InfoChar.Gender));
                                character.TypeMenu = 0;
                            }
                            break;
                        }
                    #endregion

                    #region 38-Ca lích
                    case 38:
                        {
                            if (!TaskHandler.gI().ReportTask(character, npcId))
                            {
                                switch (character.InfoChar.MapId)
                                {
                                    case 28:
                                        {
                                            if (character.InfoTask.Id >= 25)
                                            {
                                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextCalich[0], MenuNpc.Gi().MenuCalich[0], character.InfoChar.Gender));
                                            }
                                            else
                                            {
                                                character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Bạn phải hoàn thành nhiệm vụ Fide mới có thể qua tương lai"));
                                            }

                                            character.TypeMenu = 0;
                                            break;
                                        }
                                    case 102:
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextCalich[0], MenuNpc.Gi().MenuCalich[1], character.InfoChar.Gender));
                                            character.TypeMenu = 1;
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    #endregion

                    #region 39-Santa
                    case 39:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextSanta[0], MenuNpc.Gi().MenuSanta[0], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 41-Trung thu
                    case 41:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextTrungThu[0], MenuNpc.Gi().MenuTrungThu[0], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 42-Quốc vương
                    case 42:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuocVuong[0], MenuNpc.Gi().MenuQuocVuong[0], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 44-Osin
                    case 44:
                        switch (character.InfoChar.MapId)
                        {
                            case 0:
                            case 7:
                            case 14:
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Nhân dịp Mở Test, Nro Kame tổ chức sự kiện cho những cư dân săn được nhiều boss nhất\nKhi giết được 1 boss sẽ được 1 điểm sát thần\nKhi Open sẽ dùng điểm này để đổi các phần quà hấp dẫn\nBạn đang có: " + character.DiemSuKien + " điểm sát thần.", new List<string> { "Top 10", "Đóng" }, character.InfoChar.Gender));
                                break;
                            case 50:
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta có thể giúp gì cho ngươi ?", MenuNpc.Gi().MenuOsin[0], character.InfoChar.Gender));
                                    break;
                                }
                            case 154:
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta có thể giúp gì cho ngươi ?", MenuNpc.Gi().MenuOsin[1], character.InfoChar.Gender));
                                    break;
                                }
                            case 155:
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta có thể giúp gì cho ngươi ?", MenuNpc.Gi().MenuOsin[2], character.InfoChar.Gender));
                                    break;
                                }
                            case 127:
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta sẽ phù hộ ngươi bằng\nnguồn sức mạnh của Thần Kaio\n+1 triệu HP, +1 triệu KI, +10k Sức đánh\nLưu ý:sức mạnh này sẽ biến mất khi ngươi rời khỏi đây", new List<string> { "Phù hộ\n10 Ngọc", "Từ chối", "Về\nĐại hội\nVõ thuật" }, character.InfoChar.Gender));
                                    break;
                                }

                            case 52:
                                {
                                    if (Mabu12h.gI().InitMabu12h)
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Bây giờ tôi sẽ bí mât...\nđuổi theo 2 tên đồ tể...Quý vị nào muốn theo thì xin mời!", new List<string> { "OK", "Từ chối" }, character.InfoChar.Gender));
                                    }
                                    else if (Mabu2h.gI().InitMabu2h)
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Mabư đã thoát khỏi vỏ bọc\nMau đi cùng ta ngăn chặn hắn lại\ntrước khi hắn tàn phá Trái Đất này", new List<string> { "OK", "Từ chối" }, character.InfoChar.Gender));
                                    }
                                    else
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "vào lúc 12h tôi sẽ bí mât...\nđuổi theo 2 tên đồ tể...Quý vị nào muốn theo thì xin mời!", new List<string> { "OK", "Từ chối" }, character.InfoChar.Gender));
                                    }
                                    character.TypeMenu = 0;
                                    break;
                                }
                            default:
                                {
                                    if (character.Flag == 10 && character.PPower < 20) character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Cút về phe của ngươi mà thể hiện !"));
                                    else if (character.InfoChar.MapId == 120) character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Đừng vội xem thường Babiđây, ngay cả cha hắn là ma thần đạo sĩ\nBibiđây khi còn sống cũng phải sợ hắn !", new List<string> { "Hướng\ndẫn\nthêm", "Giải trừ\nphép thuật\n1 ngọc", "Về\nNhà" }, character.InfoChar.Gender));
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Đừng vội xem thường Babiđây, ngay cả cha hắn là ma thần đạo sĩ\nBibiđây khi còn sống cũng phải sợ hắn !", new List<string> { "Hướng\ndẫn\nthêm", "Giải trừ\nphép thuật\n1 ngọc", "Xuống\nTầng dưới" }, character.InfoChar.Gender));
                                    break;
                                }

                        }
                        character.TypeMenu = 0;
                        break;
                    #endregion

                    #region 46_Babiday
                    case 46:
                        {
                            if (character.Flag == 10 && character.PPower < 20)
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Cút về phe của ngươi mà thể hiện !"));
                            }
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Bọn Kaio do nhóc con Osin cầm đầu đã có mặt tại đây...Hãy chuẩn bị 'Tiếp\nKhách' nhé !", new List<string> { "Hướng\ndẫn\nthêm", "Giải trừ\nphép thuật\n1 ngọc", "Xuống\nTầng dưới", "Về nhà" }, character.InfoChar.Gender));
                            break;
                        }
                    #endregion

                    #region 47-Giu ma
                    case 47:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextGiuMa[0], MenuNpc.Gi().MenuGiuMa[character.InfoChar.isDiemDanh ? 1 : 0], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 49-Đường Tank
                    case 49:
                        {
                            switch (character.InfoChar.MapId)
                            {
                                case 0:
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextDuongTang[0], MenuNpc.Gi().MenuDuongTang[0], character.InfoChar.Gender));
                                    character.TypeMenu = 0;
                                    break;
                                case 123:
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextDuongTang[1], MenuNpc.Gi().MenuDuongTang[1], character.InfoChar.Gender));
                                    character.TypeMenu = 1;
                                    break;
                                case 122:
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextDuongTang[2], MenuNpc.Gi().MenuDuongTang[2], character.InfoChar.Gender));
                                    character.TypeMenu = 2;
                                    break;
                            }
                            break;
                        }
                    #endregion

                    #region 50-Quả trứng
                    case 50:
                        {
                            if (character.InfoChar.ThoiGianTrungMaBu <= 0) return;
                            var seconds = (character.InfoChar.ThoiGianTrungMaBu - ServerUtils.CurrentTimeMillis()) / 1000;
                            if (seconds > 0) //chưa đủ thời gian nở
                            {
                                MenuNpc.Gi().MenuQuaTrung[0][0] = "Chờ\n" + seconds + " giây nữa";
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuaTrung[0], MenuNpc.Gi().MenuQuaTrung[0], character.InfoChar.Gender));
                                character.TypeMenu = 0;
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuaTrung[0], MenuNpc.Gi().MenuQuaTrung[1], character.InfoChar.Gender));
                                character.TypeMenu = 1;
                            }
                            break;
                        }
                    #endregion

                    #region 51-Dưa hấu
                    case 51:
                        {
                            if (character.InfoChar.ThoiGianDuaHau != 0)
                            {
                                var second = (character.InfoChar.ThoiGianDuaHau - ServerUtils.CurrentTimeMillis()) / 1000;
                                if (second < 0)
                                {
                                    second = 0;
                                }

                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Khi nào chín hãy thu hoạch và hãy mang [Dưa Hấu] đến gặp Vua Hùng\nđể đổi quà nhé", new List<string> { second == 0 ? "Thu hoạch" : "OK", "Từ chối" }, character.InfoChar.Gender));
                                character.TypeMenu = second == 0 ? 1 : 0;

                            }
                            break;
                        }
                    #endregion

                    #region 52-Hùng vương
                    case 52:
                        {
                            List<string> Menus = new List<string>();
                            for (int i = 0; i < EventRuntime.DataTradeDuaHau[0].Count; i++)
                            {
                                Menus.Add($"{EventRuntime.DataTradeDuaHau[1][i]} ngọc\n{EventRuntime.DataTradeDuaHau[0][i]} quả");
                            }
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Muốn đổi hồng ngọc thì mang dưa hấu tới đêyy !", Menus, character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 53_Tapion
                    case 53:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ác quỷ truyền thuyết Hirudegarn\nđã thoát khỏi phong ấn ngàn năm\nhãy giúp tôi chế ngự nó", new List<string> { "OKER", "Từ chối" }, character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 54-Lý tiểu nương
                    case 54:
                        break;
                    #endregion

                    #region 55-Bill
                    case 55:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBill[0], MenuNpc.Gi().MenuBill[0], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 56-Whis
                    case 56:
                        {
                            switch (character.InfoChar.MapId)
                            {
                                case 5:
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta là Whis được Đại Thiên Sứ cử xuống trái đất để thu nhập lại các trang bị Huỷ\nDiệt bị kẻ xấu xa đánh cắp.Ta sẽ ban lại cho ngươi các món đồ kích hoạt viễn cổ\nnếu ngươi giao cho ta trang bị huỷ diệt", new List<string> { "Hiến tế\nhuỷ diệt", "Hướng dẫn", "Đóng" }, character.InfoChar.Gender));
                                    character.TypeMenu = 0;

                                    break;
                                default:
                                    if (character.DataTraining.DataWhis.Count > 0)
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta đang đói bụng, nếu có đồ ăn ngon thì ta sẽ tiếp tục tập với ngươi.", new List<string> { "Nói chuyện", "Học\ntuyệt kĩ", "Top 100", "Tặng\nđồ ăn" }, character.InfoChar.Gender));

                                    }
                                    else
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, String.Format("Thử đánh với ta xem nào.\nNgươi còn {0} lượt nữa cơ mà.", character.DataTraining.DataWhis.Level), new List<string> { "Nói chuyện", "Học\ntuyệt kĩ", "Top 100", "[LV: " + character.DataTraining.DataWhis.Level + "]" }, character.InfoChar.Gender));
                                    }
                                    character.TypeMenu = 0;
                                    break;
                            }
                            break;
                        }
                    #endregion

                    #region 60-GokuSSJ
                    case 60:
                        if (character.InfoChar.MapId == 80)
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta mới hạ Fide, nhưng nó đã kịp đào 1 cái lỗ\nHành tinh này sắp nổ tung rồi\nMau lượn thôi", new List<string> { "Chuẩn" }, character.InfoChar.Gender));
                            character.TypeMenu = 0;
                        }
                        else
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ngươi muốn quay về hả?", new List<string> { "Ờm" }, character.InfoChar.Gender));
                            character.TypeMenu = 1;
                        }
                        break;
                    #endregion

                    #region 61-Goku Yarad
                    case 61:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Hãy cố gắng tập luyện\nThu nhập 9.999 bí kiếp để đổi trang phục Yardat nhé !", new List<string> { "Nhận\nthưởng", "OK" }, character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 62-Potage
                    case 62:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Hãy giúp ta đánh bại bản sao\nNgươi chỉ có 5 phút để hạ hắn\nPhần thưởng của ngươi là 1 bình Commeson", new List<string> { "Hướng\ndẫn\nthêm", "OK", "Từ chối" }, character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 63-Jaco
                    case 63:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Gô Tên,Calích và Monaka đang gặp chuyện ở hành tinh Potaufeu\nHãy đến đó ngay !", new List<string> { "Đến\nPotaufeu", "Từ chối" }, character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 64-
                    case 64:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta có thể giúp gì cho ngươi?\nKhi săn Pic, Poc hè sẽ được cộng 1 điểm săn boss\nĐua Top sẽ kết thúc vào 15/8", new List<string> { "TOP\nSăn Boss", "TOP\nNhiệm vụ", "TOP\nNạp thẻ", "TOP\nĐệ tử", "Đóng" }, character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 66-Nồi bánh
                    case 66:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextNoiBanh[0], MenuNpc.Gi().MenuNoiBanh[0], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 67-Mrpopo
                    case 67:
                        {
                            if (ClanManager.Get(character.ClanId) != null)
                            {
                                if (ClanManager.Get(character.ClanId).Gas.Open)
                                {
                                    var time = (ClanManager.Get(character.ClanId).Gas.timeKhiGas - ServerUtils.CurrentTimeMillis()) / 60000;
                                    if (time < 1)
                                    {

                                        time = (ClanManager.Get(character.ClanId).Gas.timeKhiGas - ServerUtils.CurrentTimeMillis()) / 1000;
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Thượng Đế vừa phát hiện 1 loại khí đang âm thầm\nhủy diệt mọi mầm sống trên Trái Đất,\nnó được gọi là Destron Gas.\nTa sẽ đưa các cậu đến nơi ấy, các cậu sẵn sàng chưa?\nBang hội của con đang Tham gia Khí Gas Level: " + ClanManager.Get(character.ClanId).Gas.Level + ".\nCon có muốn tham gia không?\nCòn " + time + " giây nữa", new List<string> { "Thông tin\nchi tiết", "Top 100\nBang hội", "OK", "Từ chối" }, character.InfoChar.Gender));
                                    }
                                    else
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Thượng Đế vừa phát hiện 1 loại khí đang âm thầm\nhủy diệt mọi mầm sống trên Trái Đất,\nnó được gọi là Destron Gas.\nTa sẽ đưa các cậu đến nơi ấy, các cậu sẵn sàng chưa?\nBang hội của con đang Tham gia Khí Gas Level: " + ClanManager.Get(character.ClanId).Gas.Level + ".\nCon có muốn tham gia không?\nCòn " + time + " phút nữa", new List<string> { "Thông tin\nchi tiết", "Top 100\nBang hội", "OK", "Từ chối" }, character.InfoChar.Gender));
                                    }
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Thượng Đế vừa phát hiện 1 loại khí đang âm thầm\nhủy diệt mọi mầm sống trên Trái Đất,\nnó được gọi là Destron Gas.\nTa sẽ đưa các cậu đến nơi ấy, các cậu sẵn sàng chưa?", new List<string> { "Thông tin\nchi tiết", "Top 100\nBang hội", "OK", "Từ chối" }, character.InfoChar.Gender));
                                }
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Thượng Đế vừa phát hiện 1 loại khí đang âm thầm\nhủy diệt mọi mầm sống trên Trái Đất,\nnó được gọi là Destron Gas.\nTa sẽ đưa các cậu đến nơi ấy, các cậu sẵn sàng chưa?", new List<string> { "Thông tin\nchi tiết", "Top 100\nBang hội", "OK", "Từ chối" }, character.InfoChar.Gender));
                            }
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 77-Shop
                    case 77:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "|0|Xin chàoo,Tui là Trangg (Người yêu của Dev)\n|6|Tui bán tất cả mọi thứ", new List<string> { "Shop\nVán bay", "Shop\nThẻ Rada", "Shop\nCải Trang", "Shop\nRồng Băng", "Shop\nSự kiện" }, character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 74-ToriBot
                    case 74:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Cậu cần gì ở tôi?", new List<string> { "Shop\nThần bí" }, character.InfoChar.Gender));
                            //character.CharacterHandler.SendMessage(Service.Shop(character, 0, 29));
                            //character.ShopId = 29;
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 75-KingKong mùa hè
                    case 75:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Kìa mây mây ngang đầu, kìa núi núi lô nhô\nCùng em trên con đường, Đường bé xíu lô nhô\n|7|                ~~~ Đen Vâu ~~~~", new List<string> { "Shop Hè", "Shop\nVật phẩm", "Nhập\nGift Code\nSự kiện hè", "Đổi quà sự kiện", "Tặng\nBọ Cánh\nCứng", "Tặng\nNgài Đêm", "Hoàn trả\ncải trang" }, character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 76-King Furry
                    case 76:
                        {
                            character.TypeMenu = 1;
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Kìa mây mây ngang đầu, kìa núi núi lô nhô\nCùng em trên con đường, Đường bé xíu lô nhô\n|7|                ~~~ Đen Vâu ~~~~", new List<string> { "Hướng\ndẫn\nthêm", "Mua bán\nKý gửi\nSự kiện", "Từ chối" }, character.InfoChar.Gender));
                            break;
                        }
                    #endregion

                    #region 78-Fu
                    case 78:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ngươi muốn gì?", new List<string> { "Quay về\nHành tinh\nngục tù" }, character.InfoChar.Gender));
                            break;
                        }
                    #endregion

                    #region 79-Mai
                    case 79:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBXH[0], MenuNpc.Gi().MenuBXH[0], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    #region 80-Pic
                    case 80:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextCauCa[0], MenuNpc.Gi().MenuCauCa[0], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    #endregion

                    default:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, TextServer.gI().UPDATING));
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error OpenUiMenu in Menu.cs: {e.Message} \n {e.StackTrace}", e);
            }
        }
        
        public static void MenuHandler(Message message, Character character)
        {
            try
            {
                var npcId = message.Reader.ReadByte();
                var menuId = message.Reader.ReadByte();
                var optionId = message.Reader.ReadByte();
                Server.Gi().Logger.Debug($"Menu Handler --------------------------- {npcId} - {menuId} - {optionId}");
                switch (npcId)
                {
                    //Đậu thần
                    case 4:
                        {
                            MenuDauThan(character, npcId, menuId, optionId);
                            break;
                        }
                    default:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, TextServer.gI().UPDATING));
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error Menu Handler in Menu.cs: {e.Message} \n {e.StackTrace}", e);
            }
            finally
            {
                message?.CleanUp();
            }
        }
        public static void ConfirmMenuTrongTai(Character character, int npcId, int select){
            switch(character.TypeMenu){
                case 0:
                switch(select){
                    case 0: // top 100 cao thu
                            character.CharacterHandler.SendMessage(Application.Extension.Super_Champion.SieuHang.ListRank(character));
                            break;
                    case 1: // huong dan them
                    string text = "Giải đấu thể hiện đẳng cấp thực sự\nCác trận đấu diễn ra liên tục bất kể ngày đêm\nBạn hãy tham gia thi đấu để nâng hạng\nvà nhận giải thưởng khủng nhé"
                    + "Cơ cấu giải thưởng như sau\n(chốt và trao giải ngẫu nhiên từ 20h-23h mỗi ngày)\nTop 1 thưởng 100 ngọc\nTop 2-10 thưởng 20 ngọc\nTop 11-100 thưởng 5 ngọc\nTop 101-1000 thưởng 1 ngọc"
                    + "Mỗi ngày các bạn được tặng 1 vé tham dự miễn phí\n(tích lũy tối đa 3 vé) khi thua sẽ mất đi 1 vé\nKhi hết vé bạn phải trả 1 ngọc để đấu tiếp\n(trừ ngọc khi trận đấu kết thúc)"
                    + "Bạn không thể thi đấu với đấu thủ\ncó hạng thấp hơn mình\nChúc bạn may mắn, chào đoàn kết và quyết thắng";
                    character.CharacterHandler.SendMeMessage(Service.OpenUiSay((short)npcId, text));
                    break;
                    case 2: // mien phi con $ ve
                            character.CharacterHandler.SendMessage(Application.Extension.Super_Champion.SieuHang.ListRank(character));
                            break;
                    case 3: // uu tien dau ngay !
                            character.CharacterHandler.SendMessage(Application.Extension.Super_Champion.SieuHang.ListRank(character));
                            break;
                    case 4: // ve dai hoi vo thuat 
                    MapManager.JoinMap(character, 52, ServerUtils.RandomNumber(19), false, false, 0);
                    break;
                    
                }
                break;
            }
        }
        public static void confirmMenuAdmin(Character character, int npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                    {
                        switch (select)
                        {
                            case 0://Thông tin server
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(64, MenuNpc.Gi().TextMenuAdmin[0], MenuNpc.Gi().MenuAdmin[1], character.InfoChar.Gender));
                                    character.TypeMenu = 1;
                                    break;
                                }
                            case 1://Gửi thông báo
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(64, MenuNpc.Gi().TextMenuAdmin[0], MenuNpc.Gi().MenuAdmin[2], character.InfoChar.Gender));
                                    character.TypeMenu = 2;
                                    break;
                                }
                            case 2://Menu buff bẩn
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(64, MenuNpc.Gi().TextMenuAdmin[0], MenuNpc.Gi().MenuAdmin[3], character.InfoChar.Gender));
                                    character.TypeMenu = 3;
                                    break;
                                }
                            case 3://Menu Quyền lực
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(64, MenuNpc.Gi().TextMenuAdmin[0], MenuNpc.Gi().MenuAdmin[4], character.InfoChar.Gender));
                                    character.TypeMenu = 4;
                                    break;
                                }
                        }
                        break;
                    }
                case 1://TypeMenu Thông tin
                    {
                        switch (select)
                        {
                            case 0://Check Gift
                                {
                                    var inputCheckGifcode = new List<InputBox>();

                                    var inputCode = new InputBox()
                                    {
                                        Name = "Nhập Giftcode",
                                        Type = 1,
                                    };
                                    inputCheckGifcode.Add(inputCode);
                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Check giftcode", inputCheckGifcode));
                                    character.TypeInput = 12;
                                    break;
                                }
                            case 1://Player Online
                                {
                                    var @char = (Character)character;
                                    var info = $"Số người online trong Server: " + ClientManager.Gi().Characters.Count;
                                    @char.CharacterHandler.SendMessage(Service.WorldChat(null, info, 0));
                                    break;
                                }
                            case 2://Thread
                                {
                                    var @char = (Character)character;
                                    var info = $"Thread của Server: " + System.Diagnostics.Process.GetCurrentProcess().Threads.Count;
                                    @char.CharacterHandler.SendMessage(Service.WorldChat(null, info, 0));
                                    break;
                                }
                        }
                        break;
                    }
                case 2://TypeMenu Gửi thông báo
                    {
                        switch (select)
                        {
                            case 0://Toàn server
                                {
                                    var inputThongBao = new List<InputBox>();

                                    var inputNoiDung = new InputBox()
                                    {
                                        Name = "Nhập Nội Dung Thông Báo",
                                        Type = 1,
                                    };
                                    inputThongBao.Add(inputNoiDung);

                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Thông Báo", inputThongBao));
                                    character.TypeInput = 24;
                                    break;
                                }
                            case 1://Player
                                {
                                    var input = new List<InputBox>();

                                    var inputName = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật",
                                        Type = 1,
                                    };
                                    input.Add(inputName);

                                    var inputNoiDung = new InputBox()
                                    {
                                        Name = "Nhập Nội Dung Thông Báo",
                                        Type = 1,
                                    };
                                    input.Add(inputNoiDung);

                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Thông Báo", input));
                                    character.TypeInput = 25;
                                    break;
                                }
                        }
                        break;
                    }
                case 3://TypeMenu Buff bẩn
                    {
                        switch (select)
                        {
                            case 0://Buff Item
                                {
                                    var inputItem = new List<InputBox>();

                                    var inputTenCharacter = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật (0 là để buff cho chính mình)",
                                        Type = 1,
                                    };
                                    inputItem.Add(inputTenCharacter);

                                    var inputIdItem = new InputBox()
                                    {
                                        Name = "Nhập ID Item",
                                        Type = 1,
                                    };
                                    inputItem.Add(inputIdItem);

                                    var inputIndex = new InputBox()
                                    {
                                        Name = "Nhập Options",
                                        Type = 1,
                                    };
                                    inputItem.Add(inputIndex);

                                    var inputSoLuong = new InputBox()
                                    {
                                        Name = "Nhập Số Lượng",
                                        Type = 1,
                                    };
                                    inputItem.Add(inputSoLuong);

                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Buff Item", inputItem));
                                    character.TypeInput = 4;
                                    break;
                                }
                            case 1://Buff Vnd
                                {
                                    var inputBuff = new List<InputBox>();

                                    var inputName = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật (0 là để buff cho chính mình)",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputName);

                                    var inputVND = new InputBox()
                                    {
                                        Name = "Nhập số tiền",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputVND);

                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Buff VND", inputBuff));
                                    character.TypeInput = 13;
                                    break;
                                }
                            case 2://Buff Tnsm
                                {
                                    var inputBuff = new List<InputBox>();

                                    var inputName = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật (0 là để buff cho chính mình)",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputName);

                                    var inputHP = new InputBox()
                                    {
                                        Name = "Nhập chỉ số muốn buff(0: Sức mạnh, 1:Tiềm năng, 2: Cả hai)",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputHP);

                                    var inputMP = new InputBox()
                                    {
                                        Name = "Nhập số lượng",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputMP);

                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Buff Bẩn TNSM", inputBuff));
                                    character.TypeInput = 7;
                                    break;
                                }
                            case 3://Buff Chỉ số
                                {
                                    var inputBuff = new List<InputBox>();

                                    var inputName = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật (0 là để buff cho chính mình)",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputName);

                                    var inputType = new InputBox()
                                    {
                                        Name = "0: Buff, 1: Set",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputType);

                                    var inputHP = new InputBox()
                                    {
                                        Name = "Nhập HP",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputHP);

                                    var inputMP = new InputBox()
                                    {
                                        Name = "Nhập MP",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputMP);

                                    var inputSD = new InputBox()
                                    {
                                        Name = "Nhập SD",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputSD);

                                    var inputCM = new InputBox()
                                    {
                                        Name = "Nhập Chí Mạng",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputCM);

                                    var inputAmor = new InputBox()
                                    {
                                        Name = "Nhập Giáp",
                                        Type = 1,
                                    };
                                    inputBuff.Add(inputAmor);

                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Buff Bẩn Chỉ Số", inputBuff));
                                    character.TypeInput = 8;
                                    break;
                                }
                            case 4://Buff Task
                                {
                                    var inputTask = new List<InputBox>();

                                    var inputName = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật (0 là để buff cho chính mình)",
                                        Type = 1,
                                    };
                                    inputTask.Add(inputName);

                                    var inputId = new InputBox()
                                    {
                                        Name = "Nhập ID Nhiệm vụ",
                                        Type = 1,
                                    };
                                    inputTask.Add(inputId);

                                    var inputIndex = new InputBox()
                                    {
                                        Name = "Nhập Index Nhiệm vụ",
                                        Type = 1,
                                    };
                                    inputTask.Add(inputIndex);

                                    var inputCount = new InputBox()
                                    {
                                        Name = "Nhập Count Nhiệm vụ",
                                        Type = 1,
                                    };
                                    inputTask.Add(inputCount);
                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Buff Nhiệm vụ", inputTask));
                                    character.TypeInput = 9;
                                    break;
                                }
                        }
                        break;
                    }
                case 4://TypeMenu Quyền lực
                    {
                        switch (select)
                        {
                            case 0://Gọi boss
                                {
                                    var inputBoss = new List<InputBox>();

                                    var inputIdBoss = new InputBox()
                                    {
                                        Name = "Nhập ID Boss",
                                        Type = 1,
                                    };
                                    inputBoss.Add(inputIdBoss);


                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Gọi Boss", inputBoss));
                                    character.TypeInput = 5;
                                    break;
                                }
                            case 1://Ban
                                {
                                    var inputBan = new List<InputBox>();

                                    var inputName = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật",
                                        Type = 1,
                                    };
                                    inputBan.Add(inputName);

                                    var inputLyDoBan = new InputBox()
                                    {
                                        Name = "Nhập lý do ban",
                                        Type = 1,
                                    };
                                    inputBan.Add(inputLyDoBan);


                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Ban Player", inputBan));
                                    character.TypeInput = 3;
                                    break;
                                }
                            case 2://Kick
                                {
                                    var inputKick = new List<InputBox>();

                                    var inputName = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật",
                                        Type = 1,
                                    };
                                    inputKick.Add(inputName);

                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Kick Player", inputKick));
                                    character.TypeInput = 10;
                                    break;
                                }
                            case 3://Tele
                                {
                                    var inputKick = new List<InputBox>();

                                    var inputName = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật",
                                        Type = 1,
                                    };
                                    inputKick.Add(inputName);

                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Teleport", inputKick));
                                    character.TypeInput = 11;
                                    break;
                                }
                            case 4://Kéo
                                {
                                    var inputKeo = new List<InputBox>();

                                    var inputName = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật",
                                        Type = 1,
                                    };
                                    inputKeo.Add(inputName);

                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Kéo Player", inputKeo));
                                    character.TypeInput = 27;
                                    break;
                                }
                            case 5://Kill
                                {
                                    var inputKick = new List<InputBox>();

                                    var inputName = new InputBox()
                                    {
                                        Name = "Nhập tên nhân vật",
                                        Type = 1,
                                    };
                                    inputKick.Add(inputName);

                                    character.CharacterHandler.SendMessage(Service.ShowInput("Menu Cắt Chim Player", inputKick));
                                    character.TypeInput = 23;
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        public static void UiConfirm(Message message, Character character)
        {
            try
            {
                var npcId = message.Reader.ReadShort();
                var select = message.Reader.ReadByte();
                switch (npcId)
                {
                    case 56:
                        switch (character.TypeMenu)
                        {
                            case 6:
                                if (select == character.CombinneIndex.Count - 1) return;
                                character.CharacterHandler.RemoveItemBagById((short)character.CombinneIndex[select], 1);
                                character.DataTraining.DataWhis.Count = 0;
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                break;
                            case 4:
                                {
                                    switch (select)
                                    {

                                        case 0:
                                            if (character.InfoChar.Gold < 500000000)
                                            {
                                                character.CharacterHandler.SendMessage(Service.ServerMessage("Yêu Cầu 500 Triệu Vàng!"));
                                                return;
                                            }
                                            var countTbThanLinh = 0;
                                            List<int> IndexTbi = new List<int>();
                                            for (int i = 0; i < character.ItemBody.Count; i++)
                                            {
                                                if (character.ItemBody[i] != null)
                                                {
                                                    var itemBody = character.ItemBody[i];
                                                    if (ItemCache.ItemTemplate(itemBody.Id).Level == 14)
                                                    {
                                                        countTbThanLinh++;
                                                        IndexTbi.Add(itemBody.IndexUI);
                                                    }
                                                }
                                            }
                                            for (int tbi = 0; tbi < IndexTbi.Count; tbi++)
                                            {
                                                var item = character.ItemBody[IndexTbi[tbi]];
                                                var itemTemp = ItemCache.ItemTemplate(character.ItemBody[IndexTbi[tbi]].Id);
                                                character.CharacterHandler.RemoveItemBody(IndexTbi[tbi]);
                                                if (ServerUtils.RandomNumber(100) < 50)
                                                {
                                                    HandlerGhepTrangBiHuyDietForBody(character, itemTemp.Type, character.InfoChar.Gender, item.IndexUI);
                                                }
                                                else
                                                {
                                                    var ngocrong = ItemCache.GetItemDefault((short)ServerUtils.RandomNumber(16, 18));
                                                    character.CharacterHandler.AddItemToBag(true, ngocrong);
                                                }
                                            }
                                            character.MineGold(500000000);
                                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                            character.CharacterHandler.SendMessage(Service.SendBody(character));
                                            character.CharacterHandler.SendMessage(Service.SendBag(character));

                                            character.CharacterHandler.SendMessage(Service.UpdateBody(character));
                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Hiến tế trang bị thành công !"));
                                            break;
                                    }
                                }
                                break;
                            case 5:
                                {
                                    if (character.InfoChar.Gold < 500000000)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Yêu Cầu 500 Triệu Vàng!"));
                                        return;
                                    }
                                    var countTbThanLinh = 0;
                                    List<int> IndexTbi = new List<int>();
                                    for (int i = 0; i < character.Disciple.ItemBody.Count; i++)
                                    {
                                        if (character.Disciple.ItemBody[i] != null)
                                        {
                                            var itemBody = character.Disciple.ItemBody[i];
                                            if (ItemCache.ItemTemplate(itemBody.Id).Level == 14)
                                            {
                                                countTbThanLinh++;
                                                IndexTbi.Add(itemBody.IndexUI);
                                            }
                                        }
                                    }
                                    for (int tbi = 0; tbi < IndexTbi.Count; tbi++)
                                    {
                                        var item = character.Disciple.ItemBody[IndexTbi[tbi]];
                                        var itemTemp = ItemCache.ItemTemplate(character.Disciple.ItemBody[IndexTbi[tbi]].Id);
                                        character.Disciple.CharacterHandler.RemoveItemBody(IndexTbi[tbi]);
                                        if (ServerUtils.RandomNumber(100) < 50)
                                        {
                                            HandlerGhepTrangBiHuyDietForBody(character.Disciple, itemTemp.Type, itemTemp.Gender, item.IndexUI);
                                        }
                                        else
                                        {
                                            var ngocrong = ItemCache.GetItemDefault((short)ServerUtils.RandomNumber(16, 18));
                                            character.CharacterHandler.AddItemToBag(true, ngocrong);
                                        }
                                    }
                                    character.MineGold(500000000);
                                    character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                    character.CharacterHandler.SendMessage(Service.SendBody(character));
                                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                                    character.CharacterHandler.SendZoneMessage(Service.UpdateBody(character.Disciple));
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Hiến tế trang bị thành công !"));
                                    break;
                                }
                            case 1:
                                switch (character.InfoChar.MapId)
                                {
                                    case 5:
                                        switch (select)
                                        {
                                            case 0:
                                                {
                                                    var countTbThanLinh = 0;
                                                    List<int> IndexTbi = new List<int>();
                                                    for (int i = 0; i < character.ItemBody.Count; i++)
                                                    {
                                                        if (character.ItemBody[i] != null)
                                                        {
                                                            var itemBody = character.ItemBody[i];
                                                            if (ItemCache.ItemTemplate(itemBody.Id).Level == 14)
                                                            {
                                                                countTbThanLinh++;
                                                                IndexTbi.Add(i);
                                                            }
                                                        }
                                                    }
                                                    if (countTbThanLinh == 0 || IndexTbi.Count == 0)
                                                    {
                                                        character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Yêu cầu ít nhất 1 món đồ Huỷ Diệt mặc trên người!"));
                                                        return;
                                                    }
                                                    var textMenu = "Danh sách vật phẩm hiến tế cho Whis:";
                                                    for (int tbi = 0; tbi < IndexTbi.Count; tbi++)
                                                    {
                                                        var item = character.ItemBody[IndexTbi[tbi]];
                                                        textMenu += $"{ServerUtils.Color("green")}{tbi + 1}. {ItemCache.ItemTemplate(item.Id).Name}";
                                                    }
                                                    textMenu += $"{ServerUtils.Color("red")}Ngươi sẽ nhận lại 1 trang bị kích hoạt tương xứng trong thời kì viễn cỗ";
                                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(56, textMenu, new List<string> { "Hiến tế\n(500Tr)", "Từ chối" }, character.InfoChar.Gender));
                                                    character.TypeMenu = 4;
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    var countTbThanLinh = 0;
                                                    List<int> IndexTbi = new List<int>();
                                                    for (int i = 0; i < character.Disciple.ItemBody.Count; i++)
                                                    {
                                                        if (character.Disciple.ItemBody[i] != null)
                                                        {
                                                            var itemBody = character.Disciple.ItemBody[i];
                                                            if (ItemCache.ItemTemplate(itemBody.Id).Level == 14)
                                                            {
                                                                countTbThanLinh++;
                                                                IndexTbi.Add(i);
                                                            }
                                                        }
                                                    }
                                                    if (countTbThanLinh == 0 || IndexTbi.Count == 0)
                                                    {
                                                        character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Yêu cầu ít nhất 1 món đồ Huỷ Diệt mặc trên người đệ tử!"));
                                                        return;
                                                    }
                                                    var textMenu = "Danh sách vật phẩm hiến tế cho Whis:";
                                                    for (int tbi = 0; tbi < IndexTbi.Count; tbi++)
                                                    {
                                                        var item = character.Disciple.ItemBody[IndexTbi[tbi]];
                                                        textMenu += $"{ServerUtils.Color("green")}{tbi + 1}. {ItemCache.ItemTemplate(item.Id).Name}";
                                                    }
                                                    textMenu += $"{ServerUtils.Color("red")}Ngươi sẽ nhận lại 1 trang bị kích hoạt tương xứng trong thời kì viễn cỗ";
                                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(56, textMenu, new List<string> { "Hiến tế\n(500Tr)", "Từ chối" }, character.InfoChar.Gender));
                                                    character.TypeMenu = 5;
                                                    break;
                                                }
                                        }
                                        break;
                                    default:
                                        switch (select)
                                        {
                                            case 0:
                                                character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[25], 56));
                                                character.ShopId = 20;
                                                break;
                                            case 1:
                                             //   if (character.Player.Role != 1) return;
                                                character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[29], 56));
                                                character.ShopId = 21;
                                                break;
                                            case 2:
                                            //    if (character.Player.Role != 1) return;
                                                var existMat = false;
                                                for (int i = 0; i < character.ItemBag.Count; i++)
                                                {
                                                    var itmBag = character.ItemBag[i];
                                                    if (itmBag.Id >= 1280 && itmBag.Id <= 1289) existMat = true;
                                                }
                                                if (existMat)
                                                {
                                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Ngươi đã có mắt hỗn mang rồi !"));
                                                    return;
                                                }
                                                var item = ItemCache.GetItemDefault(1280);
                                                character.CharacterHandler.AddItemToBag(false, item);
                                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ItemCache.ItemTemplate(item.Id).Name));

                                                break;
                                        }
                                        break;
                                }
                                break;
                            case 2:
                                switch (select)
                                {
                                    case 0:
                                        character.CharacterHandler.SendMessage(Service.SendCombinne6(11238, (short)(character.InfoChar.Gender == 0 ? 11162 : character.InfoChar.Gender == 1 ? 11194 : 11193), npcId));
                                        character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Bư cô la, ba cô la, bư ra bư zô, đút vào đút ra, ..."));
                                        character.CharacterHandler.RemoveItemBagById(1235, 9999);
                                        character.MineGold(10000000);
                                        character.MineDiamond(99);
                                        character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                                        character.Skills.Add(new SkillCharacter()
                                        {
                                            Id = character.InfoChar.Gender == 0 ? 24 : character.InfoChar.Gender == 1 ? 26 : 25,
                                            SkillId = character.InfoChar.Gender == 0 ? 156 : character.InfoChar.Gender == 1 ? 176 : 166,
                                            CoolDown = 0,
                                            Point = 1,
                                            CurrExp = 10,
                                        });
                                        character.CharacterHandler.SendMessage(Service.AddSkill((short)(character.InfoChar.Gender == 0 ? 156 : character.InfoChar.Gender == 1 ? 176 : 166)));
                                        break;
                                }
                                break;
                            case 0:
                                switch (character.InfoChar.MapId)
                                {

                                    case 5:
                                        switch (select)
                                        {
                                            case 0://hien te than linh
                                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(56, "Ngươi muốn hiến tế cho Bản Thân hay Đệ Tử?", new List<string> { "Bản thân", "Đệ tử", "Từ chối" }, character.InfoChar.Gender));
                                                character.TypeMenu = 1;
                                                break;
                                        }
                                        break;
                                    default:
                                        switch (select)
                                        {
                                            case 3:
                                                if (character.DataTraining.DataWhis.Count > 0)
                                                {
                                                    List<short> itemCanGift = new List<short> { 880, 881, 882 };
                                                    List<string> Menu = new List<string>();
                                                    foreach (var item in character.ItemBag)
                                                    {
                                                        if (itemCanGift.Contains(item.Id))
                                                        {
                                                            Menu.Add(ItemCache.ItemTemplate(item.Id).Name);
                                                            character.CombinneIndex.Add(item.Id);
                                                        }
                                                    }
                                                    if (Menu.Count == 0)
                                                    {
                                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Cần 1 món đồ ăn cho Whis"));
                                                        return;
                                                    }
                                                    Menu.Add("Từ chối");
                                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta đang đói bụng, nếu có đồ ăn ngon thì ta sẽ tiếp tục tập với ngươi", Menu, character.InfoChar.Gender));
                                                    character.TypeMenu = 6;
                                                }
                                                else
                                                {
                                                    //thach dau whis
                                                    ThachDau.gI().ThachDauWhis(character, character.DataTraining.DataWhis.Level);
                                                }
                                                break;
                                            case 0:
                                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta có thể giúp gì cho ngươi?", new List<string> { "Chế tạo\ntrang bị\nthiên sứ","Nâng cấp\nmắt\nhỗn mang","Nhận mắt\nhỗn mang" }, character.InfoChar.Gender));
                                                character.TypeMenu = 1;
                                                break;
                                            case 1:
                                                var skillGender = character.InfoChar.Gender == 0 ? "Super Kamekameha" : character.InfoChar.Gender == 1 ? "Cadic liên hoàn chưởng" : "Ma Phong Ba";
                                                var textMenu = $"{ServerUtils.Color("green")}Ta sẽ dạy ngươi tuyệt kĩ {skillGender}\n";
                                                var Cannot = false;
                                                var checkBiKip = character.CharacterHandler.GetItemBagById(1235) != null;
                                                if (checkBiKip)
                                                {
                                                    textMenu += character.CharacterHandler.GetItemBagById(1235).Quantity >= 9999 ? $"{ServerUtils.Color("blue")}Bí kíp tuyệt kĩ {character.CharacterHandler.GetItemBagById(1235).Quantity}/9999" : $"{ServerUtils.Color("red")}Bí kíp tuyệt kĩ {character.CharacterHandler.GetItemBagById(1235).Quantity}/9999";
                                                    if (character.CharacterHandler.GetItemBagById(1235).Quantity < 9999) Cannot = true;
                                                }
                                                else
                                                {
                                                    textMenu += $"{ServerUtils.Color("red")}Bí kíp tuyệt kĩ 0/9999";
                                                    Cannot = true;
                                                }
                                                textMenu += character.InfoChar.Gold >= 10000000 ? $"{ServerUtils.Color("blue")}Giá vàng: 10.000.000" : $"{ServerUtils.Color("red")}Giá vàng: 10.000.000";
                                                textMenu += $"{(character.AllDiamond() >= 99 ? ServerUtils.Color("blue") : ServerUtils.Color("red"))}Giá ngọc: 99";
                                                if (character.InfoChar.Gold < 10000000 || character.AllDiamond() < 99) Cannot = true;
                                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, textMenu, new List<string> { "Đồng ý", "Từ chối" }, character.InfoChar.Gender));
                                                character.TypeMenu = Cannot ? 3 : 2;
                                                break;
                                        }
                                        break;

                                }
                                break;
                        }
                        break;
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                        BlackBallHandler.ForNpc.Rong_nSaoDen.ConfirmRong_nSaoDen(character, npcId, select);
                        break;
                    case 29:
                        BlackBallHandler.ForNpc.Omega_Dragon.Confirm(character, npcId, select);
                        break;
                    case 22:
                        ConfirmMenuTrongTai(character, npcId, select);
                        break;
                    case 64:
                        {
                            confirmMenuAdmin(character, npcId, select);
                            break;
                        }
                    case 46:
                        ComfirmBabiday(character, npcId, select);
                        break;
                    case 44:
                        ConfirmOsin(character, npcId, select);
                        break;
                    case 0:
                    case 1:
                    case 2:
                        {
                            ConfirmBaOngGia(character, npcId, select);
                            break;
                        }
                    case 5:
                        {
                            ConfirmMeo(character, npcId, select);
                            break;
                        }
                    case 7: {
                            ConfirmBumma(character, npcId, select);
                            break;
                        }
                    case 8: {
                            ConfirmDende(character, npcId, select);
                            break;
                        }
                    case 9: {
                            ConfirmAppule(character, npcId, select);
                            break;
                        }
                    case 10: {
                            ConfirmBrief(character, npcId, select);
                            break;
                        }
                    case 11: {
                            ConfirmCargo(character, npcId, select);
                            break;
                        }
                    case 12: {
                            ConfirmCui(character, npcId, select);
                            break;
                        }
					case 79:
                        {
                            ConfirmBXH(character, npcId, select);
                            break;
                        }
                    case 80:
                        {
                            ConfirmCauCa(character, npcId, select);
                            break;
                        }
                    case 78:
                        switch (select)
                        {
                            case 0:
                                MapManager.JoinMap(character, 155, ServerUtils.RandomNumber(0, 19), true, true, character.TypeTeleport);
                                break;
                        }
                        break;
                    case 13: {
                            ConfirmQuyLao(character, npcId, select);
                            break;
                        }
                    case 14: {
                            ConfirmTruongLaoGuru(character, npcId, select);
                            break;
                        }
                    case 15: {
                            ConfirmVuaVegeta(character, npcId, select);
                            break;
                        }
                    case 17:
                        {
                            ConfirmBoMong(character, npcId, select);
                            break;
                        }                       
                    case 54:
                       
                        break;
                        //switch (character.TypeMenu)
                        //{
                        //    case 1:
                        //        switch (select)
                        //        {
                        //            default:
                        //                var thoivang = 0;
                        //                character.ItemBag.Where(item => item != null).ToList().ForEach(item =>
                        //                {
                        //                    if (item.Id == 457) thoivang+= item.Quantity;
                        //                });
                                        
                        //                var current = DataCache.VongQuayLTN[select];
                        //                if (character.LuckyBox.Count + current[1] > 100)
                        //                {
                        //                    character.CharacterHandler.SendMessage(Service.ServerMessage("Vui lòng dọn dẹp lại rương chứa vật phẩm"));
                        //                }
                        //                if (thoivang < current[0])
                        //                {
                        //                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Bạn không đủ thỏi vàng !"));
                        //                    return;
                        //                }
                        //                character.InfoChar.PointQuayThuong += current[1];
                        //                for (int i =0; i < current[1]; i++)
                        //                {
                        //                    var random = ServerUtils.RandomNumber(100);
                        //                    var item = ItemCache.GetItemDefault(1);
                                            
                        //                    switch (random)
                        //                    {
                        //                        case <= 20:
                        //                            item = ItemCache.GetItemDefault((short)(DataCache.ItemVongQuayLiTieuNuongEpic[ServerUtils.RandomNumber(DataCache.ItemVongQuayLiTieuNuongEpic.Count)]));
                        //                            item.IndexUI = character.LuckyBox.Count;
                        //                            item.Reason = "Vòng quay Lí Tiểu Nương";
                        //                            item.Options.Add(new OptionItem()
                        //                            {
                        //                                Id = 93,
                        //                                Param = ServerUtils.RandomNumber(2, 7)
                        //                            });
                        //                            character.LuckyBox.Add(item);
                        //                            break;
                        //                        case <= 60:
                        //                            item = ItemCache.GetItemDefault((short)(DataCache.ItemVongQuayLiTieuNuongRare[ServerUtils.RandomNumber(DataCache.ItemVongQuayLiTieuNuongRare.Count)]));
                        //                            item.IndexUI = character.LuckyBox.Count;
                        //                            item.Reason = "Vòng quay Lí Tiểu Nương";
                        //                            character.LuckyBox.Add(item);

                        //                            break;
                        //                        default:
                        //                            item = ItemCache.GetItemDefault((short)(DataCache.ItemVongQuayLiTieuNuongNormal[ServerUtils.RandomNumber(DataCache.ItemVongQuayLiTieuNuongNormal.Count)]));
                        //                            item.IndexUI = character.LuckyBox.Count;
                        //                            item.Reason = "Vòng quay Lí Tiểu Nương";
                        //                            character.LuckyBox.Add(item);

                        //                            break;
                        //                    }
                        //                }
                        //                CharacterDB.SaveInventory(character, false, false, false, true);
                        //                character.CharacterHandler.RemoveItemBagById(457, current[0]);
                        //                character.CharacterHandler.SendMessage(Service.SendBag(character));
                        //                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã tích lũy được thêm " + current[1] + " điểm quay thưởng"));

                        //                break;
                        //        }
                        //        break;
                        //    case 0:
                        //        switch (select)
                        //        {
                        //            case 0:
                        //                List<string> Menus = new List<string>();
                        //                for (int i = 0; i < DataCache.VongQuayLTN.Length; i++)
                        //                {
                        //                    var current = DataCache.VongQuayLTN[i];
                        //                    if (current[0] <= 5)
                        //                    {
                        //                        Menus.Add($"Quay\n{current[1]} lần\n({current[0]} thỏi\nvàng)");
                        //                    }
                        //                    else
                        //                    {
                        //                        Menus.Add($"Quay\n{current[1]} lần\n({current[0]} thỏi vàng)");
                        //                    }
                        //                }
                        //                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Mời bạn chọn số lượng quay", Menus,character.InfoChar.Gender));
                        //                character.TypeMenu = 1;
                        //                break;
                        //            case 1:
                        //                character.CharacterHandler.SendMessage(Service.SubBox(character.LuckyBox));
                        //                character.ShopId = 1111;
                        //                break;
                        //            case 2:   
                        //                break;
                        //        }
                        //        break;
                        //}
                        //break;
                    case 18: {
                            ConfirmThanMeo(character, npcId, select);
                            break;
                        }
                    case 75:
                        switch (character.TypeMenu)
                        {
                            case 1:
                                switch (select)
                                {
                                    case 0:
                                        if (character.TypeDoiThuong == 3)
                                        {

                                            character.CharacterHandler.RemoveItemBagById(695, 99);
                                            character.CharacterHandler.RemoveItemBagById(696, 99);
                                            character.CharacterHandler.RemoveItemBagById(697, 99);
                                            character.CharacterHandler.RemoveItemBagById(698, 99);
                                            character.CharacterHandler.RemoveItemBagById(694, 99);
                                            character.MineGold(1000000000);

                                            var item = ItemCache.GetItemDefault((short)(ServerUtils.RandomNumber(1241, 1244)));
                                            item.Options.ForEach(option => {
                                                option.Param = ServerUtils.RandomNumber(20, 35);
                                            });
                                            item.Options.Add(new OptionItem()
                                            {
                                                Id = 5,
                                                Param = ServerUtils.RandomNumber(1, 10),
                                            });
                                            character.CharacterHandler.AddItemToBag(false, item);
                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ItemCache.ItemTemplate(item.Id).Name));
                                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                        }
                                        break;
                                }
                                break;
                            case 0:
                                switch (select)
                                {
                                    case 0:
                                        character.CharacterHandler.SendMessage(Service.Shop(character, 0, 37 + character.InfoChar.Gender));
                                        character.ShopId = 37 + character.InfoChar.Gender;
                                        character.TypeShop = 0;
                                        break;
                                    case 1:
                                        character.CharacterHandler.SendMessage(Service.Shop(character, 3, 36));
                                        character.ShopId = 36;
                                        character.TypeShop = 3;
                                        break;
                                    case 2:
                                        var inputGiftcode = new List<InputBox>();
                                        var inputCode = new InputBox()
                                        {
                                            Name = "Nhập mã quà tặng",
                                            Type = 1,
                                        };
                                        inputGiftcode.Add(inputCode);
                                        character.CharacterHandler.SendMessage(Service.ShowInput("Nhập Giftcode NROLOTUSMUAHESOIDONG để nhận quà", inputGiftcode));
                                        character.TypeInput = 1;
                                        break;
                                    case 3:
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, $"{ServerUtils.Color("green")}Chế tạo cải trang siêu cấp {ItemHandler.TrueItem(character, new List<int> { 695, 696, 697, 698, 694, -1 }, new List<int> { 99, 99, 99, 99, 99, 100000000 }, 3)}", new List<string> { character.TypeDoiThuong != 3 ? "Từ chối" : "Chế tạo" }, character.InfoChar.Gender));
                                        character.TypeMenu = 1;
                                        break;
                                    case 5: // tang ngai dem
                                        {
                                            if (character.CharacterHandler.GetItemBagById(1256) == null)
                                            {
                                                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn không có Ngài Đêm"));

                                                return;
                                            }
                                            character.CharacterHandler.RemoveItemBagById(1256, 1);
                                            character.CharacterHandler.PlusPotential(1000);
                                            character.CharacterHandler.PlusPower(1000);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(2, 1000));
                                            List<int> ListItem = new List<int> { 1259,1260,1250,1251   };
                                            var randomHSD = ServerUtils.RandomNumber(100);
                                            var item = ItemCache.GetItemDefault((short)(ListItem[ServerUtils.RandomNumber(ListItem.Count)]));
                                            switch (randomHSD)
                                            {
                                                case <= 5:

                                                    break;
                                                default:
                                                    item.Options.Add(new OptionItem()
                                                    {
                                                        Id = 93,
                                                        Param = ServerUtils.RandomNumber(1, 7)
                                                    });
                                                    break;
                                            }
                                            character.CharacterHandler.AddItemToBag(false, item);
                                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                                            break;
                                        }
                                    case 6:
                                        {
                                            
                                        }
                                        break;
                                    case 4:// tang bo canh cung
                                        {
                                            if (character.CharacterHandler.GetItemBagById(1255) == null)
                                            {
                                                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn không có Bọ cánh cứng"));
                                                return;
                                            }
                                            character.CharacterHandler.RemoveItemBagById(1255, 1);
                                            character.CharacterHandler.PlusPotential(1000);
                                            character.CharacterHandler.PlusPower(1000);
                                            character.CharacterHandler.SendMessage(Service.UpdateExp(2, 1000));
                                            List<int> ListItem = new List<int> { 1259, 1260, 1250, 1251 };
                                            var randomHSD = ServerUtils.RandomNumber(100);
                                            var item = ItemCache.GetItemDefault((short)(ListItem[ServerUtils.RandomNumber(ListItem.Count)]));
                                            switch (randomHSD)
                                            {
                                                case <= 5:

                                                    break;
                                                default:
                                                    item.Options.Add(new OptionItem()
                                                    {
                                                        Id = 93,
                                                        Param = ServerUtils.RandomNumber(1, 7)
                                                    });
                                                    break;
                                            }
                                            character.CharacterHandler.AddItemToBag(false, item);
                                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                                            break;
                                        }
                                }
                                break;
                        }
                        break;
                    case 19: {
                            ConfirmThuongDe(character, npcId, select);
                            break;
                        }
                    case 20: {
                            ConfirmThanVuTru(character, npcId, select);
                            break;
                        }
                    case 51:
                        ConfirmDuaHau(character, npcId, select);
                        break;
                    case 21: {
                            ConfirmBaHatMit(character, npcId, select);
                            break;
                        }
                    case 23: {
                            ChampionShip.gI().HandlerMenu(character, npcId, select);
                            break;
                        }
                    case 24: {
                            ConfirmRongThan(character, npcId, select);
                            break;
                        }
                    case 25: {
                            //character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Sẽ mở vào ngày 10/11"));
                            ConfirmLinhCanh(character, npcId, select);
                            break;
                        }
                    case 53:
                        switch (select)
                        {
                            case 0:
                                if (Hirudegarn.gI().Init)
                                {
                                    var zoneJoin = MapManager.Get(126).GetZoneNotMaxPlayer();
                                    if (zoneJoin != null)
                                    {
                                        character.CharacterHandler.SendZoneMessage(Service.SendTeleport(character.Id, 2));
                                        MapManager.Get(character.InfoChar.MapId).OutZone(character, 126);
                                        zoneJoin.ZoneHandler.JoinZone(character, true, true, character.TypeTeleport);
                                    }
                                    else
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiSay(5, TextServer.gI().MAX_NUMCHARS, false, character.InfoChar.Gender));
                                    }
                                    break;
                                }
                                break;
                        }
                        break;
                    case 361:
                        ConfirmMayDo(character, npcId, select);
                        break;
                    case 37: {
                            ConfirmBummaTL(character, npcId, select);
                            break;
                        }
                    case 38: {
                            ConfirmCalich(character, npcId, select);
                            break;
                        }
                    case 39: {
                            ConfirmSanta(character, npcId, select);
                            break;
                        }
                    case 74:
                        {
                            switch (select)
                            {
                                case 0:
                                    character.CharacterHandler.SendMessage(Service.Shop(character, 0, 29));
                                    character.TypeShop = 0;
                                    character.ShopId = 29;
                                    break;


                                case 1:
                                    character.CharacterHandler.SendMessage(Service.Shop(character, 3, 40));
                                    character.TypeShop = 3; 
                                    character.ShopId = 40;
                                    break;
                            }
                            break;
                        }
                    
                        
                    case 42: {
                            ConfirmQuocVuong(character, npcId, select);
                            break;
                        }
                    case 47:
                        {
                            ConfirmGiuMa(character, npcId, select);
                            break;
                        }
                    case 76:
                        switch (select)
                        {
                            case 1:
                                character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                                character.TypeMenu = 1;
                                break;
                        }
                        break;
                    case 49:
                        ConfirmDuongTang(character, npcId, select);
                        break;
                    case 50: {
                            if (character.InfoChar.ThoiGianTrungMaBu <= 0)
                            {
                                // UserDB.BanUser(character.Player.Id);
                                ClientManager.Gi().KickSession(character.Player.Session);
                                ServerUtils.WriteLog("hacktrung", $"Tên tài khoản {character.Player.Username} (ID:{character.Player.Id}) hack trứng");

                                var temp = ClientManager.Gi().GetPlayer(character.Player.Id);
                                if (temp != null)
                                {
                                    ClientManager.Gi().KickSession(temp.Session);
                                }
                                return;
                            }
                            ConfirmQuaTrung(character, npcId, select);
                            break;
                        }
                    case 55: {
                            ConfirmBill(character, npcId, select);
                            break;
                        }
                    case 52:
                        ConfirmHungVuong(character, npcId, select);
                        break;
                    

                    case 77:
                        ConfirmTrang(character, npcId, select);
                        break;
                    case 62:
                        ConfirmPotage(character, npcId, select);
                        break;
                    case 63:
                        ConfirmJaco(character, npcId, select);
                        break;
                    case 60:
                        ConfirmGokuSSJ60(character, npcId, select);
                        break;
                    case 61:
                        ConfirmGokuSSJ61(character, npcId, select);
                        break;
                    case 67:
                        {
                            switch (select)
                            {
                                case 0:
                                    var uisay = "Chúng ta gặp rắc rối rồiThượng Đế nói với tôi rằng có 1 loại khígọi là Destron Gas, thứ này không thuộc về nơi đây"
                                  + "\nNó tích tụ trên Trái Đấtvà nó sẽ hủy diệt mọi mô tế bào sốngCó tất cả 4 địa điểm mà Thượng Đế bảo tôi nói với cậuCậu có thể đến kiểm tra..."
                                  + "\nĐầu tiên là Thành phố Santa tọa lạc ở phía tây nam của thủ đô ở Viễn Đông."
                                  + "\nThứ hai là gần Kim Tự Tháp ở vùng Sa Mạc viễn tây của thủ đô Phía Bắc."
                                  + "\nThứ ba Vùng Đất Băng Giá ở Phương Bắc xa xôi"
                                  + "\nThứ tư là Hành tinh Bóng Tối đang che phủ một phần địa cầu\nCậu đã hiểu rõ chưa ?";
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, uisay));
                                    break;
                                case 1:
                                    ClanRank.SelectTopKhiGas(100);
                                    character.CharacterHandler.SendMessage(ClanRank.ListTopRankKhiGas());
                                    break;
                                case 2:

                                    if (character.ClanId == -1 || ClanManager.Get(character.ClanId) == null)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn chưa có bang hội !"));
                                        return;
                                    }
                                    if (ServerUtils.TimeNow().Day - ClanManager.Get(character.ClanId).TimeClanCreate.Day < 2)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Yêu cầu bang hội được thành lập trên 2 ngày"));
                                        return;
                                    }
                                    if (ClanManager.Get(character.ClanId).Gas.Count == 0)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Mai hãy quay lại, hết lượt vào rồi !"));

                                        return;
                                    }
                                    if (!ClanManager.Get(character.ClanId).Gas.Open)
                                    {
                                        var inputKhiGas = new List<InputBox>();
                                        var inputLevel = new InputBox()
                                        {
                                            Name = "(Nhập cấp độ từ 0 -> 110)",
                                            Type = 1,
                                        };
                                        inputKhiGas.Add(inputLevel);
                                        character.CharacterHandler.SendMessage(Service.ShowInput("Nhập cấp độ Khí Gas", inputKhiGas));
                                        character.TypeInput = 22;
                                    }
                                    else
                                    {
                                        var mapOld = MapManager.Get(character.InfoChar.MapId);
                                        mapOld.OutZone(character, 149);
                                        var clan = ClanManager.Get(character.ClanId);
                                        clan.Gas.GasMaps[0].JoinZone(character, 0);
                                    }



                                    break;
                            }
                            break;
                        }
                    case 28:
                    ConfirmKyGUI(character, npcId, select);
                    break;
                    case 66:
                        {
                            ConfirmNoiBanh(character, npcId, select);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error Ui Confirm in Menu.cs: {e.Message} \n {e.StackTrace}", e);
            }
            finally
            {
                message?.CleanUp();
            }
        }

        #region Menu COFIRM
        private static void ConfirmTrang(Character character, short npcid, int select)
        {

            switch (select)
            {
                case 0:
                    character.CharacterHandler.SendMessage(Service.Shop(character, 3, 30)); // shop van bay
                    character.ShopId = 30;
                    character.TypeShop = 3;
                    break;
                case 1:
                    character.CharacterHandler.SendMessage(Service.Shop(character, 3, 31)); // shop rada
                    character.ShopId = 31;
                    character.TypeShop = 3;
                    break;
                case 2:
                    character.CharacterHandler.SendMessage(Service.Shop(character, 3, 32)); // shop cai trang
                    character.ShopId = 32;
                    character.TypeShop = 3;

                    break;
                case 3:
                    character.CharacterHandler.SendMessage(Service.Shop(character, 3, 33)); // shop rong bang
                    character.ShopId = 33;
                    character.TypeShop = 3;
                    break;
                case 4:

                    character.CharacterHandler.SendMessage(Service.Shop(character, 3, 34)); // shop su kien
                    character.ShopId = 34;
                    character.TypeShop = 3;
                    break;
            }

        }
        private static void ConfirmHungVuong(Character character, short npcid, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                    if (EventRuntime.TimeCollectHatGiong <= ServerUtils.CurrentTimeMillis())
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcid, "Hãy trồng dưa hấu và mang chúng đến gặp ta đổi hồng ngọc", new List<string> { "Trồng hâu dứa" }, character.InfoChar.Gender));
                        character.TypeMenu = 1;
                        return;
                    }
                    if (character.CharacterHandler.GetItemBagById(569) == null || character.CharacterHandler.GetItemBagById(569).Quantity < EventRuntime.DataTradeDuaHau[select][0])
                    {
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn còn thiếu " + (EventRuntime.DataTradeDuaHau[select][0] - (character.CharacterHandler.GetItemBagById(569) != null ? character.CharacterHandler.GetItemBagById(569).Quantity : 0)) + " dưa hấu"));
                        return;
                    }
                    character.CharacterHandler.RemoveItemBagById(569, EventRuntime.DataTradeDuaHau[select][0]);
                    character.PlusDiamondLock(EventRuntime.DataTradeDuaHau[select][1]);
                    character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                    character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn vừa nhận được " + EventRuntime.DataTradeDuaHau[select][1] + " hồng ngọc"));
                    break;
                case 1:
                    if (character.InfoChar.ThoiGianDuaHau != 0)
                    {
                        // has dua hau
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Ngươi đã có cây hâu dứa rồi !"));
                        return;
                    }
                    else
                    {
                        character.InfoChar.ThoiGianDuaHau = DataCache._1DAY + ServerUtils.CurrentTimeMillis();
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Cây hâu dứa vừa được trồng ở nhà bạn!"));
                        EventRuntime.TimeCollectHatGiong = 300000 + ServerUtils.RandomNumber(300000);
                    }
                    break;
            }
        }
        private static void ConfirmDuaHau(Character character, short npcid, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                    switch (select)
                    {
                        case 0:

                            break;
                    }
                    break;
                case 1:
                    switch (select)
                    {
                        case 0:
                            character.InfoChar.ThoiGianDuaHau = DataCache._1DAY + ServerUtils.CurrentTimeMillis();
                            var duahau = ItemCache.GetItemDefault(569);
                            duahau.Options.Add(new OptionItem()
                            {
                                Id = 30,
                                Param = 0
                            });
                            duahau.Options.Add(new OptionItem()
                            {
                                Id = 93,
                                Param = 30
                            });
                            character.CharacterHandler.AddItemToBag(true, duahau);
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn nhận được Dưa Hấu"));
                            character.CharacterHandler.SendMessage(Service.DuaHau(character));
                            break;
                    }
                    break;
            }
        }
        private static void ConfirmGokuSSJ60(Character character, short npcid, int select)
        {

            switch (select)
            {
                case 0:
                    if (character.InfoChar.MapId == 80) MapManager.JoinMap(character, 131, ServerUtils.RandomNumber(20), false, false, 0);
                    else MapManager.JoinMap(character, 80, ServerUtils.RandomNumber(20), false, false, 0);
                    break;
            }

        }
        private static void ConfirmGokuSSJ61(Character character, short npcid, int select)
        {

            switch (select)
            {
                case 0:
                    if (character.CharacterHandler.GetItemBagById(590) == null || character.CharacterHandler.GetItemBagById(590).Quantity < 9999)
                    {
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Ngươi không có đủ 9.999 Bí Kiếp !"));
                        return;
                    }
                    var item = ItemCache.GetItemDefault((short)(592 + character.InfoChar.Gender));
                    character.CharacterHandler.AddItemToBag(false,item);
                    character.CharacterHandler.RemoveItemBagById(590, 9999);
                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                    character.CharacterHandler.SendMessage(Service.ServerMessage("Nhận thưởng thành công !"));
                    
                    break;
            }

        }
        private static void ConfirmPotage(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                    switch (select)
                    {
                        case 0:

                            break;
                        case 1: // go verus
                            character.Clone.Start(character);
                            break;
                    }
                    break;
                case 1:
                    break;
            }
        }
        private static void ConfirmJaco(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                    switch (select)
                    {
                        case 0:
                            MapManager.OutMap(character, 140);// go map clone
                            character.Clone.CloneMap[0].JoinZone(character, 0);

                            break;
                        case 1: // return
                            break;
                    }
                    break;
                case 1:
                    break;
            }
        }
        public static void GetItemNapVIP(Character character, int vip)
        {
            var item = ItemCache.GetItemDefault(1049);
            switch (vip)
            {
                case 1:
                    character.InfoChar.Gold += 1000000000;
                    ItemCache.GetItem(character, 457, 50);
                    //ItemCache.GetItem(character, 711, 1);
                    var x4 = ItemCache.GetItemDefault(711);
                    
                    character.CharacterHandler.AddItemToBag(false, x4, "Nap VIP 1");
                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                    UserDB.MineVND(character.Player,50000);
                    break;
                case 2:

                    character.InfoChar.Gold += 1000000000;
                    ItemCache.GetItem(character, 457, 100);
                    //   ItemCache.GetItem(character, 459, 10);
                    //   ItemCache.GetItem(character, 860, 1);
                    var minuong = ItemCache.GetItemDefault(860);
                    
                   // CreatePetNormal(character);
                   // var random = ServerUtils.RandomNumber(1, 100);
                   // character.PlusDiamondLock(random);
                    character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                    character.CharacterHandler.AddItemToBag(false, minuong, "Nap VIP 2");
                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                    UserDB.MineVND(character.Player,100000);
                    break;

            }
        }

        private static void ConfirmBoMong(Character character,short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                    {
                        switch (select)
                        {
                            case 0: //Gifcode
                                {
                                    var inputGiftcode = new List<InputBox>();
                                    var inputCode = new InputBox()
                                    {
                                        Name = "Nhập mã quà tặng",
                                        Type = 1,
                                    };
                                    inputGiftcode.Add(inputCode);
                                    character.CharacterHandler.SendMessage(Service.ShowInput("Mã Quà Tặng", inputGiftcode));
                                    character.TypeInput = 1;
                                    character.ShopId = npcId;
                                    break;
                                }
                            case 1: //Nhiệm vụ bò mộng
                                {
                                    character.CharacterHandler.SendMessage(DragonBoyZ.Application.Extension.Bo_Mong.BoMong_Task.BoMongDAO(character));
                                    break;
                                }
                            case 2: //Nhiệm vụ hằng ngày
                                {
                                    break;
                                }
                        }
                        break;
                    }
            }    
        }

        private static void ConfirmLinhCanh(Character character, short npcId, int select)
        {
            switch (character.TypeMenu) {
                case 0:
                    {
                        switch (select)
                        {
                            case 0: // not have reddotmaps so init and go
                                var clan = ClanManager.Get(character.ClanId);
                                if (ServerUtils.TimeNow().Day - ClanManager.Get(character.ClanId).TimeClanCreate.Day < 2)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Yêu cầu bang hội được thành lập trên 2 ngày"));
                                    return;
                                }
                                if (ServerUtils.TimeNow().Day - clan.Thành_viên.FirstOrDefault(i => i.Id == character.Id).DateJoin.Day < 2)
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ngươi phải tham gia bang hội trên 2 ngày thì mới được đi doanh trại",
                                       new List<string> { "Hướng\ndẫn\nthêm" }, character.InfoChar.Gender));
                                    character.TypeMenu = 2;
                                    return; 
                                }
                                if (clan.Reddot.Open == false)
                                {
                                    clan.Reddot.InitReddot();
                                    clan.Reddot.InitMobHp(character);
                                    clan.Reddot.InitBoss(character);
                                    var mapOld = MapManager.Get(character.InfoChar.MapId);
                                    mapOld.OutZone(character, 53);
                                    character.InfoChar.X = 63;
                                    character.InfoChar.Y = 432;
                                    clan.Reddot.ReddotMaps[0].JoinZone(character, 0);
                                    character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage("Doanh trại độc nhãn", 0, 900000));
                                    
                                }
                                else // has map doanh trai so go go go
                                {
                                    var mapOld = MapManager.Get(character.InfoChar.MapId);
                                    mapOld.OutZone(character, 53);
                                    clan.Reddot.ReddotMaps[0].JoinZone(character, 0);
                                    
                                    character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage("Doanh trại độc nhãn", 0, (int)(ServerUtils.CurrentTimeMillis() - clan.Reddot.timeDoanhTrai)));
                                }
                                break;

                        }
                    }
                    break;
            }
        }

        private static void ConfirmBaOngGia(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                    {
                        switch (select)
                        {
                            case 0: //Bảng xếp hạng
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBaOngGia[0], MenuNpc.Gi().MenuBaOngGia[1], character.InfoChar.Gender));
                                    character.TypeMenu = 1;
                                    //Update BXH
                                    CharacterDB.Update(character);
                                    if (character.Disciple != null)
                                    {
                                        DiscipleDB.Update(character.Disciple);
                                    }
                                    CharacterDB.SelectBXHSucManh(10);
                                    UserDB.SelectBXHTopNap(10);
                                    Server.Gi().Logger.PrtColor("yellow", "Update", "darkyellow", "Load data all character");
                                    break;
                                }
                            case 1: //Nhận ngọc
                                {
                                    if (character.AllDiamond() < 2000000)
                                    {
                                        character.PlusDiamond(200000000);
                                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                    }
                                    else
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Đủ rồi, tham lam vừa thôi "));
                                    }
                                    break;
                                }
                            case 2: //Nhận đệ
                                {
                                    if (character.Disciple != null || DiscipleDB.IsAlreadyExist(-character.Id))
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận đệ tử rồi !"));
                                        return;
                                    }
                                    CreatePetNormal(character);
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn vừa thu nhận được đệ tử !"));
                                    break;
                                }
                            case 3: //Đổi mật khẩu
                                {
                                    var inputChangePass = new List<InputBox>();
                                    var inputPassNow = new InputBox()
                                    {
                                        Name = "Nhập mật khẩu hiện tại",
                                        Type = 1,
                                    };
                                    inputChangePass.Add(inputPassNow);
                                    var inputPassChange = new InputBox()
                                    {
                                        Name = "Nhập mật khẩu muốn thay đổi",
                                        Type = 1,
                                    };
                                    inputChangePass.Add(inputPassChange);
                                    var inputConfirmPassChange = new InputBox()
                                    {
                                        Name = "Xác nhận mật khẩu muốn thay đổi",
                                        Type = 1,
                                    };
                                    inputChangePass.Add(inputConfirmPassChange);
                                    character.CharacterHandler.SendMessage(Service.ShowInput("Đổi mật khẩu", inputChangePass));
                                    character.TypeInput = 21;
                                    break;
                                }
                        }
                        break;
                    }
                case 1:
                    {
                        switch (select)
                        {
                            case 0: //Top Sm
                                {                                   
                                    var bangXepHangTopSM = Server.Gi().BangXepHang.GetList();
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, bangXepHangTopSM));
                                    break;
                                }
                            case 1: //Top Nạp
                                {
                                    var bangXepHangTopNap = Server.Gi().BangXepHang.GetListTopNap();
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, bangXepHangTopNap));
                                    break;
                                }
                        }
                        break;
                    }
            }    
        }

        public static void ConfrimLiTieuNuong(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:

                    break;
                case 1:
                    break;
            }
        }
        private static void ConfirmMeo(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                //Thách đấu
                case 0:
                    {
                        if (DataCache.IdMapCustom.Contains(character.InfoChar.MapCustomId))
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().DOT_NOT_TEST_HERE));
                            return;
                        }
                       
                        var testChar = character.Zone.ZoneHandler.GetCharacter(character.Challenge.PlayerChallengeID);
                        if (testChar == null) character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_FOUND_CHAR_IN_MAP));
                        else
                        {
                            switch (select)
                            {
                                //1,000 vàng
                                case 0:
                                    {
                                        if (character.InfoChar.Gold < 1000)
                                        {
                                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                                            character.Challenge.Gold = 0;
                                        }
                                        else
                                        {
                                            var text = string.Format(TextServer.gI().SEND_TEST, character.Name, ServerUtils.GetPower(character.InfoChar.Potential), 1000);
                                            character.Challenge.Gold = testChar.Challenge.Gold = 1000;
                                            Server.Gi().Logger.PrintColor("CLID:" + character.Challenge.PlayerChallengeID, "red");
                                            testChar.CharacterHandler.SendMessage(Service.PlayerVsPLayer(3, character.Id, 1000, text));
                                        }
                                        break;
                                    }
                                //10,000 vàng
                                case 1:
                                    {
                                        if (character.InfoChar.Gold < 10000)
                                        {
                                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                                            character.Challenge.Gold = 0;
                                        }
                                        else
                                        {
                                            var text = string.Format(TextServer.gI().SEND_TEST, character.Name, ServerUtils.GetPower(character.InfoChar.Potential), 10000);
                                            character.Challenge.Gold = testChar.Challenge.Gold = 10000;
                                            testChar.CharacterHandler.SendMessage(Service.PlayerVsPLayer(3, character.Id, 10000, text));
                                        }
                                        break;
                                    }
                                //100,000 vàng
                                case 2:
                                    {
                                        if (character.InfoChar.Gold < 100000)
                                        {
                                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                                            character.Challenge.Gold = 0;
                                        }
                                        else
                                        {
                                            var text = string.Format(TextServer.gI().SEND_TEST, character.Name, ServerUtils.GetPower(character.InfoChar.Potential), 100000);
                                            character.Challenge.Gold = testChar.Challenge.Gold = 100000;
                                            testChar.CharacterHandler.SendMessage(Service.PlayerVsPLayer(3, character.Id, 100000, text));
                                        }
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                //Nâng cấp đậu
                case 1:
                    {
                        var magicTree = MagicTreeManager.Get(character.Id);
                        if (magicTree == null || select == 1) return;
                        lock (magicTree)
                        {
                            var levelTree = magicTree.Level;
                            var gold = DataCache.UpgradeDauThanGold[levelTree - 1];
                            if (character.InfoChar.Gold < gold)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                                return;
                            }
                            character.MineGold(gold);
                            magicTree.IsUpdate = true;
                            magicTree.Seconds = DataCache.UpgradeDauThanTime[levelTree - 1] + ServerUtils.CurrentTimeMillis();
                            magicTree.MagicTreeHandler.HandleNgoc();
                            character.CharacterHandler.SendMessage(Service.MagicTree0(magicTree));
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                        }
                        break;
                    }
                //Huỷ nâng cấp đậu
                case 2:
                    {
                        var magicTree = MagicTreeManager.Get(character.Id);
                        if (magicTree == null || select == 1) return;
                        lock (magicTree)
                        {
                            var levelTree = magicTree.Level;
                            var gold = DataCache.UpgradeDauThanGold[levelTree - 1];
                            character.PlusGold(gold / 2);
                            magicTree.IsUpdate = false;
                            if (magicTree.Peas == magicTree.MaxPea)
                            {
                                magicTree.Seconds = 0;
                            }
                            else
                            {
                                magicTree.Seconds = 60000 * magicTree.Level + ServerUtils.CurrentTimeMillis();
                            }
                            magicTree.MagicTreeHandler.HandleNgoc();
                            character.CharacterHandler.SendMessage(Service.MagicTree0(magicTree));
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                        }
                        break;
                    }
                //Kết bạn
                case 3:
                    {
                        if (select != 0 || character.FriendTemp == null) return;
                        character.Friends.Add(character.FriendTemp);
                        var @char = ClientManager.Gi().GetCharacter(character.FriendTemp.Id);
                        @char?.CharacterHandler.SendMessage(Service.WorldChat((Character)character, string.Format(TextServer.gI().ADD_FRIEND, character.Name, @char.Name), 1));
                        character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().ADD_FRIEND_2, character.FriendTemp.Name)));
                        character.FriendTemp = null;
                        break;
                    }
                //Xoá kết bạn
                case 4:
                    {
                        if (select != 0 || character.FriendTemp == null) return;
                        character.Friends.RemoveAll(friend => friend.Id == character.FriendTemp.Id);
                        character.CharacterHandler.SendMessage(Service.ListFriend2(character.FriendTemp.Id));
                        character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().FRIEND_DELETE, character.FriendTemp.Name)));
                        character.FriendTemp = null;
                        break;
                    }
                //Dịch chuyển tới người chơi
                case 5:
                    {
                        if (select != 0 || character.EnemyTemp == null) return;
                        var charCheck = (Character)ClientManager.Gi().GetCharacter(character.EnemyTemp.Id);
                        if (charCheck == null)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().USER_OFFLINE));
                        }
                        else
                        {
                            var mapId = character.InfoChar.MapId;
                            if (DataCache.IdMapKarin.Contains(mapId) || DataCache.IdMapSpecial.Contains(mapId))
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().TELEPORT_ERROR));
                            }
                            else
                            {
                                var mapEnemy = MapManager.Get(charCheck.Zone.Map.Id);
                                var mapNow = MapManager.Get(mapId);
                                mapNow.OutZone(character, mapEnemy.Id);
                                character.InfoChar.X = charCheck.InfoChar.X;
                                mapEnemy.JoinZone((Character)character, charCheck.Zone.Id);
                                //async void Action()
                                //{
                                //    await Task.Delay(3000);
                                //    character.InfoChar.TypePk = 3;
                                //    character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 3));
                                //    character.IsRevenge = true;
                                //    character.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, "Mau đền tội !"));
                                //    charCheck.InfoChar.TypePk = 3;
                                //    charCheck.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(charCheck.Id, 3));
                                //    charCheck.IsRevenge = true;


                                //        while (charCheck != null && character != null && charCheck.IsRevenge && character.IsRevenge)
                                //        {
                                //            if (charCheck.InfoChar.MapId != character.InfoChar.MapId)
                                //            {
                                //                character.InfoChar.TypePk = 0;
                                //                character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 0));
                                //                charCheck.InfoChar.TypePk = 0;
                                //                charCheck.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(charCheck.Id, 0));
                                //                character.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, "Để nó chạy mất tiêu rồi,tiếc thật, djtme cay vãi lồn !"));
                                //                charCheck.IsRevenge = false;
                                //                character.IsRevenge = false;
                                //            }
                                //            if (character.InfoChar.IsDie)
                                //            {
                                //                character.InfoChar.TypePk = 0;
                                //                character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 0));
                                //                charCheck.InfoChar.TypePk = 0;
                                //                charCheck.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(charCheck.Id, 0));
                                //                character.CharacterHandler.SendMessage(Service.ServerMessage("Yếu mà ra dẻ quá à !"));
                                //                charCheck.IsRevenge = false;
                                //                character.IsRevenge = false;
                                //            }
                                //            if (charCheck.InfoChar.IsDie)
                                //            {
                                //                character.InfoChar.TypePk = 0;
                                //                character.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(character.Id, 0));
                                //                charCheck.InfoChar.TypePk = 0;
                                //                charCheck.CharacterHandler.SendZoneMessage(Service.ChangeTypePk(charCheck.Id, 0));
                                //                character.CharacterHandler.SendMessage(Service.ServerMessage("Báo thù thành công !"));
                                //                charCheck.IsRevenge = false;
                                //                character.IsRevenge = false;
                                //            }
                                //            Thread.Sleep(1000);
                                //        }
                                //    })).Start();
                                //}
                                //var task = new Task(Action);
                                //task.Start();
                                //}
                                //}

                                character.EnemyTemp = null;
                                break;
                            }
                        }
                        break;
                    }
                //Rời bang
                case 6:
                    {
                        if (select != 0) return;
                        var clan = ClanManager.Get(character.ClanId);
                        if (clan == null) return;
                        var me = clan.ClanHandler.GetMember(character.Id);
                        if (clan.ClanHandler.RemoveMember(me.Id))
                        {
                            var lastMess = clan.Messages.LastOrDefault();
                            var id = lastMess != null ? lastMess.Id + 1 : 0;
                            clan.ClanHandler.Chat(new ClanMessage()
                            {
                                Type = 0,
                                Id = id,
                                PlayerId = -1,
                                PlayerName = "Thông báo",
                                Role = 0,
                                Time = ServerUtils.CurrentTimeSecond() - 1000000000,
                                Text = string.Format(TextServer.gI().LEAVE_CLAN, me.Name),
                                Color = 1,
                                NewMessage = true,
                            });
                            character.ClanId = -1;
                            character.InfoChar.Bag = -1;
                            clan.ClanHandler.SendUpdateClan();
                            if (character.InfoChar.PhukienPart == -1) character.CharacterHandler.SendZoneMessage(Service.SendImageBag(character.Id, -1));
                            character.CharacterHandler.SendMessage(Service.GetImageBag(null));
                            character.CharacterHandler.SendMessage(Service.MyClanInfo());
                            character.CharacterHandler.SendZoneMessage(Service.UpdateClanId(character.Id, -1));
                            clan.ClanHandler.UpdateClanId();
                            CharacterDB.Update(character);
                            ClanDB.Update(clan);
                        }
                        break;
                    }
                //Xoá thù địch
                case 7:
                    {
                        if (select != 0 || character.EnemyTemp == null) return;
                        character.Enemies.RemoveAll(enemy => enemy.Id == character.EnemyTemp.Id);
                        character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().ENEMY_DELETE, character.EnemyTemp.Name)));
                        character.EnemyTemp = null;
                        break;
                    }
                //Đồng ý kích hoạt mã
                case 8:
                    {
                        if (select != 0 || character.InfoChar.LockInventory.PassTemp == -1) return;
                        if (character.InfoChar.Gold < 50000)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                            return;
                        }
                        character.MineGold(50000);
                        character.InfoChar.LockInventory.IsLock = true;
                        character.InfoChar.LockInventory.Pass = character.InfoChar.LockInventory.PassTemp;
                        character.InfoChar.LockInventory.PassTemp = -1;
                        character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().ACTIVE_LOCK_INVENTORY));
                        character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                        break;
                    }
                //Mở/Khoá rương
                case 9:
                    {
                        if (select != 0 || character.InfoChar.LockInventory.Pass == -1) return;
                        character.InfoChar.LockInventory.IsLock = !character.InfoChar.LockInventory.IsLock;
                        character.CharacterHandler.SendMessage(character.InfoChar.LockInventory.IsLock
                            ? Service.ServerMessage(TextServer.gI().SUCCESS_LOCK_INVENTORY)
                            : Service.ServerMessage(TextServer.gI().UNACTIVE_LOCK_INVENTORY));
                        break;
                    }
                // Nội tại
                case 10:
                    {
                        switch (select)
                        {
                            case 0: //Xem tất cả nội tại
                                {
                                    character.CharacterHandler.SendMessage(Service.SpeacialSkill(character, 1));
                                    break;
                                }
                            case 1: //Mở nội tại VIP
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(5, string.Format(MenuNpc.Gi().TextNoiTai[1], 100),
                                            MenuNpc.Gi().MenuNoiTai[1], character.InfoChar.Gender));
                                    character.TypeMenu = 11;
                                    break;
                                }
                            case 2: //Mở nội tại NORMAL
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(5, string.Format(MenuNpc.Gi().TextNoiTai[2], ServerUtils.GetMoney(50000000)),
                                            MenuNpc.Gi().MenuNoiTai[2], character.InfoChar.Gender));
                                    character.TypeMenu = 12;
                                    break;
                                }

                        }
                        break;
                    }
                case 11://mở nội tại VIP
                    {
                        switch (select)
                        {
                            case 0:

                                var specialSkillTemplate = Cache.Gi().SPECIAL_SKILL_TEMPLATES.FirstOrDefault(s => s.Key == character.InfoChar.Gender).Value;
                                if (specialSkillTemplate == null) return;
                                if (character.AllDiamond() < 100)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                                    return;
                                }
                                character.MineDiamond(100);

                                int RandomIndex = ServerUtils.RandomNumber(specialSkillTemplate.Count);
                                SpecialSkillTemplate SkillRandom = specialSkillTemplate[RandomIndex];

                                int ValueRandom = 0;

                               
                                    ValueRandom = ServerUtils.RandomNumber(SkillRandom.Min+10, SkillRandom.Max + 1);

                                string InfoRandom = SkillRandom.InfoFormat.Replace("#", ValueRandom + "");

                                character.SpecialSkill.Id = SkillRandom.Id;
                                character.SpecialSkill.Info = InfoRandom;
                                character.SpecialSkill.Img = SkillRandom.Img;
                                character.SpecialSkill.SkillId = SkillRandom.SkillId;
                                character.SpecialSkill.Value = ValueRandom;
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã mở nội tại " + InfoRandom));
                                character.CharacterHandler.SendMessage(Service.SpeacialSkill(character, 0));
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                break;
                        }
                        break;
                    }
                case 12://mở nội tại NORMAL
                    {
                        switch (select) {
                            case 0:
                     var specialSkillTemplate = Cache.Gi().SPECIAL_SKILL_TEMPLATES.FirstOrDefault(s => s.Key == character.InfoChar.Gender).Value;
                        if (specialSkillTemplate == null) return;
                        if (character.InfoChar.Gold < 50000000) {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                            return;
                        }
                        character.MineGold(50000000);

                        int RandomIndex = ServerUtils.RandomNumber(specialSkillTemplate.Count);
                        SpecialSkillTemplate SkillRandom = specialSkillTemplate[RandomIndex];

                        int ValueRandom = 0;

                        ValueRandom = ServerUtils.RandomNumber(SkillRandom.Min, SkillRandom.Max + 1);
                                
                                String    InfoRandom = SkillRandom.InfoFormat.Replace("#", ValueRandom + "");
                                

                        character.SpecialSkill.Id = SkillRandom.Id;
                        character.SpecialSkill.Info = InfoRandom;
                        character.SpecialSkill.Img = SkillRandom.Img;
                        character.SpecialSkill.SkillId = SkillRandom.SkillId;
                        character.SpecialSkill.Value = ValueRandom;
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã mở nội tại " + InfoRandom));
                        character.CharacterHandler.SendMessage(Service.SpeacialSkill(character, 0));
                        character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                        break;
                    }
                        break;
            }
                case 13://hộp siêu phàm
                    {
                        switch (select)
                        {
                            case 0:
                                {
                                    var ListDoThanLinh = new List<int> { 555, 556, 561, 562, 563 };
                                    var ListOption = new List<int> { 47, 6, 0, 7, 12 };
                                    var minParam = new List<int> { 730, 36000, 3600, 36000, 12 };
                                    var maxParam = new List<int> { 1200, 69999, 7000, 59000, 18 };
                                    for (int i2 = 0; i2 < ListDoThanLinh.Count; i2++)
                                    {
                                        var item = ItemCache.GetItemDefault((short)(ListDoThanLinh[i2]));
                                        // var ItemTemp = ItemCache.ItemTemplate(item.Id);
                                        // var option = item.Options.FirstOrDefault(i => i.Id == ListOption[ItemTemp.Type]);
                                        //option.Param = ServerUtils.RandomNumber(minParam[ItemTemp.Type], maxParam[ItemTemp.Type]);
                                        character.CharacterHandler.AddItemToBag(false, item);
                                        //  character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ItemTemp.Name));
                                    }
                                    character.CharacterHandler.RemoveItemBagById(1269, 1);
                                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                                }
                                break;
                            case 1:
                                {
                                    var ListDoThanLinh = new List<int> { 557, 558, 561, 564, 565 };

                                    var ListOption = new List<int> { 47, 6, 0, 7, 12 };
                                    var minParam = new List<int> { 730, 36000, 3600, 36000, 12 };
                                    var maxParam = new List<int> { 1200, 69999, 7000, 59000, 18 };
                                    for (int i2 = 0; i2 < ListDoThanLinh.Count; i2++)
                                    {
                                        var item = ItemCache.GetItemDefault((short)(ListDoThanLinh[i2]));
                                        // var ItemTemp = ItemCache.ItemTemplate(item.Id);
                                        // var option = item.Options.FirstOrDefault(i => i.Id == ListOption[ItemTemp.Type]);
                                        //option.Param = ServerUtils.RandomNumber(minParam[ItemTemp.Type], maxParam[ItemTemp.Type]);
                                        character.CharacterHandler.AddItemToBag(false, item);
                                        //  character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ItemTemp.Name));
                                    }
                                    character.CharacterHandler.RemoveItemBagById(1269, 1);
                                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                                }
                                break;
                            case 2:
                                {
                                    var ListDoThanLinh = new List<int> { 559, 560, 561, 566, 567 };

                                    var ListOption = new List<int> { 47, 6, 0, 7, 12 };
                                    var minParam = new List<int> { 730, 36000, 3600, 36000, 12 };
                                    var maxParam = new List<int> { 1200, 69999, 7000, 59000, 18 };
                                    for (int i2 = 0; i2 < ListDoThanLinh.Count; i2++)
                                    {
                                        var item = ItemCache.GetItemDefault((short)(ListDoThanLinh[i2]));
                                       // var ItemTemp = ItemCache.ItemTemplate(item.Id);
                                       // var option = item.Options.FirstOrDefault(i => i.Id == ListOption[ItemTemp.Type]);
                                        //option.Param = ServerUtils.RandomNumber(minParam[ItemTemp.Type], maxParam[ItemTemp.Type]);
                                        character.CharacterHandler.AddItemToBag(false, item);
                                      //  character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được " + ItemTemp.Name));
                                    }
                                    character.CharacterHandler.RemoveItemBagById(1269, 1);
                                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                                    break;
                                }
                        
                        }
                    }
                    break;
        }
    }
        private static void ConfirmMayDo(Character character, short npcId, int select)
        {
            switch (select)
            {
                case 0:
                    character.MineDiamond(10);
                    MapManager.JoinMap(character, Init.NamecBalls[0].MapId, Init.NamecBalls[0].ZoneId, false, false, 0);
                    break;
                case 1:
                    character.MineGold(100000);
                    MapManager.JoinMap(character, Init.NamecBalls[0].MapId, Init.NamecBalls[0].ZoneId, false, false, 0);
                    break;
                case 2: 
                    break;
            }
        }
        private static void ConfirmBumma(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                {
                    if (character.InfoChar.Gender != 0)
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, MenuNpc.Gi().TextBumma[1]));
                    }
                    else if (select == 0)
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBumma[0], MenuNpc.Gi().MenuShopDistrict[1], character.InfoChar.Gender));
                        character.TypeMenu = 1;
                    }
                    break;
                }
                //Show shop
                case 1:
                {
                    if(select == 1) return;
                    var shopId = 12;
                    character.CharacterHandler.SendMessage(Service.Shop(character, 0, shopId));
                    character.ShopId = shopId;
                    character.TypeShop = 0;
                    break;
                }
            }
        }

        private static void ConfirmBummaTL(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                {
                    if(select == 1) return;
                    var shopId = 22;
                    character.CharacterHandler.SendMessage(Service.Shop(character, 0, shopId));
                    character.ShopId = shopId;
                    character.TypeShop = 0;
                    break;
                }
            }
        }
        
        private static void ConfirmDende(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                {
                    if (character.InfoChar.Gender != 1)
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, MenuNpc.Gi().TextDende[1]));
                    }
                    else if (select == 0)
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextDende[0], MenuNpc.Gi().MenuShopDistrict[1], character.InfoChar.Gender));
                        character.TypeMenu = 1;
                    }
                    break;
                }
                //Show shop
                case 1:
                {
                    if(select == 1) return;
                    var idShop = 13;
                    character.CharacterHandler.SendMessage(Service.Shop(character, 0, idShop));
                    character.ShopId = idShop;
                    character.TypeShop = 0;
                    break;
                }
                case 2:
                    switch (select)
                    {
                        case 0:
                            break;
                        case 1:
                            if (character.DataNgocRongNamek.AlreadyPick(character) && character.DataNgocRongNamek.DelayWish <= ServerUtils.CurrentTimeMillis())
                            {
                                if (character.ClanId == -1 || ClanManager.Get(character.ClanId) == null)
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Yêu cầu tham gia bang hội."));
                                    return;
                                }
                                if (character.Zone.ZoneHandler.GetCharacterClanHasNamecBallInMap(character.ClanId).Count < 7)
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Yêu cầu tập trung đủ 7 thành viên trong Bang hội\nMỗi thành viên phải đeo 1 viên Ngọc Rồng Namec", true, character.InfoChar.Gender));
                                    return;
                                }                               
                                character.CharacterHandler.SendZoneMessage(Service.PublicChat(character.Id, "TAKKARABUTO...POPPORUNGA...PUPIRITTOPA,Hỡi Zồng Thiên Ơii mau mau thức dậy."));
                                Rồng_Namec.gI().ShowMenu(character);
                            }else
                            {
                                var time = (character.DataNgocRongNamek.DelayWish - ServerUtils.CurrentTimeMillis())/60000;
                                if (time <= 1)
                                {
                                    time = (character.DataNgocRongNamek.DelayWish - ServerUtils.CurrentTimeMillis()) / 1000;
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, $"Ngọc bẩn quá, xin chờ em {time} giây nữa để lau bóng ngọc\ngọi Zồng mới hiển linh"));
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, $"Ngọc bẩn quá, xin chờ em {time} phút nữa để lau bóng ngọc\ngọi Zồng mới hiển linh"));

                                }
                            }
                            break;
                    }
                    break;
            }
        }
        
        private static void ConfirmAppule(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                {
                    if (character.InfoChar.Gender != 2)
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, MenuNpc.Gi().TextAppule[1]));
                    }
                    else if (select == 0)
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextAppule[0], MenuNpc.Gi().MenuShopDistrict[1], character.InfoChar.Gender));
                        character.TypeMenu = 1;
                    }
                    break;
                }
                //Show shop
                case 1:
                {
                    if(select == 1) return;
                    var idShop = 14;
                    character.CharacterHandler.SendMessage(Service.Shop(character, 0, idShop));
                    character.ShopId = idShop;
                    character.TypeShop = 0;
                    break;
                }
            }
        }

        private static void ConfirmBrief(Character character, short npcId, int select)
        {
            var map = MapManager.Get(character.InfoChar.MapId);
            switch (map.Id)
            {
                case 153:
                    {
                        switch (character.TypeMenu)
                        {
                            case 3:
                                switch (select)
                                {
                                    case 0:
                                        var clan = ClanManager.Get(character.ClanId);
                                        var cost = ClanManager.Get(character.ClanId).Cấp_Độ * 100;
                                        if (clan.Capsule_Bang < cost)
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Không đủ capsule bang"));
                                            return;
                                        }
                                        clan.Capsule_Bang -= cost;
                                        clan.Cấp_Độ++;
                                        clan.Tối_đa_thành_viên++;
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bang hội của bạn đã lên cấp " + clan.Cấp_Độ));
                                        clan.ClanHandler.SendMessage(Service.MyClanInfo());
                                        ClanDB.Update(clan);
                                        break;
                                }
                                break;
                            case 2:
                                switch (select)
                                {
                                    case 0:
                                        var inputGiftcode = new List<InputBox>();
                                        var inputCode = new InputBox()
                                        {
                                            Name = "Nhập tên viết tắt",
                                            Type = 1,
                                        };
                                        inputGiftcode.Add(inputCode);
                                        character.CharacterHandler.SendMessage(Service.ShowInput("Tên viết tắt bang hội", inputGiftcode));
                                        character.TypeInput = 31;
                                        break;
                                    case 1:
                                        ClanManager.Get(character.ClanId).shortName = "TGDV";
                                        break;
                                    case 2:
                                        var clan = ClanManager.Get(character.ClanId);
                                        var cost = ClanManager.Get(character.ClanId).Cấp_Độ * 100;
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, $"Cần {cost} capsule bang [đang có {clan.Capsule_Bang} capsule bang] để nâng cấp bang hội" +
                                            $"\nlên cấp {clan.Cấp_Độ++}\n+1 tối đa số lượng thành viên\n+1 ô trống tối đa rương bang\n+Mở bán bùa bang cấp {clan.Cấp_Độ++}"
                                            , new List<string> { "Đồng ý", "Từ chối" }, character.InfoChar.Gender));
                                        character.TypeMenu = 3;
                                        break;
                                }
                                break;
                            
                            case 1:
                                {
                                    switch (select)
                                    {
                                        case 0:
                                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta có thể giúp gì cho bang hội của bạn?", new List<string> { "Đổi tên\ntên bang\nviết tắt", "Chọn ngẫu nhiên\ntên bang\nviết tắt", "Nâng cấp\nBang hội", "Đóng" }, character.InfoChar.Gender));
                                            character.TypeMenu = 2;
                                            break;
                                        case 2:
                                            {
                                                var map2 = MapManager.Get(character.InfoChar.MapId);
                                                var mapJoin2 = MapManager.Get(5);
                                                var zoneJoin2 = mapJoin2.GetZoneNotMaxPlayer();
                                                if (zoneJoin2!= null)
                                                {
                                                    character.CharacterHandler.SendZoneMessage(Service.SendTeleport(character.Id, character.InfoChar.Teleport));
                                                    map2.OutZone(character, mapJoin2.Id);
                                                    zoneJoin2.ZoneHandler.JoinZone(character, false, true, character.InfoChar.Teleport);
                                                }
                                                else
                                                {
                                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(5, TextServer.gI().MAX_NUMCHARS, false, character.InfoChar.Gender));
                                                }
                                                break;
                                            }
                                    }
                                }
                                break;
                        }
                        break;
                    }
                default:

                    if (map == null) return;
                    Threading.Map mapJoin;

                    if (map.Id == 84)
                    {
                        mapJoin = MapManager.Get(character.InfoChar.Gender + 24);
                    }
                    else
                    {
                        switch (select)
                        {
                            case 0:
                                {
                                    mapJoin = MapManager.Get(26);
                                    break;
                                }
                            case 1:
                                {
                                    mapJoin = MapManager.Get(25);
                                    break;
                                }
                            case 2:
                                {
                                    mapJoin = MapManager.Get(84);
                                    break;
                                }
                            default:
                                {
                                    return;
                                }
                        }
                    }

                    if (mapJoin == null) return;
                    var zoneJoin = mapJoin.GetZoneNotMaxPlayer();
                    if (zoneJoin != null)
                    {
                        character.CharacterHandler.SendZoneMessage(Service.SendTeleport(character.Id, character.InfoChar.Teleport));
                        map.OutZone(character, mapJoin.Id);
                        zoneJoin.ZoneHandler.JoinZone(character, false, true, character.InfoChar.Teleport);
                    }
                    else
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiSay(5, TextServer.gI().MAX_NUMCHARS, false, character.InfoChar.Gender));
                    }
                    break;
            }

        }
        
        private static void ConfirmCargo(Character character, short npcId, int select)
        {
            var map = MapManager.Get(character.InfoChar.MapId);
            if (map == null) return;
            Threading.Map mapJoin;
            switch (select)
            {
                case 0:
                {
                    mapJoin = MapManager.Get(24);
                    break;
                }
                case 1:
                {
                    mapJoin = MapManager.Get(26);
                    break;
                }
                case 2:
                {
                    mapJoin = MapManager.Get(84);
                    break;
                }
                default:
                {
                    return;
                }
            }

            if (mapJoin == null) return;
            var zoneJoin = mapJoin.GetZoneNotMaxPlayer();
            if (zoneJoin != null)
            {
                character.CharacterHandler.SendZoneMessage(Service.SendTeleport(character.Id, character.InfoChar.Teleport));
                map.OutZone(character, mapJoin.Id);
                zoneJoin.ZoneHandler.JoinZone(character, false, true, character.InfoChar.Teleport);
            }
            else
            {
                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, TextServer.gI().MAX_NUMCHARS, false, character.InfoChar.Gender));
            }
        }
        
        private static void ConfirmCui(Character character, short npcId, int select)
        {
            var map = MapManager.Get(character.InfoChar.MapId);
            if (map == null) return;
            Threading.Map mapJoin = null;
            switch (map.Id)
            {
                
               
                case 19:
                    {
                        if (HelpMission.Check(character))
                        {
                            if (select == 0)
                            {
                                mapJoin = MapManager.Get(68);
                            }
                            if (select == 2)
                            {
                                HelpMission.HoTroNhiemVu(character, character.InfoTask.Index);
                            }
                        }
                        else
                        {
                            if (select == 0)
                            {
                                if (character.InfoTask.Id > 28)
                                {
                                    mapJoin = MapManager.Get(109);
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Bạn phải hoàn thành nhiệm vụ để mở khóa chức năng này"));
                                }
                                break;
                            }
                            if (@select == 1)
                            {
                                mapJoin = MapManager.Get(68);
                            }
                        }
                    break;
                }
                case 68:
                {
                    if (@select == 0)
                    {
                        mapJoin = MapManager.Get(19);
                    }

                    break;
                }
                default:
                {
                    switch (@select)
                    {
                        case 0:
                        {
                            mapJoin = MapManager.Get(24);
                            break;
                        }
                        case 1:
                        {
                            mapJoin = MapManager.Get(25);
                            break;
                        }
                        case 2:
                        {
                            mapJoin = MapManager.Get(84);
                            break;
                        }
                        default:
                        {
                            return;
                        }
                    }

                    break;
                }
            }

            if (mapJoin == null) return;
            var zoneJoin = mapJoin.GetZoneNotMaxPlayer();
            if (zoneJoin != null)
            {
                character.CharacterHandler.SendZoneMessage(Service.SendTeleport(character.Id, character.InfoChar.Teleport));
                map.OutZone(character, mapJoin.Id);
                zoneJoin.ZoneHandler.JoinZone(character, false, true, character.InfoChar.Teleport);
            }
            else
            {
                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, TextServer.gI().MAX_NUMCHARS, false, character.InfoChar.Gender));
            }
        }
        private static void HandlerGhepTrangBiThanLinh(Character character, int typeItem, int gender, int id = 0)
        {
            switch (typeItem)
            {
                case 0:
                    {
                        switch (gender)
                        {
                            case 0:
                                {
                                    id = 650;
                                    break;
                                }
                            case 1:
                                {
                                    id = 652;
                                    break;
                                }
                            case 2:
                                {
                                    id = 654;
                                    break;
                                }
                        }
                        break;
                    }
                case 1:
                    {
                        switch (gender)
                        {
                            case 0:
                                {
                                    id = 651;
                                    break;
                                }
                            case 1:
                                {
                                    id = 653;
                                    break;
                                }
                            case 2:
                                {
                                    id = 655;
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        switch (gender)
                        {
                            case 0:
                                {
                                    id = 657;
                                    break;
                                }
                            case 1:
                                {
                                    id = 659;
                                    break;
                                }
                            case 2:
                                {
                                    id = 661;
                                    break;
                                }
                        }
                        break;
                    }
                case 3:
                    {
                        switch (gender)
                        {
                            case 0:
                                {
                                    id = 658;
                                    break;
                                }
                            case 1:
                                {
                                    id = 660;
                                    break;
                                }
                            case 2:
                                {
                                    id = 662;
                                    break;
                                }
                        }
                        break;
                    }
                case 4:
                    {
                        id = 656;
                        break;
                    }
            }
            var item = ItemCache.GetItemDefault((short)id);
            character.CharacterHandler.AddItemToBag(false, item);
        }
        private static void HandlerGhepTrangBiHuyDiet(Character character, int typeItem, int gender, int type)
        {
            var listItem = new List<int>() { };
            var listOption = new List<int>() { };
            var listOption2 = new List<int>() { };
            switch (typeItem)
            {
                case 0: // ao
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 0, 3, 33, 34, 136, 137, 138, 139, 230, 231, 232, 233, 555, 650 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 1, 4, 41, 42, 152, 153, 154, 155, 234, 235, 236, 237, 557,652 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 2, 5, 49, 50, 168, 169, 170, 171, 238, 239, 240, 241, 559,654 };
                                    break;
                                }
                        }
                        break;
                    }
                case 1: // quan
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 6, 9, 35, 36, 140, 141, 142, 143, 242, 243, 244, 245, 556,651 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 7, 10, 43, 44, 156, 157, 158, 159, 246, 247, 248, 249, 558, 653 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 8, 11, 51, 52, 172, 173, 174, 175, 250, 251, 252, 253, 560, 655 };
                                    break;
                                }
                        }
                        break;
                    }
                case 2: // găng
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 21, 24, 37, 38, 144, 145, 146, 147, 254, 255, 256, 257, 562 , 657};
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 22, 25, 45, 46, 160, 161, 162, 163, 258, 259, 260, 261, 564, 659 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 23, 26, 53, 54, 176, 177, 178, 179, 262, 263, 264, 265, 566, 661 };
                                    break;
                                }
                        }
                        break;
                    }
                case 3: // giay
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 27, 30, 39, 40, 148, 149, 150, 151, 266, 267, 268, 269, 563, 658 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 28, 31, 47, 48, 164, 165, 166, 167, 270, 271, 272, 273, 565, 660 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 29, 32, 55, 56, 180, 181, 182, 183, 274, 275, 276, 277, 567, 662 };
                                    break;
                                }
                        }
                        break;
                    }
                case 4: // rada
                    {
                        listItem = new List<int> { 12, 57, 58, 59, 184, 185, 186, 187, 278, 279, 280, 281, 561, 656 };
                        break;
                    }

            }
            switch (gender)
            {
                case 0:
                    {
                        listOption = new List<int> { 127, 128, 129 };
                        listOption2 = new List<int> { 139, 140, 141 };
                        break;
                    }
                case 1:
                    {
                        listOption = new List<int> { 130, 131, 132 };
                        listOption2 = new List<int> { 142, 143, 144 };
                        break;
                    }
                case 2:
                    {
                        listOption = new List<int> { 130, 131, 132 };
                        listOption2 = new List<int> { 136, 137, 138 };
                        break;
                    }
            }
            var item = ItemCache.GetItemDefault(((short)listItem[ServerUtils.RandomNumber(listItem.Count)]));
            if (type == 0)//normal
            {
                item = ItemCache.GetItemDefault(((short)listItem[0]));
            }
            item.Options.Add(new OptionItem()
            {
                Id = listOption[ServerUtils.RandomNumber(listOption.Count)],
                Param = 0,
            });
            item.Options.Add(new OptionItem()
            {
                Id = listOption2[ServerUtils.RandomNumber(listOption2.Count)],
                Param = 0,
            });
            character.CharacterHandler.AddItemToBag(false, item);
        }
        private static void HandlerGhepTrangBiHuyDietForBody(Character character, int typeItem, int gender, int index)
        {
            var listItem = new List<int>();
            var listOption = new List<int>() { 127, 128, 129 };
            //  var listOption2 = new List<int>() { };
            switch (typeItem)
            {
                case 0: // ao
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 0, 3, 33, 34, 136, 137, 138, 139, 230, 231, 232, 233, 555, 650 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 1, 4, 41, 42, 152, 153, 154, 155, 234, 235, 236, 237, 557, 652 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 2, 5, 49, 50, 168, 169, 170, 171, 238, 239, 240, 241, 559, 654 };
                                    break;
                                }
                        }
                        break;
                    }
                case 1: // quan
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 6, 9, 35, 36, 140, 141, 142, 143, 242, 243, 244, 245, 556, 651 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 7, 10, 43, 44, 156, 157, 158, 159, 246, 247, 248, 249, 558, 653 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 8, 11, 51, 52, 172, 173, 174, 175, 250, 251, 252, 253, 560, 655 };
                                    break;
                                }
                        }
                        break;
                    }
                case 2: // găng
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 21, 24, 37, 38, 144, 145, 146, 147, 254, 255, 256, 257, 562, 657 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 22, 25, 45, 46, 160, 161, 162, 163, 258, 259, 260, 261, 564, 659 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 23, 26, 53, 54, 176, 177, 178, 179, 262, 263, 264, 265, 566, 661 };
                                    break;
                                }
                        }
                        break;
                    }
                case 3: // giay
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 27, 30, 39, 40, 148, 149, 150, 151, 266, 267, 268, 269, 563, 658 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 28, 31, 47, 48, 164, 165, 166, 167, 270, 271, 272, 273, 565, 660 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 29, 32, 55, 56, 180, 181, 182, 183, 274, 275, 276, 277, 567, 662 };
                                    break;
                                }
                        }
                        break;
                    }
                case 4: // rada
                    {
                        listItem = new List<int> { 12, 57, 58, 59, 184, 185, 186, 187, 278, 279, 280, 281, 561, 656 };
                        break;
                    }

            }
            switch (gender)
            {
                
                case 1:
                    {
                        listOption = new List<int>() { 131, 132, 213, 130 };
                   //     listOption2 = new List<int> { 142, 143, 144, 214 };
                        break;
                    }
                case 2:
                    {
                        listOption = new List<int>() { 133, 134, 135 };
                        //         listOption2 = new List<int>() { 133, 134, 135 };
                        break;
                    }
            }
            if (listItem.Count == 0)
            {
                character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Vui lòng thử lại !"));
                return;
            }
            var item = ItemCache.GetItemDefault(((short)listItem[0]));
            var rand = ServerUtils.RandomNumber(listOption.Count);
            item.Options.Add(new OptionItem()
            {
                Id = listOption[rand],
                Param = 0,
            });
            item.Options.Add(new OptionItem()
            {
                Id = GetSKHDescOption(listOption[rand]),
                Param = 0,
            });
            item.Options.Add(new OptionItem()
            {
                Id = 30,
                Param = 0,
            });
            character.CharacterHandler.AddItemToBody(item, index);
        }
        public static int GetSKHDescOption(int skhId)
        {
            switch (skhId)
            {
                case 127: return 139;
                case 128: return 140;
                case 129: return 141;
                case 130: return 142;
                case 131: return 143;
                case 132: return 144;
                case 133: return 136;
                case 134: return 137;
                case 135: return 138;
                case 213: return 214;
            }
            return 73;
        }
        private static void HandlerGhepTrangBiHuyDietForBody(Disciple character, int typeItem, int gender, int index)
        {
            var listItem = new List<int>() { };
            var listOption = new List<int>() { 127, 128, 129 };
            switch (typeItem)
            {
                case 0: // ao
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 0, 3, 33, 34, 136, 137, 138, 139, 230, 231, 232, 233, 555, 650 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 1, 4, 41, 42, 152, 153, 154, 155, 234, 235, 236, 237, 557, 652 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 2, 5, 49, 50, 168, 169, 170, 171, 238, 239, 240, 241, 559, 654 };
                                    break;
                                }
                        }
                        break;
                    }
                case 1: // quan
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 6, 9, 35, 36, 140, 141, 142, 143, 242, 243, 244, 245, 556, 651 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 7, 10, 43, 44, 156, 157, 158, 159, 246, 247, 248, 249, 558, 653 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 8, 11, 51, 52, 172, 173, 174, 175, 250, 251, 252, 253, 560, 655 };
                                    break;
                                }
                        }
                        break;
                    }
                case 2: // găng
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 21, 24, 37, 38, 144, 145, 146, 147, 254, 255, 256, 257, 562, 657 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 22, 25, 45, 46, 160, 161, 162, 163, 258, 259, 260, 261, 564, 659 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 23, 26, 53, 54, 176, 177, 178, 179, 262, 263, 264, 265, 566, 661 };
                                    break;
                                }
                        }
                        break;
                    }
                case 3: // giay
                    {
                        switch (gender)
                        {
                            case 0: // trai dat
                                {
                                    listItem = new List<int> { 27, 30, 39, 40, 148, 149, 150, 151, 266, 267, 268, 269, 563, 658 };
                                    break;
                                }
                            case 1: // namec
                                {
                                    listItem = new List<int> { 28, 31, 47, 48, 164, 165, 166, 167, 270, 271, 272, 273, 565, 660 };
                                    break;
                                }
                            case 2: // xayda
                                {
                                    listItem = new List<int> { 29, 32, 55, 56, 180, 181, 182, 183, 274, 275, 276, 277, 567, 662 };
                                    break;
                                }
                        }
                        break;
                    }
                case 4: // rada
                    {
                        listItem = new List<int> { 12, 57, 58, 59, 184, 185, 186, 187, 278, 279, 280, 281, 561, 656 };
                        break;
                    }

            }
            switch (gender)
            {

                case 1:
                    {
                        listOption = new List<int>() { 131, 132, 213, 130 };
                        //     listOption2 = new List<int> { 142, 143, 144, 214 };
                        break;
                    }
                case 2:
                    {
                        listOption =  new List<int>() { 133, 134, 135 };
                        //         listOption2 = new List<int>() { 133, 134, 135 };
                        break;
                    }
            }
            var item = ItemCache.GetItemDefault(((short)listItem[0]));
            var rand = ServerUtils.RandomNumber(listOption.Count);
            item.Options.Add(new OptionItem()
            {
                Id = listOption[rand],
                Param = 0,
            });
            item.Options.Add(new OptionItem()
            {
                Id = GetSKHDescOption(listOption[rand]),
                Param = 0,
            });
            item.Options.Add(new OptionItem()
            {
                Id = 30,
                Param = 0,
            });
            character.CharacterHandler.AddItemToBody(item, index);
        }
        private static void  ConfirmQuyLao (Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 27:
                    switch (select)
                    {
                        case 0:
                            var oldDisciple = character.Disciple;
                            oldDisciple = new Disciple();
                            oldDisciple.CreatePet(character, 3, character.InfoChar.Gender);
                            oldDisciple.Player = character.Player;
                            oldDisciple.CharacterHandler.SetUpInfo();
                            character.Disciple = oldDisciple;
                            DiscipleDB.Update(oldDisciple);
                            break;
                    }
                    break;
                //Open menu 1
                case 0:
                    {
                        switch (select)
                        {
                            //Nói chuyện
                            case 0: {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[1], MenuNpc.Gi().MenuQuyLao[ClanManager.Get(character.ClanId) != null ? 5:1], character.InfoChar.Gender));
                                    character.TypeMenu = 1;
                                    break;
                                }
                            //Kho báo dưới biển
                            case 1:
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Đây là bản đồ kho báu hải tặc tí hon\nCác con cứ yên tâm lên đường\nỞ đây có ta lo\nNhớ chọn cấp độ vừa sức mình nhé", new List<string> { "Top\nBang hội", "Thành tích\nBang", ClanManager.Get(character.ClanId) != null && ClanManager.Get(character.ClanId).bdkb.Open ? "Tham gia" : "Chọn\ncấp độ", "Từ chối" }, character.InfoChar.Gender));
                                    character.TypeMenu = 7;
                                    break;
                                }
                            case 2:
                                {
                                    if (TaskHandler.CheckTask(character, 20, 1))
                                    {
                                        TaskHandler.gI().PlusSubTask(character, 1);
                                    }
                                //    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, string.Format(MenuNpc.Gi().TextQuyLao[4], character.DiemSuKien), MenuNpc.Gi().MenuQuyLao[3], character.InfoChar.Gender));
                                //    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, string.Format(MenuNpc.gI().TextQuyLao[4], character.DiemSuKien), MenuNpc.gI()).MenuQuyLao[3], character.InfoChar.Gender));
                                //    character.TypeMenu = 5;
                                    break;
                                }
                            case 3:
                               // if (ConfigManager.gI().SuKienWorldCup)
                                //{
                                //    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[5], MenuNpc.Gi().MenuQuyLao[4], character.InfoChar.Gender));
                                //}else if (ConfigManager.gI().SuKienNoel)
                                //{
                                //    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "|0|Hãy thu thập đủ x99 các loại vật phẩm sự kiện Noel\n|6|Để đổi lấy những phần quà hấp dẫn", new List<String> { "Đổi", "Từ chối" }, character.InfoChar.Gender));
                                //}
                                //else
                                //{
                                    if (TaskHandler.CheckTask(character, 19, 1))
                                    {
                                        TaskHandler.gI().PlusSubTask(character, 1);
                                    }
                                //}

                                character.TypeMenu = 6;
                                break;
                        }
                        break;
                    }
                
                //Open menu Nói chuyện
                case 7:
                    switch (select)
                    {
                        case 0:
                           
                            break;
                        case 1:
                            if (character.ClanId == -1)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Hãy vào bang hội trước"));
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chức năng đang được cập nhật"));
                            }
                            break;
                        
                        case 2:
                            if (character.ClanId == -1)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Hãy vào bang hội trước"));
                            }else if (character.CharacterHandler.GetItemBagById(611) == null)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Yêu cầu bản đồ kho báu"));

                                return;
                            }
                            else
                            {
                                var clan = ClanManager.Get(character.ClanId);
                                if (clan.bdkb.Open)
                                {
                                    var time = (clan.bdkb.timeBDKB - ServerUtils.CurrentTimeMillis()) / 60000;
                                    if (time <= 1)
                                    {
                                        time = (clan.bdkb.timeBDKB - ServerUtils.CurrentTimeMillis()) / 1000;
                                    }
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Bang hội của ngươi đã mở Bản Đồ Kho Báu Level: " + clan.bdkb.Level + "\nCòn " + time + (time <= 1 ? " giây nữa" : "' nũa"), new List<string> {"Tham gia\n(Miễn phí)","Hủy" }, character.InfoChar.Gender));
                                    character.TypeMenu = 9;
                                }
                                else
                                {
                                    var inputBDKB = new List<InputBox>();
                                    var inputLevel = new InputBox()
                                    {
                                        Name = "(Nhập cấp độ từ 0 -> 110)",
                                        Type = 1,
                                    };
                                    inputBDKB.Add(inputLevel);
                                    character.CharacterHandler.SendMessage(Service.ShowInput("Nhập cấp độ Bản Đồ Kho Báu", inputBDKB));
                                    character.TypeInput = 14;
                                }
                            }
                            break;

                    }
                    break;
                case 9:
                    {
                        switch (select)
                        {
                            case 0:
                                var clan = ClanManager.Get(character.ClanId);
                                MapManager.OutMap(character, clan.bdkb.MapBDKB[0].Id);
                                character.InfoChar.X = 78;
                                character.InfoChar.Y = 336;
                                clan.bdkb.MapBDKB[0].JoinZone(character, 0, false, false);
                                break;
                        }
                    }
                    break;
                case 1:
                    {
                        switch (select)
                        {
                            case 0:
                                {

                                    var task = Cache.Gi().TASK_TEMPLATES_0.Values.FirstOrDefault(i => i.Id == character.InfoTask.Id).SubNames[character.InfoTask.Index];
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(5, task));
                                    break;
                                }
                            case 1:
                                {
                                    if (character.InfoChar.LearnSkill != null)
                                    {
                                        var itemAdd = character.InfoChar.LearnSkill.ItemSkill;
                                        var time = character.InfoChar.LearnSkill.Time - ServerUtils.CurrentTimeMillis();
                                        var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skill => skill.Id == character.InfoChar.LearnSkill.ItemTemplateSkillId);
                                        var itemTempalte = ItemCache.ItemTemplate(itemAdd.Id);
                                        var ngoc = 5;
                                        if (time / 600000 >= 2)
                                        {
                                            ngoc += (int)time / 600000;
                                        }

                                        var menu = string.Format(TextServer.gI().ADDING_SKILL, skillTemplate.Name,
                                            itemTempalte.Level, ServerUtils.GetTime(time));
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, menu, new List<string>() { $"Học\nCấp tốc\n{ngoc} ngọc", "Huỷ", "Bỏ qua" }, character.InfoChar.Gender));
                                        character.TypeMenu = 3;
                                    }
                                    else
                                    {
                                        var idShop = 7 + character.InfoChar.Gender;
                                        character.CharacterHandler.SendMessage(Service.Shop(character, 1, idShop));
                                        character.ShopId = idShop;
                                        character.TypeShop = 0;
                                    }
                                    break;
                                }
                            case 2:
                                if (character.ClanId == -1)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Hãy vào bang hội trước"));
                                }
                                else
                                {
                                    var clan = ClanManager.Get(character.ClanId);
                                    if (clan.Thành_viên.FirstOrDefault(i=> i.Id == character.Id).Role != 0)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Ngươi không phải là Bang Chủ !"));
                                    }
                                    else
                                    {
                                        ClanManager.Remove(ClanManager.Get(character.ClanId));
                                    }
                                }
                                break;
                            case 3:
                                {
                                    var clan = ClanManager.Get(character.ClanId);
                                    if (clan != null)
                                    {
                                        MapManager.OutMap(character, clan.ClanZone.Maps[0].Id);
                                        clan.ClanZone.Maps[0].JoinZone(character, 0);
                                    }

                                    break;
                                }
                        }
                        break;
                    }
                //Học skill
                case 2:
                    {
                        switch (select)
                        {
                            //Đồng ý
                            case 0:
                                {
                                    if (character.InfoChar.LearnSkillTemp == null) return;
                                    var itemAdd = character.InfoChar.LearnSkillTemp.ItemSkill;
                                    var time = character.InfoChar.LearnSkillTemp.Time + ServerUtils.CurrentTimeMillis();
                                    var idSkill = character.InfoChar.LearnSkillTemp.ItemTemplateSkillId;
                                    character.InfoChar.Potential -= itemAdd.BuyPotential;
                                    character.InfoChar.LearnSkill = new LearnSkill()
                                    {
                                        ItemSkill = itemAdd,
                                        Time = time,
                                        ItemTemplateSkillId = idSkill,
                                        Potential = (int)itemAdd.BuyPotential
                                    };
                                    character.InfoChar.LearnSkillTemp = null;
                                    character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                                    character.CharacterHandler.SendMessage(Service.ClosePanel());
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Con đã học thành công, hãy cố gắng chờ đợi nha"));
                                    break;
                                }
                            //Từ chối
                            case 1:
                                {
                                    character.InfoChar.LearnSkillTemp = null;
                                    break;
                                }
                        }
                        break;
                    }
                //Open menu with learn skill
                case 3:
                    {
                        switch (select)
                        {
                            //Đồng ý học nhanh
                            case 0:
                                {
                                    if (character.InfoChar.LearnSkill == null) return;
                                    var itemAdd = character.InfoChar.LearnSkill.ItemSkill;
                                    var time = character.InfoChar.LearnSkill.Time - ServerUtils.CurrentTimeMillis();
                                    var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skill => skill.Id == character.InfoChar.LearnSkill.ItemTemplateSkillId);
                                    if (skillTemplate == null) return;
                                    var ngoc = 5;
                                    if (time / 600000 >= 2)
                                    {
                                        ngoc += (int)time / 600000;
                                    }
                                    if (character.AllDiamond() < ngoc) {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                                        return;
                                    }
                                    character.MineDiamond(ngoc);
                                    character.InfoChar.LearnSkill = null;
                                    character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                    ItemHandler.AddLearnSkill(character, itemAdd, skillTemplate);
                                    break;
                                }
                            //Huỷ học skill
                            case 1:
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[3], MenuNpc.Gi().MenuMeo[1], character.InfoChar.Gender));
                                    character.TypeMenu = 4;
                                    break;
                                }
                            //Open menu 1
                            case 2:
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[0], MenuNpc.Gi().MenuQuyLao[0], character.InfoChar.Gender));
                                    character.TypeMenu = 0;
                                    break;
                                }
                        }
                        break;
                    }
                //Huỷ học skill
                case 4:
                    {
                        if (select != 0) return;
                        var plusPoint = character.InfoChar.LearnSkill.Potential / 2;
                        character.CharacterHandler.PlusTiemNang(0, plusPoint, false);
                        character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().CANCEL_LEARN_SKILL));
                        character.InfoChar.LearnSkill = null;
                        character.InfoChar.LearnSkillTemp = null;
                        break;
                    }
                case 5:
                    {
                        switch (select) {
                            case 0:
                                {
                                    if (character.DiemSuKien >= 50) {
                                        var randomRate = ServerUtils.RandomNumber(0.0, 100.0);
                                        var itemAdd2 = ItemCache.GetItemDefault(1);

                                        if (randomRate <= 20.0)
                                        {
                                            itemAdd2 = ItemCache.GetItemDefault(1087);
                                        } else if (randomRate <= 40.0)
                                        {
                                            itemAdd2 = ItemCache.GetItemDefault(1088);
                                        } else if (randomRate <= 60.0)
                                        {
                                            itemAdd2 = itemAdd2 = ItemCache.GetItemDefault(1089);
                                        } else if (randomRate <= 80.0) {
                                            itemAdd2 = itemAdd2 = ItemCache.GetItemDefault(1090);
                                        } else
                                        {
                                            itemAdd2 = itemAdd2 = ItemCache.GetItemDefault(1091);
                                        }
                                        itemAdd2.Reason = "Quà Sự Kiện";
                                        itemAdd2.Options.Add(new OptionItem()
                                        {
                                            Id = 30,
                                            Param = 0,
                                        });
                                        itemAdd2.Quantity = 1;

                                        character.CharacterHandler.AddItemToBag(true, itemAdd2, "SuKien");
                                        character.DiemSuKien -= 50;
                                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                                        var template2 = ItemCache.ItemTemplate(itemAdd2.Id);
                                        character.CharacterHandler.SendMessage(
                                        Service.ServerMessage(string.Format(TextServer.gI().ADD_ITEM,
                                         $"x{itemAdd2.Quantity} {template2.Name}")));
                                        break;
                                    } else
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn còn thiếu " + (50 - character.DiemSuKien) + " Điểm Sự Kiện nữa"));
                                        break;
                                    }
                                }
                            case 1:
                                break;
                        }
                        break;
                    }
                case 6:
                    if (ConfigManager.gI().SuKienWorldCup)
                    {
                        switch (select)
                        {



                            case 0:
                                for (int dball = 1129; dball <= 1138; dball++)
                                {
                                    if (character.CharacterHandler.GetItemBagById(dball) == null || character.CharacterHandler.GetItemBagById(dball).Quantity < 10)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_GENDER));
                                        return;
                                    }
                                }
                                for (short dball = 1129; dball <= 1138; dball++)
                                {
                                    character.CharacterHandler.RemoveItemBagById(dball, 10, reason: "Menu 0 World CUP");
                                }
                                int gold = 200000000;
                                if (character.InfoChar.Gold < gold)
                                {

                                    character.CharacterHandler.SendMeMessage(Service.ServerMessage("Không đủ vàng"));
                                    return;
                                }
                                else
                                {

                                    character.MineGold(gold);
                                    var itemAdd2 = ItemCache.GetItemDefault(1144);
                                    itemAdd2.Options.Add(new OptionItem()
                                    {
                                        Id = 30,
                                        Param = 0,
                                    });
                                    itemAdd2.Quantity = 1;
                                    //  character.MineDiamond(diamond);
                                    character.CharacterHandler.AddItemToBag(true, itemAdd2, "World cup");
                                    character.CharacterHandler.SendMeMessage(Service.SendBag(character));
                                }
                                break;
                            case 1:
                                for (int dball = 1129; dball <= 1138; dball++)
                                {
                                    if (character.CharacterHandler.GetItemBagById(dball) == null || character.CharacterHandler.GetItemBagById(dball).Quantity < 10)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_GENDER));
                                        return;
                                    }
                                }
                                for (short dball = 1129; dball <= 1138; dball++)
                                {
                                    character.CharacterHandler.RemoveItemBagById(dball, 10, reason: "Menu 0 World CUP");
                                }
                                int diamond = 1000;
                                if (character.InfoChar.Diamond < diamond)
                                {

                                    character.CharacterHandler.SendMeMessage(Service.ServerMessage("Không đủ ngọc"));
                                    return;
                                }
                                else
                                {
                                    var itemAdd = ItemCache.GetItemDefault(1143);
                                    itemAdd.Options.Add(new OptionItem()
                                    {
                                        Id = 30,
                                        Param = 0,
                                    });
                                    itemAdd.Quantity = 1;
                                    character.MineDiamond(diamond);
                                    character.CharacterHandler.AddItemToBag(true, itemAdd, "World cup");
                                    character.CharacterHandler.SendMeMessage(Service.SendBag(character));
                                }

                                break;
                        }
                    } else if (ConfigManager.gI().SuKienNoel)
                    {
                        switch (select) {
                            case 0:
                        for (int dball = 1181; dball <= 1185; dball++)
                        {
                            if (character.CharacterHandler.GetItemBagById(dball) == null || character.CharacterHandler.GetItemBagById(dball).Quantity < 99)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ nguyên liệu"));
                                return;
                            }
                            else
                            {
                                character.CharacterHandler.RemoveItemBagById((short)dball, 99);
                            }
                        }

                                var tui7chulun = ItemCache.GetItemDefault((short)1187);
                                tui7chulun.Quantity = 1;
                                character.CharacterHandler.AddItemToBag(true, tui7chulun);
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng cu đã nhận được Túi 7 Thằng Lùn"));
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                break;
                    }

                    }
        
                    break;

        }
        }
        private static void ConfirmKyGUI(Character character, short npcId, int select){
            switch(select){
                case 1:
                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Chức năng tạm thời bảo trì đến khi Open"));
                    character.TypeMenu = 0;
                   // character.CharacterHandler.SendMessage(KyGUIService.OpenShopKiGui(character));
                break;
            }
        }
        private static void ConfirmTruongLaoGuru(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                //Open menu Nói chuyện
                case 0:
                {
                    switch (select)
                    {
                        case 0:
                        {
                                    
                                    break;
                        }
                        case 1:
                        {
                            if (character.InfoChar.LearnSkill != null)
                            {
                                var itemAdd = character.InfoChar.LearnSkill.ItemSkill;
                                var time = character.InfoChar.LearnSkill.Time - ServerUtils.CurrentTimeMillis();
                                var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skill => skill.Id == character.InfoChar.LearnSkill.ItemTemplateSkillId);
                                var itemTempalte = ItemCache.ItemTemplate(itemAdd.Id);
                                var ngoc = 5;
                                if (time / 600000 >= 2)
                                {
                                    ngoc += (int)time / 600000;
                                }

                                var menu = string.Format(TextServer.gI().ADDING_SKILL, skillTemplate.Name,
                                    itemTempalte.Level, ServerUtils.GetTime(time));
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, menu, new List<string>() {$"Học\nCấp tốc\n{ngoc} ngọc", "Huỷ","Bỏ qua"}, character.InfoChar.Gender));
                                character.TypeMenu = 2;
                            }
                            else
                            {
                                var idShop = 10;
                                character.CharacterHandler.SendMessage(Service.Shop(character, 1, idShop));
                                character.ShopId = idShop;
                                character.TypeShop = 1;
                            }
                            break;
                        }
                    }
                    break;
                }
                //Học skill
                case 1:
                {
                    switch (select)
                    {
                        //Đồng ý
                        case 0:
                        {
                            if(character.InfoChar.LearnSkillTemp == null) return;
                            var itemAdd = character.InfoChar.LearnSkillTemp.ItemSkill;
                            var time = character.InfoChar.LearnSkillTemp.Time + ServerUtils.CurrentTimeMillis();
                            var idSkill = character.InfoChar.LearnSkillTemp.ItemTemplateSkillId;
                            character.InfoChar.Potential -= itemAdd.BuyPotential;
                            character.InfoChar.LearnSkill = new LearnSkill()
                            {
                                ItemSkill = itemAdd,
                                Time = time,
                                ItemTemplateSkillId = idSkill,
                                Potential = (int)itemAdd.BuyPotential
                            };
                            character.InfoChar.LearnSkillTemp = null;
                            character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                            character.CharacterHandler.SendMessage(Service.ClosePanel());
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Con đã học thành công, hãy cố gắng chờ đợi nha"));
                            break;
                        }
                        //Từ chối
                        case 1:
                        {
                            character.InfoChar.LearnSkillTemp = null;
                            break;
                        }
                    }
                    break;
                }
                //Open menu with learn skill
                case 2:
                {
                    switch (select)
                    {
                        //Đồng ý học nhanh
                        case 0:
                        {
                            if(character.InfoChar.LearnSkill == null) return;
                            var itemAdd = character.InfoChar.LearnSkill.ItemSkill;
                            var time = character.InfoChar.LearnSkill.Time - ServerUtils.CurrentTimeMillis();
                            var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skill => skill.Id == character.InfoChar.LearnSkill.ItemTemplateSkillId);
                            if (skillTemplate == null) return;
                            var ngoc = 5;
                            if (time / 600000 >= 2)
                            {
                                ngoc += (int)time / 600000;
                            }
                            if(character.AllDiamond() < ngoc) {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                                return;
                            }
                            character.MineDiamond(ngoc);
                            character.InfoChar.LearnSkill = null;
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                            ItemHandler.AddLearnSkill(character, itemAdd, skillTemplate);
                            break;
                        }
                        //Huỷ học skill
                        case 1:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[3], MenuNpc.Gi().MenuMeo[1], character.InfoChar.Gender));
                            character.TypeMenu = 3;
                            break;
                        }
                        //Open menu 1
                        case 2:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[1], MenuNpc.Gi().MenuQuyLao[1], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    }
                    break;
                }
                //Huỷ học skill
                case 3:
                {
                    if(select != 0) return;
                    var plusPoint = character.InfoChar.LearnSkill.Potential / 2;
                    character.CharacterHandler.PlusTiemNang(0, plusPoint, false);
                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().CANCEL_LEARN_SKILL));
                    character.InfoChar.LearnSkill = null;   
                    character.InfoChar.LearnSkillTemp = null;   
                    break;
                }
            }
        }

        private static void ConfirmVuaVegeta(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                //Open menu Nói chuyện
                case 0:
                {
                    switch (select)
                    {
                        case 0:
                        {

                                    break;
                        }
                        case 1:
                        {
                            if (character.InfoChar.LearnSkill != null)
                            {
                                var itemAdd = character.InfoChar.LearnSkill.ItemSkill;
                                var time = character.InfoChar.LearnSkill.Time - ServerUtils.CurrentTimeMillis();
                                var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skill => skill.Id == character.InfoChar.LearnSkill.ItemTemplateSkillId);
                                var itemTempalte = ItemCache.ItemTemplate(itemAdd.Id);
                                var ngoc = 5;
                                if (time / 600000 >= 2)
                                {
                                    ngoc += (int)time / 600000;
                                }

                                var menu = string.Format(TextServer.gI().ADDING_SKILL, skillTemplate.Name,
                                    itemTempalte.Level, ServerUtils.GetTime(time));
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, menu, new List<string>() {$"Học\nCấp tốc\n{ngoc} ngọc", "Huỷ","Bỏ qua"}, character.InfoChar.Gender));
                                character.TypeMenu = 2;
                            }
                            else
                            {
                                var idShop = 11;
                                character.CharacterHandler.SendMessage(Service.Shop(character, 1, idShop));
                                character.ShopId = idShop;
                                character.TypeShop = 2;
                            }
                            break;
                        }
                    }
                    break;
                }
                //Học skill
                case 1:
                {
                    switch (select)
                    {
                        //Đồng ý
                        case 0:
                        {
                            if(character.InfoChar.LearnSkillTemp == null) return;
                            var itemAdd = character.InfoChar.LearnSkillTemp.ItemSkill;
                            var time = character.InfoChar.LearnSkillTemp.Time + ServerUtils.CurrentTimeMillis();
                            var idSkill = character.InfoChar.LearnSkillTemp.ItemTemplateSkillId;
                            character.InfoChar.Potential -= itemAdd.BuyPotential;
                            character.InfoChar.LearnSkill = new LearnSkill()
                            {
                                ItemSkill = itemAdd,
                                Time = time,
                                ItemTemplateSkillId = idSkill,
                                Potential = (int)itemAdd.BuyPotential
                            };
                            character.InfoChar.LearnSkillTemp = null;
                            character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                            character.CharacterHandler.SendMessage(Service.ClosePanel());
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Con đã học thành công, hãy cố gắng chờ đợi nha"));
                            break;
                        }
                        //Từ chối
                        case 1:
                        {
                            character.InfoChar.LearnSkillTemp = null;
                            break;
                        }
                    }
                    break;
                }
                //Open menu with learn skill
                case 2:
                {
                    switch (select)
                    {
                        //Đồng ý học nhanh
                        case 0:
                        {
                            if(character.InfoChar.LearnSkill == null) return;
                            var itemAdd = character.InfoChar.LearnSkill.ItemSkill;
                            var time = character.InfoChar.LearnSkill.Time - ServerUtils.CurrentTimeMillis();
                            var skillTemplate = Cache.Gi().SKILL_TEMPLATES.FirstOrDefault(skill => skill.Id == character.InfoChar.LearnSkill.ItemTemplateSkillId);
                            if (skillTemplate == null) return;
                            var ngoc = 5;
                            if (time / 600000 >= 2)
                            {
                                ngoc += (int)time / 600000;
                            }
                            if(character.AllDiamond() < ngoc) {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                                return;
                            }
                            character.MineDiamond(ngoc);
                            character.InfoChar.LearnSkill = null;
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                            ItemHandler.AddLearnSkill(character, itemAdd, skillTemplate);
                            break;
                        }
                        //Huỷ học skill
                        case 1:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[3], MenuNpc.Gi().MenuMeo[1], character.InfoChar.Gender));
                            character.TypeMenu = 3;
                            break;
                        }
                        //Open menu 1
                        case 2:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuyLao[1], MenuNpc.Gi().MenuQuyLao[1], character.InfoChar.Gender));
                            character.TypeMenu = 0;
                            break;
                        }
                    }
                    break;
                }
                //Huỷ học skill
                case 3:
                {
                    if(select != 0) return;
                    var plusPoint = character.InfoChar.LearnSkill.Potential / 2;
                    character.CharacterHandler.PlusTiemNang(0, plusPoint, false);
                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().CANCEL_LEARN_SKILL));
                    character.InfoChar.LearnSkill = null;   
                    character.InfoChar.LearnSkillTemp = null;   
                    break;
                }
            }
        }

        private static void ConfirmThanMeo(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                //Menu ban đầu
                case 5:
                switch(select){
                    case 0:
                    var huongdanthem = "Tập luyện vẫn tiếp tục và sức mạnh vẫn tăng khi đã Offline\n"
+ "Hiệu quả tập luyện như sau:\nThần Mèo: 20 sức mạnh mỗi phút\nYajirô: 40 sức mạnh mỗi phút\nMr.PôPô: 80 sức mạnh mỗi phút\nThượng đế: 160 sức mạnh mỗi phút"
+ "Khỉ Bubbles: 320 sức mạnh mỗi phút\nThần Vũ Trụ: 640 sức mạnh mỗi phút\nTổ sư Kaio: 1280 sức mạnh mỗi phút\n"
+ "Có thể tặng ngọc để thắng mà không cần thách đấu\n"
+ "Nếu đăng ký tập thường xuyên mỗi khi Offline không cần phải đến đây vẫn tập luyện được";
character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, huongdanthem, true));
break;
                       case 1:
                       character.DataTraining.isTraining = true;
                       character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Từ giờ, quá 30p Offline con sẽ được tự động luyện tập !"));
                    break;
                }
                break;
            case 6:
            switch(select){
                case 0:
                break;
            }
            break;
            case 7:
            switch(select){
                case 0:
                ThachDau.gI().Start(character, character.DataTraining.Level);
                break;
            }
            break;
                case 0:
                {
                        switch (select)
                        {
                            case 0:
                                {
                                    if (character.DataTraining.isTraining)
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Con đã hủy thành công đăng ký tập tự động !\ntừ giờ còn muốn tập Offline hãy tự đến đây trước"));
                                            character.DataTraining.isTraining = false;
                                        }
                                        else
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, $"Đăng ký để mỗi khi Offline quá 30 phút, con sẽ được tự động luyện tập với tốc độ\n{DataTraining.GetPotenial(character)} sức mạnh mỗi phút", new List<string> { "Hướng\ndẫn\nthêm", "Đồng ý\n1 ngọc\nmỗi lần", "Không\nĐồng ý" }, character.InfoChar.Gender));
                                            character.TypeMenu = 5;
                                        }
                                    
                                    break;
                                }
                            case 1: // nhiem vu
                                if (TaskHandler.CheckTask(character, 30, 0))
                                {
                                    TaskHandler.gI().PlusSubTask(character, 1);
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Mau tập luyện để đánh bại Tàu Pảy Pảy", true));
                                }
                                break;                       
                            case 2://tap luyen voi than meo
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, $"Con có chắc muốn tập luyện?"
                            +$"\nTập luyện với ta sẽ tăng {DataTraining.GetPotenial(0)} sức mạnh mỗi phút", new List<string>{"Đồng ý\nluyện tập", "Không\nđồng ý"}, character.InfoChar.Gender));
                            character.TypeMenu = 6;
                            break;
                            case 3://thach dau than meo
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, $"Con có chắc muốn thách đấu?"
                            +$"\nNếu thắng ta sẽ đượct tập với Yajiro, tăng {DataTraining.GetPotenial(1)} sức mạnh mỗi phút", new List<string>{"Đồng ý\ngiao đấu", "Không\nđồng ý"}, character.InfoChar.Gender));
                            character.TypeMenu = 7;
                            break;     
                        }
                    break;
                }
                case 1:
                {
                        switch (select)
                        {
                            case 0:
                                character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Tập luyện vẫn tiếp tục và sức mạnh vẫn tăng khi đã Offline"));
                                break;
                            case 1:
                                {
                                    //if (!character.DataTraining.DataTraining.isTraining)
                                    //{
                                    //    if (character.AllDiamondLock() < 1)
                                    //    {
                                    //        character.CharacterHandler.SendMessage(Service.ServerMessage("Yêu cầu có 1 hồng ngọc !"));
                                    //        return;
                                    //    }
                                    //    else
                                    //    {
                                    //        character.DataTraining.DataTraining.isTraining = true;
                                    //        character.DataTraining.DataTraining.Potetinal = 80;
                                    //        character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Từ giờ, quá 30p Offline con sẽ được tự động luyện tập !"));
                                    //    }
                                    //}

                                    break;
                                }
                        }
                        break;
                }
                case 4://tự chọn lồng đèn
                {
                    switch (select)
                    {
                        case 0:
                                {
                                    if (character.ConDuongRanDoc.isCDRD)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Hãy mau bay xuống chân tháp Karin"));
                                        character.ConDuongRanDoc.TalkWithKarinCat = true;
                                ItemCache.GetItem(character, 63);
                                    }
                                    else
                                    {
                                        var ldKeoQuan = ItemCache.GetItemDefault((short)469);
                                        ldKeoQuan.Quantity = 1;
                                        character.CharacterHandler.AddItemToBag(true, ldKeoQuan, "Đổi điểm sự kiện tt tren 1k");
                                    }
                                    break;
                                }
                        case 1:
                        {
                            var ldOngSao = ItemCache.GetItemDefault((short)467);
                            ldOngSao.Quantity = 1;
                            character.CharacterHandler.AddItemToBag(true, ldOngSao, "Đổi điểm sự kiện tt tren 1k");
                            break;
                        }
                        case 2:
                        {
                            var ldCaChep = ItemCache.GetItemDefault((short)468);
                            ldCaChep.Quantity = 1;
                            character.CharacterHandler.AddItemToBag(true, ldCaChep, "Đổi điểm sự kiện tt tren 1k");
                            break;
                        }
                        case 3:
                        {
                            var ldConGa = ItemCache.GetItemDefault((short)802);
                            ldConGa.Quantity = 1;
                            character.CharacterHandler.AddItemToBag(true, ldConGa, "Đổi điểm sự kiện tt tren 1k");
                            break;
                        }
                        case 4:
                        {
                            var ldHoiAn = ItemCache.GetItemDefault((short)471);
                            ldHoiAn.Quantity = 1;
                            character.CharacterHandler.AddItemToBag(true, ldHoiAn, "Đổi điểm sự kiện tt tren 1k");
                            break;
                        }
                        default:
                        {
                            var ldKeoQuan = ItemCache.GetItemDefault((short)469);
                            ldKeoQuan.Quantity = 1;
                            character.CharacterHandler.AddItemToBag(true, ldKeoQuan, "Đổi điểm sự kiện tt tren 1k");
                            break;
                        }
                    }
                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                    break;
                }
            }
        }

        private static void ConfirmThuongDe(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 5:
                    switch (select)
                    {
                        case 0:
                            var huongdanthem = "Tập luyện vẫn tiếp tục và sức mạnh vẫn tăng khi đã Offline\n"
        + "Hiệu quả tập luyện như sau:\nThần Mèo: 20 sức mạnh mỗi phút\nYajirô: 40 sức mạnh mỗi phút\nMr.PôPô: 80 sức mạnh mỗi phút\nThượng đế: 160 sức mạnh mỗi phút"
        + "Khỉ Bubbles: 320 sức mạnh mỗi phút\nThần Vũ Trụ: 640 sức mạnh mỗi phút\nTổ sư Kaio: 1280 sức mạnh mỗi phút\n"
        + "Có thể tặng ngọc để thắng mà không cần thách đấu\n"
        + "Nếu đăng ký tập thường xuyên mỗi khi Offline không cần phải đến đây vẫn tập luyện được";
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, huongdanthem, true));
                            break;
                        case 1:
                            character.DataTraining.isTraining = true;
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Từ giờ, quá 30p Offline con sẽ được tự động luyện tập !"));
                            break;
                    }
                    break;
                //Menu ban đầu
                case 0:
                {
                    switch (select)
                    {
                            case 1:// tap luyen voi mr popo
                            case 2:// tap luyen voi thuong de
                                break;
                            case 0:
                                switch (character.InfoChar.MapId)
                                {
                                    case 141:

                                        character.MapPrivate.Maps[0].JoinZone(character, 0);
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Hãy xuống gặp thần mèo Karin"));
                                        character.ConDuongRanDoc.isCDRD = true;
                                        break;
                                    default:
                                        if (character.DataTraining.isTraining)
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Con đã hủy thành công đăng ký tập tự động !\ntừ giờ còn muốn tập Offline hãy tự đến đây trước"));
                                            character.DataTraining.isTraining = false;
                                        }
                                        else
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, $"Đăng ký để mỗi khi Offline quá 30 phút, con sẽ được tự động luyện tập với tốc độ\n{DataTraining.GetPotenial(character)} sức mạnh mỗi phút", new List<string> { "Hướng\ndẫn\nthêm", "Đồng ý\n1 ngọc\nmỗi lần", "Không\nĐồng ý" }, character.InfoChar.Gender));
                                            character.TypeMenu = 5;
                                        }
                                        break;
                                }
                                break;
                        //Quay ngọc may mắn
                        case 4:
                                {
                                    var menu = MenuNpc.Gi().MenuThuongDe[2].ToList();
                                    if (character.LuckyBox.Count > 0)
                                    {
                                        menu.Add($"Rương phụ\n{character.LuckyBox.Count}\nmón");
                                        menu.Add($"Đóng");
                                    }
                                    else
                                    {
                                        menu.Add($"Đóng");
                                    }
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextThuongDe[2], menu, character.InfoChar.Gender));
                                    character.TypeMenu = 1;
                                   // character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Chuc nang tam dong !"));
                                    break;
                                }
                        case 3://Đến Kaio
                        {
                                    character.MapPrivate.Maps[0].OutZone(character, 48);
                                    character.MapPrivate.Maps[3].JoinZone(character, 0);
                            break;
                        }
                    }
                    break;
                }
                case 4:
                    {
                        switch (select)
                        {
                            case 1: // tap lueyn voi thuong de
                            case 2://thach dau voi thuong de
                                break;
                            case 0:
                                switch (character.InfoChar.MapId)
                                {
                                    case 141:

                                        character.MapPrivate.Maps[0].JoinZone(character, 0);
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Hãy xuống gặp thần mèo Karin"));
                                        character.ConDuongRanDoc.isCDRD = true;
                                        break;
                                    default:
                                        if (character.DataTraining.isTraining)
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Con đã hủy thành công đăng ký tập tự động !\ntừ giờ còn muốn tập Offline hãy tự đến đây trước"));
                                            character.DataTraining.isTraining = false;
                                        }
                                        else
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, $"Đăng ký để mỗi khi Offline quá 30 phút, con sẽ được tự động luyện tập với tốc độ\n{DataTraining.GetPotenial(character)} sức mạnh mỗi phút", new List<string> { "Hướng\ndẫn\nthêm", "Đồng ý\n1 ngọc\nmỗi lần", "Không\nĐồng ý" }, character.InfoChar.Gender));
                                            character.TypeMenu = 5;
                                        }
                                        break;
                                }
                                break;
                            //Quay ngọc may mắn
                            case 4:
                                {
                                    var menu = MenuNpc.Gi().MenuThuongDe[2].ToList();
                                    if (character.LuckyBox.Count > 0)
                                    {
                                        menu.Add($"Rương phụ\n{character.LuckyBox.Count}\nmón");
                                        menu.Add($"Đóng");
                                    }
                                    else
                                    {
                                        menu.Add($"Đóng");
                                    }
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextThuongDe[2], menu, character.InfoChar.Gender));
                                    character.TypeMenu = 1;

                                    break;
                                }
                            case 3://Đến Kaio
                                {
                                    character.MapPrivate.Maps[0].OutZone(character, 48);
                                    character.MapPrivate.Maps[3].JoinZone(character, 0);
                                    break;
                                }
                            case 5:
                                break;
                        }
                        break;
                    }
                case 3:
                    {
                        switch (select)
                        {
                            case 1: // tap lueyn voi mr popo
                            case 2://thach dau voi mr popo
                                break;
                            case 0:
                                switch (character.InfoChar.MapId)
                                {
                                    case 141:

                                        character.MapPrivate.Maps[0].JoinZone(character, 0);
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Hãy xuống gặp thần mèo Karin"));
                                        character.ConDuongRanDoc.isCDRD = true;
                                        break;
                                    default:
                                        if (character.DataTraining.isTraining)
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Con đã hủy thành công đăng ký tập tự động !\ntừ giờ còn muốn tập Offline hãy tự đến đây trước"));
                                            character.DataTraining.isTraining = false;
                                        }
                                        else
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, $"Đăng ký để mỗi khi Offline quá 30 phút, con sẽ được tự động luyện tập với tốc độ\n{DataTraining.GetPotenial(character)} sức mạnh mỗi phút", new List<string> { "Hướng\ndẫn\nthêm", "Đồng ý\n1 ngọc\nmỗi lần", "Không\nĐồng ý" }, character.InfoChar.Gender));
                                            character.TypeMenu = 5;
                                        }
                                        break;
                                }
                                break;
                            //Quay ngọc may mắn
                            case 4:
                                {
                                    var menu = MenuNpc.Gi().MenuThuongDe[2].ToList();
                                    if (character.LuckyBox.Count > 0)
                                    {
                                        menu.Add($"Rương phụ\n{character.LuckyBox.Count}\nmón");
                                        menu.Add($"Đóng");
                                    }
                                    else
                                    {
                                        menu.Add($"Đóng");
                                    }
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextThuongDe[2], menu, character.InfoChar.Gender));
                                    character.TypeMenu = 1;

                                    break;
                                }
                            case 3://Đến Kaio
                                {
                                    character.MapPrivate.Maps[0].OutZone(character, 48);
                                    character.MapPrivate.Maps[3].JoinZone(character, 0);
                                    break;
                                }
                        }
                        break;
                    }
                //Quay ngọc may mắn
                case 1:
                {
                    switch (select)
                    {
                        case 0:
                        {
                            if (character.LuckyBox.Count >= DataCache.LIMIT_SLOT_RUONG_PHU_THUONG_DE)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().FULL_LUCKY_BOX));
                                break;
                            }
                            character.CharacterHandler.SendMessage(Service.LuckRoll0());
                            character.ShopId = 0;
                            break;
                        }
                        case 1:
                        {
                                    if (character.LuckyBox.Count >= DataCache.LIMIT_SLOT_RUONG_PHU_THUONG_DE)
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().FULL_LUCKY_BOX));
                                        break;
                                    }
                                    character.CharacterHandler.SendMessage(Service.LuckRoll0());
                                    character.ShopId = 0;
                                    break;
                         }
                        case 2:
                        {
                            var luckRoll = character.LuckyBox;
                            if (character.LuckyBox.Count > 0)
                            {
                                character.CharacterHandler.SendMessage(Service.SubBox(luckRoll));
                                character.ShopId = 1111;
                            } 
                            break;
                        }
                       
                    }

                    break;
                }
               
            }
        }
        
        private static void ConfirmThanVuTru(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                //Menu ban đầu
                case 0:
                    {
                        switch (select)
                        {
                            
                            case 3:
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Ta sẽ đưa con đi", new List<string> { "Về\nthần điện", "Thánh địa\nKaio", "Con\nđường\nrắc độc", "Từ chối" }, character.InfoChar.Gender));
                                character.TypeMenu = 1;
                                break;
                        }
                        break;
                    }
                
                case 1:
                    switch (select)
                    {
                        case 0:
                            {
                                character.MapPrivate.Maps[3].OutZone(character, 45);
                                character.MapPrivate.Maps[0].JoinZone(character, 0);
                            }
                            break;
                        case 1:
                            {
                                character.MapPrivate.Maps[3].OutZone(character, 50);
                                character.MapPrivate.Maps[5].JoinZone(character, 0);
                            }
                            break;
                        case 2:
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Hãy mau trở về bằng con đường rắn độc\nBọn xayda đã đến Trái Đất!.", new List<string> {"TOP\nBang hội", "Thành tích\nBang", "Chọn\nCấp độ", "Từ chối" }, character.InfoChar.Gender));
                            character.TypeMenu = 2;
                            break;
                    }
                    break;
                case 2:
                    switch (select)
                    {
                        case 2:
                            var clan = ClanManager.Get(character.ClanId);
                            if (clan.cdrd.Open)
                            {
                                var timing = (clan.cdrd.timeCDRD - ServerUtils.CurrentTimeMillis()) / 1000 >= 60 ? $"({(clan.cdrd.timeCDRD - ServerUtils.CurrentTimeMillis())/60000} phút trước)" : $"({(clan.cdrd.timeCDRD - ServerUtils.CurrentTimeMillis())/1000} giây trước)";
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Bang hội con đang ở con đường rắn độc cấp độ " + clan.cdrd.Level + "\nCon có muốn đi cùng họ không? " + timing, new List<string> { "Top\nBang hội", "Thành tích\nBang", "Đồng ý", "Từ chối"}, character.InfoChar.Gender));
                                character.TypeMenu = 3;
                            }
                            else
                            {
                                //  character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, "Chức năng sẽ mở khóa khi Open"));
                                var intputCDRD = new List<InputBox>();
                                var inputLevel = new InputBox()
                                {
                                    Name = "Cấp độ",
                                    Type = 1,
                                };
                                intputCDRD.Add(inputLevel);
                                character.CharacterHandler.SendMessage(Service.ShowInput("Hãy chọn cấp từ 1-110", intputCDRD));
                                character.TypeInput = 14;
                            }
                            break;
                    }
                    break;
                case 3:
                    switch (select)
                    {
                        case 2:
                            var clan = ClanManager.Get(character.ClanId);
                            MapManager.OutMap(character, clan.cdrd.MapCDRD[0].Id);
                            character.InfoChar.X = 1103;
                            character.InfoChar.Y = 336;
                            clan.cdrd.MapCDRD[0].JoinZone(character, 0);
                            break;
                    }
                    break;
            }
            
        }
        public static void HandlerGhepTrangBiThienSu(Character character, int idAngelPiece, int gender, int percentMayMan)
        {
            var Item = ItemCache.GetItemDefault(1);
            var type = 0;
            switch (idAngelPiece)
            {
                case 1066:
                    type = 0;
                    break;
                case 1067:
                    type = 1;
                       break;
                case 1068:
                    type = 3;
                    break;
                case 1069:
                    type = 4;
                    break;
                case 1070:
                    type = 2;
                    break;
                

            }
            switch (type)
            {
                case 0:
                    Item = ItemCache.GetItemDefault((short)(1048 + gender));
                    break;
                case 1:
                    Item = ItemCache.GetItemDefault((short)(1051 + gender));
                    break;
                case 2:
                    Item = ItemCache.GetItemDefault((short)(1054 + gender));
                    break;
                case 3:
                    Item = ItemCache.GetItemDefault((short)(1057 + gender));
                    break;
                case 4:
                    Item = ItemCache.GetItemDefault((short)(1060 + gender));
                    break;
            }
            var random = ServerUtils.RandomNumber(100);
            if (random <= percentMayMan)
            {
                if (percentMayMan >= 60)
                {
                    Item.Options[0].Param += (Item.Options[0].Param * ServerUtils.RandomNumber(20, 35)) / 100;
                }else
                Item.Options[0].Param += (Item.Options[0].Param * ServerUtils.RandomNumber(1, 35)) / 100;
            }
            var chisothuong = ServerUtils.RandomNumber(1, 5);
            Item.Options.Add(new OptionItem()
            {
                Id = 41,
                Param = chisothuong,
            });
            for (int i = 0; i < chisothuong; i++)
            {
                Item.Options.Add(new OptionItem()
                {
                    Id = ServerUtils.RandomNumber(42,47),
                    Param = ServerUtils.RandomNumber(1,6),
                });
            }
            character.MineGold(200000000);
            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
            character.CharacterHandler.AddItemToBag(false, Item);
            character.CharacterHandler.SendMessage(Service.SendBag(character));
            character.CharacterHandler.SendMessage(Service.SendCombinne5(ItemCache.ItemTemplate(Item.Id).IconId));
            var ItemSave = new List<int>();
            ItemSave.Add(Item.IndexUI);
            character.CharacterHandler.SendMessage(Service.SendCombinne1(ItemSave));
        }
        private static void ConfirmBaHatMit(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 50:
                    {
                        switch (select)
                        {
                            case 0:
                                var index = character.CombinneIndex[0];
                                var item = character.CharacterHandler.GetItemBagByIndex(index);
                                if (ServerUtils.RandomNumber(100) < 50)
                                {
                                }
                                break;
                        }
                        break;
                    }
                case 23:
                    switch (select)
                    {
                        case 0:
                            var vetinh = ItemCache.GetItemDefault((short)(DataCache.IdVeTinh[ServerUtils.RandomNumber(DataCache.IdVeTinh.Count)]));
                            character.CharacterHandler.AddItemToBag(true, vetinh);
                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được " + ItemCache.ItemTemplate(vetinh.Id).Name));
                            break;
                        case 1:
                            var amulet = (short)DataCache.IdAmulet[ServerUtils.RandomNumber(DataCache.IdAmulet.Count)];
                            var itemAmulet = ItemCache.ItemTemplate(amulet);
                            if (character.InfoChar.ItemAmulet.ContainsKey(amulet))
                            {
                                if (character.InfoChar.ItemAmulet[amulet] < ServerUtils.CurrentTimeMillis())
                                {
                                    character.InfoChar.ItemAmulet[amulet] = DataCache._1HOUR + ServerUtils.CurrentTimeMillis();
                                }
                                else
                                {
                                    character.InfoChar.ItemAmulet[amulet] += DataCache._1HOUR;
                                }
                            }
                            else
                            {
                                character.InfoChar.ItemAmulet.TryAdd(amulet, DataCache._1HOUR + ServerUtils.CurrentTimeMillis());
                            }
                            character.SetupAmulet();
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được " + itemAmulet.Name));
                            break;
                    }
                    break;
                case 22:
                    switch (select)
                    {   
                        case 0: // top 100
                            break;
                        case 1: // dong y
                            Died_Ring.Runtime.gI().Register(character);
                            break;
                        case 2: // tu choi
                            break;
                        case 3: // ve dao kame
                            MapManager.JoinMap(character, 5, ServerUtils.RandomNumber(20), false, false, 0);
                            break;
                    }
                    break;
                case 21:
                    {
                        switch (select)
                        {
                            case 0:
                                {
                                    var IndexAngelPiece = character.CharacterHandler.GetItemBagByIndex(character.CombinneIndex[0]);
                                    var IndexCongThuc = character.CharacterHandler.GetItemBagByIndex(character.CombinneIndex[1]);
                                    var UseDaNangCap = character.CombinneIndex[2] == 1;
                                    var UseDaMayMan = character.CombinneIndex[3] == 1;
                                    var IndexDaNangCap = character.CharacterHandler.GetItemBagByIndex(character.CombinneIndex[4]);
                                    var IndexDaMayMan = character.CharacterHandler.GetItemBagByIndex(character.CombinneIndex[5]);
                                    var PercentNangCap = character.CombinneIndex[6];
                                    var PercentMayMan = character.CombinneIndex[7];
                                    // index 2 = check use da nang cap
                                    // index 3 = check use da may man
                                    // index 4 = index da nang cap
                                    // index 5 = index da may man
                                    if (ServerUtils.RandomNumber(100) <= PercentNangCap)
                                    {
                                        character.CharacterHandler.RemoveItemBagByIndex(IndexAngelPiece.IndexUI, 9999);
                                        character.CharacterHandler.RemoveItemBagByIndex(IndexCongThuc.IndexUI, 1);
                                        if (UseDaNangCap) character.CharacterHandler.RemoveItemBagByIndex(IndexDaNangCap.IndexUI, 1);
                                        if (UseDaMayMan) character.CharacterHandler.RemoveItemBagByIndex(IndexDaMayMan.IndexUI, 1);
                                        HandlerGhepTrangBiThienSu(character, IndexAngelPiece.Id, ItemCache.ItemTemplate(IndexCongThuc.Id).Gender, PercentMayMan);
                                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                                    }
                                    else
                                    {
                                        character.CharacterHandler.RemoveItemBagByIndex(IndexAngelPiece.IndexUI, 999);
                                        character.CharacterHandler.RemoveItemBagByIndex(IndexCongThuc.IndexUI, 1);
                                        if (UseDaNangCap) character.CharacterHandler.RemoveItemBagByIndex(IndexDaNangCap.IndexUI, 1);
                                        if (UseDaMayMan) character.CharacterHandler.RemoveItemBagByIndex(IndexDaMayMan.IndexUI, 1);
                                        character.MineGold(200000000);
                                        character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                        character.CharacterHandler.SendMessage(Service.SendCombinne3());
                                        character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Chúc con may mắn lần sau !"));
                                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                                        var itemSave = new List<int>();
                                        itemSave.Add(IndexAngelPiece.IndexUI);
                                        character.CharacterHandler.SendMessage(Service.SendCombinne1(itemSave));
                                    }
                                    character.CombinneIndex.Clear();
                                    character.CombinneIndex = null;
                                    break;
                                }

                        }
                        break;
                    }
                case 20:

                    {
                        switch (select)
                        {
                            case 0:
                                var listArray = character.CombinneIndex;
                                var item = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                                var dns = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                                character.CharacterHandler.RemoveItemBagById(dns.Id, 1);
                                character.CharacterHandler.SendMessage(Service.SendCombinne2());
                                var listOpt = new List<int> { 34, 35, 36 };

                                item.Options.Add(new OptionItem()
                                {
                                    Id = listOpt[ServerUtils.RandomNumber(listOpt.Count)],
                                    Param = 0
                                });
                                
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                //character.CharacterHandler.SendMessage(Service.SendBody(character));
                                var listIndex = new List<int>();
                                listIndex.Add(item.IndexUI);
                                character.CharacterHandler.SendMessage(Service.SendCombinne1(listIndex));
                                character.CombinneIndex.Clear();
                                character.CombinneIndex = null;
                                break;
                        }
                    }
                    break;
                case 32:
                    {
                        switch (select)
                        {
                            case 0:
                                var indexMat = character.CombinneIndex[0];
                                var indexDNS = character.CombinneIndex[1];
                                var Vang = character.CombinneIndex[2];
                                var TiLe = character.CombinneIndex[3];
                                var Ngoc = character.CombinneIndex[4];
                                var Option = character.CombinneIndex[5];
                                if (character.InfoChar.Gold < Vang)
                                {
                                    character.CharacterHandler.SendMessage(Service.DialogMessage("Không đủ vàng !"));
                                    return;
                                }

                                if (character.AllDiamond() < Ngoc)
                                {
                                    character.CharacterHandler.SendMessage(Service.DialogMessage("Không đủ ngọc !"));
                                    return;
                                }
                                character.MineDiamond(Ngoc);
                                character.MineGold(Vang);
                                character.CharacterHandler.RemoveItemBagByIndex(indexDNS, 1);
                                if (ServerUtils.RandomNumber(100) < TiLe)
                                {
                                    var item = ItemCache.GetItemDefault((short)(character.CharacterHandler.GetItemBagByIndex(indexMat).Id + 1));
                                    if (Option > 2)
                                    {
                                        var chisothuong = Option / 2;
                                        item.Options.Add(new OptionItem()
                                        {
                                            Id = 41,
                                            Param = chisothuong
                                        });
                                        for (int i = 0; i < chisothuong; i++)
                                        {
                                            item.Options.Add(new OptionItem()
                                            {
                                                Id = ServerUtils.RandomNumber(42, 47),
                                                Param = ServerUtils.RandomNumber(1, 6),
                                            });
                                        }
                                    }
                                    character.CharacterHandler.AddItemToBag(false, item);
                                    character.CharacterHandler.RemoveItemBagByIndex(indexMat, 1);
                                    character.CharacterHandler.SendMessage(Service.SendCombinne2());
                                }
                                else
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne3());
                                }
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                break;
                        }
                        break;
                    }
                case 31:

                    {
                        switch (select)
                        {
                            case 0:
                                var listArray = character.CombinneIndex;
                                var item = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                                var dns = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                                character.CharacterHandler.RemoveItemBagById(dns.Id, 2);
                                character.CharacterHandler.SendMessage(Service.SendCombinne2());
                                for (int i = 0; i  < item.Options.Count; i++)
                                {
                                    if (item.Options[i].Id == 34 || item.Options[i].Id == 35 || item.Options[i].Id == 36)
                                    {
                                        item.Options.Remove(item.Options[i]);
                                    }
                                }
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                //character.CharacterHandler.SendMessage(Service.SendBody(character));
                                var listIndex = new List<int>();
                                listIndex.Add(item.IndexUI);
                                character.CharacterHandler.SendMessage(Service.SendCombinne1(listIndex));
                                character.CombinneIndex.Clear();
                                character.CombinneIndex = null;
                                break;
                        }
                    }
                    break;
                case 16:
                    {
                        switch (select)
                        {

                            case 0:
                                {
                                    var listArray = character.CombinneIndex;
                                    var item1 = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                                    var item2 = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                                    var item3 = character.CharacterHandler.GetItemBagByIndex(listArray[2]);
                                                                        var item4 = character.CharacterHandler.GetItemBagByIndex(listArray[3]);

                                    character.CharacterHandler.SendMessage(Service.SendCombinne2());
                                    HandlerGhepTrangBiHuyDiet(character, ServerUtils.RandomNumber(1,4), character.InfoChar.Gender, 1);
                                    character.CharacterHandler.RemoveItemBagByIndex(item1.IndexUI, 1, false, reason: "Null");
                                    character.CharacterHandler.RemoveItemBagByIndex(item2.IndexUI, 1, false, reason: "Nukk");
                                    character.CharacterHandler.RemoveItemBagByIndex(item3.IndexUI, 1, false, reason: "Nukk");
                                                                        character.CharacterHandler.RemoveItemBagByIndex(item4.IndexUI, 4, false, reason: "Nukk");

                                    character.MineGold(500000000);
                                    character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                                    character.CombinneIndex.Clear();
                                    character.CombinneIndex = null;
                                    break;
                                }
                        }
                        break;
                    }
                case 17:
                    {
                        switch (select)
                        {

                            case 0:
                                {
                                    var listArray = character.CombinneIndex;
                                    var item1 = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                                                                        var item2 = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                                    character.CharacterHandler.SendMessage(Service.SendCombinne2());
                                    HandlerGhepTrangBiHuyDiet(character, ItemCache.ItemTemplate(item1.Id).Type, ItemCache.ItemTemplate(item1.Id).Gender, 1);
                                    character.CharacterHandler.RemoveItemBagByIndex(item1.IndexUI, 1, false, reason: "Null");
                                                                        character.CharacterHandler.RemoveItemBagByIndex(item2.IndexUI, 2, false, reason: "Null");

                                    character.MineGold(500000000);
                                    character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                                    character.CombinneIndex.Clear();
                                    character.CombinneIndex = null;
                                    break;
                                }
                        }
                        break;
                    }
                case 18:
                    {
                        switch (select)
                        {

                            case 0:
                                {
                                    var listArray = character.CombinneIndex;
                                    var item1 = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                                    character.CharacterHandler.SendMessage(Service.SendCombinne2());
                                    HandlerGhepTrangBiThanLinh(character, ItemCache.ItemTemplate(item1.Id).Type, ItemCache.ItemTemplate(item1.Id).Gender);
                                    character.CharacterHandler.RemoveItemBagByIndex(item1.IndexUI, 1, false, reason: "Null");
                                    character.MineGold(500000000);
                                    character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                                    character.CombinneIndex.Clear();
                                    character.CombinneIndex = null;
                                    break;
                                }
                        }
                        break;
                    }
                //Menu vách núi
                case 0:
                    {
                        if (!character.InfoChar.IsNhanBua) select += 1;
                        switch (select)
                        {
                            //Nhận bùa miễn phí
                            case 0:
                                {
                                    var idAmulet = (short)DataCache.IdAmulet[ServerUtils.RandomNumber(DataCache.IdAmulet.Count)];
                                    var timePlus = DataCache._1HOUR;
                                    if (character.InfoChar.ItemAmulet.ContainsKey(idAmulet))
                                    {
                                        character.InfoChar.ItemAmulet[idAmulet] += timePlus;
                                    }
                                    else
                                    {
                                        character.InfoChar.ItemAmulet.TryAdd(idAmulet, timePlus + ServerUtils.CurrentTimeMillis());
                                    }
                                    character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().ADD_AMULET, ItemCache.ItemTemplate(idAmulet).Name)));
                                    character.InfoChar.IsNhanBua = false;
                                    // Setup Bùa
                                    break;
                                }
                            //Cửa hàng bùa
                            case 1:
                                {
                                    character.CharacterHandler.SendMessage(
                                        Service
                                            .OpenUiConfirm(npcId, MenuNpc.Gi().TextBaHatMit[0], MenuNpc.Gi().MenuBaHatMit[2], character.InfoChar.Gender));
                                    character.TypeMenu = 2;
                                    break;
                                }
                            //Nâng cấp vật phẩm
                            case 2:
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[4], 21));
                                    character.ShopId = 0;
                                    break;
                                }
                            //Làm phép nhập đá
                            case 3:
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[5]));
                                    character.ShopId = 1;
                                    break;
                                }
                            //Nhập ngọc rồng
                            case 4:
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[6]));
                                    character.ShopId = 2;
                                    break;
                                }
                            //Nâng cấp bông tai porata
                            case 5:
                                {
                                    var bongTaiPorata2 = character.CharacterHandler.GetItemBagById(921);
                                    if (bongTaiPorata2 == null)
                                    {
                                        character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[13]));
                                        character.ShopId = 7;
                                    }
                                    else
                                    {
                                        character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[14]));//14
                                        character.ShopId = 8;//8
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                //Đảo kame
                case 1:
                    {
                        switch (select)
                        {
                            //Ép sao trang bị
                            case 0:
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[11]));
                                    character.ShopId = 3;
                                    break;
                                }
                            //MENU - Pha lê hoá trang bị
                            case 1:
                                {
                                    character.CharacterHandler.SendMessage(
                                        Service
                                            .OpenUiConfirm(npcId, MenuNpc.Gi().TextBaHatMit[2], MenuNpc.Gi().MenuBaHatMit[9], character.InfoChar.Gender));
                                    character.TypeMenu = 3;
                                    break;
                                }
                            //MENU - Chuyển hoá trang bị
                            case 2:
                                {
                                    character.CharacterHandler.SendMessage(
                                        Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBaHatMit[3], MenuNpc.Gi().MenuBaHatMit[10], character.InfoChar.Gender));
                                    character.TypeMenu = 4;
                                    break;
                                }
                            //case 3://
                            //    {
                            //        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(21, "Nâng Thường cần 1 trang bị hủy diệt\nNâng Vip cần 1 trang bị thiên sứ, hủy diệt, thần linh bất kì.Vì thế\nNâng VIP có tỉ lệ ra các trang bị thần linh, hủy diệt, thiên sứsứ kích hoạt\nNgươi muốn chọn loại nào?", new List<string> { "Nâng\nThường", "Nâng\nVIP" }, character.InfoChar.Gender));
                            //        character.TypeMenu = 19;
                            //        break;
                            //    }
                            //case 4://
                            //    {
                            //        character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[22]));
                            //        character.ShopId = 12;
                            //        break;
                            //    }
                            case 3:
                                character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[24]));
                                character.ShopId = 13;
                                break;
                            case 4:
                                character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[28]));
                                character.ShopId = 14;
                                break;
                            case 5:
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(21, "Ta có thể giúp gì cho ngươi?", MenuNpc.Gi().MenuBaHatMit[27], character.InfoChar.Gender));
                                character.TypeMenu = 27;
                                break;
                        }
                        break;
                    }
                case 27:
                    {
                        switch (select)
                        {
                            case 0:
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(21, $"{ServerUtils.Color("green")}Chế tạo bồn tắm gỗ {ItemHandler.TrueItem(character, new List<int> { 1244, 1245, 1246, 1247, -1 }, new List<int> { 50, 20, 20, 2, 5000000 }, 1)}", new List<string> { character.TypeDoiThuong != 1 ? "Từ chối" : "Chế tạo"}, character.InfoChar.Gender));
                                character.TypeMenu = 28;
                            break;
                            case 1:
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(21, $"{ServerUtils.Color("green")}Chế tạo bồn tắm vàng {ItemHandler.TrueItem(character, new List<int> { 1244, 1245, 1246, 1247, -1, -2 }, new List<int> { 50, 20, 20, 2, 5000000, 1000 }, 2)}", new List<string> { character.TypeDoiThuong != 2 ? "Từ chối" : "Chế tạo" }, character.InfoChar.Gender));
                                character.TypeMenu = 28;
                                break;
                        }
                    }
                    break;
                case 28:
                    {
                        switch (select)
                        {
                            case 0:
                                switch (character.TypeDoiThuong)
                                {
                                    case 1:
                                        {
                                            character.CharacterHandler.RemoveItemBagById(1244, 50);
                                            character.CharacterHandler.RemoveItemBagById(1245, 20);
                                            character.CharacterHandler.RemoveItemBagById(1246, 20);
                                            character.CharacterHandler.RemoveItemBagById(1247, 2);
                                            character.MineGold(5000000);
                                            var item = ItemCache.GetItemDefault(1248);
                                            character.CharacterHandler.AddItemToBag(true, item);
                                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng ngươi đã chế tạo thành công Bồn Tắm Gỗ"));
                                            break;
                                        }
                                    case 2:
                                        {
                                            character.CharacterHandler.RemoveItemBagById(1244, 50);
                                            character.CharacterHandler.RemoveItemBagById(1245, 20);
                                            character.CharacterHandler.RemoveItemBagById(1246, 20);
                                            character.CharacterHandler.RemoveItemBagById(1247, 2);
                                            character.MineGold(5000000);
                                            character.MineDiamond(2000);
                                            var item = ItemCache.GetItemDefault(1249);
                                            character.CharacterHandler.AddItemToBag(true, item);
                                            character.CharacterHandler.SendMessage(Service.SendBag(character));
                                            character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng ngươi đã chế tạo thành công Bồn Tắm Vàng"));
                                            break;
                                        }
                                }
                                break;
                        }
                    }
                    break;
                case 19:
                    {
                        switch (select)
                        {
                            case 0:
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[21]));
                                    character.ShopId = 11;
                                    break;
                                }
                            case 1:
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[20]));
                                    character.ShopId = 10;
                                    break;

                                }
                        }
                        break;
                    }
                //Cửa hàng bùa
                case 2:
                    {
                        if (@select is < 0 or > 2) select = 0;
                        var idShop = select;
                        character.CharacterHandler.SendMessage(Service.Shop(character, 0, idShop));
                        character.ShopId = idShop;
                        character.TypeShop = 0;
                        break;
                    }
                //Menu Pha lê hoá
                case 3:
                    {
                        if (select != 0) return;
                        character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[8]));
                        character.ShopId = 4;
                        break;
                    }
                //MENU - Chuyển hoá trang bị
                case 4:
                    {
                        switch (select)
                        {
                            case 0:
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[12]));
                                    character.ShopId = 5;
                                    break;
                                }
                            case 1:
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[12]));
                                    character.ShopId = 6;
                                    break;
                                }
                        }
                        break;
                    }
                //Nâng cấp trang bị
                case 5:
                    {
                        var listArray = character.CombinneIndex;
                        var dungDaBaoVe = listArray[5];
                        var daBaoVeItemIndex = listArray[6];
                        var buaNangCap = listArray[7] == 1;
                        var daBaoVe = false;
                        if (select == 1 && dungDaBaoVe == 1 && daBaoVeItemIndex != -1)
                        {
                            daBaoVe = true;
                            Console.WriteLine("Co su dung da bao ve");
                        }
                        else if (select != 0)
                        {
                            return;
                        }

                        var trangBi = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                        if (trangBi == null) return;
                        var da = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                        var soDaCanNangCap = listArray[2];
                        var gold = listArray[3];
                        var percentSuccess = listArray[4];
                        if (character.InfoChar.Gold < gold)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                            return;
                        }
                        if (da.Quantity < soDaCanNangCap)
                        {
                            character.CharacterHandler.SendMessage(
                                Service.DialogMessage(TextServer.gI().NEED_ENOUGH_STONE));
                            return;
                        }

                        var optionCheck = trangBi.Options.FirstOrDefault(option => option.Id == 72);
                        var optionCheck2 = trangBi.Options.FirstOrDefault(option => option.Id == 209);
                        var percentRandom = ServerUtils.RandomNumber(100) < percentSuccess;
                        if (percentRandom)
                        {
                            if (optionCheck == null)
                            {
                                trangBi.Options.Add(new OptionItem()
                                {
                                    Id = 72,
                                    Param = 1
                                });
                            }
                            else
                            {
                                optionCheck.Param += 1;
                            }
                            trangBi.Options.Where(option => DataCache.IdOptionGoc.Contains(option.Id)).ToList().ForEach(
                                option =>
                                {
                                    option.Param += option.Param / 10;
                                });

                            character.CharacterHandler.SendMessage(Service.SendCombinne2());
                        }
                        else
                        {
                            if (optionCheck != null)
                            {
                                // – cấp 0 lên cấp 1 xịt hay lên ko ảnh hưởng gì hết. Xác suất 80%
                                // – cấp 1 lên cấp 2 xịt hay lên ko ảnh hưởng. Xác suất 50%
                                // – cấp 2 lên cấp 3 xịt bị rớt xuống cấp 1 và giảm 1% chỉ số. Xác suất 20%
                                // – cấp 3 lên 4 xịt k giảm cấp và chỉ số. Xác suất 10%
                                // – cấp 4 lên 5 xịt rớt xuống 3 giảm 1% chỉ số. Xác suất 5%
                                // – cấp 5 lên 6 xịt ko sao. Xác suất 2%
                                // – cấp 6 lên 7 xịt xuống 5 và giảm 1% chỉ số. Xác suất 1%

                                if (optionCheck.Param > 0 && optionCheck.Param % 2 == 0 && !daBaoVe)
                                {
                                    optionCheck.Param -= 1;
                                    trangBi.Options.Where(option => DataCache.IdOptionGoc.Contains(option.Id)).ToList().ForEach(
                                        option =>
                                        {
                                            option.Param -= option.Param / 10;
                                        });
                                    if (optionCheck2 == null)
                                    {
                                        trangBi.Options.Add(new OptionItem()
                                        {
                                            Id = 209,
                                            Param = 1
                                        });
                                    }
                                    else
                                    {
                                        optionCheck2.Param += 1;
                                    }
                                }
                                

                            }
                            character.CharacterHandler.SendMessage(Service.SendCombinne3());
                        }
                        character.MineGold(gold);
                        if (daBaoVe)
                        {
                            character.CharacterHandler.RemoveItemBagByIndex(daBaoVeItemIndex, 1, false, reason: "Dùng đá bảo vệ");
                            Console.WriteLine("Xoa da bao ve");
                        }
                        if (buaNangCap)
                        {
                            character.CharacterHandler.RemoveItemBagById(1277, 1);
                        }
                        character.CharacterHandler.RemoveItemBagByIndex(da.IndexUI, soDaCanNangCap, reason: "Dùng đá nâng cấp");
                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                        character.CharacterHandler.SendMessage(Service.SendBag(character));

                        var checkDa = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                        var listIndexUi = new List<int>();
                        if (checkDa != null && checkDa.Id == da.Id)
                        {
                            listIndexUi.Add(trangBi.IndexUI);
                            listIndexUi.Add(da.IndexUI);
                        }
                        else
                        {
                            listIndexUi.Add(trangBi.IndexUI);
                        }
                        character.CharacterHandler.SendMessage(Service.SendCombinne1(listIndexUi));
                        character.CombinneIndex.Clear();
                        character.CombinneIndex = null;

                        break;
                    }
                //Nhập đá
                case 6:
                    {
                        if (select != 0) return;
                        var bagNull = character.LengthBagNull();
                        var listArray = character.CombinneIndex;
                        var item1 = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                        var item2 = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                        var idNew = (short)(220 + ServerUtils.RandomNumber(5));
                        var itemNew = ItemCache.GetItemDefault(idNew);

                        var itemBagNotMax = character.CharacterHandler.ItemBagNotMaxQuantity(itemNew.Id);
                        if (itemBagNotMax == null && bagNull < 1)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_BAG));
                            return;
                        }
                        switch (item1.Id)
                        {
                            case 225:
                                {
                                    character.CharacterHandler.RemoveItemBagByIndex(item1.IndexUI, 10, false, reason: "Nhập đá");
                                    character.CharacterHandler.RemoveItemBagByIndex(item2.IndexUI, 1, false, reason: "Nhập đá");
                                    break;
                                }
                            default:
                                {
                                    character.CharacterHandler.RemoveItemBagByIndex(item1.IndexUI, 1, false, reason: "Nhập đá");
                                    character.CharacterHandler.RemoveItemBagByIndex(item2.IndexUI, 10, false, reason: "Nhập đá");
                                    break;
                                }
                        }
                        character.MineGold(2000);
                        character.CharacterHandler.AddItemToBag(true, itemNew, "Nhập đá");
                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                        character.CharacterHandler.SendMessage(Service.SendBag(character));

                        var listIndexUi = new List<int>();
                        var itemReturn = character.CharacterHandler.GetItemBagByIndex(item1.IndexUI);
                        if (itemReturn != null && itemReturn.Id == item1.Id)
                        {
                            listIndexUi.Add(item1.IndexUI);
                        }
                        itemReturn = character.CharacterHandler.GetItemBagByIndex(item2.IndexUI);
                        if (itemReturn != null && itemReturn.Id == item2.Id)
                        {
                            listIndexUi.Add(item2.IndexUI);
                        }

                        character.CharacterHandler.SendMessage(Service.SendCombinne1(listIndexUi));
                        character.CharacterHandler.SendMessage(Service.SendCombinne4(ItemCache.ItemTemplate(itemNew.Id).IconId));
                        character.CombinneIndex.Clear();
                        character.CombinneIndex = null;
                        break;
                    }
                //Nhập ngọc rông
                case 7:
                    {
                        if (select != 0) return;
                        var bagNull = character.LengthBagNull();
                        var listArray = character.CombinneIndex;
                        if (listArray == null) return;
                        var ngocRong = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                        var idNew = (short)(ngocRong.Id - 1);
                        var itemNew = ItemCache.GetItemDefault(idNew);

                        var itemBagNotMax = character.CharacterHandler.ItemBagNotMaxQuantity(itemNew.Id);
                        if (itemBagNotMax == null && bagNull < 1)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_BAG));
                            return;
                        }
                        character.MineGold(2000);
                        character.CharacterHandler.RemoveItemBagByIndex(ngocRong.IndexUI, 7, reason: "Nhập ngọc");
                        character.CharacterHandler.AddItemToBag(true, itemNew, "Nhập ngọc");
                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                        character.CharacterHandler.SendMessage(Service.SendBag(character));

                        character.CharacterHandler.SendMessage(Service.SendCombinne5(ItemCache.ItemTemplate(itemNew.Id).IconId));

                        var listIndexUi = new List<int>();
                        var itemReturn = character.CharacterHandler.GetItemBagByIndex(ngocRong.IndexUI);
                        if (itemReturn != null && itemReturn.Id == ngocRong.Id)
                        {
                            listIndexUi.Add(ngocRong.IndexUI);
                        }

                        character.CharacterHandler.SendMessage(Service.SendCombinne1(listIndexUi));
                        character.CombinneIndex.Clear();
                        character.CombinneIndex = null;
                        break;
                    }
                //Ép sao trang bị
                case 8:
                    {
                        if (select != 0) return;
                        if (10 > character.AllDiamond())
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                            return;
                        }
                        var bagNull = character.LengthBagNull();
                        var listArray = character.CombinneIndex;
                        if (listArray == null) return;
                        var trangBi = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                        var ngocRong = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                        var optionId = listArray[2];
                        var optionParam = listArray[3];
                        if (trangBi == null || ngocRong == null) return;

                        var optionCheck = trangBi.Options.FirstOrDefault(opt => opt.Id == 102);
                        var optionUp = trangBi.Options.FirstOrDefault(opt => opt.Id == optionId);
                        var checkSql = trangBi.Options.FirstOrDefault(opt => opt.Id == 107);
                        if (checkSql.Param > 8 && optionCheck.Param >= 8)
                        {
                            return;
                        }
                        if (optionCheck == null)
                        {
                            trangBi.Options.Add(new OptionItem()
                            {
                                Id = 102,
                                Param = 1
                            });
                        }
                        else
                        {
                            optionCheck.Param++;
                        }

                        if (optionUp == null)
                        {
                            trangBi.Options.Add(new OptionItem()
                            {
                                Id = optionId,
                                Param = optionParam
                            });
                        }
                        else
                        {
                            optionUp.Param += optionParam;
                        }
                        character.MineDiamond(10);
                        character.CharacterHandler.SendMessage(Service.SendCombinne2());
                        character.CharacterHandler.RemoveItemBagByIndex(ngocRong.IndexUI, 1, reason: "Ép ngọc rồng");
                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        
                        character.CharacterHandler.SendMessage(Service.SendCombinne1(new List<int>() { trangBi.IndexUI, ngocRong.IndexUI }));
                        character.CombinneIndex.Clear();
                        character.CombinneIndex = null;
                        break;
                    }
                //Pha lê hoá trang bị
                case 9:
                    {
                        var listArray = character.CombinneIndex;
                        if (listArray == null) return;
                        var itemBag = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                        var lvOption = listArray[1];
                        if (itemBag == null) return;
                        var percentPhaLe = DataCache.PercentPhaLe[lvOption];
                        long goldPhaLe = percentPhaLe[0] * 1000000;
                        var diamondPhaLe = percentPhaLe[2];
                        

                        if (character.InfoChar.Gold < goldPhaLe)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                            return;
                        }
                        if (character.AllDiamond() < diamondPhaLe)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                            return;
                        }
                        
                        var percent = ServerUtils.RandomNumber(120);
                        switch (lvOption)
                        {
                            case 9:
                                percentPhaLe[1] = (int)0.9;
                                break;
                            case 10:
                                percentPhaLe[1] = (int)0.5;
                                break;
                            case >= 5:
                                percent = ServerUtils.RandomNumber(200);
                                break;
                        }
                                                                       var success = percent < percentPhaLe[1];

                       
                        if (success)
                        {
                            var optionPlus = itemBag.Options.FirstOrDefault(option => option.Id == 107);
                            if (optionPlus != null && optionPlus.Param >= DataCache.MAX_LIMIT_SPL)
                            {
                                return;
                            }
                            if (optionPlus != null)
                            {
                                optionPlus.Param++;
                            }
                            else
                            {
                                itemBag.Options.Add(new OptionItem()
                                {
                                    Id = 107,
                                    Param = 1
                                });
                            }
                            character.CharacterHandler.SendMessage(Service.SendCombinne2());
                        }
                        else
                        {
                            character.CharacterHandler.SendMessage(Service.SendCombinne3());
                        }
                        character.MineGold(goldPhaLe);
                        character.MineDiamond(diamondPhaLe);
                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        character.CharacterHandler.SendMessage(Service.SendCombinne1(new List<int>() { itemBag.IndexUI }));
                        character.CombinneIndex.Clear();
                        character.CombinneIndex = null;
                        break;
                    }
                //Chuyển hoá trang bị VÀNG / 10
                //Chuyển hoá trang bị NGỌC / 11
                case 10:
                case 11:
                    {
                        if (select != 0) return;
                        var listArray = character.CombinneIndex;
                        var itemLuongLong = character.CharacterHandler.GetItemBagByIndex(listArray[0]); //old
                        var itemThan = character.CharacterHandler.GetItemBagByIndex(listArray[1]); //new đồ thần
                        var levelUp = listArray[2];
                        var checkMoney = listArray[3];
                        if (itemLuongLong == null || itemThan == null) return;
                        switch (character.TypeMenu)
                        {
                            case 10:
                                {
                                    if (character.InfoChar.Gold < checkMoney)
                                    {
                                        character.CharacterHandler.SendMessage(
                                            Service.DialogMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                                        return;
                                    }
                                    else
                                    {
                                        character.MineGold(checkMoney);
                                    }
                                    break;
                                }
                            case 11:
                                {
                                    if (character.AllDiamond() < checkMoney)
                                    {
                                        character.CharacterHandler.SendMessage(
                                            Service.DialogMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                                        return;
                                    }
                                    else
                                    {
                                        character.MineDiamond(checkMoney);
                                    }
                                    break;
                                }
                        }

                        var checkLevel = itemLuongLong.Options.FirstOrDefault(opt => opt.Id == 72)?.Param;

                        var listOptionLlGoc = itemLuongLong.Options.Where(opt => DataCache.IdOptionGoc.Contains(opt.Id)).ToList();
                        itemThan.Options.ForEach(opt =>
                        {
                            var paramNew = 0;
                            var optCheck = listOptionLlGoc.FirstOrDefault(o => o.Id == opt.Id);
                            if (optCheck == null) return;
                            if (checkLevel == levelUp)
                            {
                                paramNew += optCheck.Param;
                            }
                            else
                            {
                                paramNew += optCheck.Param - optCheck.Param / 10;
                            }
                            opt.Param += paramNew;
                        });
                        var listCheckPlus = itemLuongLong.Options.Where(opt => itemThan.Options.FirstOrDefault(o => o.Id == opt.Id) == null).ToList();
                        itemThan.Options.AddRange(listCheckPlus);

                        character.CharacterHandler.RemoveItemBag(itemLuongLong.IndexUI, reason: "Chuyển hóa");
                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        character.CharacterHandler.SendMessage(Service.SendCombinne4(ItemCache.ItemTemplate(itemThan.Id).IconId));

                        var itemReturn = character.ItemBag.FirstOrDefault(item =>
                            item.Id == itemThan.Id && item.Options.Count == itemThan.Options.Count &&
                            item.IndexUI != itemThan.IndexUI) ?? itemThan;
                        character.CharacterHandler.SendMessage(Service.SendCombinne1(new List<int>() { itemReturn.IndexUI }));
                        character.CombinneIndex.Clear();
                        character.CombinneIndex = null;
                        break;
                    }
                //Nâng cấp porata
                case 12:
                    {
                        if (select != 0) return;

                        var listArray = character.CombinneIndex;
                        var bongTaiPorata = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                        var manhVoBongTai = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                        var soNgocCanNangCap = listArray[2];
                        var soVangCanNangCap = listArray[3];
                        var percentSuccess = listArray[4];
                        var isThanhCong = false;

                        if (character.InfoChar.Gold < soVangCanNangCap)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                            return;
                        }
                        if (character.AllDiamond() < soNgocCanNangCap)
                        {
                            character.CharacterHandler.SendMessage(Service.DialogMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                            return;
                        }

                     //   var optionCheck = manhVoBongTai.Options.FirstOrDefault(option => option.Id == 31);
                        var percentRandom = ServerUtils.RandomNumber(100) < percentSuccess;
                        if (percentRandom)
                        {
                            // Thành công thì xóa số lượng 9999 item, xóa item bông tai và thêm item bông tai 2
                            //if (optionCheck != null)
                            //{
                            //    optionCheck.Param -= 9999;
                            //    if (optionCheck.Param <= 0)
                            //    {
                            //        character.CharacterHandler.RemoveItemBagByIndex(manhVoBongTai.IndexUI, 1, false, reason: "NC Porata");
                            //    }
                            //}
                            character.CharacterHandler.RemoveItemBagByIndex(manhVoBongTai.IndexUI, 9999);
                            character.CharacterHandler.RemoveItemBagByIndex(bongTaiPorata.IndexUI, 1, false, reason: "NC Porata");
                            var itemAdd = ItemCache.GetItemDefault(921);
                            itemAdd.Quantity = 1;
                            character.CharacterHandler.AddItemToBag(false, itemAdd, "Nâng cấp porata");
                            character.CharacterHandler.SendMessage(Service.SendCombinne2());
                            isThanhCong = true;
                        }
                        else
                        {
                           
                            character.CharacterHandler.SendMessage(Service.SendCombinne3());
                        }

                        character.MineGold(soVangCanNangCap);
                        character.MineDiamond(soNgocCanNangCap);
                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                        character.CharacterHandler.SendMessage(Service.SendBag(character));

                        var checkManhVoBongTai = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                        var listIndexUi = new List<int>();
                        if (!isThanhCong)
                        {
                            if (checkManhVoBongTai != null && checkManhVoBongTai.Id == manhVoBongTai.Id)
                            {
                                listIndexUi.Add(bongTaiPorata.IndexUI);
                                listIndexUi.Add(manhVoBongTai.IndexUI);
                            }
                            else
                            {
                                listIndexUi.Add(bongTaiPorata.IndexUI);
                            }
                        }
                        character.CharacterHandler.SendMessage(Service.SendCombinne1(listIndexUi));
                        character.CombinneIndex.Clear();
                        character.CombinneIndex = null;
                        break;
                    }
                // Mở option porata
                case 13:
                    {
                        if (select != 0) return;

                        var listArray = character.CombinneIndex;
                        var bongTaiPorata2 = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                        var manhHonBongTai = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                        var daXanhLam = character.CharacterHandler.GetItemBagByIndex(listArray[2]);
                        var soNgocCanNangCap = listArray[3];
                        var soVangCanNangCap = listArray[4];
                        var percentSuccess = listArray[5];
                       if (soVangCanNangCap > character.InfoChar.Gold)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ vàng !"));
                            return;
                        }
                       if (soNgocCanNangCap > character.AllDiamond())
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ ngọc !"));

                            return;
                        }
                        character.MineGold(soVangCanNangCap);
                        character.MineDiamond(soNgocCanNangCap);
                        character.CharacterHandler.RemoveItemBagByIndex(manhHonBongTai.IndexUI, 10);
                        character.CharacterHandler.RemoveItemBagByIndex(daXanhLam.IndexUI, 1);
                        if (ServerUtils.RandomNumber(100) < percentSuccess)
                        {
                            character.CharacterHandler.RemoveItemBagById(921, 1);
                            var item = ItemCache.GetBongTaiCap2(921);
                            character.CharacterHandler.AddItemToBag(false, item);
                            character.CharacterHandler.SendMessage(Service.SendCombinne2());
                        }
                        else
                        {
                            //fail
                            character.CharacterHandler.SendMessage(Service.SendCombinne3());
                        }
                        
                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                        break;
                    }
                case 14:
                    {
                        //menu linh thú
                        switch (select)
                        {
                            case 0://nở trứng linh thú
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[16]));
                                    character.ShopId = 9;
                                    break;
                                }
                            case 1://nâng cấp linh thú
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[17]));
                                    character.ShopId = 10;
                                    break;
                                }
                            case 2://nâng cấp linh thú
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[18]));
                                    character.ShopId = 11;
                                    break;
                                }
                            case 3://nâng cấp linh thú
                                {
                                    character.CharacterHandler.SendMessage(Service.SendCombinne0(MenuNpc.Gi().MenuBaHatMit[19]));
                                    character.ShopId = 12;
                                    break;
                                }
                        }
                        break;
                    }
                case 15://nở trứng
                    {
                        var listArray = character.CombinneIndex;

                        var trungLinhThu = character.CharacterHandler.GetItemBagByIndex(listArray[0]);
                        var honLinhThu = character.CharacterHandler.GetItemBagByIndex(listArray[1]);
                        short trungLinhThuIcon = ItemCache.ItemTemplate(trungLinhThu.Id).IconId;

                        character.CharacterHandler.RemoveItemBagByIndex(trungLinhThu.IndexUI, 1, false, reason: "Nở trứng");
                        character.CharacterHandler.RemoveItemBagByIndex(honLinhThu.IndexUI, 99, false, reason: "Nở trứng");

                        if (listArray.Count == 3)
                        {
                            var thoiVang = character.CharacterHandler.GetItemBagByIndex(listArray[2]);
                            character.CharacterHandler.RemoveItemBagByIndex(thoiVang.IndexUI, 5, false, reason: "Nở trứng nhanh");
                        }

                        var linhThuNgauNhien = DataCache.ListPetD[ServerUtils.RandomNumber(DataCache.ListPetD.Count)];
                        var itemLinhThu = ItemCache.GetItemDefault(linhThuNgauNhien);

                        var maSoLinhThu = ServerUtils.RandomNumber(100, 100000);
                        var optionHiden = itemLinhThu.Options.FirstOrDefault(option => option.Id == 73);

                        if (optionHiden != null)
                        {
                            optionHiden.Param = maSoLinhThu;
                        }
                        else
                        {
                            itemLinhThu.Options.Add(new OptionItem()
                            {
                                Id = 73,
                                Param = maSoLinhThu,
                            });
                        }

                        character.CharacterHandler.SendMessage(Service.NpcChat(npcId, TextServer.gI().RANDOM_LINH_THU));
                        character.CharacterHandler.SendMessage(Service.SendCombinne6(trungLinhThuIcon, ItemCache.ItemTemplate(itemLinhThu.Id).IconId));

                        character.CharacterHandler.AddItemToBag(false, itemLinhThu, "Nở trứng");
                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                        character.CharacterHandler.SendMessage(Service.SendBag(character));

                        character.CharacterHandler.SendMessage(Service.SendCombinne1(new List<int>()));
                        character.CombinneIndex.Clear();
                        character.CombinneIndex = null;
                        break;
                    }
                    //case 17:
                    //    switch (select)
                    //    {
                    //        case 0:
                    //            if (character.CharacterHandler.GetItemBagById(1199) == null || character.CharacterHandler.GetItemBagById(1199).Quantity < 1)
                    //            {
                    //                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn không có mâm ngũ quả"));
                    //                return;
                    //            }
                    //            var rand = ServerUtils.RandomNumber(120);
                    //            var item = ItemCache.GetItemDefault(0);
                    //            if (rand <= 30)
                    //            {
                    //                item = ItemCache.GetItemDefault(1205);
                    //            }else if (rand <= 60)
                    //            {
                    //                item = ItemCache.GetItemDefault(1219);
                    //            }
                    //            else if (rand <= 100)
                    //            {
                    //                item = ItemCache.GetItemDefault(1202);
                    //            }else if (rand <= 120)
                    //            {
                    //                item = ItemCache.GetItemDefault(758);
                    //            }
                    //            character.CharacterHandler.AddItemToBag(trfue, item, "Bay mam ngu qua");
                    //            character.CharacterHandler.RemoveItemBagById(1199, 1);
                    //            character.CharacterHandler.SendMessage(Service.SendBag(character));

                    //            character.CharacterHandler.SendMessage(Service.SendCombinne4(ItemCache.ItemTemplate(item.Id).IconId));
                    //             break;
                    //    }
                    //    break;
                    //case 16:
                    //    switch (select)
                    //    {
                    //        case 0:
                    //            int gold = 1000000;
                    //            if (character.InfoChar.Gold < gold)
                    //            {
                    //                character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ 1tr vàng"));
                    //                return;
                    //            }
                    //            else if (character.CharacterHandler.GetItemBagById(1144) == null || character.CharacterHandler.GetItemBagById(1144).Quantity < 10)
                    //            {
                    //                character.CharacterHandler.SendMessage(Service.ServerMessage("Không có thẻ Fan Gà Nửa Mùa hoặc không đủ"));
                    //                return;
                    //            }
                    //            else
                    //            {
                    //                var capsulethuong = ItemCache.GetItemDefault(1146);
                    //                capsulethuong.Quantity = 1;
                    //                capsulethuong.Options.Add(new OptionItem()
                    //                {
                    //                    Id = 30,
                    //                    Param = 0,
                    //                });
                    //                character.CharacterHandler.AddItemToBag(true,capsulethuong);
                    //                character.CharacterHandler.RemoveItemBagById(1144, 10);
                    //                character.MineGold(gold);
                    //                character.CharacterHandler.SendMessage(Service.SendBag(character));
                    //                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được Capsule thường"));
                    //            }
                    //            break;
                    //        case 1:
                    //            int gem = 1000;
                    //            if (character.InfoChar.Diamond < gem)
                    //            {
                    //                character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ 1000 ngọc, vui lòng nạp thêm"));
                    //                return;
                    //            }
                    //            else if (character.CharacterHandler.GetItemBagById(1143) == null || character.CharacterHandler.GetItemBagById(1143).Quantity < 10)
                    //            {
                    //                character.CharacterHandler.SendMessage(Service.ServerMessage("Không có thẻ Fan cuồng bóng đá hoặc không đủ"));
                    //                return;
                    //            }
                    //            else
                    //            {
                    //                var capsulevip = ItemCache.GetItemDefault(1147);
                    //                capsulevip.Quantity = 1;
                    //                capsulevip.Options.Add(new OptionItem()
                    //                {
                    //                    Id = 30,
                    //                    Param = 0,
                    //                });
                    //                character.CharacterHandler.AddItemToBag(true, capsulevip);
                    //                character.CharacterHandler.RemoveItemBagById(1143, 10);
                    //                character.MineDiamond(gem);
                    //                character.CharacterHandler.SendMessage(Service.SendBag(character));
                    //                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được Capsule VIP"));
                    //            }
                    //            break;
            //}
            //       break;
            }
        }
        
        private static void ConfirmRongThan(Character character, short npcId, int select)
        {
            // character.CharacterHandler.SendMessage(Service.ServerMessage("select: " + select));
            switch (character.TypeDragon)
            {
                case 0:
                    
                    switch (select)
                    {
                       
                        case 0://+1 gang tay tren nguoi
                            {
                                var trangBi = character.ItemBody[2];

                                if (trangBi == null)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Trên người của bạn không có găng tay"));
                                    break;
                                }

                                var optionCheck = trangBi.Options.FirstOrDefault(option => option.Id == 72);
                                if (optionCheck == null)
                                {
                                    trangBi.Options.Add(new OptionItem()
                                    {
                                        Id = 72,
                                        Param = 1
                                    });
                                    trangBi.Options.Where(option => DataCache.IdOptionGoc.Contains(option.Id)).ToList().ForEach(
                                                    option => option.Param += option.Param / 10);
                                    character.CharacterHandler.SendMessage(Service.SendBody(character));
                                }
                                else
                                {
                                    if (optionCheck.Param < DataCache.MAX_LIMIT_UPGRADE - 1)
                                    {
                                        optionCheck.Param += 1;
                                        trangBi.Options.Where(option => DataCache.IdOptionGoc.Contains(option.Id)).ToList().ForEach(
                                                    option => option.Param += option.Param / 10);
                                        character.CharacterHandler.SendMessage(Service.SendBody(character));
                                    }
                                }
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã Ước thành công"));

                                break;
                            }
                        case 1://Doi ky nang de tu
                            {
                                var disciple = character.Disciple;
                                var disciplePower = disciple.InfoChar.Power;
                                if (disciplePower >= 150000000 && disciple.Skills.Count >= 2)
                                {
                                    var randomSkill = DataCache.IdSkillDisciple2[ServerUtils.RandomNumber(DataCache.IdSkillDisciple2.Count)];
                                    disciple.Skills[1] = new SkillCharacter() // skill 2
                                    {
                                        Id = randomSkill,
                                        SkillId = Disciple.GetSkillId(randomSkill),
                                        Point = 1,
                                    };
                                }

                                if (disciplePower >= 1500000000 && disciple.Skills.Count >= 3)
                                {
                                    var randomSkill = DataCache.IdSkillDisciple3[ServerUtils.RandomNumber(DataCache.IdSkillDisciple3.Count)];
                                    disciple.Skills[2] = new SkillCharacter() // skill 3
                                    {
                                        Id = randomSkill,
                                        SkillId = Disciple.GetSkillId(randomSkill),
                                        Point = 1,
                                    };
                                }
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã Ước thành công"));

                                break;
                            }
                        case 2://Doi ky nang de tu
                            {
                                var disciple = character.Disciple;
                                var disciplePower = disciple.InfoChar.Power;
                                if (disciplePower >= 1500000000 && disciple.Skills.Count >= 3)
                                {
                                    var randomSkill = DataCache.IdSkillDisciple3[ServerUtils.RandomNumber(DataCache.IdSkillDisciple3.Count)];
                                    disciple.Skills[2] = new SkillCharacter() // skill 2
                                    {
                                        Id = randomSkill,
                                        SkillId = Disciple.GetSkillId(randomSkill),
                                        Point = 1,
                                    };
                                }

                                if (disciplePower >= 20000000000 && disciple.Skills.Count >= 4)
                                {
                                    var randomSkill = DataCache.IdSkillDisciple4[ServerUtils.RandomNumber(DataCache.IdSkillDisciple4.Count)];
                                    disciple.Skills[3] = new SkillCharacter() // skill 3
                                    {
                                        Id = randomSkill,
                                        SkillId = Disciple.GetSkillId(randomSkill),
                                        Point = 1,
                                    };
                                }
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã Ước thành công"));

                                break;
                            }
                        case 3://+1 gang tay tren nguoi
                            {
                                var trangBi = character.Disciple.ItemBody[2];

                                if (trangBi == null)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Trên người của bạn không có găng tay"));
                                    break;
                                }

                                var optionCheck = trangBi.Options.FirstOrDefault(option => option.Id == 72);
                                if (optionCheck == null)
                                {
                                    trangBi.Options.Add(new OptionItem()
                                    {
                                        Id = 72,
                                        Param = 1
                                    });
                                    trangBi.Options.Where(option => DataCache.IdOptionGoc.Contains(option.Id)).ToList().ForEach(
                                                    option => option.Param += option.Param / 10);
                                    character.Disciple.CharacterHandler.SendMessage(Service.SendBody(character.Disciple));
                                }
                                else
                                {
                                    if (optionCheck.Param < DataCache.MAX_LIMIT_UPGRADE - 1)
                                    {
                                        optionCheck.Param += 1;
                                        trangBi.Options.Where(option => DataCache.IdOptionGoc.Contains(option.Id)).ToList().ForEach(
                                                    option => option.Param += option.Param / 10);
                                        character.Disciple.CharacterHandler.SendMessage(Service.SendBody(character.Disciple));
                                    }
                                }
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã Ước thành công"));

                                break;
                            }
                        case 4://dep trai nhat vu tru
                            {
                                var itemId = (character.InfoChar.Gender + 227);
                                var itemAdd = ItemCache.GetItemDefault((short)itemId);
                                itemAdd.Quantity = 1;
                                character.CharacterHandler.AddItemToBag(true, itemAdd, "Ước NR");

                                character.CharacterHandler.SendMessage(Service.SendBag(character));

                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã Ước thành công"));

                                break;
                            }
                     
                    }
                    // character.Zone.ZoneHandler.SendMessage(Service.CallDragon(1, 0, character));
                    character.CharacterHandler.SendMessage(Service.CallDragon(1, 0, character));
                    MapManager.SetDragonAppeared(false);
                    MapManager.IdPlayerCallDragon = -1;
                    break;
                case 1:
                    Rồng_Namec.gI().ConfirmMenu(character, npcId,select);
                    break;
                case 2:
                    BoneDragon.gI().Ước(character, select);
                    break;
                case 3:
                    IceDragon.gI().Wish(character,select);
                    break;
            }
        }
        
        private static void ConfirmCalich(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0: 
                {
                    switch(select)
                    {
                        case 0://Nói chuyện
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, MenuNpc.Gi().TextCalich[1]));
                            break;
                        }
                        case 1:
                        {
                            character.InfoMore.TransportMapId = 102;
                            character.CharacterHandler.SendMessage(Service.Transport(20));
                            // đến tương lai
                            break;
                        }
                    }
                    break;
                }
                case 1:
                {
                    if (select != 0) return;
                    character.InfoMore.TransportMapId = 24;
                    character.CharacterHandler.SendMessage(Service.Transport(20));
                    break;
                }
            }
        }

        private static void ConfirmSanta(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0: 
                {
                    switch(select)
                    {
                        case 0:
                        {
                            var idShop = 18 + character.InfoChar.Gender;
                            character.CharacterHandler.SendMessage(Service.Shop(character, 0, idShop));
                            character.ShopId = idShop;
                            character.TypeShop = 0;
                            break;
                        }
                        case 1:
                        {
                            var idShop = 3 + character.InfoChar.Gender;
                            character.CharacterHandler.SendMessage(Service.Shop(character, 0, idShop));
                            character.ShopId = idShop;
                            character.TypeShop = 0;
                            break;
                        }
                        case 2:
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, string.Format(MenuNpc.Gi().TextSanta[2], ServerUtils.GetMoneys(UserDB.GetVND(character.Player)), ServerUtils.GetMoneys(UserDB.GetTongVND(character.Player))), MenuNpc.Gi().MenuSanta[2], character.InfoChar.Gender));
                            character.TypeMenu = 1;
                            break;
                        }
                       
                        default:
                        {
                            character.CharacterHandler.SendMessage(Service.NpcChat(npcId, TextServer.gI().UPDATING));
                            break;
                        }
                    }
                    break;
                }
              
                case 1:
                {
                    switch(select)
                    {
                        case 0:
                                
                                    /// doi vang + ngoc 
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, string.Format(MenuNpc.Gi().TextSanta[5], ServerUtils.GetMoneys(UserDB.GetVND(character.Player))), MenuNpc.Gi().MenuSanta[3], character.InfoChar.Gender));
                                    character.TypeMenu = 2;

                                    //if(character.CharacterHandler.AddItemToBag(true, itemNew, "Đổi vàng")) {
                                    //  character.CharacterHandler.SendMessage(Service.SendBag(character));
                                    //character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().GET_GOLD_BAR, soLuongThoiVang)));



                                
                           
                            break;
                        
                        case 1:
                                // kich hoat thanh vien
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, string.Format(MenuNpc.Gi().TextSanta[4], ServerUtils.GetMoneys(UserDB.GetVND(character.Player))), MenuNpc.Gi().MenuSanta[6], character.InfoChar.Gender));
                                character.TypeMenu = 5;

                                break;
                    }

                    break;
                }
                case 2:
                    switch (select)
                    {
                        case 0:
                        case 1:
                            if (DatabaseManager.ConfigManager.gI().costDoiTien <= 1)
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, string.Format(MenuNpc.Gi().TextSanta[3] + "\n|1|Tình Trạng Khuyến Mãi [Không có Khuyến Mãi]", ServerUtils.GetMoneys(UserDB.GetVND(character.Player))), MenuNpc.Gi().MenuSanta[4 + select], character.InfoChar.Gender));
                                character.TypeMenu = 3 + select;
                                break;
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, string.Format(MenuNpc.Gi().TextSanta[3] + "\n|1|TÌnh Trạng Khuyến Mãi [Đang X" + DatabaseManager.ConfigManager.gI().costDoiTien +"]", ServerUtils.GetMoneys(UserDB.GetVND(character.Player))), MenuNpc.Gi().MenuSanta[4 + select], character.InfoChar.Gender));
                                character.TypeMenu = 3 + select;
                                break;
                            }
                    }
                    break;
                case 3:
                    switch (select)
                    {
                        case 0:
                            if (UserDB.GetVND(character.Player) >= 10000)
                            {
                                var thoivang = ItemCache.GetItemDefault(457);
                                thoivang.Quantity = 10 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                var soluong = thoivang.Quantity;
                                character.CharacterHandler.AddItemToBag(true, thoivang, "Đổi vàng");
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().GET_GOLD_BAR, soluong)));
                                UserDB.MineVND(character.Player, 10000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                        case 1:
                            if (UserDB.GetVND(character.Player) >= 20000)
                            {
                                var thoivang = ItemCache.GetItemDefault(457);
                                thoivang.Quantity = 20 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                var soluong = thoivang.Quantity;
                                character.CharacterHandler.AddItemToBag(true, thoivang, "Đổi vàng");
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().GET_GOLD_BAR, soluong)));
                                UserDB.MineVND(character.Player, 20000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                        case 2:
                            if (UserDB.GetVND(character.Player) >= 50000)
                            {
                                var thoivang = ItemCache.GetItemDefault(457);
                                thoivang.Quantity = 50 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                var soluong = thoivang.Quantity;
                                character.CharacterHandler.AddItemToBag(true, thoivang, "Đổi vàng");
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().GET_GOLD_BAR, soluong)));
                                UserDB.MineVND(character.Player, 50000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                        case 3:
                            if (UserDB.GetVND(character.Player) >= 100000)
                            {
                                var thoivang = ItemCache.GetItemDefault(457);
                                thoivang.Quantity = 100 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                var soluong = thoivang.Quantity;
                                character.CharacterHandler.AddItemToBag(true, thoivang, "Đổi vàng");
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().GET_GOLD_BAR, soluong)));
                                UserDB.MineVND(character.Player, 100000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                        case 4:
                            if (UserDB.GetVND(character.Player) >= 200000)
                            {
                                var thoivang = ItemCache.GetItemDefault(457);
                                thoivang.Quantity = 200 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                var soluong = thoivang.Quantity;
                                character.CharacterHandler.AddItemToBag(true, thoivang, "Đổi vàng");
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().GET_GOLD_BAR, soluong)));
                                UserDB.MineVND(character.Player, 200000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                        case 5:

                            if (UserDB.GetVND(character.Player) >= 500000)
                            {
                                var thoivang = ItemCache.GetItemDefault(457);
                                thoivang.Quantity = 500 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                var soluong = thoivang.Quantity;
                                character.CharacterHandler.AddItemToBag(true, thoivang, "Đổi vàng");
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().GET_GOLD_BAR, soluong)));
                                UserDB.MineVND(character.Player, 500000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                    }
                    break;
                case 4:
                    switch (select)
                    {
                        case 0:
                            if (UserDB.GetVND(character.Player) >= 10000)
                            {
                                var diamond = 20000 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                character.PlusDiamond(diamond);
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được " +diamond+ " Ngọc Xanh"));
                                UserDB.MineVND(character.Player, 10000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                        case 1:
                            if (UserDB.GetVND(character.Player) >= 20000)
                            {
                                var diamond = 40000 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                character.PlusDiamond(diamond);
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được " + diamond + " Ngọc Xanh"));
                                UserDB.MineVND(character.Player, 20000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                        case 2:
                            if (UserDB.GetVND(character.Player) >= 50000)
                            {
                                var diamond = 100000 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                character.PlusDiamond(diamond);
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được " + diamond + " Ngọc Xanh"));
                                UserDB.MineVND(character.Player, 50000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                        case 3:
                            if (UserDB.GetVND(character.Player) >= 100000)
                            {
                                var diamond = 200000 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                character.PlusDiamond(diamond);
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được " + diamond + " Ngọc Xanh"));
                                UserDB.MineVND(character.Player, 100000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                        case 4:
                            if (UserDB.GetVND(character.Player) >= 200000)
                            {
                                var diamond = 400000 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                character.PlusDiamond(diamond);
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được " + diamond + " Ngọc Xanh"));
                                UserDB.MineVND(character.Player, 200000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                        case 5:

                            if (UserDB.GetVND(character.Player) >= 500000)
                            {
                                var diamond = 1000000 * DatabaseManager.ConfigManager.gI().costDoiTien;
                                character.PlusDiamond(diamond);
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Chúc mừng bạn đã nhận được " + diamond + " Ngọc Xanh"));
                                UserDB.MineVND(character.Player, 500000);
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                            }
                            break;
                    }
                    break;
                case 5:
                    switch (select)
                    {
                        case 0:
                            if (UserDB.GetVND(character.Player) < 20000)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Không Đủ Tiền, Vui Lòng Nạp Thêm"));
                                return;
                            }
                            if (!character.InfoChar.IsPremium)
                            {
                                var thoivang = ItemCache.GetItemDefault(457);
                                thoivang.Quantity = 20;
                                character.PlusDiamond(100000);
                                var soluong = thoivang.Quantity;
                                character.CharacterHandler.AddItemToBag(true, thoivang, "Đổi vàng");
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                //  character.CharacterHandler.SendMessage(Service.ServerMessage(string.Format(TextServer.gI().GET_GOLD_BAR, soluong)));
                                /// ------------->>>>
                                character.InfoChar.IsPremium = true;
                                UserDB.MineVND(character.Player, 20000);
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Kích hoạt thành viên thành công !!!, Chúc bạn chơi game vui vẻ"));
                                character.CharacterHandler.SendMessage(Service.MeLoadAll(character));
                            } else
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã kích hoạt thành viên rồi !!"));
                            }
                            break;
                    }
                    
                    break;
            }
        }

        private static void ConfirmQuocVuong(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                //Menu chính
                case 0:
                    {
                        switch (select)
                        {
                            case 0:
                                {
                                    try
                                    {
                                        var limit = character.InfoChar.LitmitPower;
                                        if (limit >= DataCache.MAX_LIMIT_POWER_LEVEL)
                                        {
                                            character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Con đã đạt giới hạn tối đa"));
                                            return;
                                        }
                                        var LM = Cache.Gi().LIMIT_POWERS[limit];
                                        var ngoc = 100 * (limit + 1);
                                        var text = string.Format(TextServer.gI().UPGRADE_LEVEL_ME, ServerUtils.GetPower(LM.Power), ngoc);
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, text, MenuNpc.Gi().MenuQuocVuong[1], character.InfoChar.Gender));
                                        character.TypeMenu = 1;
                                    }
                                    catch (Exception)
                                    {
                                        character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Con đã đạt giới hạn tối đa"));
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    try
                                    {
                                        if (character.Disciple == null)
                                        {
                                            character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Con chưa có đệ tử !"));
                                            return;
                                        }
                                        
                                        var limit = character.Disciple.InfoChar.LitmitPower;
                                        if (limit >= DataCache.MAX_LIMIT_POWER_LEVEL)
                                        {
                                            character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Đệ tử con đã đạt giới hạn tối đa"));
                                            return;
                                        }
                                        var LM = Cache.Gi().LIMIT_POWERS[limit];
                                        var ngoc = 100 * (limit + 1);
                                        var text = string.Format(TextServer.gI().UPGRADE_LEVEL_ME, ServerUtils.GetPower(LM.Power), ngoc);
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, text, MenuNpc.Gi().MenuQuocVuong[1], character.Disciple.InfoChar.Gender));
                                        character.TypeMenu = 2;
                                    }
                                    catch (Exception)
                                    {
                                        character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Đệ tử con đã đạt giới hạn tối đa"));
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        var limit = character.Disciple.InfoChar.LitmitPower;
                        if (limit >= DataCache.LITMIT_OPEN_NEED_GOD_ITEM)
                        {
                            var countItem = 0;
                            foreach (var item in character.ItemBody)
                            {
                                if (item != null && ItemCache.ItemTemplate(item.Id).Level == 13)
                                {
                                    countItem++;
                                }
                            }
                            if (countItem == 0)
                            {
                                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Để mở giới hạn sức mạnh này con phải đạt cấp độ Giới Vương Thần\nvà mặc trên người ít nhất 1 trong 5 món trang bị Thần\ngồm Áo, Quần, Găng, Giày, Nhẫn", new List<string> { "OK" }, character.InfoChar.Gender));
                                character.TypeMenu = 3;
                                return;
                            }
                            else
                            {
                                var ngoc2 = 100 * (limit + 1);
                                if (ngoc2 > character.AllDiamond())
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                                    return;
                                }

                                character.Disciple.InfoChar.IsPower = true;
                                character.Disciple.InfoChar.LitmitPower += 1;
                                character.MineDiamond(ngoc2);
                                character.Disciple.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                character.Disciple.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Chúc mừng con đạt tới sức mạnh mới"));
                            }
                        }else{
                        if (limit == DataCache.MAX_LIMIT_POWER_LEVEL)
                        {
                            character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Con đã đạt giới hạn tối đa"));
                            return;
                        }
                        if (character.InfoChar.Power < 17999999999)
                        {
                            character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Con chưa đủ sức mạnh để mới giới hạn"));
                            return;
                        }
                        var ngoc = 100 * (limit + 1);
                        if (ngoc > character.AllDiamond())
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                            return;
                        }

                        character.Disciple.InfoChar.IsPower = true;
                        character.Disciple.InfoChar.LitmitPower += 1;
                        character.MineDiamond(ngoc);
                        character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                        character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Chúc mừng con đạt tới sức mạnh mới"));
                    }
                    }
                    break;
                case 1:
                {
                        switch (select)
                        {
                            case 0:
                                {
                                    break;
                                }
                            case 1:
                                {
                                    var limit = character.InfoChar.LitmitPower;
                                    if (limit >= DataCache.LITMIT_OPEN_NEED_GOD_ITEM)
                                    {
                                        var countItem = 0;
                                        foreach(var item in character.ItemBody)
                                        {
                                            if (item != null && ItemCache.ItemTemplate(item.Id).Level == 13)
                                            {
                                                countItem++;
                                            }
                                        }
                                        if (countItem == 0)
                                        {
                                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Để mở giới hạn sức mạnh này con phải đạt cấp độ Giới Vương Thần\nvà mặc trên người ít nhất 1 trong 5 món trang bị Thần\ngồm Áo, Quần, Găng, Giày, Nhẫn", new List<string> { "OK"}, character.InfoChar.Gender));
                                            character.TypeMenu = 3;
                                            return;
                                        }
                                        else
                                        {
                                            var ngoc2 = 100 * (limit + 1);
                                            if (ngoc2 > character.AllDiamond())
                                            {
                                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                                                return;
                                            }

                                            character.InfoChar.IsPower = true;
                                            character.InfoChar.LitmitPower += 1;
                                            character.MineDiamond(ngoc2);
                                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                            character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Chúc mừng con đạt tới sức mạnh mới"));
                                        }
                                    }else{
                                    if (limit == DataCache.MAX_LIMIT_POWER_LEVEL)
                                    {
                                        character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Con đã đạt giới hạn tối đa"));
                                        return;
                                    }
                                    if (character.InfoChar.Power < 17999999999)
                                    {
                                        character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Con chưa đủ sức mạnh để mới giới hạn"));
                                        return;
                                    }
                                    var ngoc = 100 * (limit + 1);
                                    if (ngoc > character.AllDiamond())
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                                        return;
                                    }

                                    character.InfoChar.IsPower = true;
                                    character.InfoChar.LitmitPower += 1;
                                    character.MineDiamond(ngoc);
                                    character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                                    character.CharacterHandler.SendMessage(Service.NpcChat(npcId, "Chúc mừng con đạt tới sức mạnh mới"));
                                    }
                                    break;
                                }
                        }
                    break;
                }
            }
        }

        private static void ConfirmGiuMa(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                //Menu chính
                case 1:
                    {
                        switch (select)
                        {
                            case 0:
                               
                                var clan = ClanManager.Get(character.ClanId);
                                if (clan.ClanBoss.Close) return;
                                switch (clan.ClanBoss.Count)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        character.MineDiamond(100);
                                        break;
                                    case >= 2:
                                        character.MineDiamond(300);
                                        break;
                                }
                                clan.ClanBoss.Start(clan);
                                clan.ClanZone.Maps[0].OutZone(character, clan.ClanZone.Maps[1].Id);
                                clan.ClanZone.Maps[1].JoinZone(character, 0);
                                character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage($"Hạ Boss Bang Hội [lần thứ {clan.ClanBoss.Level+1}] thời gian:", 200, ((int)(clan.ClanBoss.Time - ServerUtils.CurrentTimeMillis()) / 1000)));
                                break;
                        }
                        break;
                    }
                case 0:
                    {
                        switch (select)
                        {
                            case 0:
                                {
                                    var clan = ClanManager.Get(character.ClanId);
                                    if (clan.ClanBoss.Open)
                                    {
                                        clan.ClanZone.Maps[0].OutZone(character, clan.ClanZone.Maps[1].Id);
                                        clan.ClanZone.Maps[1].JoinZone(character, 0);
                                        character.CharacterHandler.SendMessage(Service.ItemTimeWithMessage($"Hạ Boss Bang Hội [lần thứ {clan.ClanBoss.Level + 1}] thời gian:", 200, ((int)(clan.ClanBoss.Time - ServerUtils.CurrentTimeMillis()) / 1000)));
                                        break;
                                    }
                                    if (clan.ClanBoss.Close)
                                    {
                                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, "Bạn đã chiến thắng hôm nay, mai hãy quay lại nhé ", new List<string> { "OK" }, character.InfoChar.Gender));

                                        character.TypeMenu = 1;
                                        break;
                                    }
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextGiuMa[1], MenuNpc.Gi().MenuGiuMa[2 + clan.ClanBoss.Count >= 2 ? 2 : clan.ClanBoss.Count], character.InfoChar.Gender));
                                    character.TypeMenu = 1;
                                    break;
                                }
                            case 1:
                                {
                                    if (!character.InfoChar.isDiemDanh)
                                    {
                                        character.InfoChar.isDiemDanh = true;

                                        ClanManager.Get(character.ClanId).Capsule_Bang++;
                                        var me = ClanManager.Get(character.ClanId).Thành_viên.FirstOrDefault(i => i.Id == character.Id);
                                        me.Capsule_Cá_Nhân++;
                                        me.Capsule_Bang++;
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn đã nhận được 1 Capsule Bang"));
                                        break;
                                    }
                                    var clan = ClanManager.Get(character.ClanId);
                                    clan.ClanZone.Maps[0].OutZone(character, 156);
                                    character.InfoMore.TransportMapId = 156;
                                    character.CharacterHandler.SendMessage(Service.Transport(3, 1));
                                }
                                break;
                            case 2:
                                {
                                    if (!character.InfoChar.isDiemDanh)
                                    {
                                        var clan = ClanManager.Get(character.ClanId);
                                        clan.ClanZone.Maps[0].OutZone(character, 156);
                                        character.InfoMore.TransportMapId = 156;
                                        character.CharacterHandler.SendMessage(Service.Transport(3, 1));
                                    }
                                    break;
                                }
                        }
                    break;
                }   
            }
        }
        
        private static void ConfirmDuongTang(Character character, short npcId, int select)
        {
            var map = MapManager.Get(character.InfoChar.MapId);
            if (map == null) return;
            Threading.Map mapJoin = null;
            switch (character.TypeMenu)
            {
                //Menu chính
                case 0:
                    {
                       switch (select)
                        {
                            case 0:
                                mapJoin = MapManager.Get(123);
                                break;
                            case 1:
                                break;
                        }
                    }
                    if (mapJoin == null) return;
                    var zoneJoin = mapJoin.GetZoneNotMaxPlayer();
                    if (zoneJoin != null)
                    {
                        character.CharacterHandler.SendZoneMessage(Service.SendTeleport(character.Id, 2));
                        map.OutZone(character, mapJoin.Id);
                        character.InfoChar.X = 106;
                        character.InfoChar.Y = 384;
                        zoneJoin.ZoneHandler.JoinZone(character, false, false, 2);
                    }
                    else
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiSay(5, TextServer.gI().MAX_NUMCHARS, false, character.InfoChar.Gender));
                    }
                    break;
                case 1:
                    {
                        switch (select)
                        {
                            case 0:
                                mapJoin = MapManager.Get(0);
                                break;
                        }
                        
                    }
                    if (mapJoin == null) return;
                    var zoneJoin2 = mapJoin.GetZoneNotMaxPlayer();
                    if (zoneJoin2 != null)
                    {
                        character.CharacterHandler.SendZoneMessage(Service.SendTeleport(character.Id,2));
                        map.OutZone(character, mapJoin.Id);
                        zoneJoin2.ZoneHandler.JoinZone(character, false, true, 2);
                    }
                    else
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiSay(5, TextServer.gI().MAX_NUMCHARS, false, character.InfoChar.Gender));
                    }
                    break;

                case 2:
                    {
                        switch (select)
                        {
                            case 2:
                                break;
                            case 0:
                                if (character.InfoChar.Gold < 500000000)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("yêu cầu 500tr vàng"));
                                    return;
                                }
                                character.DiemSuKien++;
                                var giai = character.CharacterHandler.GetItemBagById(537);
                                var khai = character.CharacterHandler.GetItemBagById(538);
                                var phong = character.CharacterHandler.GetItemBagById(539);
                                var an = character.CharacterHandler.GetItemBagById(540);
                                if (giai != null && khai != null && phong != null && an != null) {
                                    if (giai.Quantity >= 10 && khai.Quantity >= 10 && phong.Quantity >= 10 && an.Quantity >= 10)
                                    {
                                        for (short dball = 537; dball <= 540; dball++)
                                        {
                                            character.CharacterHandler.RemoveItemBagById(dball, 10, "Ngũ Hành Sơn");
                                        }
                                        var randomRate = ServerUtils.RandomNumber(0.0, 100.0);
                                        var random = ServerUtils.RandomNumber(0.0, 100.0);
                                        var itemAdd2 = ItemCache.GetItemDefault(1);

                                        if (randomRate <= 20.0)
                                        {
                                            itemAdd2 = ItemCache.GetItemDefault(544);
                                        }
                                        else if (randomRate <= 40.0)
                                        {
                                            itemAdd2 = ItemCache.GetItemDefault(545);   
                                        }
                                        else if (randomRate <= 60.0)
                                        {
                                            itemAdd2 = itemAdd2 = ItemCache.GetItemDefault(546);
                                        }
                                      
                                        else
                                        {
                                            itemAdd2 = itemAdd2 = ItemCache.GetItemDefault(543);
                                        }
                                        itemAdd2.Reason = "Ngũ Hành Sơnnn";
                                        itemAdd2.Options.Add(new OptionItem()
                                       {
                                            Id = 77,
                                            Param = ServerUtils.RandomNumber(25, 50),
                                        }) ;
                                        itemAdd2.Options.Add(new OptionItem()
                                        {
                                            Id = 103,
                                            Param = ServerUtils.RandomNumber(25, 50),
                                        });
                                        itemAdd2.Options.Add(new OptionItem()
                                        {
                                            Id = 50,
                                            Param = ServerUtils.RandomNumber(25, 50),
                                        });
                                        itemAdd2.Options.Add(new OptionItem()
                                        {
                                            Id = 101,
                                            Param = ServerUtils.RandomNumber(25, 50),
                                        });
                                        if (random <= 10)
                                        {

                                            itemAdd2.Options.Add(new OptionItem()
                                            {
                                                Id = 95,
                                                Param = ServerUtils.RandomNumber(25, 50),
                                            });
                                        } else if (random <= 30)
                                        {
                                            itemAdd2.Options.Add(new OptionItem()
                                            {
                                                Id = 96,
                                                Param = ServerUtils.RandomNumber(25, 50),
                                            });
                                        }
                                        itemAdd2.Quantity = 1;
                                        character.CharacterHandler.AddItemToBag(true, itemAdd2, "SuKien");
                                        character.CharacterHandler.SendMessage(Service.SendBag(character));
                                        var template2 = ItemCache.ItemTemplate(itemAdd2.Id);
                                        character.CharacterHandler.SendMessage(
                                        Service.ServerMessage(string.Format(TextServer.gI().ADD_ITEM,
                                         $"x{itemAdd2.Quantity} {template2.Name}")));
                                        character.MineGold(500000000);
                                        character.CharacterHandler.SendMessage(Service.BuyItem(character));
                                        break;
                                    } else
                                    {
                                        character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ Vật Phẩm"));
                                    }
                                } else
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage("Bạn Không Có Bùa"));
                                }
                                break;
                        }
                    }
                    break;
            }
        }
        private static void ComfirmBabiday(Character character, short npcId, int select)
        {
            switch (select)
            {
                case 0:
                    break;
                case 1:
                    character.MineDiamond(1);
                    break;
                case 2:
                    if (character.PPower >= 20)
                    {
                        if (character.InfoChar.MapId == 115) MapManager.JoinMap(character, 117, ServerUtils.RandomNumber(20), false, false, 0);
                        else if (character.InfoChar.MapId == 120) MapManager.JoinMap(character, 52, ServerUtils.RandomNumber(20), false, false, 0);
                        else MapManager.JoinMap(character,  character.InfoChar.MapId + 1, ServerUtils.RandomNumber(20), false, false, 0);
                        character.PPower = 0;
                    }
                    else
                    {
                        character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ TL"));
                    }
                    break;
                case 3:
                    MapManager.JoinMap(character, 52, ServerUtils.RandomNumber(20), false, false, 0);
                    break;
            }
        }
        private static void ConfirmOsin(Character character, short npcId, int select)
        {
            var map = MapManager.Get(character.InfoChar.MapId);
            if (map == null) return;
            Threading.Map mapJoin = null;
            switch (character.TypeMenu)
            {
                //Menu chính
                case 0:
                    {
                        switch (select)
                        {
                            case 0:
                                switch (character.InfoChar.MapId)
                                {
                                    case 0 or 7 or 14:
                                        break;
                                    case 50:
                                        character.MapPrivate.Maps[5].OutZone(character, 48);
                                        character.MapPrivate.Maps[3].JoinZone(character, 0);
                                        break;
                                    case 155:
                                        {
                                            MapManager.Get(character.InfoChar.MapId).OutZone(character, 154);
                                            character.MapPrivate.GetMapById(154).JoinZone(character, 0);
                                        }
                                        break;

                                    case 127:
                                        {
                                            if (character.DataEnchant.PhuHoMabu2h)
                                            {
                                                character.CharacterHandler.SendMessage(Service.ServerMessage("Ngươi đã được phù hộ rồi !"));
                                            }
                                            else
                                            {
                                                character.DataEnchant.PhuHoMabu2h = true;
                                                character.CharacterHandler.SetUpInfo();
                                                character.CharacterHandler.SendMessage(Service.MeLoadPoint(character));
                                            }
                                            break;
                                        }
                                    default:
                                        if (Mabu12h.gI().InitMabu12h)
                                        {
                                            Mabu12h.gI().Join(character);
                                        }
                                        else if (Mabu2h.gI().InitMabu2h)
                                        {
                                            mapJoin = MapManager.Get(127);
                                        }
                                        break;
                                }
                                break;
                            case 1:
                                switch (character.InfoChar.MapId)
                                {
                                    case 50:
                                        {
                                            character.MapPrivate.GetMapById(50).OutZone(character, 154);
                                            character.MapPrivate.GetMapById(154).JoinZone(character, 0);
                                        }
                                        break;
                                    case 154:
                                        {
                                            mapJoin = MapManager.Get(155);
                                            character.MapPrivate.GetMapById(154).OutZone(character, mapJoin.Id);

                                        }
                                        break;
                                    case 127:

                                        break;
                                    default:
                                        character.MineDiamond(1);
                                        break;
                                }
                                break;
                            case 2:
                                switch (character.InfoChar.MapId)
                                {
                                    case 127:
                                        {
                                            mapJoin = MapManager.Get(52);
                                        }
                                        break;
                                    default:
                                        if (character.PPower >= 20)
                                        {
                                            if (character.InfoChar.MapId == 115) MapManager.JoinMap(character, 117, ServerUtils.RandomNumber(20), false, false, 0);
                                            else if (character.InfoChar.MapId == 120) MapManager.JoinMap(character, 52, ServerUtils.RandomNumber(20), false, false, 0);
                                            else MapManager.JoinMap(character, character.InfoChar.MapId + 1, ServerUtils.RandomNumber(20), false, false, 0);
                                            character.PPower = 0;
                                        }
                                        else
                                        {
                                            character.CharacterHandler.SendMessage(Service.ServerMessage("Không đủ TL"));
                                        }
                                        break;
                                }
                                break;
                        }
                    }
                    if (mapJoin == null) return;
                    var zoneJoin = mapJoin.GetZoneNotMaxPlayer();
                    if (zoneJoin != null)
                    {
                        character.CharacterHandler.SendZoneMessage(Service.SendTeleport(character.Id, character.InfoChar.Teleport));
                        map.OutZone(character, mapJoin.Id);
                        zoneJoin.ZoneHandler.JoinZone(character, false, true, character.InfoChar.Teleport);
                    }
                    else
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiSay(5, TextServer.gI().MAX_NUMCHARS, false, character.InfoChar.Gender));
                    }
                    break;
                case 1:
                    break;

                case 2:
                   
                    break;
            }
        }
        private static void ConfirmQuaTrung(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0://Chua đủ thời gian
                {
                    switch(select)
                    {
                        case 0://Chờ, bỏ qua
                        {
                            break;
                        }
                        case 1://Dùng tiền để nở trứng
                        {
                            var disciple = character.Disciple;
                            if (disciple != null)
                            {
                                var itemDiscipleBody = disciple.ItemBody.FirstOrDefault(item => item != null);

                                if (itemDiscipleBody != null)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().PLEASE_EMPTY_DISCIPLE_BODY));
                                    return;
                                }
                            }
                            // Kiểm tra trạng thái hợp thể
                            if (character.InfoChar.Fusion.IsFusion)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().PLEASE_NOT_FUSION));
                                return;
                            }

                            if (character.InfoChar.Gold < DataCache.GIA_NO_TRUNG_MA_BU)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_GOLD));
                                return;
                            }

                            // Kiểm tra sức mạnh đệ tử 20 tỷ
                            if (character.Disciple != null && character.Disciple.InfoChar.Power < 160000000)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().DISCIPLE_NOT_ENOUGH_POWER_TO_OPEN_EGG));
                                return;
                            }

                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuaTrung[1], MenuNpc.Gi().MenuQuaTrung[2], character.InfoChar.Gender));
                            character.TypeMenu = 2;
                            break;
                        }
                    }
                    break;
                }
                case 1: //Menu đủ thời gian
                {
                    switch(select)
                    {
                        case 0: //Nở trứng
                        {
                            var disciple = character.Disciple;
                            if (disciple != null)
                            {
                                var itemDiscipleBody = disciple.ItemBody.FirstOrDefault(item => item != null);

                                if (itemDiscipleBody != null)
                                {
                                    character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().PLEASE_EMPTY_DISCIPLE_BODY));
                                    return;
                                }
                            }
                            // Kiểm tra trạng thái hợp thể
                            if (character.InfoChar.Fusion.IsFusion)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().PLEASE_NOT_FUSION));
                                return;
                            }

                            if (character.Disciple != null && character.Disciple.InfoChar.Power < 160000000)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().DISCIPLE_NOT_ENOUGH_POWER_TO_OPEN_EGG));
                                return;
                            }

                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextQuaTrung[1], MenuNpc.Gi().MenuQuaTrung[2], character.InfoChar.Gender));
                            character.TypeMenu = 3;
                            break;
                        }
                    }
                    break;
                }
                case 2:
                {
                    character.MineGold(DataCache.GIA_NO_TRUNG_MA_BU);
                    CreateDiscipleMabu(character, (sbyte)select);
                    break;
                }
                case 3:
                {
                    CreateDiscipleMabu(character, (sbyte)select);
                    break;
                }
            }
        }
        
        private static void ConfirmBill(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                {
                    if (select != 0) return;
                    var fullThucAn = character.ItemBag.FirstOrDefault(item => DataCache.ListThucAn.Contains(item.Id) && item.Quantity >= 99);

                    if (character.InfoSet.IsFullSetThanLinh && fullThucAn != null)
                    {
                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBill[2], MenuNpc.Gi().MenuBill[2], character.InfoChar.Gender));
                        character.TypeMenu = 2;
                    }
                    else 
                    {   
                        character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextBill[1], MenuNpc.Gi().MenuBill[1], character.InfoChar.Gender));
                        character.TypeMenu = 1;
                    }
                    break;
                }   
                case 2:
                {
                    if (select != 0) return;
                    character.CharacterHandler.SendMessage(Service.Shop(character, 3, 21));
                    character.ShopId = 21;
                    character.TypeShop = 3;
                    break;
                }
            }
        }
        private static void ConfirmCauCa(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0: //Menu Shop
                    {
                        switch (select)
                        {
                            case 0: //Menu Shop
                                {
                                    character.CharacterHandler.SendMeMessage(Service.Shop(character, 0, 41));
                                    character.ShopId = 41;
                                    character.TypeShop = 0;
                                    break;
                                }
                            case 1: //Hướng dẫn thêm
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId, MenuNpc.Gi().TextCauCa[2], MenuNpc.Gi().MenuCauCa[2], character.InfoChar.Gender));
                                    character.TypeMenu = 2;
                                    break;
                                }
                        } 
                        break;
                        
                    }
                case 1: //MenuCauca
                    {
                        switch (select)
                        {
                            case 0: // Mồi thường
                                {
                                    HandlerCauCa.UseCanCau(character, 0);
                                    break;
                                }
                            case 1://Mồi đặc biệt
                                {
                                    HandlerCauCa.UseCanCau(character, 1);
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        switch (select)
                        {
                            case 0:
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(5, MenuNpc.Gi().TextCauCa[3]));
                                    break;
                                }
                            case 1:
                                {
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Giờ đã có thể bắt đầu câu cá. Chat 'cauca' để bắt đầu"));
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
        private static void ConfirmBXH(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                    {
                        switch (select)
                        {
                            case 0:
                                {
                                    var bangXepHangTopNap = Server.Gi().BangXepHang.GetListTopNap();
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, bangXepHangTopNap));
                                    break;
                                }
                            case 1:
                                {
                                    var bangXepHangTopSM = Server.Gi().BangXepHang.GetList();
                                    character.CharacterHandler.SendMessage(Service.OpenUiSay(npcId, bangXepHangTopSM));
                                    break;
                                }
                        }
                        break;
                    }
            }    
        }
        private static void ConfirmNoiBanh(Character character, short npcId, int select)
        {
            switch (character.TypeMenu)
            {
                case 0:
                {
                    switch (select)
                    {
                        case 0:
                        { // nấu bằng ngọc type menu 1
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId,
                                MenuNpc.Gi().TextNoiBanh[1], MenuNpc.Gi().MenuNoiBanh[1], character.InfoChar.Gender));
                            character.TypeMenu = 1;
                            break;
                        }
                        case 1:
                        { // nấu bằng vàng type menu 2
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(npcId,
                                MenuNpc.Gi().TextNoiBanh[2], MenuNpc.Gi().MenuNoiBanh[1], character.InfoChar.Gender));
                            character.TypeMenu = 2;
                            break;
                        }
                    }

                    break;
                    }   
                case 1: // xử lý client đã chọn nấu bằng ngọc
                {
                    switch (select)
                    {
                        case 0:
                        { // kiểm tra trước tránh null chết sv
                            if (character.CharacterHandler.GetItemBagById(886) == null || character.CharacterHandler.GetItemBagById(886) == null || character.CharacterHandler.GetItemBagById(887) == null || character.CharacterHandler.GetItemBagById(888) == null || character.CharacterHandler.GetItemBagById(889) == null)
                            {
                                character.CharacterHandler.SendMessage(
                                    Service.ServerMessage("Bạn không có đủ nguyên liệu"));
                                break;
                            }

                            var randtile = ServerUtils.RandomNumber(1, 10);
                            if (character.CharacterHandler.GetItemBagById(886).Quantity < 10 ||
                                character.CharacterHandler.GetItemBagById(887).Quantity < 10 ||
                                character.CharacterHandler.GetItemBagById(888).Quantity < 10 ||
                                character.CharacterHandler.GetItemBagById(889).Quantity < 10)
                            {
                                character.CharacterHandler.SendMessage(
                                    Service.ServerMessage("Bạn không có đủ nguyên liệu"));
                            } else if (character.InfoChar.Diamond < 100)
                            {
                                character.CharacterHandler.SendMessage(
                                    Service.ServerMessage("Bạn không có đủ ngọc"));
                            } else if (character.LengthBagNull() < 1)
                            {
                                character.CharacterHandler.SendMessage(
                                    Service.ServerMessage("Hành trang cần ít nhất 1 ô trống"));
                            } else if (randtile < 5)
                            {
                                character.CharacterHandler.SendMessage(Service.NpcChat(npcId,
                                    "Ohhh nooo, bánh đã bị hỏng rồi... chúc bạn may mắn lần sau ^^"));
                                character.CharacterHandler.RemoveItemBagById(886, 10, reason:"Nấu bánh");
                                character.CharacterHandler.RemoveItemBagById(887, 10, reason:"Nấu bánh");
                                character.CharacterHandler.RemoveItemBagById(888, 10, reason:"Nấu bánh");
                                character.CharacterHandler.RemoveItemBagById(889, 10, reason:"Nấu bánh");
                                character.MineDiamond(100);
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.NpcChat(npcId,
                                    "Chúc mừng bạn đã làm bánh thành công"));
                                character.CharacterHandler.RemoveItemBagById(886, 10, reason:"Nấu bánh");
                                character.CharacterHandler.RemoveItemBagById(887, 10, reason:"Nấu bánh");
                                character.CharacterHandler.RemoveItemBagById(888, 10, reason:"Nấu bánh");
                                character.CharacterHandler.RemoveItemBagById(889, 10, reason:"Nấu bánh");
                                character.MineDiamond(100);
                                var itemAdd = ItemCache.GetItemDefault(1191);
                                character.CharacterHandler.AddItemToBag(true, itemAdd, "Làm bánh");
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                            }
                            break;
                        }
                        case 1:
                        { 
                            //hủy
                            break;
                        }
                    }

                    break;
                }
                case 2: //xử lý client đã chọn nấu bằng vàng
                {
                    switch (select)
                    {
                        case 0:
                        { // kiểm tra trước tránh null chết sv
                            if (character.CharacterHandler.GetItemBagById(886) == null || character.CharacterHandler.GetItemBagById(886) == null || character.CharacterHandler.GetItemBagById(887) == null || character.CharacterHandler.GetItemBagById(888) == null || character.CharacterHandler.GetItemBagById(889) == null)
                            {
                                character.CharacterHandler.SendMessage(
                                    Service.ServerMessage("Bạn không có đủ nguyên liệu"));
                                break;
                            }
                            
                            if (character.CharacterHandler.GetItemBagById(886).Quantity < 10 ||
                                character.CharacterHandler.GetItemBagById(887).Quantity < 10 ||
                                character.CharacterHandler.GetItemBagById(888).Quantity < 10 ||
                                character.CharacterHandler.GetItemBagById(889).Quantity < 10)
                            {
                                character.CharacterHandler.SendMessage(
                                    Service.ServerMessage("Bạn không có đủ nguyên liệu"));
                            } else if (character.InfoChar.Gold < 25000000)
                            {
                                character.CharacterHandler.SendMessage(
                                    Service.ServerMessage("Bạn không có đủ vàng"));
                            } else if (character.LengthBagNull() < 1)
                            {
                                character.CharacterHandler.SendMessage(
                                    Service.ServerMessage("Hành trang cần ít nhất 1 ô trống"));
                            }
                            else
                            {
                                character.CharacterHandler.SendMessage(Service.NpcChat(npcId,
                                    "Chúc mừng bạn đã làm bánh thành công"));
                                character.CharacterHandler.RemoveItemBagById(886, 10, reason:"Nấu bánh");
                                character.CharacterHandler.RemoveItemBagById(887, 10, reason:"Nấu bánh");
                                character.CharacterHandler.RemoveItemBagById(888, 10, reason:"Nấu bánh");
                                character.CharacterHandler.RemoveItemBagById(889, 10, reason:"Nấu bánh");
                                character.MineGold(2500000);
                                var itemAdd = ItemCache.GetItemDefault(1191);
                                character.CharacterHandler.AddItemToBag(true, itemAdd, "Làm bánh");
                                character.CharacterHandler.SendMessage(Service.SendBag(character));
                                character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                            }
                            break;
                        }
                        case 1:
                        { 
                            //hủy
                            break;
                        }
                    }

                    break;
                }
                
            }
        }
        #endregion
        #region Menu NOT COFIRM

        private static void MenuDauThan(Character character, int npcId, int menuId, int optionId)
        {
            var magicTree = MagicTreeManager.Get(character.Id);
            if(magicTree == null) return;
            lock (magicTree)
            {
                switch (menuId)
                {
                    //Thu hoạch // Dùng ngọc nâng cấp
                    case 0:
                    {
                        if (magicTree.IsUpdate)
                        {
                            var ngoc = magicTree.Diamond;
                            if (character.AllDiamond() < ngoc)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                                return;
                            }
                            character.MineDiamond(ngoc);
                            
                            magicTree.IsUpdate = false;
                            magicTree.Level++;
                            switch (magicTree.Level)
                            {
                                case < 8:
                                    magicTree.NpcId++;
                                    break;
                                case >= 10:
                                    magicTree.Level = 10;
                                    break;
                            }

                            magicTree.MaxPea += 2; 
                            magicTree.Peas = magicTree.MaxPea;
                            magicTree.Seconds = 0;
                            magicTree.Diamond = 0;
                            // MagicTreeDB.Update(magicTree);

                            magicTree.MagicTreeHandler.HandleNgoc();
                            character.CharacterHandler.SendMessage(Service.MagicTree0(magicTree));
                            character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                            MagicTreeDB.Update(magicTree);
                        }
                        else
                        {
                            if(magicTree.Peas == 0) return;
                            var quantityPea = magicTree.Peas;
                            var emptyBag = 100 - character.GetTotalDauThanBag();
                            var emptyBox = 200 - character.GetTotalDauThanBox();
                            var totalEmpty = emptyBag + emptyBox;
                            if (emptyBag <= 0 && emptyBox <= 0)
                            {
                                character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().MAX_PEAS));
                                return;
                            }
                            if(quantityPea > 0 && emptyBag > 0) {
                                if(quantityPea < emptyBag) {
                                    emptyBag = quantityPea;
                                    quantityPea = 0;
                                } else {
                                    quantityPea -= emptyBag;
                                }
                                var item = ItemCache.GetItemDefault((short)DataCache.IdDauThan[magicTree.Level - 1], emptyBag);
                                if(character.CharacterHandler.AddItemToBag(true, item, "Thu hoạch đậu")) {
                                    character.CharacterHandler.SendMessage(Service.SendBag(character));
                                }
                            }
                            if(quantityPea > 0 && emptyBox > 0) {
                                if(quantityPea < emptyBox) {
                                    emptyBox = quantityPea;
                                    quantityPea = 0;
                                } else {
                                    quantityPea -= emptyBox;
                                }
                                var item = ItemCache.GetItemDefault((short)DataCache.IdDauThan[magicTree.Level - 1], emptyBox);
                                if(character.CharacterHandler.AddItemToBox(true, item)) {
                                    character.CharacterHandler.SendMessage(Service.SendBox(character));
                                }
                            }

                            if (totalEmpty > 0)
                            {
                                character.CharacterHandler.SendMessage(Service.MagicTree0(magicTree));
                            }
                            magicTree.Peas = quantityPea;
                             
                            magicTree.Seconds = 60000 * magicTree.Level + ServerUtils.CurrentTimeMillis();
                            magicTree.IsUpdate = false;
                            magicTree.MagicTreeHandler.HandleNgoc();
                            character.CharacterHandler.SendMessage(Service.MagicTree2(quantityPea, magicTree.Level));
                        }
                        break;
                    }
                    //Nâng cấp
                    case 1:
                    {
                        if (magicTree.Level == 10)
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiSay(5, "Đậu thần đã đạt đến cấp độ tối đa", false, character.InfoChar.Gender));
                            return;
                        }
                        if (magicTree.IsUpdate)
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(5, MenuNpc.Gi().TextMeo[1], MenuNpc.Gi().MenuMeo[0], character.InfoChar.Gender));
                            character.TypeMenu = 2;
                        }
                        else
                        {
                            character.CharacterHandler.SendMessage(Service.OpenUiConfirm(5, MenuNpc.Gi().TextMeo[0], MenuNpc.Gi().MenuMeo[0], character.InfoChar.Gender));
                            character.TypeMenu = 1;
                        }
                        break;
                    }
                    //Kết hạt nhanh
                    case 2:
                    {
                        if(magicTree.IsUpdate || magicTree.Peas == magicTree.MaxPea) return;
                        var ngoc = magicTree.Diamond;
                        if (character.AllDiamond() < ngoc)
                        {
                            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().NOT_ENOUGH_DIAMOND));
                            return;
                        }
                        character.MineDiamond(ngoc);
                        magicTree.Peas = magicTree.MaxPea;
                        magicTree.Seconds = 0;
                        magicTree.IsUpdate = false;
                        magicTree.MagicTreeHandler.HandleNgoc();
                        character.CharacterHandler.SendMessage(Service.MagicTree0(magicTree));
                        character.CharacterHandler.SendMessage(Service.MeLoadInfo(character));
                        break;
                    }
                }
            }
            
        }
        

        #endregion
    
        #region Function
        public static void CreatePetNormal(Character character)
        {
            var detu = new Disciple();
            detu.CreateNewDisciple(character, ServerUtils.RandomNumber(0,2));
            detu.Player = character.Player;
            detu.CharacterHandler.SetUpInfo();
            
            character.Disciple = detu;
            character.InfoChar.IsHavePet = true;
            character.CharacterHandler.SendMessage(Service.Disciple(1, null));
            character.Zone.ZoneHandler.AddDisciple(detu);
            DiscipleDB.Create(detu);
        }
        private static void CreateDiscipleCumber(Character character, sbyte gender)
        {
            // Nếu có đệ thì đổi đệ
            var oldDisciple = character.Disciple;
            if (oldDisciple != null || DiscipleDB.IsAlreadyExist(-character.Id))
            {
                character.CharacterHandler.SendMessage(Service.OpenUiConfirm(5, "Bạn có muốn đổi đệ tử hiện tại để đổi lấy đệ tử Cumber?", MenuNpc.Gi().MenuQuyLao[3], character.InfoChar.Gender));
                character.TypeMenu = 27;
                oldDisciple = new Disciple();
                oldDisciple.CreatePet(character, 3, gender);
                oldDisciple.Player = character.Player;
                oldDisciple.CharacterHandler.SetUpInfo();
                character.Disciple = oldDisciple;
                DiscipleDB.Update(oldDisciple);
            }
            // không có thì tạo mới
            else
            {
                var disciple = new Disciple();
                disciple.CreatePet(character, 3,gender);
                disciple.Player = character.Player;
                disciple.CharacterHandler.SetUpInfo();
                character.Disciple = disciple;
                character.InfoChar.IsHavePet = true;
                character.CharacterHandler.SendMessage(Service.Disciple(1, null));
                DiscipleDB.Create(disciple);
            }
        }
        private static void CreateDiscipleMabu(Character character, sbyte gender)
        {
            // Nếu có đệ thì đổi đệ
            var oldDisciple = character.Disciple;
            if (oldDisciple != null || DiscipleDB.IsAlreadyExist(-character.Id))
            {
                oldDisciple = new Disciple();
                oldDisciple.CreateNewMaBuDisciple(character, gender);
                oldDisciple.Player = character.Player;
                oldDisciple.CharacterHandler.SetUpInfo();
                character.Disciple = oldDisciple;
                DiscipleDB.Update(oldDisciple);
            }
            // không có thì tạo mới
            else
            {
                var disciple = new Disciple();
                disciple.CreateNewMaBuDisciple(character, gender);
                disciple.Player = character.Player;
                disciple.CharacterHandler.SetUpInfo();
                character.Disciple = disciple;
                character.InfoChar.IsHavePet = true;
                character.CharacterHandler.SendMessage(Service.Disciple(1, null));
                DiscipleDB.Create(disciple);
            }
            
            // var oldDisciple = character.Disciple;
            // if (oldDisciple != null)
            // {
            //     DiscipleDB.Delete(oldDisciple.Id);
            //     character.CharacterHandler.SendMessage(Service.Disciple(0, null)); 
            //     character.InfoChar.IsHavePet = false;
            //     character.Disciple = null;
            // }
            character.CharacterHandler.SendMessage(Service.NoTrungMaBu());
            character.InfoChar.ThoiGianTrungMaBu = 0;

            // Thread.Sleep(3000);
            character.CharacterHandler.SendMessage(Service.ServerMessage(TextServer.gI().GET_NEW_MABU_DISCIPLE));
            
        }
        #endregion
    }
}