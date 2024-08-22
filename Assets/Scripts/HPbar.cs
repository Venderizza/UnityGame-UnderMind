using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Slider healthBar;
    public Vector3 offset;

    private void FixedUpdate()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    public void SetHealthValue(int currectHealth, int maxHealth)
    {
        healthBar.value = currectHealth;
        healthBar.maxValue = maxHealth;
        healthBar.gameObject.SetActive(true);
    }
}
