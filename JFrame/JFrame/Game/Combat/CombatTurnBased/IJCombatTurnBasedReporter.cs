using System.Threading.Tasks;

namespace JFramework.Game
{
    public interface IJCombatTurnBasedReporter<T> where T : JCombatUnitData, new()
    {
        IJCombatTurnBasedReport<T> GetReport();
    }
}
