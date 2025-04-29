using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minivirus : MonoBehaviour
{
    [Header("Hýz Aralýðý")]
    public float minSpeed = 2f;
    public float maxSpeed = 6f;

    private float speed;
    private Vector2 moveDirection = Vector2.left; 
  


   
    public int damage = 10; 

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    
    public void ReverseDirection()
    {
        moveDirection *= -1f; 
        
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
          
            BossHealth bossHealth = other.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
                Debug.Log("Boss'a hasar verildi!");
            }
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("EnemyDead"))
        {
            Destroy(this.gameObject);
        }
    }


}
