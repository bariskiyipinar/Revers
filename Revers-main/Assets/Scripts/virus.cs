using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virus : MonoBehaviour
{
    [Header("Fýrlatma Ayarlarý")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float minFireInterval = 1f;
    public float maxFireInterval = 3f;

    [Header("Salýným Ayarlarý")]
    public float bobAmplitude = 0.5f;   // Yukarý-aþaðý hareket mesafesi
    public float bobFrequency = 1f;     // Hýz çarpaný

    private Vector3 startPosition;      // Baþlangýç pozisyonunu saklayacaðýz

    void Start()
    {
        // Baþlangýç pozisyonunu kaydet
        startPosition = transform.position;

        // Atýþ döngüsünü baþlat
        StartCoroutine(FireRoutine());
    }

    void Update()
    {
        // Zamaný bobFrequency ile çarparak sinüs dalgasý üret
        float offsetY = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;  // :contentReference[oaicite:0]{index=0}

        // Orijinal X,Z koruyarak Y’yi güncelle
        transform.position = new Vector3(
            startPosition.x,
            startPosition.y + offsetY,
            startPosition.z
        );
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            float interval = Random.Range(minFireInterval, maxFireInterval);
            yield return new WaitForSeconds(interval);
            if (projectilePrefab != null && firePoint != null)
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}
