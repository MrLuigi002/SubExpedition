using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObstacles : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;

    [SerializeField] private int obstacleType;

    [SerializeField] private GameObject rail;

    [SerializeField] private float minWait, maxWait;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (obstacleType > obstacles.Length - 1)
        {
            obstacleType = obstacles.Length - 1;
        }

        StartCoroutine(SpawnObstacle());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnObstacle()
    {
        Instantiate(obstacles[obstacleType], rail.transform);

        yield return new WaitForSeconds(Random.Range(minWait, maxWait));

        StartCoroutine(SpawnObstacle());
    }
}
