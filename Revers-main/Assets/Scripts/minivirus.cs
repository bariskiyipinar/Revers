using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minivirus : MonoBehaviour
{
    [Header("Hýz Aralýðý")]
    public float minSpeed = 2f;
    public float maxSpeed = 6f;

    private float speed;          // Gerçek hýz burada tutulacak
    public float lifeTime = 10f;

    void Start()
    {
        // Rastgele hýzý ata (minSpeed..maxSpeed arasý, dahilî) :contentReference[oaicite:1]{index=1}
        speed = Random.Range(minSpeed, maxSpeed);
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
