using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public const float maxHealth = 100;
	public float currentHealth = maxHealth;
	public GameObject healthbar;
	public GameObject deathMenu;

    //taking damage and reducing health when the player is attacked

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
		healthbar.GetComponent<HealthBarController>().ChangeHealth (currentHealth);
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            //Debug.Log("health zero - dead!");
			//player's health is zero, game is paused, position reset
			Time.timeScale = 0;
			transform.position = new Vector3 (0,70f,0);
			deathMenu.SetActive(true);
        }
    }


}
