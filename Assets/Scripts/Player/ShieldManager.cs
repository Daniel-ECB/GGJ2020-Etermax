using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    private static ShieldManager instance;

    public List<ShieldUnit> shields;

    public static ShieldManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    void Awake()
    {
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
    public void RemoveShield(ShieldUnit _shieldUnit)
    {
        shields.Remove(_shieldUnit);

        if (shields.Count <= 0)
        {
            Debug.Log("All shields have been destroyed");
        }
    }
}
