using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

namespace JFrame
{
    public class BattleUnit : IBattleUnit
    {
        /// <summary>
        /// 有动作准备完毕，可以释放了
        /// </summary>
        public event Action<IBattleUnit, IBattleAction, List<IBattleUnit>> onActionTriggerOn;
        public event Action<IBattleUnit, IBattleAction, IBattleUnit> onActionCast;
        public event Action<IBattleUnit, IBattleAction, IBattleUnit> onActionDone;

        public event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onDamage;
        public event Action<IBattleUnit, IBattleAction, IBattleUnit> onDead;

        /// <summary>
        /// 获取战斗对象名字，暂时用ID代替
        /// </summary>
        public string Name => battleUnitInfo.id.ToString();

        /// <summary>
        /// 唯一ID
        /// </summary>
        public string UID { get; private set; }

        public int Atk
        {
            get { return battleUnitAttribute.atk; }
            set { battleUnitAttribute.atk = Math.Max(0, value); }
        }

        public int HP 
        {
            get { return battleUnitAttribute.hp; }
            set {
                battleUnitAttribute.hp = Math.Min(MaxHP, Math.Max(0, value)); 
            } 
        }

        public int MaxHP { get => battleUnitInfo.hp; }

        /// <summary>
        /// 所有动作列表
        /// </summary>
        List<IBattleAction> actions = null;

        /// <summary>
        /// 战斗单位原始数据
        /// </summary>
        BattleUnitInfo battleUnitInfo = default;

        /// <summary>
        /// 战斗单位属性
        /// </summary>
        BattleUnitAttribute battleUnitAttribute = default;

        public BattleUnit( BattleUnitInfo info, List<IBattleAction> actions)
        {
            this.UID = info.uid;
            battleUnitInfo = info;
            this.actions = actions;      
            foreach(var action in actions) {
                action.onTriggerOn += Action_onTriggerOn;
                action.onStartCast += Action_onCast;
                action.onHitTarget += Action_onDone;
            }

            HP = info.hp;
            Atk = info.atk; 
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
        private void Action_onCast(IBattleAction action, IBattleUnit firstTarget)
        {
            onActionCast?.Invoke(this, action,firstTarget);
        }

        /// <summary>
        /// 动作释放完成
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Action_onDone(IBattleAction arg1, IBattleUnit arg2)
        {
            onActionDone?.Invoke(this, arg1,arg2);
        }

        /// <summary>
        /// 更新帧了
        /// </summary>
        /// <param name="frame"></param>
        public void Update(BattleFrame frame)
        {
            foreach (var action in actions)
            {
                action.Update(frame);
            }

            //Console.WriteLine("unit update");
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
        /// 受到了伤害
        /// </summary>
        /// <param name="damage"></param>
        public void OnDamage(IBattleUnit hitter, IBattleAction action, int damage)
        {
            HP -= damage;

            onDamage?.Invoke(hitter, action, this, damage);

            if (HP <= 0)
            {
                OnDead(hitter, action);
               
            }


        }

        /// <summary>
        /// 受到治疗
        /// </summary>
        /// <param name="heal"></param>
        public void OnHeal(int heal)
        {
            HP += heal;
        }

        /// <summary>
        /// 死亡了
        /// </summary>
        private void OnDead(IBattleUnit hitter, IBattleAction action)
        {
            foreach (var a in actions)
            {
                a.SetEnable(false);
            }

            onDead?.Invoke(hitter, action, this);
        }

    }
}