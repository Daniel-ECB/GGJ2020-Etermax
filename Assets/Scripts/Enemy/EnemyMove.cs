using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    [Header("Settings")]
    public int bulletsPerSecond = default;
    public float speed = 100f;
    public bool moveUp = true;
    public bool moveDown = true;

    [Header("References")]
    public Transform spawnBullet = default;

    [Header("Assets")]
    public Bullet prefabBullet = default;

    private float delay;
    private float startTime;
    private float nextShoot;
    public GameObject[] shootPoints;
    public static Pooling<Bullet> pooling;
    private IEnumerator coroutine;

    void Start() {
        pooling = new Pooling<Bullet>(20, prefabBullet, new GameObject("PoolObjects Bullet").transform, false);
        delay = 1 / bulletsPerSecond;
        startTime=Time.time;
        
    }

    void Update() {
        //MoveUpDown();
        float timeElapse=3f;


        if(Time.time>startTime+timeElapse)
        {
             MoveRandom();
        
        startTime=Time.time;
        }
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


            private void MoveRandom()
            {
                   int pointGame=Random.Range(0,shootPoints.Length);

            
                            
                              
                        
                                                 
                            transform.position=Vector2.MoveTowards(transform.position,shootPoints[pointGame].transform.position,Time.deltaTime * speed);
                            
                    
                    
                    //transform.Translate(new Vector2(0,shootPoints[pointGame].transform.position.y)* Time.deltaTime *speed, Space.Self);

            }

           






}
