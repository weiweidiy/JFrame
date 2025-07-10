namespace JFramework.Game
{
    public interface IJCombatTurnBasedUnit : IJCombatOperatable, IJCombatCaster
    {
        /// <summary>
        /// 获取行动点，用于排序
        /// </summary>
        /// <returns></returns>
        int GetActionPoint();


    }
}
