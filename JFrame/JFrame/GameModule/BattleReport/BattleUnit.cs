using System;
using System.Collections.Generic;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

namespace JFrame
{
    public class BattleUnit : IBattleUnit
    {
        /// <summary>
        /// 有动作准备完毕，可以释放了
        /// </summary>
        public event Action<IBattleUnit, IBattleAction, List<IBattleUnit>> onActionTriggerOn;
        public event Action<IBattleUnit, IBattleAction, List<IBattleUnit>> onActionCast;
        public event Action<IBattleUnit, IBattleAction, IBattleUnit> onActionHitTarget; //动作命中对方

        public event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onDamage;
        public event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onHeal;        //回血
        public event Action<IBattleUnit, IBattleAction, IBattleUnit> onDead;
        public event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onRebord;        //复活

        public event Action<IBattleUnit, IBuffer> onBufferAdded;
        public event Action<IBattleUnit, IBuffer> onBufferRemoved;
        public event Action<IBattleUnit, IBuffer> onBufferCast;

        /// <summary>
        /// 获取战斗对象名字，暂时用ID代替
        /// </summary>
        public string Name => battleUnitInfo.id.ToString();

        /// <summary>
        /// 唯一ID
        /// </summary>
        public string UID { get; set; }

        public int Atk
        {
            get { return battleUnitAttribute.atk; }
            private set { battleUnitAttribute.atk = Math.Max(0, value); }
        }

        /// <summary>
        /// 攻击提升
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int AtkUpgrade(int value)
        {
            if (value < 0)
                throw new Exception("攻击提升数值不能为负数 " + value);

            Atk += value;
            return value;
        }

        /// <summary>
        /// 攻击力降低
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int AtkReduce(int value)
        {
            if (value < 0)
                throw new Exception("攻击降低数值不能为负数 " + value);

            var realValue = Math.Min(value, Atk); //防止减成负数
            Atk -= realValue;
            return realValue;
        }

        /// <summary>
        /// 攻击速度
        /// </summary>
        public float AtkSpeed {
            get { return battleUnitAttribute.atkSpeed; }
            set { battleUnitAttribute.atkSpeed = Math.Max(0, value); }
        }

        /// <summary>
        /// 生命值
        /// </summary>
        public int HP 
        {
            get { return battleUnitAttribute.hp; }
            private set
            {
                battleUnitAttribute.hp = Math.Min(MaxHP, Math.Max(0, value));
            }
        }

        /// <summary>
        /// 最大生命值
        /// </summary>
        public int MaxHP { get => battleUnitAttribute.maxHp;
            private set { 
                battleUnitAttribute.maxHp = value;
                if(HP > value)
                    HP = value;
            }
        }

        /// <summary>
        /// 最大生命升级
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int MaxHPUpgrade(int value)
        {
            if (value < 0)
                throw new Exception("最大生命提升数值不能为负数 " + value);

            MaxHP += value;
            HP += value; //当前生命也要上升
            return value;
        }

        /// <summary>
        /// 所有动作列表
        /// </summary>
        ActionManager actionManager = null;

        /// <summary>
        /// 战斗单位原始数据
        /// </summary>
        BattleUnitInfo battleUnitInfo = default;

        /// <summary>
        /// 战斗单位属性
        /// </summary>
        BattleUnitAttribute battleUnitAttribute = default;

        /// <summary>
        /// buff管理器
        /// </summary>
        IBufferManager bufferManager = null;

        public BattleUnit( BattleUnitInfo info, ActionManager actionManager, IBufferManager bufferManager)
        {
            this.UID = info.uid;
            battleUnitInfo = info;
            this.actionManager = actionManager;      
            if(actionManager != null && actionManager.GetAll() != null)
            {
                foreach (var action in actionManager.GetAll())
                {
                    action.onTriggerOn += Action_onTriggerOn;
                    action.onStartCast += Action_onCast;
                    action.onHitTarget += Action_onDone;
                    action.OnAttach(this);
                }
            }

            Atk = info.atk;
            MaxHP = info.hp;
            HP = info.hp;
            this.bufferManager = bufferManager;
            if(this.bufferManager != null)
            {
                this.bufferManager.onBufferAdded += BufferManager_onBufferAdded;
                this.bufferManager.onBufferRemoved += BufferManager_onBufferRemoved;
                this.bufferManager.onBufferCast += BufferManager_onBufferCast;
            }
        }

        #region 响应事件
        /// <summary>
        /// buffer添加了
        /// </summary>
        /// <param name="obj"></param>
        private void BufferManager_onBufferAdded(IBuffer obj)
        {
            onBufferAdded?.Invoke(this, obj);
        }

        /// <summary>
        /// buffer触发了
        /// </summary>
        /// <param name="obj"></param>
        private void BufferManager_onBufferCast(IBuffer obj)
        {
            onBufferCast?.Invoke(this, obj);
        }

        /// <summary>
        /// buffer移除了
        /// </summary>
        /// <param name="obj"></param>
        private void BufferManager_onBufferRemoved(IBuffer obj)
        {
            onBufferRemoved?.Invoke(this, obj);
        }

        /// <summary>
        /// 动作已经准备好了
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Action_onTriggerOn(IBattleAction action, List<IBattleUnit> targets)
        {
            onActionTriggerOn?.Invoke(this, action, targets);
        }

        /// <summary>
        /// 发动
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Action_onCast(IBattleAction action, List<IBattleUnit> targets)
        {
            onActionCast?.Invoke(this, action,targets);
        }

        /// <summary>
        /// 动作释放完成
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Action_onDone(IBattleAction arg1, IBattleUnit arg2)
        {
            onActionHitTarget?.Invoke(this, arg1,arg2);
        }

        /// <summary>
        /// 受到了伤害
        /// </summary>
        /// <param name="damage"></param>
        public void OnDamage(IBattleUnit hitter, IBattleAction action, IntValue damage)
        {
            //to do: 添加一个预伤害事件，可以修改值

            HP -= damage.Value;

            onDamage?.Invoke(hitter, action, this, damage.Value);

            if (HP <= 0)
            {
                OnDead(hitter, action);           
            }
        }

        /// <summary>
        /// 受到治疗
        /// </summary>
        /// <param name="heal"></param>
        public void OnHeal(IBattleUnit caster, IBattleAction action, IntValue heal)
        {
            //to do: 添加一个预治疗事件，可以修改值

            HP += heal.Value;

            onHeal?.Invoke(caster, action, this, heal.Value);

        }

        /// <summary>
        /// 复活
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="heal"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnReborn(IBattleUnit caster, IBattleAction action, IntValue heal)
        {
            HP += heal.Value;

            if (action != null)
            {
                foreach (var a in actionManager.GetAll())
                {
                    a.SetEnable(true);
                }
            }

            onRebord?.Invoke(caster, action, this, heal.Value);
        }

        /// <summary>
        /// 死亡了
        /// </summary>
        private void OnDead(IBattleUnit hitter, IBattleAction action)
        {
            if(action != null)
            {
                foreach (var a in actionManager.GetAll())
                {
                    a.SetEnable(false);
                }
            }

            if (bufferManager != null)
                bufferManager.Clear();

            onDead?.Invoke(hitter, action, this);
        }



        #endregion

        /// <summary>
        /// 更新帧了
        /// </summary>
        /// <param name="frame"></param>
        public void Update(BattleFrame frame)
        {
            actionManager.Update(frame);

            bufferManager.Update(frame);
        }

        /// <summary>
        /// 是否活着
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            return HP > 0;
        }

        /// <summary>
        /// 是否满血
        /// </summary>
        /// <returns></returns>
        public bool IsHpFull()
        {
            return HP == MaxHP;
        }

        /// <summary>
        /// 添加buffer
        /// </summary>
        /// <param name="bufferId"></param>
        /// <param name="foldCout"></param>
        /// <returns></returns>
        public IBuffer AddBuffer(int bufferId, int foldCout = 1)
        {
            if (bufferManager == null)
                throw new Exception("没有设置bufferManager 不能AddBuffer " + Name);

            return bufferManager.AddBuffer(this, bufferId, foldCout);
        }

        /// <summary>
        /// 获取所有buffers
        /// </summary>
        /// <returns></returns>
        public IBuffer[] GetBuffers()
        {
            return bufferManager.GetBuffers();
        }

        /// <summary>
        /// 移除buffer
        /// </summary>
        /// <param name="bufferUID"></param>
        public void RemoveBuffer(string bufferUID)
        {
            bufferManager.RemoveBuffer(bufferUID);
        }


    }
}