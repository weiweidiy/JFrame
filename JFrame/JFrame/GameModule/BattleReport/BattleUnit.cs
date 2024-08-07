﻿using System;
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
        //主体事件
        public event Action<IBattleUnit, IBattleAction, List<IBattleUnit>,float> onActionCast;
        public event Action<IBattleUnit, IBattleAction, float> onActionStartCD;
        public event Action<IBattleUnit, IBattleAction, IBattleUnit, ExecuteInfo> onHittingTarget; //动作命中对方

        //受体事件
        public event Action<IBattleUnit, IBattleAction, IBattleUnit, ExecuteInfo> onDamaging; //即将受到伤害
        public event Action<IBattleUnit, IBattleAction, IBattleUnit, ExecuteInfo> onDamaged;
        public event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onHealed;        //回血
        public event Action<IBattleUnit, IBattleAction, IBattleUnit> onDead;
        public event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onRebord;        //复活
        public event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onMaxHpUp;       //复活
        public event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onDebuffAnti;    //状态抵抗

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
 

            Atk = info.atk;
            MaxHP = info.hp;
            HP = info.hp;
            AtkSpeed = info.atkSpeed;
            Cri = info.cri;
            CriDmgRate = info.criDmgRate;
            CriDmgAnti = info.criDmgAnti;
            SkillDmgRate = info.skillDmgRate;
            SkillDmgAnti = info.skillDmgAnti;
            DmgRate = info.dmgRate;
            DmgAnti = info.dmgAnti;
            DebuffHit = info.debuffHit;
            DebuffAnti = info.debuffAnti;
            Penetrate = info.penetrate;
            Block = info.block;


            this.bufferManager = bufferManager;
            if(this.bufferManager != null)
            {
                this.bufferManager.onBufferAdded += BufferManager_onBufferAdded;
                this.bufferManager.onBufferRemoved += BufferManager_onBufferRemoved;
                this.bufferManager.onBufferCast += BufferManager_onBufferCast;
            }

            this.actionManager = actionManager;
            if(actionManager != null)
            {
                actionManager.Initialize(this);
                actionManager.onStartCast += Action_onCast;
                actionManager.onStartCD += ActionManager_onStartCD;
                actionManager.onHittingTarget += ActionManager_onHittingTarget;
            }
        }



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
        /// 是否是增益
        /// </summary>
        /// <param name="bufferId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsBuffer(int bufferId)
        {
            return bufferManager.IsBuff(bufferId);
        }

        /// <summary>
        /// 是否满血
        /// </summary>
        /// <returns></returns>
        public bool IsHpFull()
        {
            return HP == MaxHP;
        }

        public IBattleAction[] GetActions()
        {
            return actionManager.GetAll().ToArray();
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
        /// 发动
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Action_onCast(IBattleAction action, List<IBattleUnit> targets, float duration)
        {
            onActionCast?.Invoke(this, action,targets, duration);
        }

        /// <summary>
        /// 进入CD了
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cd"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ActionManager_onStartCD(IBattleAction action, float cd)
        {
            onActionStartCD?.Invoke(this, action, cd);
        }

        /// <summary>
        /// 即将命中目标
        /// </summary>
        /// <param name="action"></param>
        /// <param name="target"></param>
        /// <param name="info"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ActionManager_onHittingTarget(IBattleAction action, IBattleUnit target, ExecuteInfo info)
        {
            onHittingTarget?.Invoke(this, action, target,info);
        }

        /// <summary>
        /// 受到了伤害
        /// </summary>
        /// <param name="damage"></param>
        public void OnDamage(IBattleUnit hitter, IBattleAction action, ExecuteInfo damage)
        {
            //to do: 添加一个预伤害事件，可以修改值
            onDamaging?.Invoke(hitter, action,this, damage);

            if (HP <= 0 || damage.Value == 0)
                return;

            HP -= damage.Value;

            onDamaged?.Invoke(hitter, action, this, damage);

            if (HP <= 0)
            {
                OnDead(hitter, action);           
            }
        }

        /// <summary>
        /// 受到治疗
        /// </summary>
        /// <param name="heal"></param>
        public void OnHeal(IBattleUnit caster, IBattleAction action, ExecuteInfo heal)
        {
            //to do: 添加一个预治疗事件，可以修改值

            HP += heal.Value;

            onHealed?.Invoke(caster, action, this, heal.Value);

        }

        /// <summary>
        /// 生命上限提高
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="hp"></param>
        public void OnMaxHpUp(IBattleUnit caster, IBattleAction action, ExecuteInfo hp)
        {
            MaxHPUpgrade(hp.Value);

            onMaxHpUp?.Invoke(caster,action, this, hp.Value);
        }

        /// <summary>
        /// 复活
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="heal"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnReborn(IBattleUnit caster, IBattleAction action, ExecuteInfo heal)
        {
            HP += heal.Value;

            //to do: 移到actionmanager中
            if (action != null)
            {
                foreach (var a in actionManager.GetAll())
                {
                    a.SetDead(false);
                }
            }

            onRebord?.Invoke(caster, action, this, heal.Value);
        }

        /// <summary>
        /// 死亡了
        /// </summary>
        private void OnDead(IBattleUnit hitter, IBattleAction action)
        {
            if(actionManager != null)
                actionManager.OnDead();

            if (bufferManager != null)
                bufferManager.Clear();

            onDead?.Invoke(hitter, action, this);
        }

        /// <summary>
        /// 状态抵抗了
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="info"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDebuffAnti(IBattleUnit caster, IBattleAction action,  int debuffId)
        {
            onDebuffAnti?.Invoke(caster,action,this, debuffId);
        }

        /// <summary>
        /// 眩晕了
        /// </summary>
        /// <param name="duration"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnStunning(float duration)
        {
            if(actionManager != null)
                actionManager.OnStunning(duration);
        }

        /// <summary>
        /// 眩晕恢复
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResumeFromStunning()
        {
            if (actionManager != null)
                actionManager.OnResumeFromStunning();
        }

        #endregion

        #region buff
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

        #endregion

        #region 属性
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
        public float AtkSpeed
        {
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
        public int MaxHP
        {
            get => battleUnitAttribute.maxHp;
            private set
            {
                battleUnitAttribute.maxHp = value;
                if (HP > value)
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



        public float Cri
        {
            get { return battleUnitAttribute.cri; }
            private set
            {
                battleUnitAttribute.cri = value;
            }
        }

        public float CriDmgRate
        {
            get { return battleUnitAttribute.criDmgRate; }
            private set
            {
                battleUnitAttribute.criDmgRate = value;
            }
        }

        public float CriDmgAnti
        {
            get { return battleUnitAttribute.criDmgAnti; }
            private set
            {
                battleUnitAttribute.criDmgAnti = value;
            }
        }

        public float SkillDmgRate
        {
            get { return battleUnitAttribute.skillDmgRate; }
            private set
            {
                battleUnitAttribute.skillDmgRate = value;
            }
        }

        public float SkillDmgAnti
        {
            get { return battleUnitAttribute.skillDmgAnti; }
            private set
            {
                battleUnitAttribute.skillDmgAnti = value;
            }
        }

        public float DmgRate
        {
            get { return battleUnitAttribute.dmgRate; }
            private set
            {
                battleUnitAttribute.dmgRate = value;
            }
        }

        public float DmgAnti
        {
            get { return battleUnitAttribute.dmgAnti; }
            private set
            {
                battleUnitAttribute.dmgAnti = value;
            }
        }

        public float DebuffHit
        {
            get { return battleUnitAttribute.debuffHit; }
            private set
            {
                battleUnitAttribute.debuffHit = value;
            }
        }

        public float DebuffAnti
        {
            get { return battleUnitAttribute.debuffAnti; }
            private set
            {
                battleUnitAttribute.debuffAnti = value;
            }
        }

        public float DebuffAntiUpgrade(float value)
        {
            if (value < 0)
                throw new Exception("攻击提升数值不能为负数 " + value);

            DebuffAnti += value;
            return value;
        }

        public float DebuffAntiReduce(float value)
        {
            if (value < 0)
                throw new Exception("攻击降低数值不能为负数 " + value);

            var realValue = Math.Min(value, DebuffAnti); //防止减成负数
            DebuffAnti -= realValue;
            return realValue;
        }


        public float Penetrate
        {
            get { return battleUnitAttribute.penetrate; }
            private set
            {
                battleUnitAttribute.penetrate = value;
            }
        }

        public float Block
        {
            get { return battleUnitAttribute.block; }
            private set
            {
                battleUnitAttribute.block = value;
            }
        }




        #endregion
    }
}