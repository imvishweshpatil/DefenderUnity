using NSubstitute;
using NUnit.Framework;

public class state_machine_tests
{
    [Test]
    public void initial_set_state_switches_to_state()
    {
        var stateMachine = new StateMachine();
        var firstState = Substitute.For<IState>();
        stateMachine.SetState(firstState);
        Assert.AreSame(firstState, stateMachine.CurrentState);
    }
}