using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private float delayPowerUp = default;
    [Space]
    [SerializeField] private string titleWin = default;
    [SerializeField] private string commentWin = default;
    [Space]
    [SerializeField] private string titleLose = default;
    [SerializeField] private string commentLose = default;

    [Header("Assets")]
    [SerializeField] private GameObject prefabPowerUp = default;

    [Header("References")]
    [SerializeField] private Button buttonPlay = default;
    [SerializeField] private Button buttonCredits = default;
    [SerializeField] private Button buttonBack = default;
    [Space]
    [SerializeField] private TextMeshProUGUI textResult = default;
    [SerializeField] private TextMeshProUGUI textComment = default;
    [SerializeField] private Button buttonRestart = default;
    [Space]
    [SerializeField] private Animator animator = default;

    private enum State { MainMenu, Credits, InGame, EndGame }
    private State state = State.MainMenu;

    private float nextPowerUp;

    public Action onStartGame;
    public Action<bool> onGameOver;

    public Action onPowerUp;

    private readonly int CONST_PLAY = Animator.StringToHash("Play");
    private readonly int CONST_CREDITS = Animator.StringToHash("Credits");
    private readonly int CONST_BACK = Animator.StringToHash("Back");
    private readonly int CONST_GAMEOVER = Animator.StringToHash("GameOver");

    public static GameManager instance;

    private void Awake() {
        instance = this;

        buttonPlay.onClick.AddListener(OnClickPlay);
        buttonCredits.onClick.AddListener(OnClickCredits);
        buttonBack.onClick.AddListener(OnClickBack);
        buttonRestart.onClick.AddListener(OnClickRestart);
    }

    private void Update() {
        if (state == State.InGame && Time.time >= nextPowerUp) {
            nextPowerUp = Time.time + delayPowerUp;
            Instantiate(prefabPowerUp);
            onPowerUp?.Invoke();
        }
    }

    private void OnClickPlay() {
        if (state != State.MainMenu) return;
        state = State.InGame;

        onStartGame?.Invoke();
        nextPowerUp = Time.time + delayPowerUp;

        animator.SetTrigger(CONST_PLAY);
    }

    private void OnClickCredits() {
        if (state != State.MainMenu) return;
        state = State.Credits;

        animator.SetTrigger(CONST_CREDITS);
    }

    private void OnClickBack() {
        if (state != State.Credits) return;
        state = State.MainMenu;

        animator.SetTrigger(CONST_BACK);
    }

    private void OnClickRestart() {
        if (state != State.EndGame) return;
        state = State.MainMenu;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver(bool win) {
        textResult.text = win ? titleWin : titleLose;
        textComment.text = win ? commentWin : commentLose;
        state = State.EndGame;
        animator.SetTrigger(CONST_GAMEOVER);
        onGameOver?.Invoke(win);
    }
}