using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUnit : MonoBehaviour, IAttackable
{
    #region Fields

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth = 1000;
    private int currentHealth;

    private bool isDeath = false;

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
    }

    /// <summary>
    /// Kills the Shield.
    /// </summary>
    public void Kill()
    {
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
        // There's no point in damaging a death shield
        if (isDeath)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        healthBar.ReduceBar(damage);

        if (currentHealth <= 0)
            Kill();
    }

    #endregion
}
