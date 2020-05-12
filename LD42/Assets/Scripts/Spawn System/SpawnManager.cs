using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int maxClients;
  
    public float maxTimeToSpawn;
    public List<GameObject> customers;
    public List<Transform> targetPos;
    public List<Transform> leavingTarget;
    public Transform Exit;
    public Transform Entrance;
    public int probToGiver;
    public bool openDoor;
    public bool closeShop;
    
    


    private int objectId;
    private Clients currentCustomer;
    public int currentNumOfClients;
    public int numOfOccupieds = 0;
    float timeBetweenSpawns = 0.0f;

    void Start()
    {        
        openDoor = false;
    }

    private void Update()
    {
        SpawnCustomer();
    }

    void SpawnCustomer()
    {
        if (openDoor && !closeShop)
        {
            timeBetweenSpawns -= Time.deltaTime;

            if (timeBetweenSpawns <= 0 && currentNumOfClients < maxClients)
            {
                timeBetweenSpawns = maxTimeToSpawn;

                if (CheckAnyUnoccupied())
                {

                    int objectIndex = Random.Range(0, MainManager.StoreManager.objectToSell.Count);

                    currentCustomer = Instantiate(customers[Random.Range(0,customers.Count)], transform.position, Quaternion.identity).GetComponent<Clients>();
                    MainManager.currentClients.Add(currentCustomer);
                    DecideGiver(currentCustomer);
                    currentNumOfClients++;
                    if (!currentCustomer.giver)
                    {
                        currentCustomer.objectId = MainManager.StoreManager.objectToSell[objectIndex].GetId();
                        currentCustomer.objectImage.sprite = MainManager.StoreManager.objectToSell[objectIndex].GetSprite();
                        MainManager.StoreManager.objectToSell.Remove(MainManager.StoreManager.objectToSell[objectIndex]);
                    }
                    currentCustomer.myTarget = RandomTarget();
                    currentCustomer.myTarget.GetComponent<TargetScript>().ImOccupied(true);
                    currentCustomer.leavingPos = RandomLeavingPoint();
                    currentCustomer.Exit = Exit;
                    currentCustomer.Entrance = Entrance;
                }


            }
        }

        if (closeShop)
        {
            timeBetweenSpawns = 0.0f;
        }
    }
        

    Transform RandomTarget()
    {
        Transform currentTarget = targetPos[Random.Range(0, targetPos.Count)];

        
            while (currentTarget.GetComponent<TargetScript>().occupied)
            {
                currentTarget = targetPos[Random.Range(0, targetPos.Count)];                
            }

        return currentTarget;

    }

    Transform RandomLeavingPoint()
    {
        return leavingTarget[Random.Range(0, leavingTarget.Count)];
    }

    bool CheckAnyUnoccupied()
    {      

        foreach (Transform target in targetPos)
        {           

            if (target.GetComponent<TargetScript>().occupied)
            {
                numOfOccupieds++;

                if (numOfOccupieds >= targetPos.Count)
                {
                    
                    return false;
                }
            }
        }

        numOfOccupieds = 0;
        return true;
    }

    private void DecideGiver(Clients customer)
    {
        if (MainManager.StoreManager.objectToSell.Count == 0)
        {           
            customer.giver = true;
        }
        else
        {
            int score = Random.Range(0, 99);

            if(score<= probToGiver)
            {
                customer.giver = true;
            }
            else
            {
                customer.giver = false;
            }
        }
           


    }

}
