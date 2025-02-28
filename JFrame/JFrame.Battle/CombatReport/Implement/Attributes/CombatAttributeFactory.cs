using System.Collections.Generic;

namespace JFrame
{
    public enum CombatAttribute
    {
        ATK = 101,
        MaxHP = 102,
        CurHp = 103,
        MoveSpeed = 104,

        BPDamage = 202, //臂炮伤害加成
        BPDamageAnit = 203,

        Critical = 206, //暴击率
        CriticalAnti = 207, //暴击抵抗
        CriticalDamage = 208,
        Cd = 209, //cd 加成
        ControlHit = 212,
        ControlAnti = 213,

        DamageAdvance = 214, //伤害加成
        DamageAnti = 215,    //伤害抵抗

        Hit = 216,
        Dodge = 217,


        //AtkSpeed = 1000,
        //CriticalDamageResist,
        //SkillDamageEnhance,
        //SkillDamageReduce,
        //DamageEnhance,
        //DamageReduce,
        //Block,
        //Puncture,
        //ATKRate = 2000,
        //HPRate = 2001,

    }

    public class CombatAttributeFactory
    {
        /// <summary>
        /// 創建所有屬性對象
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public List<IUpdateable> CreateAllAttributes(CombatUnitInfo unitInfo)
        {
            var result = new List<IUpdateable>();
            var hp = new CombatAttributeDouble(CombatAttribute.CurHp.ToString(), unitInfo.hp, double.MaxValue);
            var atk = new CombatAttributeDouble(CombatAttribute.ATK.ToString(), unitInfo.atk, double.MaxValue);
            var maxHp = new CombatAttributeDouble(CombatAttribute.MaxHP.ToString(), unitInfo.maxHp, double.MaxValue);

            var bpDamage = new CombatAttributeDouble(CombatAttribute.BPDamage.ToString(), unitInfo.bpDamage, double.MaxValue);
            var bpDamageAnit = new CombatAttributeDouble(CombatAttribute.BPDamageAnit.ToString(), unitInfo.bpDamageAnti, double.MaxValue);

            var cri = new CombatAttributeDouble(CombatAttribute.Critical.ToString(), unitInfo.cri, double.MaxValue); //暴击率
            var criAnti = new CombatAttributeDouble(CombatAttribute.CriticalAnti.ToString(), unitInfo.criAnti, double.MaxValue);
            var criDmgRate = new CombatAttributeDouble(CombatAttribute.CriticalDamage.ToString(), unitInfo.criDamage, double.MaxValue);

            var damageAdvance = new CombatAttributeDouble(CombatAttribute.DamageAdvance.ToString(), unitInfo.damageAdvance, double.MaxValue);
            var damageAnti = new CombatAttributeDouble(CombatAttribute.DamageAnti.ToString(), unitInfo.damageAnti, double.MaxValue);
            var controlHit = new CombatAttributeDouble(CombatAttribute.ControlHit.ToString(), unitInfo.controlHit, double.MaxValue);
            var controlAnti = new CombatAttributeDouble(CombatAttribute.ControlAnti.ToString(), unitInfo.controlAnti, double.MaxValue);
            var hit = new CombatAttributeDouble(CombatAttribute.Hit.ToString(), unitInfo.hit, double.MaxValue);
            var dodge = new CombatAttributeDouble(CombatAttribute.Dodge.ToString(), unitInfo.dodge, double.MaxValue);


            //to do : 其他屬性
            result.Add(hp);
            result.Add(atk);
            result.Add(maxHp);
            result.Add(bpDamage);
            result.Add(bpDamageAnit);
            result.Add(cri);
            result.Add(criAnti);
            result.Add(criDmgRate);
            result.Add(damageAdvance);
            result.Add(damageAnti);
            result.Add(controlHit);
            result.Add(controlAnti);
            result.Add(hit);
            result.Add(dodge);

            return result;
        }
    }
}