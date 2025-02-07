using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private PlayerStateMachine m_PlayerStateMachine;
    public PlayerStateMachine PlayerStateMachine { get => m_PlayerStateMachine; }

    private Rigidbody2D m_Rigidbody;
    public Rigidbody2D Rigidbody { get => m_Rigidbody; }
    private Collider2D m_Collider;
    [SerializeField] private Animator m_Animator;
    public Animator Animator { get => m_Animator; }
    private SpriteRenderer m_SpriteRenderer;

    public float m_Speed = 4f;
    public float m_JumpForce = 6f;

    private Vector3 m_GroundCheckVectorOrigin;
    private float m_GroundCheckFactor = 1.08f;
    private float m_GroundCheckLength = 0.05f;


    private float m_HorizontalInput;
    private float m_VerticalInput;
    private bool m_IsGrounded;
    public bool IsGrounded { get => m_IsGrounded;}
    private bool m_IsCrouched;
    public bool IsCrouched { get => m_IsCrouched; }

    private void Awake()
    {
        Assert.IsNotNull(m_Animator, "ERROR: m_Animator not assined in PlayerController.cs");

        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<Collider2D>();
        m_SpriteRenderer = Animator.GetComponent<SpriteRenderer>();

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
         m_HorizontalInput = Input.GetAxis(Constants.AXIS_HORIZONTAL);
         m_VerticalInput = Input.GetAxis(Constants.AXIS_VERTICAL);

        #if UNITY_EDITOR
        Debug.DrawRay(transform.position + m_GroundCheckVectorOrigin, m_GroundCheckLength * Vector2.down, Color.red);
        #endif
        var hit = Physics2D.Raycast(transform.position + m_GroundCheckVectorOrigin, m_GroundCheckLength * Vector2.down, m_GroundCheckLength);
        m_IsGrounded = (null != hit.transform && hit.transform.gameObject.layer == LayerMask.NameToLayer(Constants.LAYER_GROUND));
        m_IsCrouched = m_IsGrounded && m_VerticalInput < 0;
        
        if (m_HorizontalInput > 0) m_SpriteRenderer.flipX = false;
        if (m_HorizontalInput < 0) m_SpriteRenderer.flipX = true;
        if (m_IsGrounded)
        {
            m_Rigidbody.velocity = new Vector2(m_HorizontalInput * m_Speed, m_Rigidbody.velocity.y);
        }
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 1000, 20), "Player State: " + PlayerStateMachine.CurrentState);
    }
}