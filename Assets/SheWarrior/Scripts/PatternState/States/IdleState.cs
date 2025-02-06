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
        // player.Animator.SetBool("isIdle", true);
    }

    public void Execute()
    {
        // if we're no longer grounded, transition to jumping
        if (!m_Player.IsGrounded)
        {
            m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.jumpState);
        }

        // if we move above a minimum threshold, transition to walking
        if (Mathf.Abs(m_Player.Rigidbody.velocity.x) > 0.1f)
        {
            m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.walkState);
        }        
    }

    public void Exit()
    {
        // player.Animator.SetBool("isIdle", false);
    }	
}
