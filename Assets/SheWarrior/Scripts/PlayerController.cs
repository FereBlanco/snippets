using NUnit.Framework;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{

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
    public bool IsGrounded { get => m_IsGrounded; private set { m_IsGrounded = value; } }
    private bool m_IsCrouched;
    public bool IsCrouched { get => m_IsCrouched; private set { m_IsCrouched = value; } }
    private bool m_IsAttacking;
    public bool IsAttacking { get => m_IsAttacking; private set { m_IsAttacking = value; } }
    [SerializeField] private AnimationClip m_AttackAnimationClip;
    private WaitForSeconds m_WaitTimeAttack;
    public WaitForSeconds WaitTimeAttack { get => m_WaitTimeAttack; }

    private void Awake()
    {
        Assert.IsNotNull(m_Animator, "ERROR: m_Animator not assined in PlayerController.cs");
        Assert.IsNotNull(m_AttackAnimationClip, "ERROR: m_AttackAnimationClip not assined in PlayerController.cs");

        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<Collider2D>();
        m_SpriteRenderer = Animator.GetComponent<SpriteRenderer>();

        m_GroundCheckVectorOrigin = new Vector3(0f, -1 * m_GroundCheckFactor * m_Collider.bounds.extents.y, 0f);

        m_WaitTimeAttack = new WaitForSeconds(m_AttackAnimationClip.length);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && IsGrounded && !IsCrouched && !IsAttacking)
        {
            m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_JumpForce);
        }

        if (Input.GetKey(KeyCode.Space) && IsGrounded && !IsCrouched && !IsAttacking && 0 == m_HorizontalInput)
        {
            IsAttacking = true;
            StartCoroutine(WaitAttackTime());
        }
    }

    private void FixedUpdate()
    {
         m_HorizontalInput = Input.GetAxisRaw(Constants.AXIS_HORIZONTAL);
         m_VerticalInput = Input.GetAxisRaw(Constants.AXIS_VERTICAL);

        #if UNITY_EDITOR
        Debug.DrawRay(transform.position + m_GroundCheckVectorOrigin, m_GroundCheckLength * Vector2.down, Color.red);
        #endif
        var hit = Physics2D.Raycast(transform.position + m_GroundCheckVectorOrigin, m_GroundCheckLength * Vector2.down, m_GroundCheckLength);
        IsCrouched = IsGrounded && !IsAttacking && m_VerticalInput < 0;
        IsGrounded = (null != hit.transform && hit.transform.gameObject.layer == LayerMask.NameToLayer(Constants.LAYER_GROUND));
        
        if (m_HorizontalInput > 0) m_SpriteRenderer.flipX = false;
        if (m_HorizontalInput < 0) m_SpriteRenderer.flipX = true;
        if (IsGrounded && !IsCrouched && !IsAttacking)
        {
            m_Rigidbody.velocity = new Vector2(m_HorizontalInput * m_Speed, m_Rigidbody.velocity.y);
        }
    }

    public void WaitFrameBetweenStatesTransition()
    {
        StartCoroutine(WaitOneFrame());
    }
    IEnumerator WaitAttackTime()
    {
        yield return m_WaitTimeAttack;
        IsAttacking = false;
    }

    IEnumerator WaitOneFrame()
    {
        yield return null;
    }     
}