using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSPawner : MonoBehaviour {

    public GameObject Box;
    public Transform Point;
    public Transform PropHolder;
    float timeToSpawn;

    int SpawnNum;

    public IEnumerator Spawner;
    

    public void StartSpawn ()
    {
        SpawnNum = Random.Range(5, 10);
        Spawner = SpawnCo();
        StartCoroutine(Spawner);
    }

    public IEnumerator SpawnCo ()
    {
        timeToSpawn = Random.Range(0.1f, 1);
        yield return new WaitForSeconds(timeToSpawn);
        Instantiate(Box, Point.position,Quaternion.identity, PropHolder);
        SpawnNum--;
        if (SpawnNum > 0)
        {
            Spawner = SpawnCo();
            StartCoroutine(Spawner);
        }
    }

}
