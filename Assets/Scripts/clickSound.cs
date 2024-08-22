using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickSound : MonoBehaviour
{
    [SerializeField] private AudioSource click;

    public void PlaySound()
    {
        click.Play();
    }
}
