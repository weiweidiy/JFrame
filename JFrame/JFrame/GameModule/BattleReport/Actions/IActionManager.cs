namespace JFrame
{
    public interface IActionManager
    {
        bool IsBusy { get; }

        void Update(BattleFrame frame);
    }
}