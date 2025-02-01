namespace JFrame
{
    public interface ICombatBuffer
    {
        /// <summary>
        /// 釋放著
        /// </summary>
        CombatUnit SourceUnit { get; set; }
    }
}