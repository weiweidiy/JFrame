namespace JFrame
{
    /// <summary>
    /// 攻击速度下降（只对普通攻击生效）： arg[1] ：百分比
    /// </summary>
    public class DebufferAttackSpeedDown : BufferAttackSpeedUp
    {
        public DebufferAttackSpeedDown(string UID, int id, int foldCount, float[] args) : base(UID, id, foldCount, args)
        {
        }

        protected override float CalcCD(float originValue)
        {
            var x = (1 - GetValue());
            if (x == 0) 
                throw new System.Exception("减少攻速公式分母不能为0 ");

            return originValue / (1 - GetValue());
        }
    }
}


