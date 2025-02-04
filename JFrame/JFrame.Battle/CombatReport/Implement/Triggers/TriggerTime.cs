namespace JFrame
{
    /// <summary>
    /// 根據時間觸發  type = 3 參數0：時長
    /// </summary>
    public class TriggerTime : BaseTrigger
    {
        float delta = 0f;

        public override void Reset()
        {
            base.Reset();

            delta = 0f;
        }

        public override void Update(BattleFrame frame)
        {
            base.Update(frame);

            delta += frame.DeltaTime;

            if(delta >= GetDuration())
            {
                SetOn(true);
            }

        }

        public float GetDuration()
        {
            return GetCurArg(0);
        }
    }
}