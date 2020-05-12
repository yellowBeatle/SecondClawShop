using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walkers : MonoBehaviour {

    
   
    
    public NavMeshAgent myAgent;
    public Transform myTarget;
    public Transform leavingPos;    
    public float radius;    
    public Animator myAnimator;
    public List<Material> materials;
    public List<SkinnedMeshRenderer> renderers;
    public float myWaitingTime;

    Transform ShopPos;
    Transform LeavePos;

    enum MyStates
    {
        Arriving, Purchasing, Leaving
    }

    MyStates currentState;    

    // Use this for initialization
    void Start ()
    {
        ApplyMaterial(materials[Random.Range(0, materials.Count - 1)]);
    }
	
	// Update is called once per frame
	void Update ()
    {
        ApplyCurrentState();
    }

    void ApplyMaterial(Material mat)
    {
        foreach (SkinnedMeshRenderer mesh in renderers)
        {
            mesh.material = mat;
        }
    }

    private void ChangeState(MyStates nextState)
    {
        currentState = nextState;

        switch (currentState)
        {
            case MyStates.Arriving:
                myAnimator.SetBool("Walk", true);
                break;                   
        }
    }

    private void Arrive()
    {
        if (Vector3.Distance(transform.position, myTarget.position) <= radius)
        {              
            ChangeState(MyStates.Purchasing);
        }
    }

    private void StartCounting()
    {
        myWaitingTime -= Time.deltaTime;  

        if (myWaitingTime <= 0)
        {            
            ChangeState(MyStates.Leaving);
        }
    }

    private void Destroy()
    {
        if (Vector3.Distance(transform.position, leavingPos.position) <= radius)
        {            
            Destroy(this.gameObject);           
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

                myAgent.isStopped = true;
                
                StartCounting();               

                break;

            case MyStates.Leaving:
                myAgent.isStopped = false;
                myAgent.SetDestination(leavingPos.position);
                Destroy();             
                 break;            
        }
    }

}
