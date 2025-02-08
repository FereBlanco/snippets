using System.Collections;
using UnityEngine;

public class CrouchState : IState
{
    private PlayerController m_Player;
    private bool m_ExitExecuted;

    public CrouchState(PlayerController player)
    {
        m_Player = player;
    }

    public void Enter()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_CROUCH, true);
        m_Player.Animator.SetBool(Constants.BOOL_STATE_CROUCH_TO_IDLE, false);
        m_ExitExecuted = false;
    }

    public void Execute()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_CROUCH, false);

        // if player releases down button, transition to crouching
        if (!m_Player.IsCrouched && !m_ExitExecuted)
        {
            Debug.Log("RELEASE!!!");
            m_ExitExecuted = true;
            m_Player.Animator.SetBool(Constants.BOOL_STATE_CROUCH_TO_IDLE, true);
            m_Player.StartCoroutine(DelayExit());
            // m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.idleState);
            // this.Exit();
        }        
    }

    IEnumerator DelayExit()
    {
        m_Player.Animator.SetBool(Constants.BOOL_STATE_CROUCH_TO_IDLE, false);
        yield return new WaitForSeconds(0.5f);
        m_Player.PlayerStateMachine.TransitionTo(m_Player.PlayerStateMachine.idleState);
    }

    public void Exit()
    {
        Debug.Log("POST RELEASE!!!");
    }	
}
