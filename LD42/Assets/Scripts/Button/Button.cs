using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable {
    
    public Animator animatorController;
    public int costToPress;
    public Transform doorPos;   

    bool firstTime = true;
    SpawnManager spawner;
    public ShopEventManager shop;
    public HoleSPawner sSpawner;



    // Use this for initialization
    void Start ()
    {
        spawner = MainManager.StoreManager.spawnManager;
	}

    public override void OnPointerEnter()
    {
        if (!firstTime && !spawner.closeShop)
            MainManager.HUDManager.DisplayHelp(costToPress.ToString());
    }

    public override void OnPointerExit()
    {
        if (!firstTime)
            MainManager.HUDManager.EraseHelp();
    }

    public override void OnInteract()
    {            

        if (!firstTime)
        {           

            if (costToPress <= MainManager.m_Instance.Score && !spawner.closeShop)
            {
                MainManager.m_Instance.AddScore(-costToPress);
                MainManager.HUDManager.DisplayScore(MainManager.m_Instance.Score);

                animatorController.Play("Press");

                MainManager.SoundManager.PlaySound("Button Sound", transform.position);

                if (!spawner.closeShop)
                {
                    StopCoroutine(shop.holeSpawnerSan.SpawnCo ());
                    shop.holeSpawnerSan.gameObject.SetActive(false);

                    spawner.closeShop = true;
                    MainManager.SoundManager.PlaySound("CloseDoor", doorPos.position);

                }
                
            }
            else if(spawner.closeShop)
            {
                StartCoroutine(shop.holeSpawnerSan.SpawnCo());

                shop.holeSpawnerSan.gameObject.SetActive(true);
                spawner.closeShop = false;
                animatorController.Play("Press");
                MainManager.SoundManager.PlaySound("DoorBell", doorPos.position);
            }

        }

        if (firstTime)
        {
            animatorController.Play("Press");
            MainManager.SoundManager.PlaySound("Button Sound", transform.position);
            spawner.openDoor = true;            
            firstTime = false;
        }



    }
}
