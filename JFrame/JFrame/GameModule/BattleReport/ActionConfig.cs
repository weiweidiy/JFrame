namespace JFrame
{
    public class ActionConfig
    {
        public virtual float GetDuration(int id) //没有用
        {
            return 0f;
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
            return 3f; //to do: 计算数值
        }

        /// <summary>
        /// 搜索器类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int GetFinderType(int id)
        {
            return 1; //normaltargetfinder
        }

        /// <summary>
        /// 搜索器参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual float GetFinderArg(int id)
        {
            return 1f;
        }

        /// <summary>
        /// 获取执行器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int GetExcutorType(int id)
        {
            return 1;
        }

        /// <summary>
        /// 执行器参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual float GetExcutorArg(int id)
        {
            return 1f;
        }
    }
}