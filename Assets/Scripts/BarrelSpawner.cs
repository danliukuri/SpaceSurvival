using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject barrelPrefab;
    [SerializeField] float spawnCircleRadius;
    [SerializeField] float noSpawnCircleRadius;
    [SerializeField] int spawnCount;
    [SerializeField] float spawnDelayDistance;
    Vector3 offset;
    Vector3 previousPlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3();
        Spawn(spawnCount);
        previousPlayerPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, previousPlayerPosition) > spawnDelayDistance)
        {
            Spawn(spawnCount);
            previousPlayerPosition = player.position;
        }
    }
    void Spawn(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            float spawnRadius = spawnCircleRadius * Random.value + noSpawnCircleRadius;
            float ang = Random.value * 360;
            offset.x = Mathf.Sin(ang * Mathf.Deg2Rad) * spawnRadius;
            offset.z = Mathf.Cos(ang * Mathf.Deg2Rad) * spawnRadius;
            Instantiate(barrelPrefab, player.position + offset, Random.rotation);
        }
    }
}
