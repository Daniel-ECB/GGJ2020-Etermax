using UnityEngine;

public class SuperBullet : MonoBehaviour {

    [Header("Settings")]
    public float bulletSpeed = 3f;
    public float rateBullet = 5f;
    public int damage = 1;

    private bool wasShieldHit;

    private void Update() {
        transform.Translate(transform.right * bulletSpeed * Time.deltaTime * rateBullet);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other) {
            switch (other.tag) {
                case "SuperShield": {
                    if (!wasShieldHit) {
                        other.GetComponentInParent<ShieldUnit>().DeactivateSuperShield();
                        bulletSpeed = -bulletSpeed;
                        wasShieldHit = true;
                    }
                }
                break;
                case "ShieldUnit": {
                    other.GetComponent<IAttackable>().Kill();
                    Destroy(gameObject);
                }
                break;
                case "Enemy": {
                    if (wasShieldHit) {
                        other.GetComponent<IAttackable>().TakeDamage(damage);
                        Destroy(gameObject);
                    }
                }
                break;
                default: {
                    Destroy(gameObject);
                }
                break;
            }
        }
    }
}