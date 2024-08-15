using System;

namespace JFrame
{
    public interface INewBattleTrigger : IAttachable
    {
        BattleTriggerType TriggerType { get; }

        void SetEnable(bool isOn);

        bool GetEnable();

        void Update(BattleFrame frame);

        //void OnAttach(IBattleAction action);

        void Restart();

        bool IsOn();

        /// <summary>
        /// 获取CD
        /// </summary>
        /// <returns></returns>
        float[] GetArgs();

        /// <summary>
        /// 设置cd
        /// </summary>
        /// <param name="cd"></param>
        /// <exception cref="NotImplementedException"></exception>
        void SetArgs(float[] args);

        /// <summary>
        /// 设置无效
        /// </summary>
        void SetOn(bool isOn);
    }
}