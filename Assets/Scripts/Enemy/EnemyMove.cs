using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour, IAttackable {

    [Header("Settings")]
    public int lives = 3;
    public float minDelay;
    public float maxDelay;
    public int bulletsPerSecond = default;
    public float speed = 100f;
    public bool moveUp = true;
    public bool moveDown = true;

    [Header("Assets")]
    public Bullet prefabBullet = default;
    public AudioClip clipShoot = default;

    [Header("References")]
    public Transform spawnBullet = default;

    private float delay;
    private float nextShoot;
    public static Pooling<Bullet> pooling;

    private enum State { Idle, Moving, Shooting };
    private State state = State.Idle;
    private float startPosY;
    private float t;
    private int indexTarget;
    private bool active;

    void Start() {
        pooling = new Pooling<Bullet>(20, prefabBullet, new GameObject("PoolObjects Bullet").transform, false);
        delay = 1.0f / bulletsPerSecond;
        GameManager.instance.onStartGame += OnStartGame;
        GameManager.instance.onGameOver += OnGameOver;
    }

    void Update() {
        if (!active) return;

        switch (state) {
            case State.Idle: {
                SetTarget();
            }
            break;
            case State.Moving: {
                t += Time.deltaTime * speed;
                if (t > 1) {
                    state = State.Shooting;
                    StartCoroutine(SetTargetC());
                    t = 1;
                    transform.position = new Vector2(transform.position.x, ShieldManager.Instance.shields[indexTarget].transform.position.y);
                } else {
                    transform.position = new Vector2(transform.position.x, Mathf.Lerp(startPosY, ShieldManager.Instance.shields[indexTarget].transform.position.y, t));
                }
            }
            break;
            case State.Shooting: {
                ShootBullet();
            }
            break;
            default: {
                Debug.LogError("Estado de la nave no reconocido!");
            }
            break;
        }
    }

    private void OnStartGame() {
        active = true;
    }

    private void OnGameOver() {
        active = false;
    }

    private void ShootBullet() {
        if (Time.time >= nextShoot) {
            nextShoot = Time.time + delay;
            Bullet bullet = pooling.Get();
            bullet.transform.position = spawnBullet.position;
            bullet.transform.rotation = spawnBullet.rotation;
            SoundManager.instance.PlayOneShot(clipShoot);
        }
    }

    private IEnumerator SetTargetC() {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        SetTarget();
    }

    private void SetTarget() {
        if (ShieldManager.Instance.shields.Count == 0) active = false;

        int newIndex = Random.Range(0, ShieldManager.Instance.shields.Count);
        if (newIndex == indexTarget) {
            newIndex++;
            if (newIndex >= ShieldManager.Instance.shields.Count) {
                newIndex = 0;
            }
        }
        startPosY = transform.position.y;
        indexTarget = newIndex;
        t = 0;
        state = State.Moving;
    }

    public void TakeDamage(int damage) {
        lives -= 1;

        if (lives <= 0) {
            Kill();
            GameManager.instance.GameOver(true);
        }
    }

    public void Kill() => gameObject.SetActive(false);
}