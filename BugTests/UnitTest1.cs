using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugPro;
using System;
namespace BugTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test01_InitialStateIsNew()
        {
            var bug = new Bug();
            Assert.AreEqual(State.New, bug.CurrentState);
        }

        [TestMethod]
        public void Test02_OpenMovesToTriage()
        {
            var bug = new Bug();
            bug.Open();
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }

        [TestMethod]
        public void Test03_FixMovesToFixing()
        {
            var bug = new Bug();
            bug.Open();
            bug.Fix();
            Assert.AreEqual(State.Fixing, bug.CurrentState);
        }

        [TestMethod]
        public void Test04_SolvedMovesToClosed()
        {
            var bug = new Bug();
            bug.Open();
            bug.Fix();
            bug.Solved();
            Assert.AreEqual(State.Closed, bug.CurrentState);
        }

        [TestMethod]
        public void Test05_CloseInvalidMovesToClosed()
        {
            var bug = new Bug();
            bug.Open();
            bug.CloseInvalid();
            Assert.AreEqual(State.Closed, bug.CurrentState);
        }

        [TestMethod]
        public void Test06_NotSolvedMovesToReopened()
        {
            var bug = new Bug();
            bug.Open();
            bug.Fix();
            bug.NotSolved();
            Assert.AreEqual(State.Reopened, bug.CurrentState);
        }

        [TestMethod]
        public void Test07_ReopenMovesToTriage()
        {
            var bug = new Bug();
            bug.Open();
            bug.Fix();
            bug.NotSolved();
            bug.Reopen();
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }

        [TestMethod]
        public void Test08_ReturnFromClosedToTriage()
        {
            var bug = new Bug();
            bug.Open();
            bug.CloseInvalid();
            bug.Return();
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }

        [TestMethod]
        public void Test09_DeferStaysInTriage()
        {
            var bug = new Bug();
            bug.Open();
            bug.Defer();
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }

        [TestMethod]
        public void Test10_FullCycleReopenAndFix()
        {
            var bug = new Bug();
            bug.Open();
            bug.Fix();
            bug.NotSolved();
            bug.Reopen();
            bug.Fix();
            bug.Solved();
            Assert.AreEqual(State.Closed, bug.CurrentState);
        }

        [TestMethod]
        public void Test11_InvalidTransitionThrows()
        {
            var bug = new Bug();
            Assert.ThrowsException<InvalidOperationException>(() => bug.Fix());
        }

        [TestMethod]
        public void Test12_InitialStateSetToTriage()
        {
            var bug = new Bug(State.Triage);
            Assert.AreEqual(State.Triage, bug.CurrentState);
        }
    }
}
