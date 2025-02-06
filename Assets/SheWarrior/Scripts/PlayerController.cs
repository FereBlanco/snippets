using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    private PlayerStateMachine m_PlayerStateMachine;
    public PlayerStateMachine PlayerStateMachine { get => m_PlayerStateMachine; }

    private Rigidbody2D m_Rigidbody;
    public Rigidbody2D Rigidbody { get => m_Rigidbody; }
    private Collider2D m_Collider;

    public float m_Speed = 4f;
    public float m_JumpForce = 6f;

    private Vector3 m_GroundCheckVectorOrigin;
    private float m_GroundCheckFactor = 1.05f;
    private float m_GroundCheckLength = 0.2f;


    private bool m_IsGrounded;
    public bool IsGrounded { get => m_IsGrounded;}

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<Collider2D>();

        m_GroundCheckVectorOrigin = new Vector3(0f, -1 * m_GroundCheckFactor * m_Collider.bounds.extents.y, 0f);

        m_PlayerStateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        m_PlayerStateMachine.Initialize(m_PlayerStateMachine.idleState);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && m_IsGrounded)
        {
            m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_JumpForce);
        }

        m_PlayerStateMachine.Execute();
    }

    private void FixedUpdate()
    {
        var hit = Physics2D.Raycast(transform.position + m_GroundCheckVectorOrigin, m_GroundCheckLength * Vector2.down, m_GroundCheckLength);
        m_IsGrounded = (null != hit.transform && hit.transform.gameObject.layer == LayerMask.NameToLayer(Constants.LAYER_GROUND));
        
        if (m_IsGrounded)
        {
            float horizontalInput = Input.GetAxis(Constants.AXIS_HORIZONTAL);
            m_Rigidbody.velocity = new Vector2(horizontalInput * m_Speed, m_Rigidbody.velocity.y);
        }
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 1000, 20), "Player State: " + PlayerStateMachine.CurrentState);
    }
}