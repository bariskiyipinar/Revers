using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
     private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Transporter"))
        {
            rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            Debug.Log("Temas edildi");
        }
    }

}
