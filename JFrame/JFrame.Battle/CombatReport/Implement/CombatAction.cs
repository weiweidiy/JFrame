using System;
using System.Collections.Generic;

namespace JFrame
{
    public class CombatAction : ICombatAction , ICombatUpdatable , IUnique
    {
        public string Uid => throw new NotImplementedException();

        //public ActionType Type => throw new NotImplementedException();

        //public ActionMode Mode => throw new NotImplementedException();

        public event Action<ICombatAction> onCanCast;
        public event Action<ICombatAction, List<ICombatUnit>, float> onStartCast;
        public event Action<ICombatAction, float> onStartCD;
        public event Action<ICombatAction, ICombatUnit, ExecuteInfo> onHittingTarget;
        public event Action<ICombatAction, ICombatUnit, ExecuteInfo, ICombatUnit> onHittedComplete;

        public bool CanCast()
        {
            throw new NotImplementedException();
        }

        public float Cast()
        {
            throw new NotImplementedException();
        }

        public float EnterCD()
        {
            throw new NotImplementedException();
        }

        public List<ICombatUnit> FindTargets(object[] args)
        {
            throw new NotImplementedException();
        }

        public float GetCastDuration()
        {
            throw new NotImplementedException();
        }

        public float[] GetCdArgs()
        {
            throw new NotImplementedException();
        }

        public IBattleTrigger GetCDTrigger()
        {
            throw new NotImplementedException();
        }

        public float[] GetConditionTriggerArgs()
        {
            throw new NotImplementedException();
        }

        public string GetCurState()
        {
            throw new NotImplementedException();
        }

        public float[] GetExecutorArgs()
        {
            throw new NotImplementedException();
        }

        public float[] GetFinderArgs()
        {
            throw new NotImplementedException();
        }

        public void Interrupt()
        {
            throw new NotImplementedException();
        }

        public bool IsCDComplete()
        {
            throw new NotImplementedException();
        }

        public bool IsExecuting()
        {
            throw new NotImplementedException();
        }

        public void NotifyCanCast()
        {
            throw new NotImplementedException();
        }

        public void NotifyStartCast(List<ICombatUnit> targets, float duration)
        {
            throw new NotImplementedException();
        }

        public void NotifyStartCD(float cd)
        {
            throw new NotImplementedException();
        }

        public void OnEnable()
        {
            throw new NotImplementedException();
        }

        public void OnStart()
        {
            throw new NotImplementedException();
        }

        public void OnStop()
        {
            throw new NotImplementedException();
        }

        public void ReadyToExecute(ICombatUnit caster, ICombatAction action, List<ICombatUnit> targets)
        {
            throw new NotImplementedException();
        }

        public void ResetCdArgs(float[] args)
        {
            throw new NotImplementedException();
        }

        public void ResetConditionTriggerArgs(float[] args)
        {
            throw new NotImplementedException();
        }

        public void ResetExecutorArgs(float[] args)
        {
            throw new NotImplementedException();
        }

        public void ResetFinderArgs(float[] args)
        {
            throw new NotImplementedException();
        }

        public void SetCdArgs(float[] args)
        {
            throw new NotImplementedException();
        }

        public void SetConditionTriggerArgs(float[] args)
        {
            throw new NotImplementedException();
        }

        public void SetEnable(bool enable)
        {
            throw new NotImplementedException();
        }

        public void SetExecutorArgs(float[] args)
        {
            throw new NotImplementedException();
        }

        public void SetFinderArgs(float[] args)
        {
            throw new NotImplementedException();
        }

        public void SetActive(bool active)
        {
            throw new NotImplementedException();
        }

        public void Standby()
        {
            throw new NotImplementedException();
        }

        public void Update(BattleFrame frame)
        {
            throw new NotImplementedException();
        }

        public void UpdateConditionTriggers(BattleFrame frame)
        {
            throw new NotImplementedException();
        }

        public void UpdateExecutors(BattleFrame frame)
        {
            throw new NotImplementedException();
        }

        public void UpdateCdTriggers(BattleFrame frame)
        { throw new NotImplementedException(); }
    }




}