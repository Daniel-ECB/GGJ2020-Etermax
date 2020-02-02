using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour {
    #region Fields

    private static ShieldManager instance;

    [Header("Settings")]
    public float superShieldLifeTime = 10f;

    [Header("References")]
    [Tooltip("Amount of health a shield will recover with a click.")] public int healAmount = 10;
    public bool canHeal = true;

    public bool activateSuperShield = false;


    [Header("References")]
    public List<ShieldUnit> shields;

    #endregion

    #region Properties

    /// <summary>
    /// Gets and (private) sets the ShieldManager instance.
    /// </summary>
    public static ShieldManager Instance {
        get { return instance; }
        private set { instance = value; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Awake is called when the script is first loaded.
    /// </summary>
    void Awake() {
        // Simple Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Removes a ShielUnit from the reference List.
    /// </summary>
    /// <param name="_shieldUnit">The ShieldUnit to be removed.</param>
    public void RemoveShield(ShieldUnit _shieldUnit) {
        shields.Remove(_shieldUnit);

        if (shields.Count <= 0) {
            GameManager.instance.GameOver(false);
        }
    }

    #endregion
}
