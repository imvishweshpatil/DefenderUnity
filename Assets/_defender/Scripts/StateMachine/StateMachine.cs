using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEditor.Animations;

public class StateMachine
{
    private readonly List<StateTransition> _stateTransition = new List<StateTransition>();
    private readonly List<StateTransition> _anystateTransition = new List<StateTransition>();
    public IState CurrentState { get; private set; }

    public void SetState(IState state)
    {
        CurrentState = state;
    }

    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        var transition = new StateTransition(from, to, condition);
        _stateTransition.Add(transition);
    }

    public void Tick()
    {
        var transition = CheckForTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }

        CurrentState.Tick();
    }

    public void AddAnyTransition(IState to, Func<bool> condition)
    {
        var transition = new StateTransition(null, to, condition);
        _anystateTransition.Add(transition);
    }

    private StateTransition CheckForTransition()
    {
        var transition = _anystateTransition.FirstOrDefault(t => t.Condition());
        return transition ?? _stateTransition.FirstOrDefault(t => t.From == CurrentState && t.Condition());
    }
}