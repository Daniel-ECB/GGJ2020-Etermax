using UnityEngine;

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
        if (clip == null) return;

        if (indexFX + 1 < audioSourceFxs.Length) {
            indexFX++;
        } else {
            indexFX = 0;
        }

        AudioSource audioSource = audioSourceFxs[indexFX];

        if (audioSource.isPlaying) {
            audioSource.Stop();
        }

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnGameOver() {
        indexFX = 0;
        audioSourceBackground.clip = clipsBackgroundMusic[2];
        audioSourceBackground.loop = false;
        audioSourceBackground.Play();
    }

    public void OnWin() {
        indexFX = 0;
        audioSourceBackground.clip = clipsBackgroundMusic[3];
        audioSourceBackground.loop = false;
        audioSourceBackground.Play();
    }

    public bool IsPlayingSound(AudioSource audioSrc) {
        return audioSrc.isPlaying;
    }
}
