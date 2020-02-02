﻿using UnityEngine;

public class SoundManager : MonoBehaviour {
    [Header("Assets")]
    public AudioClip[] clipsBackgroundMusic = default;

    [Header("References")]
    public AudioSource audioSourceBackground = default;
    public AudioSource[] audioSourceFxs = default;

    private int indexClip = 0;
    private int indexFX = 0;

    public static SoundManager instance;

    private void Awake() {
        instance = this;
        if (clipsBackgroundMusic.Length > 0) {
            audioSourceBackground.clip = clipsBackgroundMusic[0];
            audioSourceBackground.Play();
        }
    }

    void Update() {
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic() {
        if (clipsBackgroundMusic.Length == 0) return;

        if (!audioSourceBackground.isPlaying) {
            Debug.Log("Clip Index: " + indexClip);
            if (indexClip + 1 < clipsBackgroundMusic.Length) {
                indexClip++;
            } else {
                indexClip = 1;
            }

            audioSourceBackground.clip = clipsBackgroundMusic[indexClip];
            audioSourceBackground.Play();
        }
    }

    public void PlayOneShot(AudioClip clip) {

        if (indexFX + 1 < audioSourceFxs.Length) {
            indexFX++;
        } else {
            indexFX = 0;
        }

        if (IsPlayingSound(audioSourceFxs[indexFX])) {
            StopSound(audioSourceFxs[indexFX]);
        }
        audioSourceFxs[indexFX].PlayOneShot(clip);
    }


    public void StopSound(AudioSource audioSrc) {
        if (audioSrc.isPlaying == true)
            audioSrc.Stop();
    }

    public void OnGameOver() {

    }

    public bool IsPlayingSound(AudioSource audioSrc) {
        return (audioSrc.isPlaying == true);
    }
}
