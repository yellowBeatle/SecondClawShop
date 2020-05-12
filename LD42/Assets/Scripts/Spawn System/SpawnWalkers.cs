using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWalkers : MonoBehaviour {

    public List<GameObject> walkers;
    public List<Transform> targetPos;
    public List<Transform> exitPos;
    public float maxTimeToSpawn;
    

    Walkers currentClient;
    float timeToSpawn;

    // Use this for initialization
    void Start ()
    {
        timeToSpawn = maxTimeToSpawn;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnWalkersTown();

    }

    private void SpawnWalkersTown()
    {
        timeToSpawn -= Time.deltaTime;

        if (timeToSpawn <= 0)
        {
            timeToSpawn = maxTimeToSpawn;

            currentClient = Instantiate(walkers[Random.Range(0, walkers.Count)],transform.position,Quaternion.identity).GetComponent<Walkers>();
            currentClient.myTarget = RandomTarget();            
            currentClient.leavingPos = RandomLeavingPoint();            
        }
    }

    Transform RandomTarget()
    {
        
        return targetPos[Random.Range(0, targetPos.Count)];
    }

    Transform RandomLeavingPoint()
    {
        return exitPos[Random.Range(0, exitPos.Count)];
    }
}
