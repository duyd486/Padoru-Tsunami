using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioClipRefsSO audioClipRefsSOl;

    private AudioSource audioSource;




    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ZumbaiManager.Instance.OnZumbaiExplosed += ZumbaiManager_OnZumbaiExplosed;
        ScoreManager.Instance.OnCandyChanged += ScoreManager_OnCandyChanged;
    }

    private void ScoreManager_OnCandyChanged(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSOl.candy, 0.1f);
    }

    private void ZumbaiManager_OnZumbaiExplosed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSOl.bomb, 0.3f);
    }

    private void PlaySound(AudioClip[] audioClipArr, float volume = 1f)
    {
        PlaySound(audioClipArr[Random.Range(0, audioClipArr.Length)], volume);
    }

    private void PlaySound(AudioClip audioClip, float volumeMultiply = 1f)
    {
        audioSource.PlayOneShot(audioClip, volumeMultiply);
    }
}
