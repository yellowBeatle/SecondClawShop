using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottonTruck : Interactable
{
    public int pointsNeeded = 0;
    public  TruckManager truck;
    public Animator animatorController;

    public override void OnPointerEnter()
    {
        MainManager.HUDManager.DisplayHelp (pointsNeeded.ToString());
    }

    public override void OnPointerExit()
    {
        MainManager.HUDManager.EraseHelp();
    }

    public override void OnInteract()
    {

        if (truck.ok == false && pointsNeeded < MainManager.m_Instance.Score)
        {
            MainManager.m_Instance.AddScore (-pointsNeeded);
            MainManager.HUDManager.DisplayScore(MainManager.m_Instance.Score);

            animatorController.Play("Press");
            truck.StartTruck();
        }

    }
}
