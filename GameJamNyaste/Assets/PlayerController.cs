using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_GroundCheck1;
    [SerializeField] private Transform m_CeilingCheck;
    [SerializeField] private Collider2D m_CrouchDisableCollider;

    const float k_GroundedRadius = .2f;
    public bool m_Grounded;
    const float k_CeilingRadius = .2f;
    private Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    [SerializeField] private float m_MaxSpeed = 10f;
    [SerializeField] private float m_Acceleration = 5f;

    // Sprinting Settings
    [Header("Sprinting Settings")]
    [SerializeField] private float sprintMultiplier = 1.3f;
    public Animator animator;
    // Dash Settings
    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private bool isDashing = false;
    private float dashTimer = 0f;

    private Camera mainCam;

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    void Start()
    {
        mainCam = Camera.main;
    }

    void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        Collider2D[] colliders1 = Physics2D.OverlapCircleAll(m_GroundCheck1.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
        for (int i = 0; i < colliders1.Length; i++)
        {
            if (colliders1[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        if (m_Grounded || m_AirControl)
        {
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                move *= m_CrouchSpeed;

                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Sprinting check
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move *= sprintMultiplier;
            }

            // Dash check
            if (Input.GetKeyDown(KeyCode.F))
            {
                Dash();
            }

            float targetSpeed = Mathf.MoveTowards(m_Rigidbody2D.velocity.x, move * m_MaxSpeed, m_Acceleration * Time.fixedDeltaTime);
            Vector3 targetVelocity = new Vector2(targetSpeed, m_Rigidbody2D.velocity.y);

            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

            jumpBufferCounter = 0f;
        }
    }

    private void Dash()
    {
        if (!isDashing && dashTimer <= 0f)
        {
            isDashing = true;
            dashTimer = dashCooldown;

            // Determine dash direction based on mouse position
            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dashDirection = (mousePos - transform.position).normalized;

            // Set the player's velocity to achieve the dash
            m_Rigidbody2D.velocity = dashDirection * dashDistance / dashDuration;

            Debug.Log($"Dash initiated. Direction: {dashDirection}");

            // Reset dash after a short duration
            StartCoroutine(ResetDashAfterDelay(dashDuration));
        }
    }

    private IEnumerator ResetDashAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetDash();
    }

    private void ResetDash()
    {
        isDashing = false;
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Update()
    {
        if (Input.GetButtonUp("Jump") && m_Rigidbody2D.velocity.y > 0f)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        if (m_Grounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Update dash timer
        if (dashTimer > 0f)
        {
            dashTimer -= Time.deltaTime;
        }
        animator.SetBool("IsJumping", m_Grounded);
    }
}



