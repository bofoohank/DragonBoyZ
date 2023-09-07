using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Extensions.Configuration;
using DragonBoyZ.Application.Constants;
using DragonBoyZ.Application.Manager;
using DragonBoyZ.Application.IO;
using DragonBoyZ.Logging;
using DragonBoyZ.Model.BangXepHang;
using InitData = DragonBoyZ.DatabaseManager.InitData;
using Task = System.Threading.Tasks.Task;
using DragonBoyZ.Application;
using DragonBoyZ.Application.Extension;
using DragonBoyZ.Application.Extension.ChampionShip;
using DragonBoyZ.Application.Extension.Yardat;
using DragonBoyZ.Application.Extension.Event;
using DragonBoyZ.Application.Extension.BlackballWar;
using DragonBoyZ.Application.Extension.Bosses.Mabu12Gio;
using DragonBoyZ.Application.Extension.Bosses.Mabu2Gio;

using DragonBoyZ.Application.Extension.Namecball;
using System.Text;
using System.Collections.Generic;
using DragonBoyZ.Application.Extension.Bosses.BigBoss;
using DragonBoyZ.Application.Extension.Ký_gửi;
using DragonBoyZ.Application.Extension.NamecballWar;
using System.Runtime.InteropServices;
using DragonBoyZ.Sources.Application.Extension;
using DragonBoyZ.Sources.Application.Activity.CauCa;

namespace DragonBoyZ.Application.Threading
{
    public class Server
    {
        private static Server Instance { get; set; } = null;
        public static readonly object SQLLOCK = new object();
        public static readonly object IPLOCK = new object();
        private IPAddress IpAddress { get; set; }
        private TcpListener Listener { get; set; }
        public bool IsRunning { get; set; }
        public bool IsSaving { get; set; }
        private Thread RunServer { get; set; }
        public IServerLogger Logger { get; set; }
        public IConfiguration Config { get; set; }

        private DatabaseManager.InitData _initData;
        private Thread _serverRun;
        private ClanRunTime _clanRun;
        private MagicTreeRunTime _magicTreeRun;
		public BxhRunTime BangXepHang;
        public NpcAutoChat _autoChat;


        public ABoss ABoss;
        public long DelayLogin { get; set; }

        public long StartServerTime { get; set; }
        public int CountLogin { get; set; }
        public bool LockCloneGiaoDich { get; set; }

        public readonly string DROP_KEY = "dropsuperdrop";

        public static Server Gi()
        {
            return Instance ??= new Server();
        }

        public Server()
        {
            IpAddress = IPAddress.Parse(DatabaseManager.ConfigManager.gI().ServerHost);
            Listener = new TcpListener(IpAddress, DatabaseManager.ConfigManager.gI().ServerPort);
            RunServer = null;
        }
        public Task Runtime { get; set; }
        public void StartRunTime()
        {
            Runtime = new Task(AnotherRunTime);
            Runtime.Start();
        }
        public async void AnotherRunTime()
        {
            _serverRun.Start();
            _clanRun.StartClan();
            _magicTreeRun.StartMagicTree();
            Yardat.Init();
            BangXepHang.Start();
            ThiefBear.gI().Refesh();
            while (IsRunning)
            {
                var timeserver = ServerUtils.CurrentTimeMillis();
                var currentNow = ServerUtils.TimeNow();
                Hirudegarn.gI().InitHirudegarn(currentNow.Hour);
                ABoss.gI().AutoBoss(timeserver);
                NamecballWar.gI().Update(currentNow);
                //   EventRuntime.Runtime(timeserver);
                ChampionShip.gI().InitDaiHoiVoThuat(timeserver);
                BlackBallRuntime.CurrentRunTime();
                Mabu12h.gI().AutoInit(timeserver);
                Mabu2h.gI().AutoInit(timeserver);
                Init.AutoInit(timeserver);
                ThiefBear.gI().Init(timeserver);
                await Task.Delay(1000);
            }
        }

        public void InitServerRuntime()
        {
            _serverRun.Start();
            _clanRun.StartClan();
            _magicTreeRun.StartMagicTree();
            Yardat.Init();
            BangXepHang.Start();
            new Thread(new ThreadStart(() =>
                {
                    while (IsRunning)
                    {
                        var timeserver = ServerUtils.CurrentTimeMillis();
                        var currentNow = ServerUtils.TimeNow();
                        Hirudegarn.gI().InitHirudegarn(currentNow.Hour);
                        ABoss.gI().AutoBoss(timeserver);
                        NamecballWar.gI().Update(currentNow);
                        //   EventRuntime.Runtime(timeserver);
                        ChampionShip.gI().InitDaiHoiVoThuat(timeserver);
                        BlackBallRuntime.CurrentRunTime();
                        Mabu12h.gI().AutoInit(timeserver);
                        Mabu2h.gI().AutoInit(timeserver);
                        Init.AutoInit(timeserver);
                        ThiefBear.gI().Init(timeserver);
                        Thread.Sleep(1000);
                    }
                })).Start();
            ThiefBear.gI().Refesh();

        }
        private void InitServer()
        {
            _initData = new DatabaseManager.InitData();
            if (_clanRun == null)
            {
                _clanRun = new ClanRunTime();
            }

            if (_magicTreeRun == null)
            {
                _magicTreeRun = new MagicTreeRunTime();
            }


            if (BangXepHang == null)
            {
                BangXepHang = new BxhRunTime();
            }
            if (ABoss == null)
            {
                ABoss = new ABoss();
            }
            if (_autoChat == null)
            {
                _autoChat = new NpcAutoChat();
            }
        }
		
        public void StartServer(bool running, IServerLogger logger, IConfiguration config, bool isRestart)
        {
            Logger = logger;
            Config = config;
            IsRunning = running;
           // Socket_Client.CreateSocket();
            DelayLogin = ServerUtils.CurrentTimeMillis();
            StartServerTime = ServerUtils.CurrentTimeMillis();
            LockCloneGiaoDich = true;
            CountLogin = 0;
            //Console.WriteLine("color a");
            //List<List<long>> Object = new List<List<long>>();
            //for (int i = 0; i <10; i++)
            //{
            //    List<long> Object2 = new List<long>();
            //   // var clanMember = ClientManager.Gi().GetCharacter(clan.Thành_viên[i].Id);
            //    Object2.Add(10000 * i);
            //    Object2.Add(1000*i);
            //    Object.Add(Object2);
            //}
            //Object.Sort((g1, g2) => g2[1].CompareTo(g1[1]));
            //foreach(var item in Object)
            //{
            //    Console.WriteLine(" " + item[1]);
            //}

            //Update giá cá sau mỗi lần mở server
            HandlerCauCa.UpdateGiaCa(1002, HandlerCauCa.RandomGiaCaNoc());
            HandlerCauCa.UpdateGiaCa(1003, HandlerCauCa.RandomGiaCa7Mau());
            HandlerCauCa.UpdateGiaCa(1004, HandlerCauCa.RandomGiaCaDieuHong());



            Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("\n");
            Console.WriteLine("   __");
			Console.WriteLine("  |  \"--.--.._                                             __..    ,--.");
			Console.WriteLine("  |       `.   \"-.'\"\"\\_...-----..._   ,--. .--..-----.._.\"|   |   /   /");
			Console.WriteLine("  |_   _    \\__   ).  \\           _/_ |   \\|  ||  ..    >  `.  |  /   /");
			Console.WriteLine("    | | `.   ._)  /|\\  \\ .-\"\"\"\":-\"   \"-.   `  ||  |.'  ,'`. |  |_/_  /");
			Console.WriteLine("    | |_.'   |   / \"\"`  \\  ===/  ..|..  \\     ||      < \"\"  `.  \"  |/__");
			Console.WriteLine("    `.      .    \\ ,--   \\-..-\\   /\"\\   /     ||  |>   )--   |    /    |");
			Console.WriteLine("     |__..-'__||__\\   |___\\ __.:-.._..-'_|\\___||____..-/  |__|--\"\"____/");
			Console.ResetColor();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("                           _______________________");
			Console.WriteLine("                          /                      ,'");
			Console.WriteLine("                         /      ___            ,'");
			Console.WriteLine("                        /   _.-'  ,'        ,-'\x20  /");
			Console.WriteLine("                       / ,-' ,--.'        ,'   .'/");
			Console.WriteLine("                      /.'     `.         '.  ,' /");
			Console.WriteLine("                     /      ,-'       ,\"--','  /");
			Console.WriteLine("                          ,'        ,'  ,'    /");
			Console.WriteLine("                         ,-'      ,' .-'     /");
			Console.WriteLine("                      ,-'                   /");
			Console.WriteLine("                    ,:___________TGDV_Hank_/");
			Console.WriteLine("\n");
						
            Console.ResetColor();
            if (!IsRunning) return;
            InitServer();

            
			Logger.Print("\n [Info]","red");
			string linkServer = DatabaseManager.ConfigManager.gI().Link;
			int startIndex = linkServer.IndexOf('"') + 1;
			int endIndex = linkServer.IndexOf(':', startIndex);
			Logger.Print("   Server Name: " + linkServer.Substring(startIndex, endIndex - startIndex));
            Logger.Print("   Port: " + DatabaseManager.ConfigManager.gI().ServerPort);
			
			
            Listener.Start();

            //new Thread(new ThreadStart(AutoBossHandler.AFide)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.AColer)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.ACell)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.ATieuDoiSatThu)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.AChilled)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.SpawnAndroid1)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.SpawnAndroid2)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.SpawnAndroid3)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.AOngGiaNoel)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.ADanEmFide)).Start();
            // new Thread(new ThreadStart(AutoBossHandler.ABoss)).Start();
            //_bossRun.SpawnBroly();
            //Start Magic tree
            //new Thread(new ThreadStart(AutoBossHandler.ABlackGoku)).Start();
            //new Thread(new ThreadStart(AutoBossHandler.ABroly)).Start();

            _serverRun = new Thread(() =>
            {
                while (IsRunning)
                {
                    try
                    {
                       // lock (IPLOCK)
                        //{
                        var client = Listener.AcceptTcpClient();
                      //  _TpcClient.Add(client);
                      //  var clientMarshal = CollectionsMarshal.AsSpan(_TpcClient);
                      //  for (int i = 0; i < clientMarshal.Length; i++)
                      //  {
                        //    client = _TpcClient[i];
                            if (!client.Connected) continue;
                            var ipv4 = client.Client.RemoteEndPoint?.ToString()?.Split(':')[0];
                            var session = new Session_ME(client, ipv4);

                            session.StartSession();
                            ClientManager.Gi().Add(session);
							string text = $"Session: {session.Id} Connecting -- Ip: {ipv4} -- size: {client.SendTimeout} ms";
							Logger.PrtColor("blue","Player","darkblue",text);
                            //}
                        //}
                        //Logger.Info("Accecpt ClientMarshal Length: "+clientMarshal.Length);
                        //_TpcClient.Clear();
                    }
                    catch (Exception)
                    {
                        IsRunning = false;
                    }
                }
                SaveData();
                IsSaving = false;
                Task.Run(() =>
                {
                    while (!MagicTreeRunTime.IsStop || !ClanRunTime.IsStop || !ABoss.gI().IsStop)
                    {
                        //Ignore
                    }
                    Logger.Print("Server Shutdown Success!");
                });
            });
            /// _serverRun.Start();
            // _clanRun.StartClan();
            // _magicTreeRun.StartMagicTree();
            // _bossRun.StartBossRunTime();
            _autoChat.StartNpcAutoChat();

            StartRunTime();

            //InitServerRuntime();


            //AutoBossHandler.ABoss();
            //ABoss.gI().StartBossRunTime();
            // ChampionShip.gI().InitDaiHoiVoThuat();
            //DragonBoyZ.Sources.Application.Extension.Chẵn_Lẻ_Momo.HandlerChanLe.Update();
            //DragonBoyZ.Sources.Application.Extension.Ngọc_Zồng_Bi_Đen.Init.gI().InitBlackBall();
            //DragonBoyZ.Sources.Application.Extension.Ngọc_Zồng_Bi_Đen.BlackBallHandler.gI().Runtime();
            //DragonBoyZ.Sources.Application.Extension.Bosses.Mabư_12_giờ.Mabu12h.gI().AutoInit();
            //DragonBoyZ.Sources.Application.Extension.Bosses.Mabư_2_giờ.Mabu2h.gI().AutoInit();
            // Yardat.Init();
            //DragonBoyZ.Sources.Application.Extension.BlackballWar.BlackBallRuntime.CurrentRunTime();
            //EventRuntime.InitSuKienRuntime();
            //DragonBoyZ.Sources.Application.Extension.Namecball.Init.AutoInit();
            //DragonBoyZ.Sources.Application.Extension.Bosses.BigBoss.ThiefBear.gI().Init();
            // BangXepHang.Start();
        }

        public void StopServer()
        {
            Listener.Stop();
            IsSaving = true;
        }

        private void SaveData()
        {
            ClientManager.Gi().Clear();
            Logger.Print("Save DATA Player Server Sucess!!!");
            KyGUIMySQL.UpdateAllItem();
            Logger.Print("Save DATA ITEM KY GUI Server Sucess!!!");
        }

        public void RestartServer()
        {

            StopServer();
            Task.Run(() =>
            {
                while (IsSaving || !MagicTreeRunTime.IsStop || !ClanRunTime.IsStop)
                {
                    continue;
                }
                StartServer(true, Logger, Config, true);
                Logger.Print("Server Restart Success!");
            });
        }
    }
}