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
        m_Player.Animator.SetBool(Constants.BOOL_STATE_FALL, true);
    }

    public void Execute()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_FALL, false);
        
        if (m_Player.IsGrounded)
        {
            if (Mathf.Abs(m_Player.Rigidbody.velocity.x) <= 0.1f)
            {
                m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.idleState);
            }
            else
            {
                m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.runState);
            }
        }
    }

    public void Exit()
    {
    }	
}
