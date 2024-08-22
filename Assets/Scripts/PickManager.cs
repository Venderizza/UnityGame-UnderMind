using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickManager : MonoBehaviour
{
    private static int numbPoint = 5;
    private static int numbPickables = 3;
    [SerializeField] private GameObject[] Pickables = new GameObject[numbPickables];
    [SerializeField] private Transform[] pickPoints = new Transform[numbPoint];
    private static int InstantDelayMin = 7;
    private static int InstantDelayMax = 12;
    private int numberOfPickables = 0;

    private static System.Random rnd = new System.Random();

    private int InstantPointRand = rnd.Next(0, numbPoint);
    private int InstantDelay = rnd.Next(InstantDelayMin, InstantDelayMax);
    private int InstantElement = rnd.Next(0, numbPickables);

    private void Update()
    {
        if (numberOfPickables < 1)
            StartTimer();
    }

    private void OnEnable()
    {
        EventManager.onPick += ClearPickables;
    }
    private void OnDisable()
    {
        EventManager.onPick -= ClearPickables;
    }

    void StartTimer()
    {
        Invoke("CreatePickableElement", InstantDelay);
        numberOfPickables++;
 
    }

    void CreatePickableElement()
    {
        Instantiate(Pickables[InstantElement], pickPoints[InstantPointRand].transform);
        Debug.Log(numberOfPickables);

        InstantDelay = rnd.Next(InstantDelayMin, InstantDelayMax);
        InstantElement = rnd.Next(0, numbPickables);
        InstantPointRand = rnd.Next(0, numbPoint);
        
    }
    void ClearPickables(string nothing1, int nothing2)
    {
        numberOfPickables = 0;
    }
}
