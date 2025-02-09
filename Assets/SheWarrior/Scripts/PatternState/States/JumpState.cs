using UnityEngine;

public class JumpState : IState
{
    private PlayerController m_PlayerController;
    private StateMachine m_StateMachine;

    public JumpState(PlayerController player, StateMachine stateMachine)
    {
        m_PlayerController = player;
        m_StateMachine = stateMachine;
    }

    public void Enter()
    {
        m_PlayerController.Animator.SetBool(Constants.BOOL_STATE_JUMP, true);
    }

    public void Execute()
    {
        if (m_PlayerController.Rigidbody.velocity.y < 0)
        {
            m_StateMachine.TransitionTo(m_StateMachine.FallState);
        }
    }

    public void Exit()
    {
        m_PlayerController.Animator.SetBool(Constants.BOOL_STATE_JUMP, false);
    }	
}
