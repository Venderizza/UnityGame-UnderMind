using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    private int numberWeapon = 0;
    private int currect;

	void Update()
	{
		currect = numberWeapon;
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			if (numberWeapon >= transform.childCount - 1) numberWeapon = 0;
			else numberWeapon++;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			if (numberWeapon <= 0) numberWeapon = transform.childCount - 1;
			else numberWeapon--;
		}
		if (currect != numberWeapon) selectWeapon();
	}

	void selectWeapon()
	{
		int i = 0;
		foreach (Transform weapon in transform)
		{
			if (i == numberWeapon) weapon.gameObject.SetActive(true);
			else weapon.gameObject.SetActive(false);
			i++;
		}
		EventManager.onSelectWeapon?.Invoke(numberWeapon);
	}
}
