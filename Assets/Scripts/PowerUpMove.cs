using UnityEngine;

public class PowerUpMove : MonoBehaviour {

    [SerializeField] private float powerUpSpeed = 5f;
    [SerializeField] private float minX = -13;
    [SerializeField] private float maxX = -1;
    [SerializeField] private float posY = 4;
    [SerializeField] private float posYDeath = -4;

    void Start() {
        transform.position = new Vector2(Random.Range(minX, maxX), posY);
    }

    void Update() {
        transform.Translate(-transform.up * Time.deltaTime * powerUpSpeed);
        if (transform.position.y <= posYDeath) {
            Destroy(gameObject);
        }
    }
}