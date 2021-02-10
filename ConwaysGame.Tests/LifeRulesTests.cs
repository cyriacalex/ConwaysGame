using ConwaysGame;
using NUnit.Framework;
using System;
using static ConwaysGame.LifeRules;

namespace ConwaysGame.Tests
{
    [TestFixture]
    public class LifeRulesTests
    {
        [Test]
        public void LiveCell_FewerThan2LiveNeighbors_Dies([Values(0,1)] int liveNeighbors)
        {
            //Arrange
            var currentState = CellState.Alive;
             
            //Act
            CellState newState = GetNewState(currentState, liveNeighbors);
            //Assert
            Assert.AreEqual(CellState.Dead, newState);
        }

        [Test]
        public void LiveCell_2OR3LiveNeighbors_Lives([Values(2,3)] int liveNeighbors)
        {
            //Arrange
            var currentState = CellState.Alive;
            //Act
            CellState newState = GetNewState(currentState, liveNeighbors);
            //Assert
            Assert.AreEqual(CellState.Alive, newState);
        }

        [Test]
        public void LiveCell_4ORMoreLiveNeighbors_Dies([Values(4,5,6,7,8)] int liveNeighbors)
        {
            //Arrange
            var currentState = CellState.Alive;
            //Act
            CellState newState = GetNewState(currentState, liveNeighbors);
            //Assert
            Assert.AreEqual(CellState.Dead, newState);
        }

        [Test]
        public void DeadCell_3LiveNeighbors_Lives()
        {
            //Arrange
            var liveNeighbors = 3;
            var currentState = CellState.Dead;
            //Act
            var newState = GetNewState(currentState, liveNeighbors);
            //Assert
            Assert.AreEqual(CellState.Alive, newState);
        }
    }
}
