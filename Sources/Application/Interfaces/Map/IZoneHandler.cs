using System.Threading.Tasks;
using DragonBoyZ.Application.Interfaces.Character;
using DragonBoyZ.Application.IO;
using DragonBoyZ.Model.Character;
using DragonBoyZ.Model.Item;
using DragonBoyZ.Model.Map;
using DragonBoyZ.Model.Monster;
using DragonBoyZ.Model;

namespace DragonBoyZ.Application.Interfaces.Map
{
    public interface IZoneHandler
    {
        Zone Zone { get; set; }
        void JoinZone(Model.Character.Character _char, bool isDefault, bool isTeleport, int typeTeleport);
        void OutZone(ICharacter _char, bool isOutZone = false);
        void InitMob();
        void Update();
        void Close();
        void SendMessage(Message message, bool isSkillMessage);
        void SendMessage(Message message, int id);
        void LeaveItemMap(ItemMap itemMap);
        void LeaveItemMap(ItemMap itemMap, MonsterMap monster);
        void RemoveItemMap(int id);
        int GetItemMapNotId();
        void RemoveCharacter();
        void AddDisciple(Disciple disciple);
        void RemoveDisciple(Disciple disciple);
        void AddPet(Pet pet);
        void RemovePet(Pet pet);
        void RemoveMonsterMe(int id);
        ICharacter GetCharacter(int id);
        MonsterMap GetMonsterMap(int id);
    }
}