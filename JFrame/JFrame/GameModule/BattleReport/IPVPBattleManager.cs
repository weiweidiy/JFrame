using System.Collections.Generic;
using static JFrame.PVPBattleManager;

namespace JFrame
{
    public interface IPVPBattleManager
    {
        void Initialize(Dictionary<BattlePoint, BattleUnitInfo> attacker, Dictionary<BattlePoint, BattleUnitInfo> defence, ActionDataSource dataSource, BufferDataSource bufferDataSource, IBattleReporter reporter);

        void Release();

        BattleTeam CreateTeam(Team team, Dictionary<BattlePoint, BattleUnitInfo> units);

        void Update();

        void AddTeam(Team team, BattleTeam teamObj);
    }
}