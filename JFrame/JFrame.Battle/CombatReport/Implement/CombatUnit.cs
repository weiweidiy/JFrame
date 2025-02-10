using JFrame.BattleReportSystem;
using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace JFrame
{
    public class CombatUnit : ICombatUnit, ICombatUpdatable, ICombatMovable, IExtraDataClaimable
    {
        //public event Action<ICombatUnit, ICombatAction, List<ICombatUnit>, float> onActionCast;
        //public event Action<ICombatUnit, ICombatAction, float> onActionStartCD;
        //public event Action<ICombatUnit, ICombatAction, ICombatUnit, ExecuteInfo> onHittingTarget;
        //public event Action<CombatExtraData> onDamaging;
        //public event Action<CombatExtraData> onDamaged;
        //public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onHealed;
        //public event Action<CombatExtraData> onDead;
        //public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onRebord;
        //public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onMaxHpUp;
        //public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onDebuffAnti;
        //public event Action<ICombatUnit, int, ExecuteInfo> onBufferAdding;
        //public event Action<ICombatUnit, ICombatBuffer> onBufferAdded;
        //public event Action<ICombatUnit, ICombatBuffer> onBufferRemoved;
        //public event Action<ICombatUnit, ICombatBuffer> onBufferCast;
        //public event Action<ICombatUnit, ICombatBuffer, int, float[]> onBufferUpdate;
        public event Action<CombatExtraData> onActionCast;
        public event Action<CombatExtraData> onActionStartCD;
        public event Action<CombatExtraData> onHittingTarget;
        public event Action<CombatExtraData> onDamaging;
        public event Action<CombatExtraData> onDamaged;
        public event Action<CombatExtraData> onHealed;
        public event Action<CombatExtraData> onDead;
        public event Action<CombatExtraData> onRebord;
        public event Action<CombatExtraData> onMaxHpUp;
        public event Action<CombatExtraData> onDebuffAnti;
        public event Action<CombatExtraData> onBufferAdding;
        public event Action<CombatExtraData> onBufferAdded;
        public event Action<CombatExtraData> onBufferRemoved;
        public event Action<CombatExtraData> onBufferCast;
        public event Action<CombatExtraData> onBufferUpdate;
        public event Action<CombatExtraData> onStartMove;
        public event Action<CombatExtraData> onSpeedChanged;
        public event Action<CombatExtraData> onEndMove;

        /// <summary>
        /// 唯一id
        /// </summary>
        public string Uid { get; private set; }

        CombatExtraData _extraData ;
        public CombatExtraData ExtraData
        {
            get => _extraData;
            set { _extraData = value; }
        }


        /// <summary>
        /// buffer管理器
        /// </summary>
        CombatBufferManager bufferManager;

        /// <summary>
        /// 動作管理器
        /// </summary>
        CombatActionManager actionManager;

        /// <summary>
        /// 屬性管理器
        /// </summary>
        CombatAttributeManger attributeManger;

        /// <summary>
        /// 上下文
        /// </summary>
        CombatContext context;

        /// <summary>
        /// 坐標
        /// </summary>
        CombatVector position;

        /// <summary>
        /// 移動方向速度
        /// </summary>
        CombatVector velocity;

        /// <summary>
        /// 目标点
        /// </summary>
        CombatVector targetPsoition;

        /// <summary>
        /// 是否在移動中
        /// </summary>
        bool isMoving;



        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        /// <param name="actions"></param>
        /// <param name="buffers"></param>
        /// <param name="attributes"></param>
        public void Initialize(string uid, CombatContext context, List<CombatAction> actions, List<CombatBuffer> buffers, CombatAttributeManger attributeManager /*List<IUnique> attributes*/)
        {
            this.context = context;
            Uid = uid;
            actionManager = new CombatActionManager();
            bufferManager = new CombatBufferManager();
            this.attributeManger = attributeManager;

            _extraData = new CombatExtraData();
            _extraData.SourceUnit = this;
            _extraData.Value = (long)GetAttributeCurValue(PVPAttribute.ATK);

            if (actions != null)
            {
                actionManager.AddRange(actions);
                actionManager.Initialize(this);
            }

            if (buffers != null)
                bufferManager.AddRange(buffers);

            actionManager.onTriggerOn += ActionManager_onTriggerOn;
            actionManager.onStartExecuting += ActionManager_onStartExecuting;
            actionManager.onStartCD += ActionManager_onStartCD;

            //不能在初始化里跑，team收不到
            //StartMove();
        }

        public void Start()
        {
            StartMove();
            StartAction();
        }

        public void Stop() { StopMove(); }  


        private void ActionManager_onTriggerOn(CombatExtraData extraData)
        {
            if (GetTargetPostion() != null)
                return;

            if (extraData.Action.Mode == ActionMode.Passive)
                return;

            StopMove();
        }

        private void ActionManager_onStartCD(CombatExtraData extraData)
        {
            onActionStartCD?.Invoke(extraData);
        }

        private void ActionManager_onStartExecuting(CombatExtraData extraData)
        {
            onActionCast?.Invoke(extraData);
        }

        public void Release()
        {
            Uid = "";
            bufferManager = null;
            actionManager = null;
            attributeManger = null;
            isMoving = false;
        }

        public void UpdatePosition(BattleFrame frame)
        {
            if (IsMoving())   //只是自己移动了，其他单位还没有移动
            {
                position += velocity;
                if (GetTargetPostion() == null)
                    return;

                if (Math.Abs(GetTargetPostion().x - position.x) < 0.5f)
                    StopMove();
            }
        }

        public void Update(BattleFrame frame)
        {
            actionManager.Update(frame); 
            bufferManager.Update(frame);

        }

        /// <summary>
        /// 获取当前属性值
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual object GetAttributeCurValue(PVPAttribute attribute)
        {
            if (attributeManger == null)
                throw new Exception("combat unit attributemanager = null ");

            var attr = attributeManger.Get(attribute.ToString());
            if (attr != null)
            {
                if (attr is CombatAttributeLong)
                {
                    var fattr = attr as CombatAttributeLong;
                    return fattr.CurValue;
                }

                if (attr is CombatAttributeDouble)
                {
                    var fattr = attr as CombatAttributeDouble;
                    return fattr.CurValue;
                }

                if (attr is CombatAttributeInt)
                {
                    var fattr = attr as CombatAttributeInt;
                    return fattr.CurValue;
                }

            }
            throw new Exception("沒有找到屬性" + attribute.ToString());
        }

        public virtual object GetAttributeMaxValue(PVPAttribute attribute)
        {
            if (attributeManger == null)
                throw new Exception("combat unit attributemanager = null ");

            var attr = attributeManger.Get(attribute.ToString());
            if (attr != null)
            {
                if (attr is CombatAttributeLong)
                {
                    var fattr = attr as CombatAttributeLong;
                    return fattr.MaxValue;
                }

                if (attr is CombatAttributeDouble)
                {
                    var fattr = attr as CombatAttributeDouble;
                    return fattr.MaxValue;
                }

                if (attr is CombatAttributeInt)
                {
                    var fattr = attr as CombatAttributeInt;
                    return fattr.MaxValue;
                }

            }
            throw new Exception("沒有找到屬性" + attribute.ToString());
        }

        /// <summary>
        /// 是否還活著
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual bool IsAlive()
        {
            var hpAttr = GetAttributeManager().Get(PVPAttribute.HP.ToString());
            if (hpAttr != null)
            {
                var attr = hpAttr as CombatAttributeLong;
                return attr.CurValue > 0;
            }
            throw new Exception("沒有找到Hp屬性");
        }

        /// <summary>
        /// 是否滿血
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool IsHpFull()
        {
            var hpAttr = GetAttributeManager().Get(PVPAttribute.HP.ToString());
            if (hpAttr != null)
            {
                var attr = hpAttr as CombatAttributeLong;
                return attr.IsMax();
            }
            throw new Exception("沒有找到Hp屬性");
        }




        public void OnDamage(CombatExtraData extraData)
        {
            //to do: 添加一个预伤害事件，可以修改值
            // onDamaging?.Invoke(hitter, action, this, damage);
            onDamaging?.Invoke(extraData);

            var attrManager = GetAttributeManager();
            var hpAttr = attrManager.Get(PVPAttribute.HP.ToString());
            if (hpAttr == null)
                throw new Exception("沒有找到Hp屬性 " + Uid);

            var damage = extraData.Value;

            var attr = hpAttr as CombatAttributeLong;
            if (attr.CurValue <= 0 || damage == 0)
                return;

            attr.Minus(damage);

            //onDamaged?.Invoke(hitter, action, this, damage);
            onDamaged?.Invoke(extraData);

            if (attr.CurValue <= 0)
            {
                OnDead(extraData);
            }
        }

        /// <summary>
        /// 角色死亡了
        /// </summary>
        /// <param name="extraData"></param>
        private void OnDead(CombatExtraData extraData)
        {
            StopMove();

            //所有action設置成非活動狀態，不會出發
            actionManager.SetAllActive(false);

            //to do:清除所有buffer

            //onDead?.Invoke(hitter, action, this);
            onDead?.Invoke(extraData);
        }

        /// <summary>
        /// 收到治疗了
        /// </summary>
        /// <param name="extraData"></param>
        /// <exception cref="Exception"></exception>
        public void OnHeal(CombatExtraData extraData)
        {
            var attrManager = GetAttributeManager();
            var hpAttr = attrManager.Get(PVPAttribute.HP.ToString());
            if (hpAttr == null)
                throw new Exception("沒有找到Hp屬性 " + Uid);

            var healValue = extraData.Value;
            var attr = hpAttr as CombatAttributeLong;
            attr.Plus(healValue);

            onHealed?.Invoke(extraData);
        }

        public void OnReborn(CombatExtraData extraData)
        {
            throw new NotImplementedException();
        }

        public void OnAttrChanged(CombatExtraData extraData)
        {
            throw new NotImplementedException();
        }

        public void OnCrowdControlAnti(CombatExtraData extraData)
        {
            throw new NotImplementedException();
        }

        public void OnCrowdControled(CombatExtraData extraData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 獲取所有action
        /// </summary>
        /// <returns></returns>
        public List<CombatAction> GetActions()
        {
            return actionManager.GetAll();
        }

        /// <summary>
        /// 設置坐標
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(CombatVector position)
        {
            this.position = position;
        }

        /// <summary>
        /// 設置速度，目前只在初始化时候设置
        /// </summary>
        /// <param name="speed"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetSpeed(CombatVector speed)
        {
            velocity = speed;
        }

        /// <summary>
        /// 设置目标点
        /// </summary>
        /// <param name="position"></param>
        public void SetTargetPosition(CombatVector position)
        {
            targetPsoition = position;
        }

        /// <summary>
        /// 获取目标点位
        /// </summary>
        /// <returns></returns>
        public CombatVector GetTargetPostion()
        {
            return targetPsoition;
        }
        /// <summary>
        /// 开始移动
        /// </summary>
        public void StartMove()
        {
            isMoving = true;
            _extraData.Velocity = GetSpeed();
            onStartMove?.Invoke(_extraData);
        }

        /// <summary>
        /// 停止移动
        /// </summary>
        public void StopMove()
        {
            isMoving = false;
            onEndMove?.Invoke(_extraData);
        }

        public bool IsMoving()
        {
            return isMoving;
        }

        void StartAction()
        {
            actionManager.Start();
        }

        public virtual CombatVector GetPosition()
        {
            return position;
        }

        public CombatVector GetSpeed()
        {
            return velocity;
        }

        public virtual CombatAttributeManger GetAttributeManager()
        {
            return attributeManger;
        }


    }




}