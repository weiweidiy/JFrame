namespace JFrame
{
    public interface ICombatFormula
    {
        double GetHitValue(CombatExtraData extraData);

        bool IsHit(CombatExtraData extraData);
    }
}