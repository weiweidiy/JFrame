namespace JFrame
{
    public interface IAttachable
    {
        IAttachOwner Owner { get; }

        void OnAttach(IAttachOwner target);

        void OnDetach();  
    }

    public interface IAttachOwner
    {
       string Name { get; }

        IBattleUnit Owner { get; }

        /// <summary>
        /// 获取层数
        /// </summary>
        /// <returns></returns>
        float GetFoldCount();

        /// <summary>
        /// 获取周期
        /// </summary>
        /// <returns></returns>
        float GetDuration();

        /// <summary>
        /// 设置是否有效
        /// </summary>
        /// <param name="valid"></param>
        void SetValid(bool valid);
    }

}

