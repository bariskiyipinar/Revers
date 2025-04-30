using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Character : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator CharacterAnim;
    [SerializeField] private Animator Entry1,Entry2;
    [SerializeField] private GameObject HearthPrefab;
    [SerializeField] private GameObject BrokenObstacles;
    [SerializeField] private AudioSource DamageSound;
    private bool facingRight = true;
    private bool IsGrounded = false;
    [SerializeField] private List<RectTransform> heartPoints;
    private int heartCount = 0; 

    private void Start()
    {
        CharacterAnim.SetBool("IsIdle", true);
        CharacterAnim.SetBool("IsWalking", false);
        CharacterAnim.SetBool("IsFalling", false);
        Entry1.enabled = false;
        Entry2.enabled = false;
       
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
        HandleFallingAnimation();
    }

    void HandleFallingAnimation()
    {
    
        if (!IsGrounded && Mathf.Abs(rb.velocity.y) > 0.01f)
        {
            CharacterAnim.SetBool("IsFalling", true);
            CharacterAnim.SetBool("IsIdle", false);
            CharacterAnim.SetBool("IsWalking", false);
        }
        else
        {
            CharacterAnim.SetBool("IsFalling", false);
        }
    }
    void Reverse()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            FindObjectOfType<GravityGlitchEffect>()?.TriggerGlitch();
            rb.gravityScale *= -1;
            Vector3 scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;

            CharacterAnim.SetBool("IsFalling", true);
            CharacterAnim.SetBool("IsIdle", false);
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

        if (collision.gameObject.CompareTag("Transporter"))
        {
            float pushSpeed = 1000f;

           
            Vector2 pushDirection = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0 ? Vector2.left : Vector2.right;

 
            rb.velocity = new Vector2(pushDirection.x * pushSpeed, rb.velocity.y);
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
           
            IsGrounded = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Card1"))
        {
            Entry1.enabled = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Card2"))
        {
            Entry2.enabled = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy1"))
        {
            AddHeart();
            DamageSound.Play();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Happy"))
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(TimeLevel1(3));

        }
        if (collision.gameObject.CompareTag("Happy2"))
        {
            rb.velocity=Vector2.zero;
            StartCoroutine(TimeLevel2(3));
        }

        if (collision.gameObject.CompareTag("Broken"))
        {
            Destroy(collision.gameObject);
            BrokenObstacles.SetActive(true);
        }
        if (collision.gameObject.CompareTag("MiniEnemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    void AddHeart()
    {
        if (heartCount < heartPoints.Count)
        {
            GameObject heart = Instantiate(HearthPrefab, heartPoints[heartCount].position, Quaternion.identity);
            heart.transform.SetParent(heartPoints[heartCount].transform, false);

            heartCount++;

            if (heartCount >= 3) 
            {
                RestartGame();
            }
        }
    }

    IEnumerator TimeLevel1(int time)
    {
        CharacterAnim.SetTrigger("Happy");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Level2");
    }
    IEnumerator TimeLevel2(int time)
    {
        CharacterAnim.SetTrigger("Happy");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Boss");
    }
    void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
