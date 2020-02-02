using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{

    [Header("Settings")]
    public float minDelay, maxDelay;
    public int bulletsPerSecond = default;
    public float speed = 100f;
    public bool moveUp = true;
    public bool moveDown = true;

    [Header("References")]
    public Transform spawnBullet = default;

    [Header("Assets")]
    public Bullet prefabBullet = default;

    private float delay;
    private float nextShoot;
    public static Pooling<Bullet> pooling;

    private enum State { Idle, Moving, Shooting };
    private State state = State.Idle;
    private float startPosY;
    private float t;
    private int indexTarget;

    void Start()
    {
        pooling = new Pooling<Bullet>(20, prefabBullet, new GameObject("PoolObjects Bullet").transform, false);
        delay = 1 / bulletsPerSecond;

    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                {
                    SetTarget();
                }
                break;
            case State.Moving:
                {
                    t += Time.deltaTime * speed;
                    if (t > 1)
                    {
                        state = State.Shooting;
                        StartCoroutine(SetTargetC());
                        t = 1;
                        transform.position = new Vector2(transform.position.x, ShieldManager.Instance.shields[indexTarget].transform.position.y);
                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x, Mathf.Lerp(startPosY, ShieldManager.Instance.shields[indexTarget].transform.position.y, t));
                    }
                }
                break;
            case State.Shooting:
                {
                    ShootBullet();
                }
                break;
            default:
                {
                    Debug.LogError("Estado de la nave no reconocido!");
                }
                break;
        }
    }

    private void ShootBullet()
    {
        if (Time.time >= nextShoot)
        {
            nextShoot = Time.time + delay;
            Bullet bullet = pooling.Get();
            bullet.transform.position = spawnBullet.position;
            bullet.transform.rotation = spawnBullet.rotation;
        }
    }

    private IEnumerator SetTargetC()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        SetTarget();
    }

    private void SetTarget()
    {
        int newIndex = Random.Range(0, ShieldManager.Instance.shields.Count);
        if (newIndex == indexTarget)
        {
            newIndex++;
            if (newIndex >= ShieldManager.Instance.shields.Count)
            {
                newIndex = 0;
            }
        }
        startPosY = transform.position.y;
        indexTarget = newIndex;
        t = 0;
        state = State.Moving;
    }
}
