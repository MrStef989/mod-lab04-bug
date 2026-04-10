using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugPro;

namespace BugTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test01_InitialStateIsOpen()
        {
            var bug = new Bug();
            Assert.AreEqual(State.Open, bug.CurrentState);
        }

        [TestMethod]
        public void Test02_AssignFromOpen()
        {
            var bug = new Bug();
            bug.Assign();
            Assert.AreEqual(State.Assigned, bug.CurrentState);
        }

        [TestMethod]
        public void Test03_StartFromAssigned()
        {
            var bug = new Bug();
            bug.Assign();
            bug.Start();
            Assert.AreEqual(State.InProgress, bug.CurrentState);
        }

        [TestMethod]
        public void Test04_ResolveFromInProgress()
        {
            var bug = new Bug();
            bug.Assign();
            bug.Start();
            bug.Resolve();
            Assert.AreEqual(State.Resolved, bug.CurrentState);
        }

        [TestMethod]
        public void Test05_CloseFromResolved()
        {
            var bug = new Bug();
            bug.Assign();
            bug.Start();
            bug.Resolve();
            bug.Close();
            Assert.AreEqual(State.Closed, bug.CurrentState);
        }

        [TestMethod]
        public void Test06_ReopenFromClosed()
        {
            var bug = new Bug();
            bug.Assign();
            bug.Start();
            bug.Resolve();
            bug.Close();
            bug.Reopen();
            Assert.AreEqual(State.Reopened, bug.CurrentState);
        }

        [TestMethod]
        public void Test07_ReopenFromResolved()
        {
            var bug = new Bug();
            bug.Assign();
            bug.Start();
            bug.Resolve();
            bug.Reopen();
            Assert.AreEqual(State.Reopened, bug.CurrentState);
        }

        [TestMethod]
        public void Test08_AssignFromReopened()
        {
            var bug = new Bug();
            bug.Assign();
            bug.Start();
            bug.Resolve();
            bug.Close();
            bug.Reopen();
            bug.Assign();
            Assert.AreEqual(State.Assigned, bug.CurrentState);
        }

        [TestMethod]
        public void Test09_ReassignFromInProgress()
        {
            var bug = new Bug();
            bug.Assign();
            bug.Start();
            bug.Assign();
            Assert.AreEqual(State.Assigned, bug.CurrentState);
        }

        [TestMethod]
        public void Test10_FullCycleWithReopen()
        {
            var bug = new Bug();
            bug.Assign();
            bug.Start();
            bug.Resolve();
            bug.Close();
            bug.Reopen();
            bug.Assign();
            bug.Start();
            Assert.AreEqual(State.InProgress, bug.CurrentState);
        }

        [TestMethod]
        public void Test11_InvalidTransitionThrows()
        {
            var bug = new Bug();
            Assert.ThrowsException<InvalidOperationException>(() => bug.Start());
        }

        [TestMethod]
        public void Test12_InitialStateAssigned()
        {
            var bug = new Bug(State.Assigned);
            Assert.AreEqual(State.Assigned, bug.CurrentState);
        }
    }
}