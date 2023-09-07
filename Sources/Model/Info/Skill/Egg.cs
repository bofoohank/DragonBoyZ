using DragonBoyZ.Model.Monster;

namespace DragonBoyZ.Model.Info.Skill
{
    public class Egg
    {
        public MonsterPet Monster{ get; set; }
        public long Time { get; set; }

        public Egg()
        {
            Monster = null;
            Time = -1;
        }
    }
}