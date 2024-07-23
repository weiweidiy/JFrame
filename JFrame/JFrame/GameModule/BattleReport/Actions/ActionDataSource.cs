using System.Collections.Generic;

namespace JFrame
{
    public class ActionDataSource
    {
        protected PVPBattleManager pvpManager { get; }

        public ActionDataSource(PVPBattleManager pvpManager)
        {
            this.pvpManager = pvpManager;
        }



        /// <summary>
        /// 获取触发器类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int GetTriggerType(string unitUID, int unitId, int actionId)
        {
            return 1; //CDTrigger
        }

        /// <summary>
        /// 获取触发器参数
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public virtual float GetTriggerArg(string unitUID, int unitId, int actionId)
        {
            return 3f; //to do: 计算数值
        }

        /// <summary>
        /// 搜索器类型
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public virtual int GetFinderType(string unitUID, int unitId, int actionId)
        {
            return 1; //normaltargetfinder
        }

        /// <summary>
        /// 搜索器参数
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public virtual float GetFinderArg(string unitUID, int unitId, int actionId)
        {
            return 1f;
        }

        /// <summary>
        /// 获取执行器
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public virtual List<int> GetExcutorTypes(string unitUID, int unitId, int actionId)
        {
            return new List<int>() { 1 };
        }

        /// <summary>
        /// 执行器参数
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public virtual float[] GetExcutorArg(string unitUID, int unitId, int actionId, int executorType)
        {
            return new float[] { 1f , 0.5f,0.25f, 1f };//1:次数, 2：延迟 3:多段攻击间隔 4:倍率：
        }
    }
}