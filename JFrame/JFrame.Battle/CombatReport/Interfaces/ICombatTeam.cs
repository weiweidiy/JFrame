using System;
using System.Collections.Generic;

namespace JFrame
{
    public interface ICombatTeam<TUnit, TAction, TBuffer>
    {

        //event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, List<IBattleUnit>> onActionTriggerOn;
        /// <summary>
        /// int : teamId , float : duration
        /// </summary>
        event Action<int, TUnit, TAction, List<TUnit>, float> onActionCast;
        //event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit> onActionDone;
        event Action<int, TUnit, TAction, float> onActionStartCD;

        event Action<int, TUnit, TAction, TUnit, ExecuteInfo> onDamage;
        event Action<int, TUnit, TAction, TUnit, int> onHeal;
        event Action<int, TUnit, TAction, TUnit> onDead;
        event Action<int, TUnit, TAction, TUnit, int> onReborn;
        event Action<int, TUnit, TAction, TUnit, int> onMaxHpUp;
        event Action<int, TUnit, TAction, TUnit, int> onDebuffAnti;
        event Action<int, TUnit, TBuffer> onBufferAdded;
        event Action<int, TUnit, TBuffer> onBufferRemoved;
        event Action<int, TUnit, TBuffer> onBufferCast;
        event Action<int, TUnit, TBuffer, int, float[]> onBufferUpdate;


        int Team { get; }

        TUnit GetUnit(string uid);

        void AddUnit(TUnit unit);
        void RemoveUnit(TUnit unit);

        List<TUnit> GetUnits();

        int GetUnitCount();

        bool IsAllDead();

        void Update(BattleFrame frame);

    }
}