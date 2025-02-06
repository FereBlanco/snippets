
using System;

[Serializable]
public class PlayerStateMachine
{
    public IState CurrentState { get; private set; }

    public IdleState idleState;
    public WalkState walkState;
    public JumpState jumpState;
    public FallState fallState;

    public PlayerStateMachine(PlayerController player)
    {
        idleState = new IdleState(player);
        walkState = new WalkState(player);
        jumpState = new JumpState(player);
        fallState = new FallState(player);
    }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void TransitionTo(IState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void Execute()
    {
        if (null != CurrentState)
        {
            CurrentState.Execute();
        }
    }
}
