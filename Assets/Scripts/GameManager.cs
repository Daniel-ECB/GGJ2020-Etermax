using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private string titleWin = default;
    [SerializeField] private string commentWin = default;
    [Space]
    [SerializeField] private string titleLose = default;
    [SerializeField] private string commentLose = default;

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

    public Action onStartGame;
    public Action onGameOver;

    private readonly int CONST_PLAY = Animator.StringToHash("Play");
    private readonly int CONST_CREDITS = Animator.StringToHash("Credits");
    private readonly int CONST_BACK = Animator.StringToHash("Back");

    public static GameManager instance;

    private void Awake() {
        instance = this;

        buttonPlay.onClick.AddListener(OnClickPlay);
        buttonCredits.onClick.AddListener(OnClickCredits);
        buttonBack.onClick.AddListener(OnClickBack);
        buttonRestart.onClick.AddListener(OnClickRestart);
    }

    private void OnClickPlay() {
        if (state != State.MainMenu) return;
        state = State.InGame;

        onStartGame?.Invoke();
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
        if (state != State.MainMenu) return;
        state = State.MainMenu;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver(bool win) {
        textResult.text = win ? titleWin : titleLose;
        textComment.text = win ? commentWin : commentLose;
    }
}