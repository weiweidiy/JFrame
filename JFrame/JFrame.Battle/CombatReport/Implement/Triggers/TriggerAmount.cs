using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 直接触发 type = 2 参数0： 次数 
    /// </summary>
    public class TriggerAmount : CombatBaseTrigger
    {
        int count = 0;
        public TriggerAmount(CombatBaseFinder finder) : base(finder)
        {
        }

        public override int GetValidArgsCount()
        {
            return 1;
        }

        /// <summary>
        /// 退出当前状态
        /// </summary>
        public override void OnExit()
        {
            base.OnExit();

            count++;
        }

        protected int GetAmountArg()
        {
            return (int)GetCurArg(0);
        }

        protected override void OnUpdate(ComabtFrame frame)
        {
            base.OnUpdate(frame);

            if (count < GetAmountArg())
            {
                if (finder != null)
                {
                    var targets = finder.FindTargets(ExtraData); //获取目标
                    if (targets != null && targets.Count > 0)
                    {
                        _extraData.Targets = targets;
                        SetOn(true);
                    }
                }
                else
                    SetOn(true);
            }

        }
    }
}