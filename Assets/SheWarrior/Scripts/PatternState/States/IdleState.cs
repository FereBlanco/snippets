using UnityEngine;

public class IdleState : IState
{
    private PlayerController m_PlayerController;
    private StateMachine m_StateMachine;

    public IdleState(PlayerController player, StateMachine stateMachine)
    {
        m_PlayerController = player;
        m_StateMachine = stateMachine;
    }

    public void Enter()
    {
        m_PlayerController.Animator.SetBool(Constants.BOOL_STATE_IDLE, true);
    }

    public void Execute()
    {
        if (m_PlayerController.IsAttacking)
        {
            m_StateMachine.TransitionTo(m_StateMachine.AttackState);
        }
        else if (m_PlayerController.IsCrouched)
        {
            m_StateMachine.TransitionTo(m_StateMachine.CrouchState);
        }
        else if (!m_PlayerController.IsGrounded)
        {
            m_StateMachine.TransitionTo(m_StateMachine.JumpState);
        }
        else if (Mathf.Abs(m_PlayerController.Rigidbody.velocity.x) > 0.1f)
        {
            m_StateMachine.TransitionTo(m_StateMachine.RunState);
        }        


    }

    public void Exit()
    {
        m_PlayerController.Animator.SetBool(Constants.BOOL_STATE_IDLE, false);
    }	
}
