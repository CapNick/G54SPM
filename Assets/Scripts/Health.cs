using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public const float MaxHealth = 100;
	public float CurrentHealth = MaxHealth;
	public GameObject Healthbar;
	public GameObject DeathMenu;

    //taking damage and reducing health when the player is attacked

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
		Healthbar.GetComponent<HealthBarController>().ChangeHealth (CurrentHealth);
        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
       
			//player's health is zero, game is paused, position reset
		    
			Time.timeScale = 0;
			transform.position = new Vector3 (0,70f,0);
			DeathMenu.SetActive(true);
        }
    }

	public void Respawn()
	{
		Time.timeScale = 1;
		DeathMenu.SetActive (false);
		CurrentHealth = MaxHealth;
		Healthbar.GetComponent<HealthBarController>().ChangeHealth (CurrentHealth);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
