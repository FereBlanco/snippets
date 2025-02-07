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
        m_Player.Animator.SetBool(Constants.BOOL_STATE_JUMP, true);
    }

    public void Execute()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_JUMP, false);
        
        if (m_Player.Rigidbody.velocity.y < 0)
        {
            m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.fallState);
        }
    }

    public void Exit()
    {
    }	
}
