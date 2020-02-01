using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHits;
    private float currentHits;

    void Start()
    {
        // The value of maxHits is set in the Awake method of ShieldUnit
        currentHits = maxHits;
    }

    /// <summary>
    /// Reduces the content of the HealthBar.
    /// </summary>
    /// <param name="_amount">Amount that will be reduced from the bar.</param>
    public void ReduceBar(float _amount)
    {
        currentHits -= _amount;
        transform.localScale = new Vector3(currentHits / maxHits, 1.0f, 1.0f);
    }
}
