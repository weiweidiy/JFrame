using System.Text;
using System.Threading.Tasks;

namespace JFramework.Game
{
    /// <summary>
    /// 战报式战斗对象
    /// </summary>
    public interface IJCombatReportable 
    {
        Task<IJCombatResult> GetResult();
    }
}
