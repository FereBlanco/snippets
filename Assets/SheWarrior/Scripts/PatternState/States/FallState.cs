using UnityEngine;

public class FallState : IState
{
    private PlayerController m_Player;

    public FallState(PlayerController player)
    {
        m_Player = player;
    }

    public void Enter()
    {
        // player.Animator.SetBool("isFall", true);
    }

    public void Execute()
    {
        if (m_Player.IsGrounded)
        {
            if (Mathf.Abs(m_Player.Rigidbody.velocity.x) <= 0.1f)
            {
                m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.idleState);
            }
            else
            {
                m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.walkState);
            }
        }
    }

    public void Exit()
    {
        // player.Animator.SetBool("isFall", false);
    }	
}
