using Stateless;

namespace BugPro
{
    public enum State
    {
        Open, Assigned, InProgress, Resolved, Closed, Reopened
    }

    public enum Trigger
    {
        Assign, Start, Resolve, Close, Reopen
    }

    public class Bug
    {
        private StateMachine<State, Trigger> _machine;
        public State CurrentState => _machine.State;

        public Bug(State initialState = State.Open)
        {
            _machine = new StateMachine<State, Trigger>(initialState);

            _machine.Configure(State.Open)
                .Permit(Trigger.Assign, State.Assigned);

            _machine.Configure(State.Assigned)
                .Permit(Trigger.Start, State.InProgress)
                .Permit(Trigger.Reopen, State.Reopened);

            _machine.Configure(State.InProgress)
                .Permit(Trigger.Resolve, State.Resolved)
                .Permit(Trigger.Assign, State.Assigned);

            _machine.Configure(State.Resolved)
                .Permit(Trigger.Close, State.Closed)
                .Permit(Trigger.Reopen, State.Reopened);

            _machine.Configure(State.Closed)
                .Permit(Trigger.Reopen, State.Reopened);

            _machine.Configure(State.Reopened)
                .Permit(Trigger.Assign, State.Assigned);
        }

        public void Assign()   => _machine.Fire(Trigger.Assign);
        public void Start()    => _machine.Fire(Trigger.Start);
        public void Resolve()  => _machine.Fire(Trigger.Resolve);
        public void Close()    => _machine.Fire(Trigger.Close);
        public void Reopen()   => _machine.Fire(Trigger.Reopen);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var bug = new Bug();
            Console.WriteLine($"State: {bug.CurrentState}");
            bug.Assign();
            Console.WriteLine($"State: {bug.CurrentState}");
            bug.Start();
            Console.WriteLine($"State: {bug.CurrentState}");
            bug.Resolve();
            Console.WriteLine($"State: {bug.CurrentState}");
            bug.Close();
            Console.WriteLine($"State: {bug.CurrentState}");
            bug.Reopen();
            Console.WriteLine($"State: {bug.CurrentState}");
            bug.Assign();
            Console.WriteLine($"State: {bug.CurrentState}");
        }
    }
}