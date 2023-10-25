using Unity.Plastic.Antlr3.Runtime.Misc;

public class StateTransition
{
    public readonly IState From;
    public readonly IState To;
    public readonly Func<bool> Condition;
    public StateTransition(IState from, IState to, Func<bool> condition)
    {
        From = from;
        To = to;
        Condition = condition;
    }
}