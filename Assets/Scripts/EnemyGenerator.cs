using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private static int numPoints = 2;
    private static int numEnemies = 3;
    [SerializeField] private Transform[] spawnPoints = new Transform[numPoints];
    [SerializeField] private GameObject[] enemy = new GameObject[numEnemies];

    private static System.Random rnd = new System.Random();
    private int spawnPoint = rnd.Next(0, numPoints);
    private int spawnDelay = rnd.Next(5, 15);

    private int generatedEnemies = 0;
    [SerializeField] private int maxGenerations;

    private int kills = 0;

    private void OnEnable()
    {
        EventManager.onDeathOfEnemy += CalculateKills;
    }
    private void OnDisable()
    {
        EventManager.onDeathOfEnemy -= CalculateKills;
    }

    private void Update()
    {
        if (generatedEnemies < maxGenerations)
        {
            Invoke("ChooseEnemy", spawnDelay);
            generatedEnemies++;
            spawnDelay = rnd.Next(5, 20);
        }
    }

    void ChooseEnemy()
    {
        if (kills < 10)
        {
            CreateEnemy(0);
        }
        else if (kills >= 10 && kills < 20)
        {
            CreateEnemy(rnd.Next(0, numEnemies - 1));
        }
        else if (kills >= 20 && kills <= 40) 
        {
            CreateEnemy(rnd.Next(0, numEnemies));
        }
        else if (kills > 40)
        {
            CreateEnemy(rnd.Next(1, numEnemies));
        }
    }
    void CreateEnemy(int index)
    {
        if (index == 0) EventManager.onFirstEnemy0?.Invoke();
        else if (index == 1) EventManager.onFirstEnemy1?.Invoke();
        else if (index == 2) EventManager.onFirstEnemy2?.Invoke();

        Instantiate(enemy[index], spawnPoints[spawnPoint].transform.position, spawnPoints[spawnPoint].rotation);
        spawnPoint = rnd.Next(0, numPoints);
    }

    void CalculateKills()
    {
        kills++;
        generatedEnemies--;
    }
}
