using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IAttackable
{
    public int health;
    public int damage;
    public GameObject target;


    public void Kill()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
            Kill();

        //End of game
    }
}
