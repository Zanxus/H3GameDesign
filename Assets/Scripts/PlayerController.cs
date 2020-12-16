using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float JumpForce;
    private float MoveInput;

    private Rigidbody2D rb;
    private Collider2D collider;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int maxExtraJumps; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        extraJumps = maxExtraJumps;
        PlatformLogic.OnScoreEvent += Platform_OnScoreEvent;
    }

    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius,whatIsGround);


        MoveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);

        if (facingRight == false && MoveInput > 0)
        {
            Flip();
        } else if(facingRight == true && MoveInput < 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * JumpForce;
            extraJumps--;
        }else if(Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * JumpForce;
        }


        transform.rotation = new Quaternion();
    }

    private void Platform_OnScoreEvent(float score)
    {
        extraJumps = maxExtraJumps;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}
