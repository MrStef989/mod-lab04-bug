using Stateless;

namespace BugPro
{
    public enum State
    {
        New,
        Triage,
        Fixing,
        Closed,
        Reopened
    }

    public enum Trigger
    {
        Open,
        Fix,
        Defer,
        CloseInvalid,
        Solved,
        NotSolved,
        Reopen,
        Return
    }

    public class Bug
    {
        private StateMachine<State, Trigger> _machine;
        public State CurrentState => _machine.State;

        public Bug(State initialState = State.New)
        {
            _machine = new StateMachine<State, Trigger>(initialState);

            _machine.Configure(State.New)
                .Permit(Trigger.Open, State.Triage);

            _machine.Configure(State.Triage)
                .Permit(Trigger.Fix, State.Fixing)
                .PermitReentry(Trigger.Defer)
                .Permit(Trigger.CloseInvalid, State.Closed);

            _machine.Configure(State.Fixing)
                .Permit(Trigger.Solved, State.Closed)
                .Permit(Trigger.NotSolved, State.Reopened);

            _machine.Configure(State.Closed)
                .Permit(Trigger.Return, State.Triage);

            _machine.Configure(State.Reopened)
                .Permit(Trigger.Reopen, State.Triage);
        }

        public void Open()         => _machine.Fire(Trigger.Open);
        public void Fix()          => _machine.Fire(Trigger.Fix);
        public void Defer()        => _machine.Fire(Trigger.Defer);
        public void CloseInvalid() => _machine.Fire(Trigger.CloseInvalid);
        public void Solved()       => _machine.Fire(Trigger.Solved);
        public void NotSolved()    => _machine.Fire(Trigger.NotSolved);
        public void Reopen()       => _machine.Fire(Trigger.Reopen);
        public void Return()       => _machine.Fire(Trigger.Return);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Bug Workflow Demo ===\n");

            Console.WriteLine("Scenario 1: Bug fixed successfully");
            var bug1 = new Bug();
            Console.WriteLine($"  {bug1.CurrentState}");
            bug1.Open();
            Console.WriteLine($"  Open -> {bug1.CurrentState}");
            bug1.Fix();
            Console.WriteLine($"  Fix -> {bug1.CurrentState}");
            bug1.Solved();
            Console.WriteLine($"  Solved -> {bug1.CurrentState}");

            Console.WriteLine("\nScenario 2: Bug closed as invalid");
            var bug2 = new Bug();
            bug2.Open();
            Console.WriteLine($"  Open -> {bug2.CurrentState}");
            bug2.CloseInvalid();
            Console.WriteLine($"  CloseInvalid -> {bug2.CurrentState}");
            bug2.Return();
            Console.WriteLine($"  Return -> {bug2.CurrentState}");

            Console.WriteLine("\nScenario 3: Bug not solved, reopened");
            var bug3 = new Bug();
            bug3.Open();
            bug3.Fix();
            Console.WriteLine($"  Fixing -> {bug3.CurrentState}");
            bug3.NotSolved();
            Console.WriteLine($"  NotSolved -> {bug3.CurrentState}");
            bug3.Reopen();
            Console.WriteLine($"  Reopen -> {bug3.CurrentState}");
        }
    }
}
