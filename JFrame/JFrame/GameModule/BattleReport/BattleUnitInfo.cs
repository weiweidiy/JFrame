using System.Collections.Generic;

namespace JFrame
{
    public struct BattleUnitInfo
    {
        public string uid;
        public int id;
        public List<int> actionsId;
        public int hp;
        public int atk;
        public float atkSpeed;
    }

    public struct BattleUnitAttribute
    {
        public int hp;
        public int maxHp;
        public int atk;
        public float atkSpeed;
    }
}