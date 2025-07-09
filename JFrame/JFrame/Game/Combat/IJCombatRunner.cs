using System.Threading.Tasks;

namespace JFramework.Game
{
    public interface IJCombatRunner
    {
        void SetCombat(IJCombatLifeCycle combat);

        Task<IJCombatResult> RunCombat();
    }
}
