using UnityEngine;

public class SoundManager : MonoBehaviour {
    [Header("Assets")]
    public AudioClip[] clipsBackgroundMusic = default;

    [Header("References")]
    public AudioSource audioSourceBackground = default;
    public AudioSource[] audioSourceFxs = default;

    private int indexClip = 0;
    private int indexFX = 0;
    private bool isGameOver;

    public static SoundManager instance;

    private void Awake() {
        instance = this;
        if (clipsBackgroundMusic.Length > 0) {
            audioSourceBackground.clip = clipsBackgroundMusic[0];
            audioSourceBackground.Play();
        }
    }

    private void Start() {
        GameManager.instance.onGameOver += OnGameOver;
    }

    void Update() {
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic() {
        if (clipsBackgroundMusic.Length == 0 || isGameOver) return;

        if (!audioSourceBackground.isPlaying) {
            Debug.Log("Clip Index: " + indexClip);
            if (indexClip + 1 < clipsBackgroundMusic.Length -2) {
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

    public void OnGameOver(bool win) {
        isGameOver = true;
        indexClip = 0;
        if (win) {
            audioSourceBackground.clip = clipsBackgroundMusic[3];
        } else {
            audioSourceBackground.clip = clipsBackgroundMusic[2];
        }
        audioSourceBackground.loop = false;
        audioSourceBackground.Play();
    }

    public bool IsPlayingSound(AudioSource audioSrc) {
        return audioSrc.isPlaying;
    }
}
