using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement6 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 7f;

    [Tooltip("Точка під ногами для перевірки землі")]
    public Transform groundCheck;

    [Tooltip("Радіус сфери перевірки землі")]
    public float groundCheckRadius = 0.2f;

    [Tooltip("Які шари вважаються землею")]
    public LayerMask groundLayers;

    [Tooltip("Мінімальний час між стрибками (секунди)")]
    public float jumpCooldown = 0.1f;

    private Rigidbody _rb;
    private bool _isGrounded;
    private float _lastJumpTime = -999f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateGrounded();
        HandleMovement();
        HandleJump();
    }

    void UpdateGrounded()
    {
        Vector3 checkPos = groundCheck == null
            ? transform.position + Vector3.down * 0.9f
            : groundCheck.position;

        // проста сфера під ногами
        _isGrounded = Physics.CheckSphere(
            checkPos,
            groundCheckRadius,
            groundLayers,
            QueryTriggerInteraction.Ignore
        );
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * h + transform.forward * v;
        moveDir.y = 0f;

        Vector3 currentVel = _rb.linearVelocity;
        Vector3 desiredHor = moveDir.normalized * moveSpeed;

        _rb.linearVelocity = new Vector3(desiredHor.x, currentVel.y, desiredHor.z);
    }

    void HandleJump()
    {
        bool cooldownReady = Time.time >= _lastJumpTime + jumpCooldown;
        bool fallingOrStill = _rb.linearVelocity.y <= 0.05f;
        bool canJumpNow = _isGrounded && cooldownReady && fallingOrStill;

        // DEBUG – подивитись, чому не стрибає
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Space pressed. grounded={_isGrounded}, cooldownReady={cooldownReady}, vy={_rb.linearVelocity.y}");
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJumpNow)
        {
            _lastJumpTime = Time.time;

            Vector3 v = _rb.linearVelocity;
            v.y = 0f;
            _rb.linearVelocity = v;

            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 checkPos = groundCheck == null
            ? transform.position + Vector3.down * 0.9f
            : groundCheck.position;

        Gizmos.DrawWireSphere(checkPos, groundCheckRadius);
    }
}