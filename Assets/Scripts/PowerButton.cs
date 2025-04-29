using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerButton : MonoBehaviour
{
    public Button powerButton; 
    public minivirus virusPrefab; 
    private bool isInRange = false; 
    
    void Start()
    {
        if (powerButton != null)
        {
            powerButton.gameObject.SetActive(false);
        }
    }

    
    public void OnButtonPressed()
    {
        if (isInRange)
        {
            
            minivirus[] allMiniviruses = FindObjectsOfType<minivirus>(); 
            foreach (var virus in allMiniviruses)
            {
                virus.ReverseDirection(); 
            }
        }
    }

  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MiniEnemy"))
        {
            isInRange = true;
            if (powerButton != null)
            {
                powerButton.gameObject.SetActive(true); 
            }
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MiniEnemy"))
        {
            isInRange = false;
            if (powerButton != null)
            {
                powerButton.gameObject.SetActive(false); 
            }
        }
    }
}
