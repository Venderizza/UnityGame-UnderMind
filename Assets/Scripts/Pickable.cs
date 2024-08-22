using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private int value;

    private void Start()
    {
        EventManager.onFirstPickable?.Invoke(gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventManager.onPick?.Invoke(gameObject.tag, value);
            Destroy(gameObject);
        }
    }
}
