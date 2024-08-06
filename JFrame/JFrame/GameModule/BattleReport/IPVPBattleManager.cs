using System.Collections.Generic;
using static JFrame.PVPBattleManager;

namespace JFrame
{
    public interface IPVPBattleManager
    {
        void Initialize(Dictionary<BattlePoint, BattleUnitInfo> attacker, Dictionary<BattlePoint, BattleUnitInfo> defence, ActionDataSource dataSource, BufferDataSource bufferDataSource, IBattleReporter reporter, FormulaManager formulaManager);

        void Release();

        BattleTeam CreateTeam(Team team, Dictionary<BattlePoint, BattleUnitInfo> units);

        void Update();

        void AddTeam(Team team, BattleTeam teamObj);

        List<IBattleUnit> GetUnits(Team team);

        Team GetOppoTeam(Team team);

        Team GetFriendTeam(IBattleUnit unit);

        float GetBattleTimeLimit();

    }
}