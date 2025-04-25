using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GravityGlitchEffect : MonoBehaviour
{
    private Volume volume;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;

    private static GravityGlitchEffect instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        volume = FindObjectOfType<Volume>();

        if (volume != null)
        {
            volume.profile.TryGet(out chromaticAberration);
            volume.profile.TryGet(out lensDistortion);

            if (chromaticAberration != null)
            {
                chromaticAberration.active = true;
                chromaticAberration.intensity.value = 0f;
            }

            if (lensDistortion != null)
            {
                lensDistortion.active = true;
                lensDistortion.intensity.value = 0f;
            }
        }
      
    }

    public void TriggerGlitch()
    {
      
        StartCoroutine(GlitchRoutine());
    }

    private IEnumerator GlitchRoutine()
    {
        float glitchDuration = 0.5f;
        float elapsed = 0f;

        while (elapsed < glitchDuration)
        {
            if (chromaticAberration != null)
                chromaticAberration.intensity.value = Random.Range(0.7f, 1f);

            if (lensDistortion != null)
                lensDistortion.intensity.value = Random.Range(-0.6f, -0.2f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (chromaticAberration != null)
            chromaticAberration.intensity.value = 0f;

        if (lensDistortion != null)
            lensDistortion.intensity.value = 0f;
    }


}
