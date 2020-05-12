using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Clients : MonoBehaviour {

    public Transform myObject;
    public float myWaitingTime;
    float totalWaitingTime;
    public NavMeshAgent myAgent;
    public Transform myTarget;
    public Transform leavingPos;
    public Transform Entrance;
    public Transform Exit;
    public float radius;
    public int objectId;
    public Transform player;
    public SpriteRenderer objectImage;
    public Transform giftPos;
    public bool giver;
    private GameObject myGift;
    public GameObject particles;
    public Animator myAnimator;
    public List<Material> materials;
    public List<SkinnedMeshRenderer> renderers;   
    public Collider myCollider;

    Transform ShopPos;
    Transform LeavePos;

    public GameObject canvas;
    public Image fillImage;
  

    enum MyStates
    {
        Arriving, Purchasing, Giving, Leaving
    }

    MyStates currentState;

    SpriteRenderer myImage;
    Objects currentObject = null;

	// Use this for initialization
	void Start ()
    {
        ShopPos = myTarget;
        myTarget = Entrance;
        LeavePos = leavingPos;
        leavingPos = Exit;

        myImage = myObject.GetComponent<SpriteRenderer>();
        transform.GetComponentInChildren<RotateImage>().player = player;

        totalWaitingTime = myWaitingTime;

        if (giver)
            GetGift();

        currentState = MyStates.Arriving;

        ApplyMaterial(materials[Random.Range(0, materials.Count - 1)]);
	}

    void ApplyMaterial(Material mat)
    {
        foreach(SkinnedMeshRenderer mesh in renderers)
        {
            mesh.material = mat;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        ApplyCurrentState();        

    }

    public void SetMyTarget(Transform target)
    {
        myTarget = target;
    }

    private void Arrive()
    {    
        

        if (Vector3.Distance(transform.position, myTarget.position) <= radius)
        {
            if (myTarget != Entrance)
            {
                if (!giver)
                {
                    ChangeState(MyStates.Purchasing);
                }
                else
                {
                    ChangeState(MyStates.Giving);
                }
            }
            else
            {
                myTarget = ShopPos;
                myAgent.SetDestination(myTarget.position);
                
            }
            
            
        }
    }

    private void StartCounting()
    {
        
        myWaitingTime -= Time.deltaTime;

        fillImage.fillAmount = myWaitingTime / totalWaitingTime;


        if (currentObject != null)
        {
            if (currentObject.GetId() == objectId )
            {
                Instantiate(particles, transform.position + Vector3.up, Quaternion.identity, transform);
                Destroy(currentObject.gameObject);
                myCollider.enabled = false;               
                MainManager.SoundManager.PlaySound("Happy", transform.position);
                myAnimator.SetTrigger("Victory");
                
                MainManager.StoreManager.spawnManager.numOfOccupieds--;
                myTarget.GetComponent<TargetScript>().ImOccupied(false);
                MainManager.m_Instance.AddScore(10);
                MainManager.HUDManager.DisplayScore(MainManager.m_Instance.Score);
                ChangeState(MyStates.Leaving);
            }
            else
            {
            currentObject.transform.position = player.transform.position;

                MainManager.StoreManager.AddStrike();
                myCollider.enabled = false;
                MainManager.SoundManager.PlaySound("AngryGrunt", transform.position);
                myAnimator.SetTrigger("Defeat");
               
                MainManager.StoreManager.spawnManager.numOfOccupieds--;
                myTarget.GetComponent<TargetScript>().ImOccupied(false);
                ChangeState(MyStates.Leaving);
            }
        }

        if (myWaitingTime <= 0)
        {
            MainManager.StoreManager.AddStrike();
            myCollider.enabled = false;
            MainManager.SoundManager.PlaySound("AngryGrunt", transform.position);
            myAnimator.SetTrigger("Defeat");
            
            MainManager.StoreManager.spawnManager.numOfOccupieds--;
            myTarget.GetComponent<TargetScript>().ImOccupied(false);
            ChangeState(MyStates.Leaving);
        }
               
     
    }

    private void ChangeState(MyStates nextState)
    {

        currentState = nextState;

        switch(currentState)
        {
            case MyStates.Arriving:
                myAnimator.SetBool("Walk", true);
                break;
            case MyStates.Purchasing:
                myAnimator.SetBool("Walk", false);
                break;
            case MyStates.Leaving:
                myAnimator.SetBool("Walk", true);
                break;
            case MyStates.Giving:
                myAnimator.SetBool("Walk", false);
                break;
        }
    }

    private void Destroy()
    {          
 
        if (Vector3.Distance(transform.position, leavingPos.position) <= radius)
        {
            if (leavingPos != Exit)
            {
                MainManager.StoreManager.spawnManager.currentNumOfClients--;
                Destroy(this.gameObject);
            }
            else
            {                
                leavingPos = LeavePos;
                myAgent.SetDestination(leavingPos.position);
            }
            
        }
    }

    private void WaitingToGive()
    {
        myWaitingTime -= Time.deltaTime;

        fillImage.fillAmount = myWaitingTime / totalWaitingTime;

        if (myWaitingTime <= 0 )
        {
            MainManager.StoreManager.AddStrike();
            myCollider.enabled = false;
            MainManager.SoundManager.PlaySound("AngryGrunt", transform.position);
            myAnimator.SetTrigger("Defeat");
            MainManager.StoreManager.spawnManager.numOfOccupieds--;
            myTarget.GetComponent<TargetScript>().ImOccupied(false);
            
            ChangeState(MyStates.Leaving);
            return;
        }

        if(giftPos.childCount == 0)
        {
            Instantiate(particles, transform.position + Vector3.up, Quaternion.identity, transform);            
            myCollider.enabled = false;
            MainManager.StoreManager.spawnManager.numOfOccupieds--;
            myTarget.GetComponent<TargetScript>().ImOccupied(false);            
            myAnimator.SetTrigger("Victory");
            MainManager.m_Instance.AddScore(5);            
            
            MainManager.HUDManager.DisplayScore(MainManager.m_Instance.Score);
            ChangeState(MyStates.Leaving);
        }
    }

    private void ApplyCurrentState()
    {
        switch (currentState)
        {
            case MyStates.Arriving:

                

                myAgent.SetDestination(myTarget.position);
                Arrive();


                break;

            case MyStates.Purchasing:

                myAgent.isStopped = false;
                myImage.enabled = true;
                StartCounting();

                canvas.SetActive(true);

                transform.LookAt(player);

                break;

            case MyStates.Leaving:
                canvas.SetActive(false);
                myImage.enabled = false;
                if (myAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("Walk"))
                {
                    myAgent.SetDestination(leavingPos.position);
                    
                    
                    MainManager.currentClients.Remove(this);
                    Destroy();

                    
                }
                break;

            case MyStates.Giving:

                WaitingToGive();
                canvas.SetActive(true);

                transform.LookAt(player);


                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Object")
        {            
            currentObject = collision.transform.GetComponent<Objects>();      
        }        
    }

    private void GetGift()
    {
        myGift = Instantiate(MainManager.StoreManager.objects[Random.Range(0, MainManager.StoreManager.objects.Count)],giftPos.position,Quaternion.identity);
        myGift.transform.SetParent(giftPos);
        myGift.transform.GetComponent<Rigidbody>().isKinematic = true;
        MainManager.StoreManager.objectToSell.Add(myGift.transform.GetComponent<Objects>());
    }
}
