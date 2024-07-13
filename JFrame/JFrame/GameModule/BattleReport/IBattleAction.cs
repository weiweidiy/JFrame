using System;
using System.Collections.Generic;

namespace JFrame
{
    public interface IBattleAction
    {
        /// <summary>
        /// 可以触发了
        /// </summary>
        event Action<IBattleAction, List<IBattleUnit>> onReady;

        void Update(BattleFrame frame);

        void Cast(List<IBattleUnit> units);
    }

    public class ActionConfig
    {
        public float GetDuration(int id)
        {
            return 1f;
        }
    }
}