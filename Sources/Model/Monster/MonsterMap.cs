
using DragonBoyZ.Application.Handlers.Monster;
using DragonBoyZ.Model.ModelBase;

namespace DragonBoyZ.Model.Monster
{
    public class MonsterMap : MonsterBase
    {
        public MonsterMap()
        {
            IsMobMe = false;
            IsDie = false;
            MonsterHandler = new MonsterMapHandler(this);
        }
    }
}