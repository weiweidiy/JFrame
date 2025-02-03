using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace JFrame
{
    public class CombatUnit : ICombatUnit, ICombatUpdatable, ICombatMovable
    {
        public event Action<ICombatUnit, ICombatAction, List<ICombatUnit>, float> onActionCast;
        public event Action<ICombatUnit, ICombatAction, float> onActionStartCD;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, ExecuteInfo> onHittingTarget;
        public event Action<CombatExtraData> onDamaging;
        public event Action<CombatExtraData> onDamaged;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onHealed;
        public event Action<CombatExtraData> onDead;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onRebord;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onMaxHpUp;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onDebuffAnti;
        public event Action<ICombatUnit, int, ExecuteInfo> onBufferAdding;
        public event Action<ICombatUnit, ICombatBuffer> onBufferAdded;
        public event Action<ICombatUnit, ICombatBuffer> onBufferRemoved;
        public event Action<ICombatUnit, ICombatBuffer> onBufferCast;
        public event Action<ICombatUnit, ICombatBuffer, int, float[]> onBufferUpdate;

        /// <summary>
        /// 唯一id
        /// </summary>
        public string Uid { get; private set; }

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
        public void Initialize(CombatContext context, List<CombatAction> actions, List<CombatBuffer> buffers, List<IUnique> attributes)
        {
            this.context = context;
            Uid = Guid.NewGuid().ToString();
            attributeManger = new CombatAttributeManger();
            actionManager = new CombatActionManager();
            bufferManager = new CombatBufferManager();

            if (attributes != null)
                attributeManger.AddRange(attributes);
            if (actions != null)
                actionManager.AddRange(actions);
            if (buffers != null)
                bufferManager.AddRange(buffers);
        }

        public void Release()
        {
            Uid = "";
            bufferManager = null;
            actionManager = null;
            attributeManger = null;
            isMoving = false;
        }

        public void Update(BattleFrame frame)
        {
            actionManager.Update(frame);
            bufferManager.Update(frame);
            if (IsMoving())
            {
                position += velocity;
            }
        }

        /// <summary>
        /// 是否還活著
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool IsAlive()
        {
            var hpAttr = attributeManger.Get(PVPAttribute.HP.ToString());
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
            var hpAttr = attributeManger.Get(PVPAttribute.HP.ToString());
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
            //所有action設置成非活動狀態，不會出發
            actionManager.SetAllActive(false);

            //to do:清除所有buffer

            //onDead?.Invoke(hitter, action, this);
            onDead?.Invoke(extraData);
        }

        public void OnHeal(CombatExtraData extraData)
        {
            throw new NotImplementedException();
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
        /// 設置速度
        /// </summary>
        /// <param name="speed"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetSpeed(CombatVector speed)
        {
            velocity = speed;
        }

        public void StartMove()
        {
            isMoving = true;
        }

        public void StopMove()
        {
            isMoving = false;
        }

        public bool IsMoving()
        {
            return isMoving;
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