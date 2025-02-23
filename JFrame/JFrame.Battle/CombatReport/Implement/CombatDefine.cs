using System.Collections.Generic;

namespace JFrame
{
    public enum ActionComponentType
    {
        ConditionFinder,
        ConditionTrigger,
        DelayTrigger,
        ExecutorFinder,
        ExecuteFormulator,
        Executor,
        CdTrigger
    }

    public enum CombatTeamType
    {
        Combine, //组合 类似gjj+hero 公用gjj属性
        Single,  //独立 每个单位独自计算
    }

    public enum UnitMainType
    {

        Gjj = 1 << 0,  //1
        Hero = 1 << 1, //2
        Monster = 1 << 2, //4
        Boss = 1 << 3, //8
    }

    public enum UnitSubType
    {

        Ground = 1 << 4, //16
        Sky = 1 << 5, //32
    }

    /// <summary>
    /// action組件：4種類型
    /// </summary>
    public class ActionComponentInfo
    {
        public int id;
        public float[] args;
    }



    /// <summary>
    /// action数据结构
    /// </summary>
    public class ActionInfo
    {
        public string uid;
        public ActionType type;
        public ActionMode mode;
        public int groupId; //技能組id，覺醒后都是同一組
        public int sortId; //技能排序ID
        public Dictionary<ActionComponentType, List<ActionComponentInfo>> componentInfo;
    }

    /// <summary>
    /// unit数据结构
    /// </summary>
    public class CombatUnitInfo
    {
        public string uid;
        public int id;
        public Dictionary<int, ActionInfo> actionsData;
        public Dictionary<int, Dictionary<ActionComponentType, List<ActionComponentInfo>>> buffersData;
        public UnitMainType mainType;
        public UnitSubType unitSubType;
        public long hp;
        public long maxHp;
        public long atk;
        public float atkSpeed;
        public float cri; //暴击率 0~1的值 百分比
        public float criDmgRate; //暴击伤害加成百分比
        public float criDmgAnti; //暴击伤害抵抗百分比
        public float skillDmgRate; //技能伤害加成百分比
        public float skillDmgAnti; //技能伤害抵抗百分比
        public float dmgRate; //伤害加成百分比
        public float dmgAnti; //伤害抵抗百分比
        public float debuffHit; //0~1异常状态命中百分比
        public float debuffAnti; //0~1异常状态抵抗百分比
        public float penetrate; //穿透 0~1 百分比
        public float block;     //格挡 0~1 百分比
        public CombatVector position; //初始坐標點
        public CombatVector moveSpeed; //移動速度，向左就是負數，向右是正數
        public CombatVector targetPosition;//目标点

    }

    /// <summary>
    /// buffer数据结构
    /// </summary>
    public class CombatBufferInfo : IUnique
    {
        //public string uid;
        public int id;
        /// <summary>
        /// 可叠加最大层数
        /// </summary>
        public int foldMaxCount;
        /// <summary>
        /// 当前层数
        /// </summary>
        //public int foldCount;
        public CombatBufferFoldType foldType;
        //public float duration; //持续时间
        public Dictionary<int, ActionInfo> actionsData;

        public string Uid { get; set; }
    }
}
