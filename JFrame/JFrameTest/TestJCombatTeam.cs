using JFramework;
using JFramework.Game;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using JFrameTest;

namespace JFramework.Game.Tests
{
    [TestFixture]
    public class JCombatTeamTests
    {
        private IJCombatOperatable _unit1;
        private IJCombatOperatable _unit2;
        private IJCombatOperatable _unit3;
        private List<IJCombatOperatable> _testUnits;
        private Func<IJCombatOperatable, string> _keySelector;
        private JCombatTeam _combatTeam;

        [SetUp]
        public void SetUp()
        {
            // Create mock units
            _unit1 = Substitute.For<IJCombatOperatable>();
            _unit2 = Substitute.For<IJCombatOperatable>();
            _unit3 = Substitute.For<IJCombatOperatable>();

            // Setup key selector (using unit's hash code as key for testing)
            _keySelector = unit => unit.GetHashCode().ToString();

            // Create test team
            _testUnits = new List<IJCombatOperatable> { _unit1, _unit2, _unit3 };
            _combatTeam = new JCombatTeam("team1", _testUnits, _keySelector);
        }

        [Test]
        public void Constructor_InitializesWithUnits_AllUnitsAdded()
        {
            // Act
            var allUnits = _combatTeam.GetAllUnits();

            // Assert
            Assert.AreEqual(3, allUnits.Count);
            CollectionAssert.Contains(allUnits, _unit1);
            CollectionAssert.Contains(allUnits, _unit2);
            CollectionAssert.Contains(allUnits, _unit3);
        }

        [Test]
        public void GetAllUnit_ReturnsAllUnits()
        {
            // Act
            var result = _combatTeam.GetAllUnits();

            // Assert
            Assert.AreEqual(_testUnits.Count, result.Count);
            CollectionAssert.AreEquivalent(_testUnits, result);
        }

        [Test]
        public void IsAllDead_AllUnitsDead_ReturnsTrue()
        {
            // Arrange
            _unit1.IsDead().Returns(true);
            _unit2.IsDead().Returns(true);
            _unit3.IsDead().Returns(true);

            // Act
            var result = _combatTeam.IsAllDead();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsAllDead_OneUnitAlive_ReturnsFalse()
        {
            // Arrange
            _unit1.IsDead().Returns(true);
            _unit2.IsDead().Returns(false); // One unit alive
            _unit3.IsDead().Returns(true);

            // Act
            var result = _combatTeam.IsAllDead();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsAllDead_AllUnitsAlive_ReturnsFalse()
        {
            // Arrange
            _unit1.IsDead().Returns(false);
            _unit2.IsDead().Returns(false);
            _unit3.IsDead().Returns(false);

            // Act
            var result = _combatTeam.IsAllDead();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsAllDead_EmptyTeam_ReturnsTrue()
        {
            // Arrange
            var emptyTeam = new JCombatTeam("team1", new List<IJCombatOperatable>(), _keySelector);

            // Act
            var result = emptyTeam.IsAllDead();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DictionaryContainer_CanGetUnitByKey()
        {
            // Arrange
            var key = _keySelector(_unit2);

            // Act
            var unit = _combatTeam.Get(key);

            // Assert
            Assert.AreEqual(_unit2, unit);
        }

        [Test]
        public void DictionaryContainer_GetNonExistentKey_ReturnsNull()
        {
            // Act
            //var unit = _combatTeam.Get("nonexistent_key");

            // Assert
            Assert.IsNull(_combatTeam.Get("nonexistent_key"));
        }

        [Test]
        public void GetUnit_WithValidUid_ShouldReturnCorrectUnit()
        {
            // 准备模拟数据
            var unit1 = Substitute.For<IJCombatOperatable>();
            unit1.Uid.Returns("1");

            var unit2 = Substitute.For<IJCombatOperatable>();
            unit2.Uid.Returns("2");

            var units = new List<IJCombatOperatable> { unit1, unit2 };

            // 创建测试对象
            var team = new JCombatTeam("team1", units, u => u.Uid);

            // 执行测试
            var result = team.GetUnit("1");

            // 验证结果
            Assert.That(result, Is.EqualTo(unit1));
        }

        // 测试异常情况：UID不存在时返回null
        [Test]
        public void GetUnit_WithInvalidUid_ShouldReturnNull()
        {
            // 准备模拟数据
            var unit1 = Substitute.For<IJCombatOperatable>();
            unit1.Uid.Returns("1");

            var units = new List<IJCombatOperatable> { unit1 };

            // 创建测试对象
            var team = new JCombatTeam("team1", units, u => u.Uid);

            // 执行测试
            //var result = team.GetUnit("nonexistent_unit");

            // 验证结果
            Assert.IsNull( team.GetUnit("nonexistent_unit"));
        }

        // 测试边界情况：空单位列表
        [Test]
        public void GetUnit_WithEmptyUnitList_ShouldReturnNull()
        {
            // 准备空列表
            var units = new List<IJCombatOperatable>();

            // 创建测试对象
            var team = new JCombatTeam("team1", units, u => u.Uid);

            // 执行测试
           // var result = team.GetUnit("any_unit");

            // 验证结果
            Assert.IsNull(team.GetUnit("any_unit"));
        }
    }
}