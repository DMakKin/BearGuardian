using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // Скорость движения
    [SerializeField] private float jumpForce; // Сила прыжка
    [SerializeField] private LayerMask groundLayer; // Слой земли
    [SerializeField] private Transform groundCheck; // Точка проверки земли
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float speedOfRotation;

    private float moveInputX;
    private float moveInputZ;
    private Rigidbody rb;
    //private Animator animator;
    private bool isGrounded;
    private Vector3 movement;
    private Vector3 newVelocity;
    

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
        if (isGrounded)
        {
            moveInputX = Input.GetAxis("Horizontal");
            moveInputZ = Input.GetAxis("Vertical");
            movement = new Vector3(moveInputX, 0f, moveInputZ) * moveSpeed;
            newVelocity = new(movement.x, rb.velocity.y, movement.z);
            rb.velocity = newVelocity;
        }


        if (moveInputX != 0 || moveInputZ != 0)
        {
            //transform.forward = new Vector3(moveInputX, 0, moveInputZ); // Поворачиваем персонажа в сторону движения

            Vector3 targetDirection = new Vector3(moveInputX, 0, moveInputZ);
                        
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
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
    //    animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x)); // Скорость для анимации бега
    //    animator.SetBool("IsGrounded", isGrounded); // Условие для анимации прыжка
    //}
}
