using JFramework;

namespace JFrame.Game
{
    public interface IJCombatActionComponent : IJCombatLifeCycle
    {
        void SetOwner(IJCombatAction owner);

        IJCombatAction GetOwner();
    }
}
