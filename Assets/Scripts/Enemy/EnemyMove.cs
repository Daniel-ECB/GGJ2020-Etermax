using UnityEngine;

public class EnemyMove : MonoBehaviour {

    [Header("Settings")]
    public int bulletsPerSecond = default;
    public float speed = 3f;
    public bool moveUp = true;
    public bool moveDown = true;

    [Header("References")]
    public Transform spawnBullet = default;

    [Header("Assets")]
    public Bullet prefabBullet = default;

    private float delay;
    private float nextShoot;

    public static Pooling<Bullet> pooling;

    void Start() {
        pooling = new Pooling<Bullet>(20, prefabBullet, new GameObject("PoolObjects Bullet").transform, false);
        delay = 1 / bulletsPerSecond;
    }

    void Update() {
        MoveUpDown();
        ShootBullet();
    }

    private void ShootBullet() {
        if (nextShoot <= Time.time) {
            nextShoot = Time.time + delay;
            Bullet bullet = pooling.Get();
            bullet.transform.position = spawnBullet.position;
            bullet.transform.rotation = spawnBullet.rotation;
        }
    }

    private void MoveUpDown() {
        if (moveUp) {
            transform.Translate(transform.up * Time.deltaTime * speed);
            transform.position = new Vector2(transform.position.x, transform.position.y);
        };
        if (transform.position.y >= 4) {
            transform.position = new Vector2(transform.position.x, transform.position.y);
            moveUp = false;
            moveDown = true;
        }
        if (!moveUp) {
            transform.Translate(-transform.up * Time.deltaTime * speed);
        }
        if (transform.position.y <= -4) {
            transform.position = new Vector2(transform.position.x, transform.position.y);
            moveDown = false;
            moveUp = true;
        }
        if (!moveDown) {
            transform.Translate(transform.up * Time.deltaTime * speed);
        }
    }
}
