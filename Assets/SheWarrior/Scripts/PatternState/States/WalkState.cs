using UnityEngine;

public class WalkState : IState
{
    private PlayerController m_Player;

    public WalkState(PlayerController player)
    {
        m_Player = player;
    }

    public void Enter()
    {
        // player.Animator.SetBool("isWalk", true);
    }

    public void Execute()
    {
        // if we are no longer grounded, transition to jumping
        if (!m_Player.IsGrounded)
        {
            m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.jumpState);
        }

        // if we slow to within a minimum velocity, transition to idling/standing
        if (Mathf.Abs(m_Player.Rigidbody.velocity.x) <= 0.1f)
        {
            m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.idleState);
        }
    }

    public void Exit()
    {
        // player.Animator.SetBool("isWalk", false);
    }	
}
