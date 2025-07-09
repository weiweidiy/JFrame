using System;
using System.Threading.Tasks;

namespace JFramework.Game
{
    /// <summary>
    /// 可以战斗播报化的对象
    /// </summary>
    public abstract class JCombat : IJCombatReportable, IJCombatLifeCycle
    {
        protected IJCombatQuery jCombatQuery;

        IJCombatRunner jCombatRunner;
        public JCombat(IJCombatQuery jCombatQuery, IJCombatRunner jCombatRunner)
        {
            this.jCombatQuery = jCombatQuery;
            this.jCombatRunner = jCombatRunner;
            jCombatRunner.SetCombat(this);
        }

        /// <summary>
        /// 计算战斗结果并返回
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IJCombatResult> GetResult()
        {
            return jCombatRunner.RunCombat();
        }

        /// <summary>
        /// 战斗开始
        /// </summary>
        public virtual void OnStart()
        {
            var units = jCombatQuery.GetUnits();
            if (units != null)
            {
                foreach (var unit in units)
                {
                    unit.OnStart();
                }
            }
        }

        /// <summary>
        /// 逻辑更新
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        /// 战斗结束
        /// </summary>
        public virtual void OnStop()
        {
            var units = jCombatQuery.GetUnits();
            if (units != null)
            {
                foreach (var unit in units)
                {
                    unit.OnStop();
                }
            }

        }


    }
}
