namespace JFrame
{
    /// <summary>
    /// 根據時間觸發  type = 3 參數0：時長
    /// </summary>
    public class TriggerTime : CombatBaseTrigger
    {

        float delta = 0f;

        public TriggerTime(CombatBaseFinder finder) : base(finder)
        {
        }

        public override int GetValidArgsCount()
        {
            return 1;
        }

        public float GetDuration()
        {
            return GetCurArg(0);
        }

        public override void Reset()
        {
            base.Reset();

            delta = 0f;
        }

        protected override void OnUpdate(ComabtFrame frame)
        {
            base.OnUpdate(frame);

            delta += frame.DeltaTime;

            if(delta >= GetDuration())
            {
                SetOn(true);
            }

        }




    }
}