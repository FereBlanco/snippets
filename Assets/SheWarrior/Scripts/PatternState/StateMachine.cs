
using System;

[Serializable]
public class StateMachine
{
    public IState CurrentState { get; private set; }

    public IdleState IdleState;
    public RunState RunState;
    public JumpState JumpState;
    public FallState FallState;
    public CrouchState CrouchState;
    public AttackState AttackState;

    private PlayerController m_Player;

    public StateMachine(PlayerController player)
    {
        m_Player = player;

        IdleState = new IdleState(player, this);
        RunState = new RunState(player, this);
        JumpState = new JumpState(player, this);
        FallState = new FallState(player, this);
        CrouchState = new CrouchState(player, this);
        AttackState = new AttackState(player, this);
    }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void TransitionTo(IState newState)
    {
        CurrentState.Exit();
        m_Player.WaitFrameBetweenStatesTransition();
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
