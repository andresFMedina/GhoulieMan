using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float maxSpeed = 6.0f;
    public float moveDirection;
    public float jumpSpeed = 600.0f;
    public bool facingRight = true;
    private Rigidbody rigidbody;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip jumpAudio;
    public AudioClip attackAudio;


    public bool isGrounded;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float knifeSpeed = 600.0f;
    public Transform knifeSpawn;
    public Rigidbody knifePrefab;
    Rigidbody clone;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        groundCheck = GameObject.Find("GroundCheck").transform;
        knifeSpawn = GameObject.Find("KnifeSpawn").transform;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("IsJumping");
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
            audioSource.PlayOneShot(jumpAudio);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }
        else if (moveDirection < 0.0f && facingRight)
        {
            Flip();
        }
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
        knifeSpawn.transform.Rotate(Vector3.up, 180.0f, Space.World);        
        
    }

    void Attack()
    {
        animator.SetTrigger("attacking");
    }

    public void CallFireProjectile()
    {
        clone = Instantiate(knifePrefab, knifeSpawn.position, knifeSpawn.rotation);
        clone.AddForce(knifeSpawn.transform.right * knifeSpeed);
        audioSource.PlayOneShot(attackAudio);
    }
}
