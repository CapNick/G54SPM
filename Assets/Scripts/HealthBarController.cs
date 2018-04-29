using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{

    private Slider healthBar;

    // Use this for initialization
    void Start()
    {
        healthBar = GetComponent<Slider>();
    }

	public void ChangeHealth(float health)
    {
		healthBar.value = health;
    }
}