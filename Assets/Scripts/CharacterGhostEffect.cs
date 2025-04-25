using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGhostEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isGhost = false;

    public float ghostEffectDuration = 1f;  
    public float fadeSpeed = 0.5f;  

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;  
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("GhostStart") && !isGhost)
        {
           
            isGhost = true;
            StartCoroutine(GhostEffect());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("GhostStart") && isGhost)
        {
            isGhost = false;
            StartCoroutine(RestoreOriginalAppearance());
        }
    }

    private IEnumerator GhostEffect()
    {
        float timeElapsed = 0f;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.2f);  

        
        while (timeElapsed < ghostEffectDuration)
        {
            spriteRenderer.color = Color.Lerp(originalColor, targetColor, timeElapsed / ghostEffectDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator RestoreOriginalAppearance()
    {
        float timeElapsed = 0f;
        while (timeElapsed < ghostEffectDuration)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, timeElapsed / ghostEffectDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
