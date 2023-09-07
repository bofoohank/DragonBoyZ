using DragonBoyZ.Model.Map;
using DragonBoyZ.Model.Character;
using System;

namespace DragonBoyZ.Application.Train{
    public class DataTraining{
        public class DataTapLuyen
        {
            public bool isPractice { get; set; }
            public long currentTimePractice { get; set; }
            public DataTapLuyen()
            {
                isPractice = false;
                currentTimePractice = -1;
            }
        }
        public class Whis
        {
            public int Level { get; set; }
            public long Time { get; set; }
            public int Count { get; set; }
            public long TimeStart { get; set; }
            public Whis()
            {
                Level = 1;
                Time = -1;
                Count = 0;
                TimeStart = -1;
            }
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
        public DataTapLuyen DataTapLuyenn;
        public long Potenial{get;set;}
        public int MapTraning{get;set;}
        public Zone OldMap{get;set;}
        public bool isTraining{get;set;}
        public long lastTimeLogout{get;set;}
        public int Level{get;set;}
        public Whis DataWhis { get; set; }
        public DataTraining(){
            Potenial = -1;
            MapTraning = -1;
            OldMap = null;
            isTraining = false;
            lastTimeLogout = -1;
            Level = 0;
            DataTapLuyenn = new DataTapLuyen();
            DataWhis = new Whis();
        }
        public void Dispose(Character character)
        {
            character.DataTraining.DataWhis.Dispose();
            character.DataTraining.DataWhis = null;
            character.DataTraining.DataTapLuyenn = null;
            GC.SuppressFinalize(this);
            character.DataTraining = null;
        }
        public static int GetPotenial(Character character){
            return (int)(Math.Pow(2, character.DataTraining.Level) * 10);
        }
        public static int GetPotenial(int level){
            return (int)(Math.Pow(2, level) * 10);
        }
    }
}