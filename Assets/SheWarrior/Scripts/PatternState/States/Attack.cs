using System.Collections;
using UnityEngine;

public class AttackState : IState
{
    private PlayerController m_PlayerController;
    private StateMachine m_StateMachine;

    public AttackState(PlayerController player, StateMachine stateMachine)
    {
        m_PlayerController = player;
        m_StateMachine = stateMachine;
    }

    public void Enter()
    {
        m_PlayerController.Animator.SetBool(Constants.BOOL_STATE_ATTACK, true);
    }

    public void Execute()
    {
        if (!m_PlayerController.IsAttacking)
        {
            m_StateMachine.TransitionTo(m_StateMachine.IdleState);
        }        
    }

    public void Exit()
    {
        m_PlayerController.Animator.SetBool(Constants.BOOL_STATE_ATTACK, false);
    }	
}
