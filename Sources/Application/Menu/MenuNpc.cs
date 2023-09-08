using System.Collections.Generic;
using System.Net.NetworkInformation;
using DragonBoyZ.Application.Constants;
using DragonBoyZ.Application.IO;
using DragonBoyZ.Sources.Application.Activity.CauCa;

namespace DragonBoyZ.Application.Handlers.Menu
{
    public class MenuNpc
    {
        private static MenuNpc _instance;

        public MenuNpc()
        {
            
        }

        public static MenuNpc Gi()
        {
            if (_instance == null) _instance = new MenuNpc();
            return _instance;
        }

        #region Menu Đổi vật phẩm
        public static string TextDoiThuong(Model.Character.Character character, string openingText, List<int> ItemId, List<int> Quantitys, int TypeDoiThuong, int countGold, int countDiamo)
        {
            List<string> ListText = new List<string>();
            ListText.Add($"{ServerUtils.Color("title_brown")}{openingText}");
            if (ItemId.Count != Quantitys.Count) return "";

            if (countGold == 0 && countDiamo == 0)
            {
                for (int i = 0; i < ItemId.Count; i++)
                {
                    if (character.CharacterHandler.GetItemBagById(ItemId[i]) != null)
                    {
                        var item = character.CharacterHandler.GetItemBagById(ItemId[i]);

                        if (item.Quantity >= Quantitys[i])
                        {
                            ListText.Add($"{ServerUtils.Color("blue")}{ItemCache.ItemTemplate((short)(ItemId[i])).Name} {item.Quantity}/{Quantitys[i]}");
                        }
                        else
                        {
                            TypeDoiThuong = -1;
                            ListText.Add($"{ServerUtils.Color("red")}{ItemCache.ItemTemplate((short)(ItemId[i])).Name} {item.Quantity}/{Quantitys[i]}");
                        }
                    }
                    else
                    {
                        ListText.Add($"{ServerUtils.Color("red")}{ItemCache.ItemTemplate((short)(ItemId[i])).Name} 0/{Quantitys[i]}");
                        TypeDoiThuong = -1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < ItemId.Count; i++)
                {
                    if (character.CharacterHandler.GetItemBagById(ItemId[i]) != null)
                    {
                        var item = character.CharacterHandler.GetItemBagById(ItemId[i]);

                        if (item.Quantity >= Quantitys[i])
                        {
                            ListText.Add($"{ServerUtils.Color("blue")}{ItemCache.ItemTemplate((short)(ItemId[i])).Name} {item.Quantity}/{Quantitys[i]}");
                        }
                        else
                        {
                            TypeDoiThuong = -1;
                            ListText.Add($"{ServerUtils.Color("red")}{ItemCache.ItemTemplate((short)(ItemId[i])).Name} {item.Quantity}/{Quantitys[i]}");
                        }
                    }
                    else
                    {
                        ListText.Add($"{ServerUtils.Color("red")}{ItemCache.ItemTemplate((short)(ItemId[i])).Name} 0/{Quantitys[i]}");
                        TypeDoiThuong = -1;
                    }
                }
                var gold = character.InfoChar.Gold;
                var diamo = character.InfoChar.Diamond;
                if (countGold != 0 && countDiamo != 0)
                {
                    if (gold >= countGold && diamo >= countDiamo)
                    {
                        ListText.Add($"{ServerUtils.Color("yellow")}Giá vàng: {ServerUtils.GetMoney(gold)}/{countGold}");
                        ListText.Add($"{ServerUtils.Color("green")}Giá ngọc: {ServerUtils.GetMoney(diamo)}/{countDiamo}");
                    }
                    else
                    {
                        TypeDoiThuong = -1;
                        ListText.Add($"{ServerUtils.Color("red")}Giá vàng: {ServerUtils.GetMoney(gold)}/{countGold}");
                        ListText.Add($"{ServerUtils.Color("red")}Giá ngọc: {ServerUtils.GetMoney(diamo)}/{countDiamo}");
                    }
                }
                else
                {
                    if (countGold != 0)
                    {
                        if (gold >= countGold)
                        {
                            ListText.Add($"{ServerUtils.Color("yellow")}Giá vàng: {ServerUtils.GetMoney(gold)}/{countGold}");
                        }
                        else
                        {
                            TypeDoiThuong = -1;
                            ListText.Add($"{ServerUtils.Color("red")}Giá vàng: {ServerUtils.GetMoney(gold)}/{countGold}");
                        }
                    }
                    if (countDiamo != 0)
                    {
                        if (diamo >= countDiamo)
                        {
                            ListText.Add($"{ServerUtils.Color("green")}Giá ngọc: {ServerUtils.GetMoney(diamo)}/{countDiamo}");
                        }
                        else
                        {
                            TypeDoiThuong = -1;
                            ListText.Add($"{ServerUtils.Color("red")}Giá ngọc: {ServerUtils.GetMoney(diamo)}/{countDiamo}");
                        }
                    }
                }
            }
            character.TypeDoiThuong = TypeDoiThuong;
            var Text = "";
            for (int j = 0; j < ListText.Count; j++)
            {
                Text += ListText[j];
            }
            return Text;
        }
        public static List<string> MenuDoiThuong(Model.Character.Character character)
        {
            List<string> result = new List<string>(); // Tạo danh sách kết quả

            if (character.TypeDoiThuong != 3)
            {
                result.Add("Từ chối");
            }
            else
            {
                result.Add("Đổi");
            }

            return result;
        }

        #endregion

        #region Bảng xếp hạng
        public List<string> TextBXH = new List<string>()
        {
            "Bảng xếp hạng"
        };

        public List<List<string>> MenuBXH = new List<List<string>>()
        {
            new List<string>()
            {
                "BXH\nSự kiện\nTop nạp",
                "BXH\nSự kiện\nSức mạnh"
            },
        };
        #endregion

        #region Bò Mộng
        public List<string> TextBoMong = new List<string>()
        {
            "Nếu ngươi chịu khó làm nhiệm vụ, ta sẽ thưởng cho ngươi những viên ngọc xanh..."
        };
        public List<List<string>> MenuBoMong = new List<List<string>>()
        {
            new List<string>()
            {
                "Giftcode",
                "Nhận ngọc\nMiễn phí",
                "Nhiệm vụ\nHằng ngày"
            },
        };
        #endregion

        #region Ba Ông Già
        public List<string> TextBaOngGia = new List<string>()
        {
            "Ta có thể giúp gì cho con?",
            "Con đã học kỹ năng thành công",
            "Hãy chọn máy chủ con muốn đổi"
        };
        public List<List<string>> MenuBaOngGia = new List<List<string>>()
        {
            new List<string>()
            {
                "Bảng\nXếp Hạng",
                "Nhận\nNgọc xanh",
                "Nhận\nĐệ tử",
                "Đổi\nMật khẩu"
            },
            new List<string>()
            {
                "Top\nSức mạnh",
                "Top\nNạp"
            },
        };
        #endregion

        #region Menu Admin
        public List<string> TextMenuAdmin = new List<string>()
        {
            $"|7|Menu Admin TGDV",
        };
        public List<List<string>> MenuAdmin = new List<List<string>>()
        {
             new List<string>()//0
            {
                "Thông tin\nServer",
                "Gửi\nthông báo",
                "Menu\nBuff bẩn",
                "Quyền lực\nVip Pro"
            },
            new List<string>()//1
            {
                "Check\nGiftCode",
                "Player Online",
                "Thread",
            },
            new List<string>()//2
            {
                "Toàn Server",
                "Player",
                "Auto Chat NPC"
            },
            new List<string>()//3
            {
                "Buff Item",
                "Buff VND",
                "Buff TNSM",
                "Buff\nChỉ số",
                "Buff\nNhiệm vụ"
            },
            new List<string>()//4
            {
                "Gọi Boss",
                "Ban",
                "Kick",
                "Tele",
                "Kéo",
                "Kill"
            }
         };
        #endregion

        #region Event câu cá
        public List<string> TextCauCa = new List<string>()
        {
            $"{ServerUtils.Color("title_brown")}Giá cá này hôm nay:" +
            $"{ServerUtils.Color("green")}Cá nóc: {HandlerCauCa.GetGiaCa(1002)/1000000}tr vàng\nCá bảy màu: {HandlerCauCa.GetGiaCa(1003)/1000000}tr vàng\nCá diêu hồng: {HandlerCauCa.GetGiaCa(1004)/1000000}tr vàng",

            $"{ServerUtils.Color("title_brown")}Hãy chọn loại mồi để bắt đầu câu" +
            $"{ServerUtils.Color("green")}Mồi thường: {CacheCauCa.GiaMoiCauThuong/1000000}tr vàng {CacheCauCa.PercentNormal}% thành công" +
            $"{ServerUtils.Color("green")}Mồi đặc biệt: {CacheCauCa.GiaMoiCauDacBiet/1000000}tr vàng {CacheCauCa.PercentSpecial}% thành công",

            $"{ServerUtils.Color("title_brown")}Nếu không thể sử dụng cần câu hãy bấm vào 'Trợ giúp'",

            $"Hướng dẫn câu cá để sinh tồn tại DragonBoyZ ...\r\n" +
            $"Mua cần câu và chat 'cauca' để câu cá (Lưu ý phải đứng ở dưới biển mới có tác dụng)\r\n" +
            $"Với mồi câu đặc biệt sẽ tăng tỉ lệ ra cá đắt tiền hơn\r\n" +
            $"Chỉ khi sử dụng mồi đặc biệt mới có thể rơi ra phụ kiện đeo lưng, cải trang,...\r\n" +
            $"Giá cá sẽ thay đổi thường xuyên\r\n" +
            $"Chúc anh có trải nghiệm vui vẻ",

            $"{ServerUtils.Color("title_brown")}Hãy chọn phần thưởng"

        };
        public List<List<string>> MenuCauCa = new List<List<string>>()
        {
            new List<string>()
            {
                "Cửa hàng",
                "Đổi thưởng",
                "Hướng\ndẫn thêm"
            },
            new List<string>()
            {
                "Mồi câu thường",
                "Mồi đặc biệt",
            },
            new List<string>()
            {
                "Hướng dẫn",
                "Trợ giúp",
            },
            new List<string>()
            {
                "Xô\nCá xanh",
                "Xô\nCá vàng"
            },
        };
        public static List<List<string>> TextTrangThai = new List<List<string>>()
        {
            new List<string>() // Thành công
            {
                "À há! Cá đây rồi",
                "May ghê",
                "Thấy anh ghê hông"
            },
            new List<string>() // Thất bại
            {
                "Vãi l*n",
                "Móe",
                "Lừa rồi"
            }
        };
        #endregion

        public List<List<string>> MenuBroly = new List<List<string>>()
        {
            new List<string>()
            {
                "VIP 1",
                "VIP 2",
                "Từ chối",
            },
             new List<string>()
            {
               "50K VND",
               "Từ chối",
            },
             new List<string>()
            {
                 "100K VND",
                 "Từ chối",
            }
        };

        public List<string> TextMeo = new List<string>()
        {
            "Bạn có muốn nâng cấp đậu thần không",
            "Bạn có muốn huỷ nâng cấp đậu thần không (nhận lại 50% vàng)",
            "Bạn có muốn kết bạn với {0} không?",
            "Bạn có muốn xoá kết bạn với {0} không?",
            "Bạn có muốn dịch chuyển tới người chơi {0} không?\bTốn 10 ngọc để dịch chuyển và thời gian tự do là 20 phút",
            "Bạn có chắc chắn muốn rời bang hội ?",
            "{0} (sức mạnh {1})\nBạn muốn cược bao nhiêu vàng?",
            "Bạn có muốn xoá {0} khỏi danh sách thù địch không?",
            "Bạn chưa từng kích hoạt mã bảo vệ lần nào\nBạn có muốn dùng 50k vàng để kích hoạt không, mã bảo vệ của bạn là: {0}",
            "Bạn có muốn mở khoá rương không?",
            "Bạn có muốn khoá rương lại không?",
        };
        public List<List<string>> MenuHeThong = new List<List<string>>()
        {
            new List<string>()
            {
                "Hủy",
                "Trang\nSau",
            },
             new List<string>()
            {
                "Hủy",
               // "Trang\nSau",
            },
        };
        public List<List<string>> MenuOsin = new List<List<string>>()
        {
            new List<string>()
            {
                "Đến Kaio",
                "Đến\nHành tinh\nbill",
                "Từ chối",
            },
             new List<string>()
            {
              "Cừa hàng",
                "Đến\nhành tinh\nngục tù",
             "Từ chối",
            },
              new List<string>()
            {
              "Đến\nHành tinh\nBill",
              "Từ chối",
            },
        };
        
        public List<List<string>> MenuRobo = new List<List<string>>()
        {
            new List<string>()
            {
                "OK",
                "Từ chối",
            },

        };
        public List<List<string>> MenuMeo = new List<List<string>> ()
        {
            new List<string>()
            {
                "OK", 
                "Huỷ",
            },
            new List<string>()
            {
                "Đồng ý", 
                "Huỷ",
            },
            new List<string>()
            {
                "1,000\nvàng", 
                "10,000\nvàng", 
                "100,000\nvàng"
            },
        };

        public List<string> TextNoiBanh= new List<string>()
        {
            "Bạn muốn nấu bánh bằng gì?",
            "Để nấu bánh trung thu cần 10 trứng vịt muối, x10 bột mì, x10 gà quay, x10 đậu xanh + 100 ngọc. Bạn có đồng ý không?",
            "Để nấu bánh trung thu cần 10 trứng vịt muối, x10 bột mì, x10 gà quay, x10 đậu xanh + 25tr vàng. Bạn có đồng ý không?",
        };
        
        public List<List<string>> MenuNoiBanh = new List<List<string>>()
        {
            new List<string>()
            {
                "Bằng ngọc", 
                "Bằng vàng",
            },
            new List<string>()
            {
                "Đồng ý", 
                "Hủy",
            },
             new List<string>()
            {
                "Từ chối",
                
            },
        };
        public List<List<string>> MenuGokuVoThan = new List<List<string>>()
        {
            new List<string>()
            {
                "Nâng Áo",
                "Nâng Quần",
                 "Nâng Găng",
                  "Nâng Giày",
                   "Nâng Rada",
            },
             new List<string>()
            {
               "Nâng cấp",
               "Từ chối",
            },
             new List<string>()
            {
              "Hủy",
             },

        };

        public List<string> TextThanMeo= new List<string>()
        {
            "Muốn uống nước thánh không cưng?",
            $"{ServerUtils.Color("red")}Bảng đổi điểm sự kiện xem tại thông báo của NRO Thần Long \b"+
            // $"{ServerUtils.Color("blue")}300 điểm : x10 đá bảo vệ, 5 viên cs trung thu, 10 thỏi vàng\b" + 
            // $"{ServerUtils.Color("blue")}500 điểm : x15 đá bảo vệ, 10 viên cs trung thu, 10 thỏi vàng\b" + 
            // $"{ServerUtils.Color("blue")}1000 điểm : x20 đá bảo vệ, 25 viên cs trung thu, item Lồng đèn, 20 thỏi vàng\b" + 
            // $"{ServerUtils.Color("blue")}3000 điểm : x30 đá bảo vệ, x99 viên cs trung thu, item lồng đèn, 30 thỏi vàng\b" + 
            // $"{ServerUtils.Color("blue")}5000 điểm : x35 đá bảo vệ, x99 viên cs trung thu, item lồng đèn, 35 thỏi vàng, ngẫu nhiên (phóng lợn, mèo mun, v.v)\b" + 
            // $"{ServerUtils.Color("blue")}5000 điểm : x35 đá bảo vệ, x99 viên cs trung thu, item lồng đèn, 35 thỏi vàng, ngẫu nhiên (phóng lợn, mèo mun, v.v)\b" + 
            // $"{ServerUtils.Color("blue")}7000 điểm : x50 đá bảo vệ, x99 viên cs trung thu, item lồng đèn, 40 thỏi vàng, ngẫu nhiên item, trang bị hủy diệt 10% trở lên\b" + 
            // $"{ServerUtils.Color("blue")}10000 điểm : x99 đá bảo vệ, x99 viên cs trung thu, item lồng đèn, 50 thỏi vàng, ngẫu nhiên item, trang bị hủy diệt 12% trở lên\b" + 
            $"{ServerUtils.Color("red")}BẠN CHỈ CÓ THỂ ĐỔI ĐIỂM DUY NHẤT MỘT LẦN",
            "Vui lòng chọn loại lồng đèn muốn nhận?",
        };
        
        public List<List<string>> MenuThanMeo = new List<List<string>>()
        {
            new List<string>()
            {
                "Đổi quà sự kiện", 
                "Đổi quà tích nạp",
            },
            //đổi sự kiện
            new List<string>()
            {
                "Đổi", 
                "Đóng",
            },
            //chọn lồng đèn
            new List<string>()
            {
                "SỨC ĐÁNH", 
                "HP",
                "KI",
                "CHÍ MẠNG",
                "GIÁP",
            },
        };

        public List<string> TextBaHatMit = new List<string>()
        {
            "Ngươi tìm ta có việc gì ?",
            "|2|Con muốn biến 10 mảnh đá vụn thành\n1 viên đá nâng cấp ngẫu nhiên\b|1|Cần 10 Mảnh đá vụ\nCần 1 bình nước phép\b|2|Cần 2 k vàng",
            "Ngươi Muốn pha lê hoá trang bị bằng cách nào",
            "Ta sẽ biến trang bị mới cấp cao hơn của ngươi thành trang bị có cấp độ và sao pha lê của trang bị cũ",
            "Ngươi muốn đổi Capsule World Cup ?\n|0|Ngươi đang có {0} thẻ Fan gà nửa mùa\n{1} thẻ Fan cuồng bóng đá\n|6|Đổi Capsule thường: cần 10 thẻ Fan gà nửa mùa và 1tr vàng\n2)Đổi Capsule VIP:cần 10 thẻ Fan cuồng bóng đá và 500 ngọc"
        };
        public List<string> TextDuongTang = new List<string>()
        {
            "A mi phò phò, thí chủ hãy giúp giải cứu đồ đệ của bần tăng đang bị\nphong ấn tại ngũ hành sơn.\nLưu ý: Tiềm năng khi đánh quái trong Ngũ Hành Sơn là X2",
            "Thí chủ muốn trở về sao ?",
            "A mi phò phò, thí chủ thu nhập bùa '[Giải Khai Phong Ấn]',Mỗi chữ 10 cái",
        };
         public List<List<string>> MenuDuongTang = new List<List<string>>()
        {
            new List<string>()
            {
                "Đồng ý",
                "Từ chối",
                "Nhận thưởng",
            },
            new List<string>()
            {
                "Đồng ý", 
                "Từ chối",
            },
            new List<string> ()
            {
            "Giải\nPhong Ấn",
            "Về\nLàng Aru",
            "Top\nHoa Quả",
            }
        };
        public List<List<string>> MenuBaHatMit = new List<List<string>>()
        {
            new List<string>() // 0
            {
                "Thưởng Bùa Ngẫu Nhiên", 
                "Cửa hàng Bùa", 
                "Nâng cấp Vật phẩm", 
                "Làm phép Nhập đá", 
                "Nhập Ngọc Rồng",
                "Nâng cấp\nBông tai\nPorata"
            },
            new List<string>() // 1
            {
                "Cửa hàng Bùa", 
                "Nâng cấp Vật phẩm", 
                "Làm phép Nhập đá", 
                "Nhập Ngọc Rồng",
                "Nâng cấp\nBông tai\nPorata"
            },
            new List<string>() // 2
            {
                "Bùa 1h", 
                "Bùa 8h",
                "Bùa\n1 Tháng"
            },
            new List<string>() // 3
            {
                "Ép sao\ntrang bị", 
                "Pha lê\nhoá\ntrang bị", 
                "Chuyển hoá\nTrang bị",
               
                "Tinh chế\ntrang bị",
                "Phân giải\ntinh chế",
                "Chế tạo\nbồn tắm"
            },
            new List<string>() // 4
            {
                "Vào hành trang\nChọn trang bị\n(Áo,quần,găng,giày hoặc rada)\nChọn loại đá để nâng cấp\nSau đó chọn 'Nâng cấp'", 
                "Ta sẽ phù phép\ncho trang bị của ngươi\ntrở nên mạnh mẽ"
            },
            new List<string>() // 5
            {
                "Vào hành trang\nChọn 10 mảnh đá vụn\nChọn 1 bình nước phép\nSau đó chọn 'Làm phép'", 
                "Ta sẽ phù phép\ncho 10 mảnh đá vụn\ntrở thành 1 đá nâng cấp "
            },
            new List<string>() // 6
            {
                "Vào hành trang\nChọn 7 viên ngọc cùng sao\nSau đó chọn 'Làm phép'", 
                "Ta sẽ phù phép\ncho 7 viên Ngọc Rồng\nthành 1 viên Ngọc Rồng cấp cao"
            },
            new List<string>() // 7
            {
                "Làm phép\n2k vàng", 
                "Từ chối"
            },
            new List<string>()  // 8
            {
                "Vào hành trang\nChọn trang bị\n(Áo,quần,găng,giày hoặc rada)\nSau đó chọn 'Nâng cấp'", 
                "Ta sẽ phù phép\ncho trang bị của ngươi\ntrở thành trang bị pha lê"
            },
 

            new List<string>()//9
            {
                "Bằng ngọc", 
                "Từ chối"
            }, 

            new List<string>() // 10 
            {
                "Chuyển hoá\nDùng vàng", 
                "Chuyển hoá\nDùng ngọc"
            },

            new List<string>() // 11
            {
                "Vào hành trang\nChọn trang bị\n(Áo,quần,găng,giày hoặc rada)có ô đặt\n sao pha lê\n Chọn loại sao pha lê\nSau đó chọn 'Nâng cấp'",
                "Ta sẽ phù phép\ncho trang bị của ngươi\ntrở nên mạnh mẽ"
            },

            new List<string>() // 12
            {
                "Vào hành trang\n Chọn trang bị gốc\n (Áo,quần,găng,giày,rada) \ntừ cấp 4 trở lên\nChọn tiếp trang bị mới\nchưa nâng cấp cần nhập thể\nsau đó chọn 'Nâng cấp'",
                "Lưu ý trang bị mới\n phải hơn trang bị cũ 1 bậc"
            },
            new List<string>()  // 13 nâng cấp porata cấp 2
            {
                "Vào hành trang\nChọn bông tai Porata\nChọn mảnh vỡ bông tai để nâng cấp, số lượng\n9999 cái\nSau đó chọn 'Nâng cấp'", 
                "Ta sẽ phù phép\ncho bông tai Porata của người\nthành cấp 2"
            },
            new List<string>()  // 14 mở chỉ số porata cấp 2
            {
                "Vào hành trang\nChọn bông tai Porata cấp 2\nChọn mảnh hồn porata số lượng 99 cái và\nđá xanh lam để nâng cấp.\nSau đó chọn 'Nâng cấp'", 
                "Ta sẽ phù phép\ncho bông tai Porata cấp 2 của người\ncó 1 chỉ số ngẫu nhiên"
            },

            new List<string>() // 15
            {
                "Nở trứng\nLinh thú", 
                "Mở chỉ số\nLinh thú", 
                "Đổi chỉ số\nLinh thú", 
                "Nâng cấp\nLinh thú",
            },
            new List<string>() // 16
            {
                "Vào hành trang\nChọn 1 trứng linh thú\nChọn 99 hồn linh thú\n(Chọn 5 thỏi vàng nếu ngươi muốn nở nhanh)\nSau đó chọn 'Nâng cấp'", 
                "Ta sẽ làm phép\n cho trứng của ngươi sẽ nở"
            },
            new List<string>() // 17
            {
                "Vào hành trang\nChọn linh thú hạng C\nChọn 2x99 hồn linh thú\nChọn 2 thỏi vàng\nSau đó chọn 'Nâng cấp'", 
                "Ta sẽ làm phép\n mở chỉ số cho linh thú của ngươi"
            },
            new List<string>() // 18
            {
                "Vào hành trang\nChọn 1 linh thú có chỉ số\nChọn 99 hồn linh thú\nChọn 1 thỏi vàng\nSau đó chọn 'Nâng cấp'", 
                "Ta sẽ làm phép\n đổi chỉ số cho linh thú của ngươi"
            },
            new List<string>() // 19
            {
                "Vào hành trang\nChọn 2 linh thú cùng bậc thường để nâng cấp CỘNG\nHOẶC\n1 linh thú cấp CỘNG và 1 linh thú cấp thường để nâng hạng\n(Yêu cầu cùng loại linh thú\nvà cùng loại chỉ số)\nSau đó chọn 'Nâng cấp'", 
                "Ta sẽ làm phép\n linh thú của ngươi sẽ tiến hóa"
            },
            new List<string>() // 20
            {
                "Vào hành trang\nChọn 1 trang bị thiên sứ, hủy diệt, thần linh\nSau đó chọn x4 đá ngũ sắc\nSau đó chọn 'Nâng cấp'",
                "Trang bị kích hoạt\nnhận được ngẫu nhiên"
            },
            new List<string>() // 21
            {
                "Vào hành trang\nChọn 1 trang bị hủy diệt\nSau đó chọn x2 đá ngũ sắc\nSau đó chọn 'Nâng cấp'",
                "Trang bị kích hoạt\nnhận được ngẫu nhiên"
            },
            new List<string>() // 22
            {
                "Vào hành trang\nChọn 1 trang bị thần linh\nSau đó chọn 'Nâng cấp'",
                "Nâng cấp thần linh -> hủy diệt"
            },
            new List<string> // 23
            {
                "Vào hành trang\nChọn mảnh đồ thiên sứ và công thức\nĐá nâng cấp (Nếu có)\nĐá may mắn(nếu có)\nthêm theo thứ tự",
                "Ta sẽ chế tạo\nTrang bị thiên sứ cho ngươi",
            },
            new List<string> // 24
            {
                "Vào hành trang\nChọn Trang bị và Đá Ngũ Sắc\nthêm theo thứ tự",
                "Ta sẽ tinh chế\nTrang bị của ngươi !",
            },
            new List<string> //25
            {
                "Cần 1 công thức\nMảnh trang bị tương ứng\n1 đá nâng cấp(tùy chọn)\n1 đá may mắn(tùy chọn)",
                "Chế tạo\ntrang bị thiên sứ",
                
            },
            new List<string>() // 26
            {
                "Ép sao\ntrang bị",
                "Pha lê\nhoá\ntrang bị",
                "Chuyển hoá\nTrang bị",
                "Nâng cấp\nhủy diệt",
                "Nâng cấp\nthần linh",
                "Tinh chế\ntrang bị",
                "Thông tin\nSự kiện"
            },
            new List<string>() // 27
            {
                "Chế tạo Bồn\nTắm gỗ",
                "Chế tạo Bồn\nTắm vàng"
            },
             new List<string>() // 28
            {
             "Vào hành trang\nChọn Trang bị và 2 Đá Ngũ Sắc\nthêm theo thứ tự",
                "Ta sẽ xóa tinh chế\nTrang bị của ngươi !",
            },
             new List<string>() // 29
            {
             "Vào hành trang\nChọn Mắt hỗn mang và đá ngũ sắc",
                "Ta sẽ nâng cấp mắt của ngươi !",
            },
        };
        
        public List<string> TextBumma = new List<string>()
        {
            "Cưng cần trang bị gì cứ đến chỗ chị nhé",
            "Chị chỉ bán đồ cho người Trái Đất thôi nha cưng!",
        };
        
        public List<List<string>> MenuShopDistrict = new List<List<string>>()
        {
            new List<string>()
            {
                "Cửa hàng",
            },
            new List<string>()
            {
                "Cửa hàng", 
                "Từ chối",
            },
        };
        
        public List<string> TextDende = new List<string>()
        {
            "Anh, chị cần trang bị gì cứ đến chỗ em nhé",
            "Em... Chỉ bán đồ cho người Namếc thôi...!",
        };
        public List<string> TextAppule = new List<string>()
        {
            "Cậu cần trang bị gì cứ đến chỗ tôi nhé",
            "Ta chỉ bán đồ cho người Xayda siêu mạnh thôi, cút về hành tinh của ngươi mà mua đi!",
        };
        
        public List<string> TextBrief= new List<string>()
        {
            "Tàu vũ trụ Trái Đất sử dụng công nghệ mới nhất, có thể đưa ngươi đi bất kì đâu, miễn có tiền trả là được.",
            "Ta sẽ đưa ngươi trở về hành tinh của mình an toàn!",
        };
        
        public List<List<string>> MenuBrief = new List<List<string>>()
        {
            new List<string>()
            {
                "Đến Xayda", 
                "Đến Namếc", 
                "Siêu thị",
            },
            new List<string>()
            {
                "Đến Xayda", 
                "Đến Namếc",
            },
        };
        
        public List<string> TextCargo= new List<string>()
        {
            "Tàu vũ trụ Namec sử dụng công nghệ mới nhất, có thể đưa ngươi đi bất kì đâu, miễn có tiền trả là được.",
        };
        
        public List<List<string>> MenuCargo = new List<List<string>>()
        {
            new List<string>()
            {
                "Đến\nTrái Đất", 
                "Đến Xayda", 
                "Siêu thị",
            },
            new List<string>()
            {
                "Đến\nTrái Đất", 
                "Đến Xayda", 
            },
        };
        
        public List<string> TextCui = new List<string>()
        {
            "Tàu vũ trụ Xayda sử dụng công nghệ mới nhất, có thể đưa ngươi đi bất kì đâu, miễn có tiền trả là được.",
            "Đội quân Fide đang ở thung lũng Nappa, ta sẽ đưa ngươi đến đó",
            "Ngươi muốn về thành phố Vegeta ?",
        };
        
        public List<List<string>> MenuCui = new List<List<string>>()
        {
            new List<string>()
            {
                "Đến\nTrái Đất", "Đến Namếc", "Siêu thị",
            },
            new List<string>()
            {
                "Đến\nTrái Đất", "Đến Namếc", 
            },
            new List<string>()
            {
                "Đến Cold", "Đến Nappa", "Từ Chối"
            },
            new List<string>()
            {
                "Đồng ý", "Từ Chối"
            },
        };
        
        public List<string> TextQuyLao = new List<string>()
        {
            "Con muốn hỏi gì nào?",
            "Chào con, to rất vui khi gặp con\nCon muốn làm gì nào?",
            "Mở vào ngày 10/11",
            "Con có muốn huỷ học kỹ năng này và nhận lại 50% số tiềm năng không ?",
            "|2|Con đang có: {0} Điểm Sự Kiện\n" +"Con có chắc muốn đổi 50 Điểm Sự Kiện lấy:\n|2|Cải trang [Diệt Quỷ]\n" + "|7|Cơ hội nhận được [Đá bảo vệ]\n bảo vệ trang bị không bị rớt cấp\n khi nâng cấp thất bại",
            "Con có muốn dùng 10 chữ,mỗi chữ 3 cái\n để đổi lấy vật phẩm đặc biệt không?\n1) Fan gà nửa mùa với 200 triệu vàng\n2) Fan cuồng bóng đá với 1000 ngọc",
        };

        public List<List<string>> MenuQuyLao = new List<List<string>>()
        {
            new List<string>()
            {
                "Nói\nchuyện", "Kho báu\ndưới biển","Bỏ qua\nNV\nTrung úy","Bỏ qua\nNV\nĐHVT",
            },
            new List<string>()
            {
                "Nhiệm vụ", "Học\nKỹ năng"
            },
            new List<string>()
            {
                "Top\nBang hội", "Thành\ntích bang", "Chọn\ncấp độ", "Từ chối"
            },
            new List<string>()
            {
                "Đổi","Từ chối"
            },
            new List<string>()
            {
                "Tùy chọn\n1","Tùy chọn\n2","Đóng"
            },
             new List<string>()
            {
                "Nhiệm vụ", "Học\nKỹ năng", "Giải tán\nBang hội", "Về khu\nvực bang"
            },
        };

        public List<string> TextSanta = new List<string>()
        {
            "Xin chào, ta có một số vật phẩm đặc biệt, cậu có muốn xem không?",
            "Giới hạn vàng của bạn đang là {0} vàng, bạn có muốn nâng thêm 200Tr không?",
            "|0|Con đang có: {0} VND\nTổng Nạp: {1} VND",
            "|7|Con đang có: {0} VND",
            "|7|Con đang có: {0} VND\n|6|Muốn kích hoạt thành viên cần 20.000 VND",
            "|7|Con đang có: {0} VND\n|6|Con muốn đổi vàng hay ngọc ?",
        };
        
        public List<List<string>> MenuSanta = new List<List<string>>()
        {
            new List<string>()
            {
                 "Cửa\nhàng", "Tiệm\nHớt tóc"
            },
            new List<string>()
            {
                "Nâng 200Tr\nGiá 200Tr", "Từ chối"
            },
            new List<string>()
            {
                "Đổi vàng\nngọc", "Kích hoạt\nThành viên", "Từ chối"
            },
            new List<string>()
            {
                "Đổi vàng","Đổi Ngọc",
            },
            new List<string>()
            {
                "10K\n10 Thỏi","20K\n20 Thỏi", "50K\n50 Thỏi", "100k\n100 Thỏi", "200k\n200 Thỏi", "500k\n500 Thỏi"
            },
            new List<string>()
            {
                "10K\n20K Ngọc","20K\n40K Ngọc", "50K\n100K Ngọc", "100k\n200K Ngọc", "200k\n400k Ngọc", "500k\n1m Ngọc"
            },
            new List<string>()
            {
                "OK","Hủy"
            }
        };
        
        public List<string> TextQuocVuong = new List<string>()
        {
            "Con muốn nâng giới hạn sức mạnh\ncho bản thân hay đệ tử?"
        };
        
        public List<List<string>> MenuQuocVuong = new List<List<string>>()
        {
            new List<string>()
            {
                "Bản thân", "Đệ tử", "Từ chối"
            },
            new List<string>()
            {
                "Nâng\nGiới hạn\nSức mạnh","Nâng ngọc","OK"
            },
            new List<string>()
            {
                "Nâng ngay\ncho đệ tử\n%d ngọc","OK"
            },
        };

        public List<string> TextThuongDe = new List<string>()
        {
            "",
            "Con đã mạnh hơn ta, ta sẽ chỉ đường cho con đến Kaio để gặp thần Vũ Trụ Phương Bắc\nNgài là thần cai quản vũ trụ này, hãy theo ngài ấy học võ công",
            "Con có thể chọn từ 1 đến 7 viên\ngiá mỗi viên là 4 ngọc\nƯu tiên dùng vé quay trước.",
            "Con có chắc muốn xóa tất cả vật phẩm trong rương phụ không?"
        };

        public List<List<string>> MenuThuongDe = new List<List<string>>()
        {
            new List<string>()
            {
                "Bản thân"
            },
            new List<string>()
            {
                "{0}",
                "Tập luyện\nvới\nMr.Pôpô",
                "Tập luyện\nvới\nThuợng Đế",
                "Đến\nKaio",
                "Vòng quay\nMay mắn",
            },
            new List<string>()
            {
                "Vòng quay\nMay mắn",
                "Vòng quay\nĐặc biệt\nSự kiện",
              
            },
            new List<string>()
            {
                "Xóa",
                "Từ chối",
            },
        };

        public List<string> TextThanVuTru = new List<string>()
        {
            "Con muốn điều gì?",
        };

        public List<List<string>> MenuThanVuTru = new List<List<string>>()
        {
            new List<string>()
            {
                "Đăng ký\ntập\ntự động",
                "Tập luyện\nvới\nBubbles",
                "Tập luyện\nvới\nThần Vũ\nTrụ",
                "Di chuyển"
            },
        };

        // Rồng thần
        public string TextRongThan = "Ta sẽ ban cho ngươi 1 điều ước, hãy suy nghĩ thật kỹ trước khi quyết định";

        public List<string> MenuDieuUocRongThan = new List<string>()
        {
            "+1 Găng tay trên người (Max 7)",
            "Đổi kỹ năng 2,3 của đệ",
            "Đổi kỹ năng 3,4 của đệ",
            "+1 Găng tay của đệ tử (Max 7)",
            "Đẹp trai nhất vũ trụ",
        };

        // Trứng ma bư
        public List<string> TextQuaTrung = new List<string>()
        {
            "Bạn có chắc chắn thay thế để tự hiện tại thành đệ tử Ma Bư?",
            "Hãy chọn hành tinh cho đệ tử Ma Bư của bạn.",
        };

        public List<List<string>> MenuQuaTrung = new List<List<string>>()
        {
            new List<string>()
            {
                "{0}",
                "Nở Ngay\n1,1 Tỷ Vàng",
                "Đóng",
            },
            new List<string>()
            {
                "Nở",
                "Đóng",
            },
            new List<string>()
            {
                "Trái Đất",
                "Namếc",
                "Xayda"
            },
        };

        // Text nạp card
        public List<string> TextNapThe = new List<string>()
        {
            "Con hãy chọn loại thẻ mà con muốn nạp.",
            "Tốt lắm, giờ con hãy chọn mệnh giá thẻ nạp trước khi nhập mã nha\nLưu ý: Khi chọn sai mệnh giá sẽ bị trừ 50%."
        };


        public List<List<string>> MenuNapThe = new List<List<string>>()
        {
            new List<string>()
            {
                "Viettel",
                "Vinaphone",
                "Mobifone",
                "Zing",
                "Hủy"
            },
            new List<string>()
            {
                "10,000đ",
                "20,000đ",
                "30,000đ",
                "50,000đ",
                "Mệnh giá\nkhác",
                "Hủy",
            },
            new List<string>()
            {
                "100,000đ",
                "200,000đ",
                "300,000đ",
                "500,000đ",
                "1,000,000đ",
                "Hủy",
            },
        };

        // Text nội tại
        public List<string> TextNoiTai = new List<string>()
        {
            "Nội tại là một kỹ năng bị động hỗ trợ đặc biệt\nBạn có muốn mở hoặc thay đổi nội tại không?",
            "Bạn có muốn đổi Nội Tại khác với giá là {0} ngọc?",
            "Bạn có muốn mở Nội Tại Bằng Vàng với giá là {0} vàng?"
        };

        public List<List<string>> MenuNoiTai = new List<List<string>>() 
        {
            new List<string>()
            {
                "Xem\ntất cả\nNội tại",
                "Mở VIP",
                "Mở\nNội tại",
                "Từ chối"
            },
            new List<string>()
            {
                "Mở Nội Tại",
                "Từ chối"
            },
            new List<string>()
            {
                "Mở Bằng Vàng",
                "Từ chối"
            },
        };

        // Ca lich
        public List<string> TextCalich = new List<string>()
        {
            "Chào chú, cháu có thể giúp gì?",
            @"20 năm trước bọn Android sát thủ đã đánh bại nhóm bảo vệ trái đất của Sôngoku và Cađíc, Pôcôlô..
            Riêng Sôngoku vì bệnh tim đã chết trước đó nên không thể tham gia trận đánh...
            Từ đó đến nay bọn chúng tàn phá Trái Đất không hề thương tiếc. Cháu và mẹ may mắn sống sót nhờ lẩn trốn tại tầng hầm của công ty Capsule...
            Cháu tuy cũng là siêu Xayda nhưng cũng không thể làm gì được bọn Android sát thủ...
            Chỉ có Sôngoku mới có thể đánh bại bọn chúng, mẹ cháu đã chế tạo thành công cỗ máy thời gian và cháu quay về quá khứ để cứu Sôngoku...
            Bệnh của Gôku ở quá khứ là nan y, nhưng với trình độ y học tương lai chỉ cần uống thuốc là khỏi...
            Hãy đi theo cháu đến tương lai giúp nhóm Gôku đánh bại bọn Android sát thủ. Khi nào chú cần sự giúp đỡ của cháu hãy đến đây nhé.",
        };

        public List<List<string>> MenuCalich = new List<List<string>>() 
        {
            new List<string>()
            {
                "Kể chuyện",
                "Đi đến\nTương lai",
                "Từ chối",
            },
            new List<string>()
            {
                "Quay về\nQuá khứ",
                "Từ chối",
            },
        };

        public List<string> TextGiuMa = new List<string>()
        {
            "Ngươi đang muốn tìm mảnh vỡ và mảnh hồn bông tai Porata trong truyền thuyết, ta sẽ đưa ngươi đến đó.",
            "Thời gian khiên chiến Boss là 30 phút.\nTop 5 đánh boss +15 Capsule Bang\nTop 10 đánh boss +10 Capsule Bang\nTop 11 trở lên đánh boss +5 Capsule Bang\nNgười đánh boss cuối cùng sẽ được thưởng thêm 10 Capsule Bang\nMở cửa vào ngày thứ 7, chủ nhật hàng tuần",
        };

        public List<List<string>> MenuGiuMa = new List<List<string>>() 
        {
            new List<string>()
            {
               "Khiêu chiến\nBoss",
               "Điểm danh\n+1 Capsule\nBang",
               "OK",
               "Đóng"
            },
            new List<string>()
            {
               "Khiêu chiến\nBoss",
               "OK",
               "Đóng"
            },
             new List<string>()
            {
               "Miễn phí",
               "Đóng",
            },
             new List<string>()
            {
               "100 Ngọc",
               "Đóng",
            },
             new List<string>()
            {
               "300 Ngọc",
               "Đóng",
            },
        };

        public List<string> TextBill = new List<string>()
        {
            "Ngươi tìm ta có việc gì.",
            "Ngươi trang bị đủ bộ 5 món trang bị Thần\nvà mang 99 phần đồ ăn tới đây...\nrồi ta nói chuyện tiếp.",
            "Đói bụng quá...ngươi mang cho ta 99 phần đồ ăn, ta sẽ đổi cho một món đồ Hủy Diệt bằng THỎI VÀNG.\nNếu tâm trạng ta vui ngươi có thể nhận được trang bị tăng đến 15%"
        };


        public List<List<string>> MenuBill = new List<List<string>>() 
        {
            new List<string>()
            {
                "Nói chuyện",
                "Từ chối",
            },
            new List<string>()
            {
                "OK",
            },
            new List<string>()
            {
                "OK",
                "Từ chối",
            },
        };

        public List<string> TextTrungThu= new List<string>()
        {
            "Vui trung thu cùng NRO Thần Long để nhận được nhiều phần quà hấp dẫn.\nBạn muốn xem gì?",
        };
        
        public List<List<string>> MenuTrungThu = new List<List<string>>()
        {
            new List<string>()
            {
                // "Cửa hàng\nTrung thu",
                // "BXH\nSự kiện\nTrung thu", 
                "BXH\nSự kiện\nTop nạp",
            },
        };
    }
}