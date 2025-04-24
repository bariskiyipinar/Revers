using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator CharacterAnim;

    private bool facingRight = true;
    private bool IsGrounded = false;
  

    private void Start()
    {
        CharacterAnim.SetBool("IsIdle", true);
        CharacterAnim.SetBool("IsWalking", false);
        CharacterAnim.SetBool("IsFalling", false);
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        Reverse();
    }

    void MoveCharacter()
    {
      

        float inputX = Input.GetAxis("Horizontal");

       
        if (rb.gravityScale < 0)
            inputX *= -1;
        

        float moveX = inputX * speed;
        rb.velocity = new Vector2(moveX, rb.velocity.y);

        if (Mathf.Abs(moveX) > 0.01f)
        {
            CharacterAnim.SetBool("IsIdle", false);
            CharacterAnim.SetBool("IsWalking", true);

            if (moveX > 0 && !facingRight)
                Flip();
            
            else if (moveX < 0 && facingRight)
                Flip();
        }
        else
        {
            CharacterAnim.SetBool("IsIdle", true);
            CharacterAnim.SetBool("IsWalking", false);
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; 
        transform.localScale = localScale;
      
        
    }


    private void Update()
    {
        Reverse();
       
    }

    void Reverse()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
      
            rb.gravityScale *= -1;
            Vector3 scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;

            CharacterAnim.SetBool("IsFalling", true);
            IsGrounded = false;

           
            StartCoroutine(EnableMovementAfterDelay(0.3f)); 
        }
    }
    private IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
      
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
         
            IsGrounded = true;
            CharacterAnim.SetBool("IsFalling", false); 
        }

        
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
           
            IsGrounded = false;
        }
    }

}
