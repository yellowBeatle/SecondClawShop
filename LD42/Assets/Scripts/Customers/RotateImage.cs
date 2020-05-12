using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour {

    public Transform player;
    
	void Start ()
    {
        player = MainManager.m_Instance.player;
    }
	void Update ()
    {        
        transform.LookAt(player,Vector3.up);
	}
}
