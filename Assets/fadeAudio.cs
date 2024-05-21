using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeAudio : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component
    public float delayBeforeFade = 1f;  // Delay before starting to fade
    public float fadeDuration = 2f;  // Duration of the fade to zero

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to fade the audio
        StartCoroutine(FadeOutAudio());
    }

    // Coroutine to fade out the audio
    IEnumerator FadeOutAudio()
    {
        // Wait for the specified delay before starting the fade
        yield return new WaitForSeconds(delayBeforeFade);

        // Get the initial volume of the audio source
        float startVolume = audioSource.volume;

        // Fade the volume to zero over the fade duration
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        // Ensure the volume is set to zero at the end
        audioSource.volume = 0;
    }
}