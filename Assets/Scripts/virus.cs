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
    public float bobAmplitude = 0.5f;   
    public float bobFrequency = 1f;     

    private Vector3 startPosition;     

    void Start()
    {
     
        startPosition = transform.position;

      
        StartCoroutine(FireRoutine());
    }

    void Update()
    {
        
        float offsetY = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;  

     
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
            {
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                Debug.Log("Mermi fýrlatýldý!"); 
            }
        }
    }

}
