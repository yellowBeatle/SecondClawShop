using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Transform ItemHolder = null;
    public InteractableRaycaster InteractableRaycaster = null;
    public float ThrowForce = 5f;

    private bool m_ItemPicked = false;
    private ItemComponent m_CurrentItem = null;

    private bool wantToEnableInteraction = false;

    private void Start()
    {
        MainManager.m_Instance.player = this.transform;
    }

    private void Update()
    {
        if(m_ItemPicked && !InteractableRaycaster.InteractionInThisFrame)
        {
            if(Input.GetMouseButtonDown(0))
            {
                m_ItemPicked = false;
                m_CurrentItem.ThrowItem(InteractableRaycaster.transform.forward * ThrowForce);
                m_CurrentItem = null;
                InteractableRaycaster.AllowInteraction = true;
            }
            else if(Input.GetMouseButtonDown(1))
            {
                m_ItemPicked = false;
                m_CurrentItem.LeaveItem();
                m_CurrentItem = null;
                InteractableRaycaster.AllowInteraction = true;
            }
        }
    }

    public void PickItem(ItemComponent newItem)
    {
        InteractableRaycaster.DisableInteraction();
        m_ItemPicked = true;
        m_CurrentItem = newItem;
        m_CurrentItem.transform.SetParent(null);
        InteractableRaycaster.AllowInteraction = false;
    }
}
