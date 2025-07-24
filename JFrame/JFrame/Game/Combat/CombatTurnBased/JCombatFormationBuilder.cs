using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatFormationBuilder<T, TUnit> : IJCombatFormationBuilder<T, TUnit> where T : JCombatFormationInfo<TUnit> where TUnit : JCombatUnitInfo, new()
    {
        protected IJCombatUnitBuilder<TUnit> unitBuilder;

        List<T> formationInfos = new List<T>();
        public JCombatFormationBuilder(IJCombatUnitBuilder<TUnit> unitBuilder)
        {
            this.unitBuilder = unitBuilder;
        }

        public abstract List<T> Build();
    }


}
