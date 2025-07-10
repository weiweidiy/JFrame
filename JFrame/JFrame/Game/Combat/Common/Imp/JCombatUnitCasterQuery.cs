namespace JFramework.Game
{
    public class JCombatUnitCasterQuery : IJcombatCasterQuery
    {
        IJCombatAttributeable caster;
        public JCombatUnitCasterQuery(IJCombatAttributeable caster)
        {
            this.caster = caster;
        }
        public string GetCasterUid()
        {
            return caster.Uid;
        }
    }

    //在buffer的构造函数中创建，返回的是Buffer的Caster
    public class JCombatBufferCasterQuery : IJcombatCasterQuery
    {
        public string GetCasterUid()
        {
            throw new System.NotImplementedException();
        }
    }
}
