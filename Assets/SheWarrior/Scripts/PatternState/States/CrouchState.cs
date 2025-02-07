using UnityEngine;

public class CrouchState : IState
{
    private PlayerController m_Player;

    public CrouchState(PlayerController player)
    {
        m_Player = player;
    }

    public void Enter()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_CROUCH, true);
    }

    public void Execute()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_CROUCH, false);

        // if player releases down button, transition to crouching
        if (!m_Player.IsCrouched)
        {
            Debug.Log("RELEASE!!!");
            m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.idleState);
        }        
    }

    public void Exit()
    {
    }	
}
