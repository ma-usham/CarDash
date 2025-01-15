using System.Collections.Generic;
using UnityEngine;

public class AiCarManager : MonoBehaviour
{
    //private const float leftLane = -1.1f;
    //private const float centerLane = 0f;
    //private const float rightLane = 1.1f;
    private float[] Lanes = { -1.1f, 0f,1.1f };
    private const float yPos = -2.82f;

    [SerializeField] private GameObject aiCarPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int initialCarCount = 15;

    private List<GameObject> aiCars = new List<GameObject>();


    private void Start()
    {
       SpawnCars();
    }

    private void Update()
    {
        if (player.transform.position.z > aiCars[0].transform.position.z + 1 ) // 1 is ai car length
        {
            SpawnCars();
            DestroyCar();
           
        }
    }

    void SpawnCars()
    {
        GameObject car = Instantiate(aiCarPrefab, CarSpawnPosition(), Quaternion.identity);
        aiCars.Add(car);
    }

    Vector3 CarSpawnPosition()
    {
        return new Vector3(Lanes[Random.Range(0, Lanes.Length)], yPos , player.position.z + 50);
    }
    private void DestroyCar()
    {
        GameObject oldCar = aiCars[0];
        aiCars.RemoveAt(0);
        Destroy(oldCar);
    }




}
