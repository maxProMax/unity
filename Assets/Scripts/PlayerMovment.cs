using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    private SpriteRenderer sprite;
    private float dirX = 0f;

    [SerializeField] private float speed = 7f;
    [SerializeField] private float power = 5f;
    [SerializeField] private LayerMask jumpbleGround;

    private enum MoveState { idle, running, falling, jumping }
  
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    
    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        playerRB.velocity = new Vector2(dirX * speed, playerRB.velocity.y);
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, power);
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        MoveState state;

        

        if (dirX > 0f)
        {
            state = MoveState.running;
            sprite.flipX = false;
           
        }
        else if (dirX < 0f)
        {
            state = MoveState.running;
            sprite.flipX = true;
            
        }
        else
        {
            state = MoveState.idle;
        }

        if (playerRB.velocity.y > 0.1f)
        {
            state = MoveState.jumping;
        } else if (playerRB.velocity.y < -0.1f)
        {
            state = MoveState.falling;
        }

        animator.SetInteger("moveState", ((int)state));
    }


    private bool IsGrounded ()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down.normalized, 0.1f, jumpbleGround);
    }
}
