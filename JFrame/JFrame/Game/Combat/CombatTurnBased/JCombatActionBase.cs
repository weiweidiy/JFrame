using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    /// <summary>
    /// 战斗行为基类，触发器触发执行，
    /// </summary>
    public abstract class JCombatActionBase : IJCombatAction
    {
        public string Uid {get; private set;}

        public JCombatActionBase(string uid)
        {
            this.Uid = uid;
        }

        public abstract void Run(IJCombatQuery query);

        public abstract void Execute(List<IJCombatUnit> targets);
    }
}
