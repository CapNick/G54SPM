using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{

    private Slider healthBar;
    public const int maxHealth = 100;
    public int currentHealth = maxHealth;

    // Use this for initialization
    void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
    }


}