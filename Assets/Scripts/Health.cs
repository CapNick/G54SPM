using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;


    //taking damage and reducing health when the player is attacked

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("health zero - dead!");
        }
    }

    //damage on collision

    public void OnCollisionEnter(Collision collision)
    {
       /* int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }*/

        /*
         if (health != null)
        {
            health.TakeDamage(10);
        }
        */
    }
}
