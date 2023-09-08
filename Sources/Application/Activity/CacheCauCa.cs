using DragonBoyZ.Application.Handlers.Menu;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DragonBoyZ.Sources.Application.Activity.CauCa
{
    public class CacheCauCa
    {



        public static JsonSerializerSettings SettingNull = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public static readonly long Exp_SM = 10000000; //Exp khi câu thành công. Thất bại nhận được 1 nửa
        public static readonly long Exp_TN = 10000000; //Exp khi câu thành công. Thất bại nhận được 1 nửa

        public static readonly long GiaMoiCauThuong = 10000000;
        public static readonly long GiaMoiCauDacBiet = 35000000;
        public static readonly int TimeCauCa = 10000;
        public static List<int> IdItemCauCa = new List<int> { 1002, 1003, 1004 };
        public static List<int> IdCaiTrangCauCa = new List<int> { 1113 };

        public static readonly int PercentNormal = 50;
        public static readonly int PercentSpecial = 75;
        public static readonly int PercentDropItem = 30;
        public static readonly int PercentDropItemVV = 10;

        //Tỷ lệ loại cá

        public static readonly int PercentNormal_CaBayMau = 40;
        public static readonly int PercentNormal_CaDieuHong = 20;

        public static readonly int PercentSpecial_CaBayMau = 55;
        public static readonly int PercentSpecial_CaDieuHong = 35;

        //Giá bán cá
        public static List<int> Price_Canoc = new List<int> { 14000000, 16000000 };
        public static List<int> Price_CaBayMau = new List<int> { 19000000, 22000000 };
        public static List<int> Price_CaDieuHong = new List<int> { 23000000, 25000000 };


        //Đổi thưởng

        public static int Id_XoCaXanh = 1005;     //Xô cá xanh
        public static int CountGold_XoCaXanh = 0;
        public static int CountDiamo_XoCaXanh = 0;
        public static List<int> IdItemNeed_XoCaXanh = new List<int> { 1002, 1003 };
        public static List<int> CountItemNeed_XoCaXanh = new List<int> { 10, 10 };

        public static int Id_XoCaVang = 1006;     //Xô cá vàng
        public static int CountGold_XoCaVang = 0;
        public static int CountDiamo_XoCaVang = 0;
        public static List<int> IdItemNeed_XoCaVang = new List<int> { 1002, 1003, 1004 };
        public static List<int> CountItemNeed_XoCaVang = new List<int> { 10, 10, 10 };


        //Sử dụng xô cá
        public static List<int> Item_Cap2 = new List<int> { 1150, 1151, 1152, 1153, 1154 };
        public static List<int> Item_CaiTrangCC = new List<int> { 1012, 1011, 1010 };
        public static List<int> Item_CaiTrangSanhDieu = new List<int> { 1241, 1242, 1243 };
        public static List<int> Item_PhuKien = new List<int> { 1007, 1000, 996 };

        public static readonly int Percent_OpenXoCaXanh_Vang = 60;
        public static readonly int Percent_OpenXoCaXanh_Item_HSD = 85;
    }
}
