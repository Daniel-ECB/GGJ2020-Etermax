using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;

    private void OnMouseDown()
    {
        //Debug.Log("The player has pressed the PowerUp");

        if (powerUpType == PowerUpType.SuperShield)
        {
            //Debug.Log("The player will have a better shield");
            ShieldManager.Instance.canHeal = false;
            ShieldManager.Instance.activateSuperShield = true;
        }

        Destroy(gameObject);
    }
}

public enum PowerUpType
{
    SuperShield,
    RecoverHealth
}
