using System.Collections;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerStateMachine : MonoBehaviour
{
    private StateMachine m_PlayerStateMachine;
    public StateMachine StateMachine { get => m_PlayerStateMachine; private set { m_PlayerStateMachine = value; } }

    [SerializeField] PlayerController m_PlayerController;

    private void Awake()
    {
        m_PlayerController = GetComponent<PlayerController>();
        StateMachine = new StateMachine(m_PlayerController);
    }

    private void Start()
    {
        StateMachine.Initialize(StateMachine.IdleState);
    }

    private void Update()
    {
        StateMachine.Execute();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 1000, 20), "Player State: " + StateMachine.CurrentState);
    }

}
