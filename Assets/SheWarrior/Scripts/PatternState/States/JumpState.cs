using UnityEngine;

public class JumpState : IState
{
    private PlayerController m_Player;

    public JumpState(PlayerController player)
    {
        m_Player = player;
    }

    public void Enter()
    {
        // player.Animator.SetBool("isJump", true);
    }

    public void Execute()
    {
        if (m_Player.Rigidbody.velocity.y < 0)
        {
            m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.fallState);
        }
    }

    public void Exit()
    {
        // player.Animator.SetBool("isJump", false);
    }	
}
