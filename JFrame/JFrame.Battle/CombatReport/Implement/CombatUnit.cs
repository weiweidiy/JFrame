using System;
using System.Collections.Generic;

namespace JFrame
{
    public class CombatUnit : ICombatUnit, ICombatUpdatable
    {
        public event Action<ICombatUnit, ICombatAction, List<ICombatUnit>, float> onActionCast;
        public event Action<ICombatUnit, ICombatAction, float> onActionStartCD;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, ExecuteInfo> onHittingTarget;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, ExecuteInfo> onDamaging;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, ExecuteInfo> onDamaged;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onHealed;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit> onDead;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onRebord;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onMaxHpUp;
        public event Action<ICombatUnit, ICombatAction, ICombatUnit, int> onDebuffAnti;
        public event Action<ICombatUnit, int, ExecuteInfo> onBufferAdding;
        public event Action<ICombatUnit, ICombatBuffer> onBufferAdded;
        public event Action<ICombatUnit, ICombatBuffer> onBufferRemoved;
        public event Action<ICombatUnit, ICombatBuffer> onBufferCast;
        public event Action<ICombatUnit, ICombatBuffer, int, float[]> onBufferUpdate;

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

        public void Initialize(CombatContext context)
        {
            Uid = Guid.NewGuid().ToString();
            bufferManager = new CombatBufferManager();
            actionManager = context.combatActionManager;
            attributeManger = context.combatAttributeManger;
        }

        public void Release()
        {
            Uid = "";
            bufferManager = null;
            actionManager = null;
            attributeManger = null;
        }

        public void Update(BattleFrame frame)
        {
            actionManager.Update(frame);
            bufferManager.Update(frame);
        }

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
            throw new NotImplementedException();
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
    }




}