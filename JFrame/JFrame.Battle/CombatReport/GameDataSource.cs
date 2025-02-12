using System.Collections.Generic;

namespace JFrame
{
    public abstract class GameDataSource
    {
        public abstract string GetUid();
        public abstract int GetUnitId();
        public abstract long GetHp();
        public abstract long GetMaxHp();
        public abstract long GetAtk();
        public abstract List<int> GetActions();
        public abstract CombatVector GetPosition();
        public abstract CombatVector GetVelocity();
        public abstract CombatVector GetTargetPosition();


    }
}