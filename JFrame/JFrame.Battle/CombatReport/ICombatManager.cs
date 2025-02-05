

using System;
using System.Collections.Generic;

namespace JFrame
{
    public interface ICombatTeam
    {
        /// <summary>
        /// 需要记录在报告里的战斗事件
        /// </summary>
        event Action<int, IBattleUnit, IBattleAction, List<IBattleUnit>, float> onActionCast;
        event Action<int, IBattleUnit, IBattleAction, IBattleUnit, ExecuteInfo> onDamage;
        event Action<int, IBattleUnit, IBattleAction, IBattleUnit, int> onHeal;
        event Action<int, IBattleUnit, IBattleAction, IBattleUnit> onDead;
        event Action<int, IBattleUnit, IBattleAction, IBattleUnit, int> onReborn;
        event Action<int, IBattleUnit, IBattleAction, IBattleUnit, int> onMaxHpUp;
        event Action<int, IBattleUnit, IBattleAction, IBattleUnit, int> onDebuffAnti;
        event Action<int, IBattleUnit, IBuffer> onBufferAdded;
        event Action<int, IBattleUnit, IBuffer> onBufferRemoved;

        /// <summary>
        /// buff触发
        /// </summary>
        event Action<int, IBattleUnit, IBuffer> onBufferCast;
    }
    /// <summary>
    /// 战斗管理器
    /// </summary>
    public interface ICombatManager
    {
        void Start();

        /// <summary>
        /// 获取战报数据
        /// </summary>
        /// <returns></returns>
        object GetResult();

        /// <summary>
        /// 重置
        /// </summary>
        void Stop();

        /// <summary>
        /// 增加队伍
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="team"></param>
        void AddTeam(int teamId, ICombatTeam team);

        /// <summary>
        /// 添加一个单位
        /// </summary>
        /// <param name="unit"></param>
        void AddUnit(IBattleUnit unit);

        #region 查询方法

        /// <summary>
        /// 获取队伍
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        ICombatTeam GetTeam(int teamId);

        /// <summary>
        /// 获取队伍所有单位
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        List<IBattleUnit> GetUnits(int team);

        /// <summary>
        /// 获取对手队伍
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        int GetOppoTeam(int team);

        /// <summary>
        /// 根据单位获取对手队伍
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        int GetOppoTeam(IBattleUnit unit);

        /// <summary>
        /// 根据单位获取友方队伍
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        int GetFriendTeam(IBattleUnit unit);

        /// <summary>
        /// 或者战斗时间限制
        /// </summary>
        /// <returns></returns>
        float GetCombatTimeLimit();

        /// <summary>
        /// 是否是增益buff还是debuff
        /// </summary>
        /// <param name="buffId"></param>
        /// <returns></returns>
        bool IsBuffer(int buffId);

        /// <summary>
        /// 获取透传参数
        /// </summary>
        /// <returns></returns>
        object GetExtraData();
        #endregion

    }
}