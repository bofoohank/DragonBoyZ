
using System.Security.Policy;
using DragonBoyZ.Application.Handlers.Monster;
using DragonBoyZ.Application.Interfaces.Character;
using DragonBoyZ.Model.ModelBase;
using Zone = DragonBoyZ.Model.Map.Zone;

namespace DragonBoyZ.Model.Monster
{
    public class MonsterPet : MonsterBase
    {
        public MonsterPet(ICharacter character, Map.Zone zone, short template, long hp, int damage)
        {
            IdMap = (short)character.Id;
            Id = template;
            X = character.InfoChar.X;
            Y = character.InfoChar.Y;
            Zone = zone;
            Character = character;
            IsMobMe = true;
            HpMax = hp;
            OriginalHp = hp;
            Hp = hp;
            Damage = damage;
            IsDie = false;
            MonsterHandler = new MonsterPetHandler(this);
        }
    }
}