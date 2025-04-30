using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeVolumeEffects();
    }

    private void InitializeVolumeEffects()
    {
        volume = FindObjectOfType<Volume>();

        if (volume != null && volume.profile != null)
        {
            volume.profile.TryGet(out chromaticAberration);
            volume.profile.TryGet(out lensDistortion);

            if (chromaticAberration != null)
            {
                chromaticAberration.intensity.Override(0f);
                chromaticAberration.active = true;
            }

            if (lensDistortion != null)
            {
                lensDistortion.intensity.Override(0f);
                lensDistortion.active = true;
            }
        }
        else
        {
            Debug.LogWarning("Volume or Volume Profile not found in scene!");
        }
    }

    public void TriggerGlitch()
    {
        if (isGlitching) return; 
        StartCoroutine(GlitchRoutine());
    }

    private bool isGlitching = false;

    private IEnumerator GlitchRoutine()
    {
        isGlitching = true;

        float glitchDuration = 0.5f;
        float elapsed = 0f;

        while (elapsed < glitchDuration)
        {
            if (chromaticAberration != null)
                chromaticAberration.intensity.Override(Random.Range(0.7f, 1f));

            if (lensDistortion != null)
                lensDistortion.intensity.Override(Random.Range(-0.6f, -0.2f));

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (chromaticAberration != null)
            chromaticAberration.intensity.Override(0f);

        if (lensDistortion != null)
            lensDistortion.intensity.Override(0f);

        isGlitching = false;
    }
}
