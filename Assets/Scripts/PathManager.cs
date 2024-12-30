using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int initialRoadCount = 15;
    [SerializeField] private float roadLength = 3.2f;
    private Vector3 spawnRoadPos =Vector3.zero;

    private List<GameObject> roadSections = new List<GameObject>();
    private void Start()
    {
        for(int i = 0; i < initialRoadCount; i++)
        {
            SpawnRoad();
        }
    }
    private void Update()
    {
        if (player.transform.position.z > roadSections[0].transform.position.z + roadLength)
        {
            //SpawnRoad();
            DestroyRoad();
        }
    }

    private void SpawnRoad()
    {
        GameObject road = Instantiate(roadPrefab, spawnRoadPos, Quaternion.identity);
        road.transform.parent = this.transform;
       // road.isStatic = true;
        roadSections.Add(road);
        spawnRoadPos += Vector3.forward * roadLength;
    }

    private void DestroyRoad()
    {
        GameObject oldRoad = roadSections[0];
        roadSections.RemoveAt(0);
        oldRoad.transform.position = spawnRoadPos;
        spawnRoadPos += Vector3.forward * roadLength;

        // Re-add it to the end of the list
        roadSections.Add(oldRoad);
    }


}
