using UnityEngine;

public class RunState : IState
{
    private PlayerController m_Player;

    public RunState(PlayerController player)
    {
        m_Player = player;
    }

    public void Enter()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_TO_RUN, true);
    }

    public void Execute()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_TO_RUN, false);

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
    }	
}
