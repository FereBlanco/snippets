using UnityEngine;

public class IdleState : IState
{
    private PlayerController m_Player;

    public IdleState(PlayerController player)
    {
        m_Player = player;
    }

    public void Enter()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_IDLE, true);
    }

    public void Execute()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_IDLE, false);
        
        // if we're no longer grounded, transition to jumping
        if (!m_Player.IsGrounded)
        {
            m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.jumpState);
        }

        // if we move above a minimum threshold, transition to walking
        if (m_Player.IsGrounded && Mathf.Abs(m_Player.Rigidbody.velocity.x) > 0.1f)
        {
            m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.runState);
        }        

        // if player pushes down button, transition to crouching
        if (m_Player.IsCrouched)
        {
            m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.crouchState);
        }        
    }

    public void Exit()
    {
    }	
}
