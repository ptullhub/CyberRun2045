using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] musicTracks;

    private void Start()
    {
        GameManager.GameInstance.onGameOver.AddListener(StopMusic);
        StartCoroutine(PlayMusicLoop());
    }

    private IEnumerator PlayMusicLoop()
    {
        while (true)
        {
            // Pick a random track
            AudioClip clip = musicTracks[Random.Range(0, musicTracks.Length)];
            audioSource.clip = clip;
            audioSource.Play();

            // Wait until the track finishes
            yield return new WaitForSeconds(clip.length);
        }
    }

    private void StopMusic()
    {
        StopCoroutine(PlayMusicLoop());
        audioSource.Stop();
    }
}
