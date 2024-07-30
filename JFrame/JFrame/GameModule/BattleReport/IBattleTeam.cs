using System;
using System.Collections.Generic;

namespace JFrame
{
    public interface IBattleTeam
    {
        //event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, List<IBattleUnit>> onActionTriggerOn;
        event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, List<IBattleUnit>,float> onActionCast;
        //event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit> onActionDone;

        event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit, int> onDamage;
        event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit, int> onHeal;
        event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit> onDead;
        event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit, int> onReborn;
        event Action<PVPBattleManager.Team, IBattleUnit, IBattleAction, IBattleUnit, int> onMaxHpUp;

        event Action<PVPBattleManager.Team, IBattleUnit, IBuffer> onBufferAdded;
        event Action<PVPBattleManager.Team, IBattleUnit, IBuffer> onBufferRemoved;
        event Action<PVPBattleManager.Team, IBattleUnit, IBuffer> onBufferCast;

        IBattleUnit GetUnit(BattlePoint point);

        void AddUnit(BattlePoint point, IBattleUnit unit);

        List<IBattleUnit> GetUnits();

        int GetUnitCount();

        bool IsAllDead();

        void Update(BattleFrame frame);

        PVPBattleManager.Team Team { get; } 
    }
}