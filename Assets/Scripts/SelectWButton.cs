using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWButton : MonoBehaviour
{
    [SerializeField] private Button sword;
    [SerializeField] private Button gun;

    private void OnEnable()
    {
        EventManager.onSelectWeapon += Select;
    }
    private void OnDisable()
    {
        EventManager.onSelectWeapon -= Select;
    }

    void Select(int index)
    {
        switch (index)
        {
            case 0:
                sword.Select();
                break;
            case 1:
                gun.Select();
                break;
        }
    }
}
