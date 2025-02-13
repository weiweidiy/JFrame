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
            var hp = new CombatAttributeDouble(CombatAttribute.CurHp.ToString(), unitInfo.hp, double.MaxValue);
            var atk = new CombatAttributeDouble(CombatAttribute.ATK.ToString(), unitInfo.atk, double.MaxValue);
            var maxHp = new CombatAttributeDouble(CombatAttribute.MaxHP.ToString(), unitInfo.maxHp, double.MaxValue);
            var cri = new CombatAttributeDouble(CombatAttribute.Critical.ToString(), unitInfo.cri, 1f);
            var criDmgRate = new CombatAttributeDouble(CombatAttribute.CriticalDamage.ToString(), unitInfo.criDmgRate, double.MaxValue);
            //var criDmgAnti = new CombatAttributeDouble(CombatAttribute.CriticalDamageResist.ToString(), unitInfo.criDmgAnti, 1f); //暴击伤害抵抗百分比
            //var skillDmgRate = new CombatAttributeDouble(CombatAttribute.SkillDamageEnhance.ToString(), unitInfo.skillDmgRate, double.MaxValue); //技能伤害加成百分比
            //var skillDmgAnti = new CombatAttributeDouble(CombatAttribute.SkillDamageReduce.ToString(), unitInfo.skillDmgAnti, 1f);
            //var dmgRate = new CombatAttributeDouble(CombatAttribute.DamageEnhance.ToString(), unitInfo.dmgRate, double.MaxValue);
            //var dmgAnti = new CombatAttributeDouble(CombatAttribute.DamageReduce.ToString(), unitInfo.dmgAnti, 1f);
            var debuffHit = new CombatAttributeDouble(CombatAttribute.ControlHit.ToString(), unitInfo.debuffHit, 1f);
            var debuffAnti = new CombatAttributeDouble(CombatAttribute.ControlResistance.ToString(), unitInfo.debuffAnti, 1f);
            //var penetrate = new CombatAttributeDouble(CombatAttribute.Puncture.ToString(), unitInfo.penetrate, 1f);
            //var block = new CombatAttributeDouble(CombatAttribute.Block.ToString(), unitInfo.block, 1f);

            //to do : 其他屬性
            result.Add(hp);
            result.Add(atk);
            result.Add(maxHp);
            result.Add(cri);
            result.Add(criDmgRate);
            //result.Add(criDmgAnti);
            //result.Add(skillDmgAnti);
            //result.Add(skillDmgRate);
            //result.Add(dmgRate);
            //result.Add(dmgAnti);
            result.Add(debuffHit);
            result.Add(debuffAnti);
            //result.Add(penetrate);
            //result.Add(block);
            return result;
        }
    }
}