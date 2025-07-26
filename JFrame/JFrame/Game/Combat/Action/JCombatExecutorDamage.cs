using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatExecutorDamage : JCombatExecutorBase
    {
        public JCombatExecutorDamage(IJCombatTargetsFinder finder, IJCombatFormula formulua) : base(finder, formulua)
        {
        }

        protected override void DoExecute(object triggerArgs, List<IJCombatCasterTargetableUnit> FinderTargets)
        {
            //触发器找到的目标
            var targets = triggerArgs as List<IJCombatCasterTargetableUnit>;

            if (FinderTargets != null)
            {
                DoDamage(FinderTargets);
                return;
            }

            if(targets != null)
            {
                DoDamage(targets);
            }
            else
            {
                throw new Exception("JCombatExecutorDamage: No targets found for damage execution.");
            }
        }

        void DoDamage(List<IJCombatCasterTargetableUnit> finalTargets)
        {
            var uid = Guid.NewGuid().ToString();
            foreach (var target in finalTargets)
            {
                var hitValue = formulua.CalcHitValue(target);
                var sourceUnitUid = GetOwner().GetCaster();
                var sourceActionUid = GetOwner().Uid;
                var data = new JCombatDamageData(uid, sourceUnitUid, sourceActionUid, hitValue, 0, target.Uid);
                target.OnDamage(data);
            }
        }
    }
}
