namespace JFrame
{
    public class ActionConfig
    {
        public virtual float GetDuration(int id)
        {
            return 0f;
        }

        public virtual int GetFinderType(int id)
        {
            return 1; //normaltargetfinder
        }

        /// <summary>
        /// 获取触发器类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int GetTriggerType(int id)
        {
            return 1; //CDTrigger
        }

        /// <summary>
        /// 获取触发器参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual float GetTriggerArg(int id)
        {
            return 3f;
        }
    }
}