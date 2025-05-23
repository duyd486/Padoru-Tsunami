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

    }

    private void ZumbaiManager_OnZumbaiExplosed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSOl.bomb, 0.2f);
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
