using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUnit : MonoBehaviour, IAttackable
{
    #region Fields

    [SerializeField] private GameObject superShieldObject;
    private bool hasSuperShield = false;

    [Header("Health Support")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth = 1000;
    private int currentHealth;

    private bool isDeath = false;
    private Collider2D shieldCollider;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the current health of the shield
    /// </summary>
    public int ShieldHealth { get { return currentHealth; } }

    #endregion

    #region Methods

    /// <summary>
    /// Awake is called when the script is first loaded.
    /// </summary>
    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.maxHits = maxHealth;

        shieldCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Kills the Shield.
    /// </summary>
    public void Kill()
    {
        // Prevents the shield from being destroyed
        if (hasSuperShield)
            return;

        // Instantly killing the shield
        if (currentHealth > 0)
        {
            currentHealth = 0;
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
    public void TakeDamage(int damage)
    {
        // There's no point in damaging a death shield or a protected one
        if (isDeath || hasSuperShield)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        healthBar.ReduceBar(damage);

        if (currentHealth <= 0)
            Kill();
    }

    /// <summary>
    /// Recovers the health of the ShieldUnit by a certain amount.
    /// </summary>
    /// <param name="_amount">Amount to heal.</param>
    public void RecoverHealth(int _amount)
    {
        currentHealth = Mathf.Min(currentHealth + ShieldManager.Instance.healAmount, maxHealth);
        healthBar.SetHealthBarContent(currentHealth);
    }

    /// <summary>
    /// Creates a SuperShield on the normal shield.
    /// </summary>
    private void ActivateSuperShield()
    {
        // Only one Super Shield at once
        ShieldManager.Instance.canHeal = true;
        ShieldManager.Instance.activateSuperShield = false;

        // Creating and destroying super shield
        superShieldObject.SetActive(true);
        Invoke("DeactivateSuperShield", ShieldManager.SUPER_SHIELD_LIFETIME);
        hasSuperShield = true;
    }

    /// <summary>
    /// Deactivates the Super Shield
    /// </summary>
    private void DeactivateSuperShield()
    {
        superShieldObject.SetActive(false);
        hasSuperShield = false;
    }

    private void OnMouseDown()
    {
        //Debug.Log("The player has clicked on " + name);

        if (ShieldManager.Instance.canHeal)
        {
            RecoverHealth(ShieldManager.Instance.healAmount);
        }
        else if (ShieldManager.Instance.activateSuperShield)
        {
            ActivateSuperShield();
        }
    }

    #endregion
}
