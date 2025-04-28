using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator CharacterAnim;

    private bool facingRight = true;

    private void Start()
    {
        CharacterAnim.SetBool("IsIdle", true);
        CharacterAnim.SetBool("IsWalking", false);
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        float moveX = Input.GetAxis("Horizontal") * speed;
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
}
