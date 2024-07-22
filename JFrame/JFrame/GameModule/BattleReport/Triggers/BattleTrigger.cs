using System;

namespace JFrame
{
    public abstract class BattleTrigger
    {
        public event Action onTrigger;

        bool isOn = true;

        public abstract void Update(BattleFrame frame);

        /// <summary>
        /// 影响是否触发，如果设置为false , 则不会触发
        /// </summary>
        /// <param name="isOn"></param>
        public void SetEnable(bool isOn) => this.isOn = isOn;
        public bool GetEnable() => isOn;

        public void NotifyOnTrigger()
        {
            if (isOn)
                onTrigger?.Invoke();
        }
    }
}