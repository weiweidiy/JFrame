namespace JFramework.Game
{
    public interface IJCombatFilter : IJCombatActionComponent
    {
        bool Filter(IJCombatFilterArgs filterArgs/*object triggerArgs, IJCombatExecutorArgs executorArgs, IJCombatCasterTargetableUnit target*/);
    }
}
