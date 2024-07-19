using System.Collections.Generic;

namespace JFrame
{
    public class DataSource
    {
        //public virtual float GetDuration(int id) //没有用
        //{
        //    return 0f;
        //}



        /// <summary>
        /// 获取触发器类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int GetTriggerType(int unitId, int actionId)
        {
            return 1; //CDTrigger
        }

        /// <summary>
        /// 获取触发器参数
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public virtual float GetTriggerArg(int unitId, int actionId)
        {
            return 3f; //to do: 计算数值
        }

        /// <summary>
        /// 搜索器类型
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public virtual int GetFinderType(int unitId, int actionId)
        {
            return 1; //normaltargetfinder
        }

        /// <summary>
        /// 搜索器参数
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public virtual float GetFinderArg(int unitId, int actionId)
        {
            return 1f;
        }

        /// <summary>
        /// 获取执行器
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public virtual List<int> GetExcutorType(int unitId, int actionId)
        {
            return new List<int>() { 1 };
        }

        /// <summary>
        /// 执行器参数
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public virtual float[] GetExcutorArg(int unitId, int actionId, int executorType)
        {
            return new float[] { 1f , 1f, 0.5f};//1:次数, 2:倍率：3：延迟
        }
    }
}