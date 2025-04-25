using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; 
    public float smoothTime = 0.3f; 
    private Vector3 velocity = Vector3.zero;  

    private CinemachineVirtualCamera vCam;

    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    void LateUpdate()
    {
       
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
