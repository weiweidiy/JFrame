using System.Threading.Tasks;

namespace JFramework.Game
{
    public interface IRunner
    {
        void SetRunable(IRunable combat);

        Task Run();

        //Task<IJCombatResult> RunCombat();
    }
}
