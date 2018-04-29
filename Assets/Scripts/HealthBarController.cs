using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{

    private Slider _healthBar;

    // Use this for initialization
    void Start()
    {
        _healthBar = GetComponent<Slider>();
    }

	//changes value of health in health bar UI
	public void ChangeHealth(float health)
    {
		_healthBar.value = health;
    }
}