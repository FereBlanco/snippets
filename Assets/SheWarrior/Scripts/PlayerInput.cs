using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float m_HorizontalInput;                
    public float HorizontalInput {
        get { return m_HorizontalInput; }
        private set { m_HorizontalInput = value; }
    }

    private float m_VerticalInput;
    public float VerticalInput {
        get { return m_VerticalInput; }
        private set { m_VerticalInput = value; }
    }
    
    private KeyCode m_AttackKey = KeyCode.Space;
    private bool m_AttackInput;
    public bool AttackInput {
        get { return m_AttackInput; }
        private set { m_AttackInput = value; }
    }
    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw(Constants.AXIS_HORIZONTAL);
        VerticalInput = Input.GetAxisRaw(Constants.AXIS_VERTICAL);        
        AttackInput = Input.GetKey(m_AttackKey);
    }
}
