namespace JFrame
{

    /// <summary>
    ///  type 1 查找触发器，只要查找器找到了对象，就触发
    /// </summary>
    public class TriggerFinder : CombatBaseTrigger
    {
        public override int GetValidArgsCount()
        {
            return 0;
        }


        public TriggerFinder(CombatBaseFinder finder) : base(finder)
        {
        }

        protected override void OnUpdate(CombatFrame frame)
        {
            base.OnUpdate(frame);

            if(finder != null)
            {
                var targets = finder.FindTargets(ExtraData); //获取目标
                if(targets != null && targets.Count > 0) 
                {
                    _extraData.Targets = targets;
                    SetOn(true);
                }
            }
        }


    }
}