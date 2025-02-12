using System.Collections.Generic;

namespace JFrame
{
    public class CombatAttributeFactory
    {
        /// <summary>
        /// 創建所有屬性對象
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public List<IUnique> CreateAllAttributes(CombatUnitInfo unitInfo)
        {
            var result = new List<IUnique>();
            var hp = new CombatAttributeDouble(PVPAttribute.HP.ToString(), unitInfo.hp, double.MaxValue);
            var atk = new CombatAttributeDouble(PVPAttribute.ATK.ToString(), unitInfo.atk, double.MaxValue);
            var maxHp = new CombatAttributeDouble(PVPAttribute.MaxHP.ToString(), unitInfo.atkSpeed, double.MaxValue);
            var cri = new CombatAttributeDouble(PVPAttribute.Critical.ToString(), unitInfo.cri, 1f);
            var criDmgRate = new CombatAttributeDouble(PVPAttribute.CriticalDamage.ToString(), unitInfo.criDmgRate, double.MaxValue);
            var criDmgAnti = new CombatAttributeDouble(PVPAttribute.CriticalDamageResist.ToString(), unitInfo.criDmgAnti, 1f); //暴击伤害抵抗百分比
            var skillDmgRate = new CombatAttributeDouble(PVPAttribute.SkillDamageEnhance.ToString(), unitInfo.skillDmgRate, double.MaxValue); //技能伤害加成百分比
            var skillDmgAnti = new CombatAttributeDouble(PVPAttribute.SkillDamageReduce.ToString(), unitInfo.skillDmgAnti, 1f);
            var dmgRate = new CombatAttributeDouble(PVPAttribute.DamageEnhance.ToString(), unitInfo.dmgRate, double.MaxValue);
            var dmgAnti = new CombatAttributeDouble(PVPAttribute.DamageReduce.ToString(), unitInfo.dmgAnti, 1f);
            var debuffHit = new CombatAttributeDouble(PVPAttribute.ControlHit.ToString(), unitInfo.debuffHit, 1f);
            var debuffAnti = new CombatAttributeDouble(PVPAttribute.ControlResistance.ToString(), unitInfo.debuffAnti, 1f);
            var penetrate = new CombatAttributeDouble(PVPAttribute.Puncture.ToString(), unitInfo.penetrate, 1f);
            var block = new CombatAttributeDouble(PVPAttribute.Block.ToString(), unitInfo.block, 1f);

            //to do : 其他屬性
            result.Add(hp);
            result.Add(atk);
            result.Add(maxHp);
            result.Add(cri);
            result.Add(criDmgRate);
            result.Add(criDmgAnti);
            result.Add(skillDmgAnti);
            result.Add(skillDmgRate);
            result.Add(dmgRate);
            result.Add(dmgAnti);
            result.Add(debuffHit);
            result.Add(debuffAnti);
            result.Add(penetrate);
            result.Add(block);
            return result;
        }
    }
}