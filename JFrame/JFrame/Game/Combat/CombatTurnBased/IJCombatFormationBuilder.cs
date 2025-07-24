using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatFormationBuilder<T,TUnit> where T: JCombatFormationInfo<TUnit> where TUnit : JCombatUnitInfo
    {
        List<T> Build();
    }

    
}
