using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    private int currentHealth;

    private Animator dead;
    public Image healthBar;

    public Image flashImage;

    void Start()
    {
        currentHealth = maxHealth; 
        UpdateHealthBar(); 
        dead=GetComponent<Animator>();
        dead.SetBool("Isdead", false);
    }

 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        Debug.Log("Boss Health: " + currentHealth);

        if (currentHealth < 0)
            currentHealth = 0; 

        UpdateHealthBar(); 

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

   
    void Die()
    {
        Debug.Log("Boss öldü!");
        dead.SetBool("Isdead", true);
        StartCoroutine(FinishGame());
    }
     IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0;
    }
  
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            
            float HealtBarScale = Mathf.Clamp01((float)currentHealth / (float)maxHealth);
            healthBar.transform.localScale = new Vector3(HealtBarScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
        else
        {
            Debug.LogWarning("Health Bar UI öğesi atanmadı!");
        }
    }



}
