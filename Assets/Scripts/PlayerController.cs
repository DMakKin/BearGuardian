using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce; 
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float speedOfRotation;

    private float moveInputX;
    private float moveInputZ;
    private Rigidbody rb;
    //private Animator animator;
    private bool isGrounded;
    private Vector3 movement;
    private Vector3 newVelocity;
    private Quaternion targetRotation;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        //Animate();
    }

    void Move()
    {
        moveInputX = Input.GetAxis("Horizontal");
        moveInputZ = Input.GetAxis("Vertical");
        movement = new Vector3(moveInputX, 0f, moveInputZ) * moveSpeed;
        newVelocity = new(movement.x, rb.velocity.y, movement.z);
        rb.velocity = newVelocity;


        if (moveInputX != 0 || moveInputZ != 0)
        {
            //transform.forward = new Vector3(moveInputX, 0, moveInputZ);

            Vector3 targetDirection = new Vector3(moveInputX, 0, moveInputZ);
                        
            targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speedOfRotation);
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    //void Animate()
    //{
    //    animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    //    animator.SetBool("IsGrounded", isGrounded);
    //}
}
