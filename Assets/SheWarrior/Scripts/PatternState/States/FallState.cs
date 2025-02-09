using UnityEngine;

public class FallState : IState
{
    private PlayerController m_PlayerController;
    private StateMachine m_StateMachine;

    public FallState(PlayerController player, StateMachine stateMachine)
    {
        m_PlayerController = player;
        m_StateMachine = stateMachine;
    }

    public void Enter()
    {
        m_PlayerController.Animator.SetBool(Constants.BOOL_STATE_FALL, true);
    }

    public void Execute()
    {
        if (m_PlayerController.IsGrounded)
        {
            m_StateMachine.TransitionTo(m_StateMachine.IdleState);
        }
    }

    public void Exit()
    {
        m_PlayerController.Animator.SetBool(Constants.BOOL_STATE_FALL, false);
    }	
}
