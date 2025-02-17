using System.Collections;
using UnityEngine;

public class CrouchState : IState
{
     private PlayerController m_PlayerController;
    private StateMachine m_StateMachine;

    public CrouchState(PlayerController player, StateMachine stateMachine)
    {
        m_PlayerController = player;
        m_StateMachine = stateMachine;
    }

    public void Enter()
    {
        m_PlayerController.Animator.SetBool(Constants.BOOL_STATE_CROUCH, true);
    }

    public void Execute()
    {
        if (!m_PlayerController.IsCrouched)
        {
            m_StateMachine.TransitionTo(m_StateMachine.IdleState);
        }        
    }

    public void Exit()
    {
        m_PlayerController.Animator.SetBool(Constants.BOOL_STATE_CROUCH, false);
    }	
}
