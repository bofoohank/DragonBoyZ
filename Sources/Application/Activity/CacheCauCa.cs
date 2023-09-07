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
        Random random = new Random();
        public static List<int> Price_Canoc = new List<int> { 14000000, 16000000};
        public static List<int> Price_CaBayMau = new List<int> { 19000000, 22000000 };
        public static List<int> Price_CaDieuHong = new List<int> { 23000000, 25000000 };
    }
}
