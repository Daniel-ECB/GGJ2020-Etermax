using UnityEngine;

public class ShieldUnit : MonoBehaviour, IAttackable {
    #region Fields

    [SerializeField] private GameObject superShieldObject = default;
    [SerializeField] private GameObject myCanvas = default;
    private bool hasSuperShield = false;

    [Header("Health Support")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth = 1000;

    private bool isDeath = false;
    private Collider2D shieldCollider;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the current health of the shield
    /// </summary>
    public int ShieldHealth { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    /// Awake is called when the script is first loaded.
    /// </summary>
    private void Awake() {
        ShieldHealth = maxHealth;
        healthBar.maxHits = maxHealth;

        shieldCollider = GetComponent<Collider2D>();
    }

    private void Start() {
        GameManager.instance.onStartGame += OnStartGame;
    }

    private void OnStartGame() => myCanvas.SetActive(true);

    /// <summary>
    /// Kills the Shield.
    /// </summary>
    public void Kill() {
        // Prevents the shield from being destroyed
        if (hasSuperShield)
            return;

        // Instantly killing the shield
        if (ShieldHealth > 0) {
            ShieldHealth = 0;
            healthBar.SetHealthBarContent(0);
        }

        isDeath = true;
        Debug.Log("The shield " + name + " was destroyed");
        ShieldManager.Instance.RemoveShield(this);
    }

    /// <summary>
    /// Makes the shield take damage.
    /// </summary>
    /// <param name="damage">Damage dealt to the shield.</param>
    public void TakeDamage(int damage) {
        // There's no point in damaging a death shield or a protected one
        if (isDeath || hasSuperShield)
            return;

        ShieldHealth = Mathf.Max(0, ShieldHealth - damage);
        healthBar.ReduceBar(damage);

        if (ShieldHealth <= 0)
            Kill();
    }

    private void OnMouseDown() {
        //Debug.Log("The player has clicked on " + name);
        if (isDeath)
            return;

        if (ShieldManager.Instance.canHeal) {
            RecoverHealth(ShieldManager.Instance.healAmount);
        } else if (ShieldManager.Instance.activateSuperShield) {
            ActivateSuperShield();
        }
    }

    /// <summary>
    /// Recovers the health of the ShieldUnit by a certain amount.
    /// </summary>
    /// <param name="_amount">Amount to heal.</param>
    public void RecoverHealth(int _amount) {
        ShieldHealth = Mathf.Min(ShieldHealth + ShieldManager.Instance.healAmount, maxHealth);
        healthBar.SetHealthBarContent(ShieldHealth);
    }

    /// <summary>
    /// Creates a SuperShield on the normal shield.
    /// </summary>
    private void ActivateSuperShield() {
        // Only one Super Shield at once
        ShieldManager.Instance.canHeal = true;
        ShieldManager.Instance.activateSuperShield = false;

        // Creating and destroying super shield
        superShieldObject.SetActive(true);
        Invoke("DeactivateSuperShield", ShieldManager.Instance.superShieldLifeTime);
        hasSuperShield = true;
    }

    /// <summary>
    /// Deactivates the Super Shield
    /// </summary>
    private void DeactivateSuperShield() {
        superShieldObject.SetActive(false);
        hasSuperShield = false;
    }

    #endregion
}