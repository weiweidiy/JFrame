using JFramework;
using JFrame.Game;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using JFrameTest;

namespace JFrame.Game.Tests
{
    [TestFixture]
    public class JCombatTeamTests
    {
        private IJCombatUnit _unit1;
        private IJCombatUnit _unit2;
        private IJCombatUnit _unit3;
        private List<IJCombatUnit> _testUnits;
        private Func<IJCombatUnit, string> _keySelector;
        private JCombatTeam _combatTeam;

        [SetUp]
        public void SetUp()
        {
            // Create mock units
            _unit1 = Substitute.For<IJCombatUnit>();
            _unit2 = Substitute.For<IJCombatUnit>();
            _unit3 = Substitute.For<IJCombatUnit>();

            // Setup key selector (using unit's hash code as key for testing)
            _keySelector = unit => unit.GetHashCode().ToString();

            // Create test team
            _testUnits = new List<IJCombatUnit> { _unit1, _unit2, _unit3 };
            _combatTeam = new JCombatTeam(_testUnits, _keySelector);
        }

        [Test]
        public void Constructor_InitializesWithUnits_AllUnitsAdded()
        {
            // Act
            var allUnits = _combatTeam.GetAllUnit();

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
            var result = _combatTeam.GetAllUnit();

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
            var emptyTeam = new JCombatTeam(new List<IJCombatUnit>(), _keySelector);

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
            Assert.Throws<KeyNotFoundException>(() => _combatTeam.Get("nonexistent_key"));
        }
    }
}