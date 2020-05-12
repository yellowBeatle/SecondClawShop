using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : Interactable
{
    public Rigidbody Rigidbody = null;

    private bool m_Picked = false;
    private Transform m_Holder = null;

    public override void OnInteract()
    {
        if(!m_Picked)
        {
            transform.rotation = Quaternion.identity;
            m_Picked = true;
            m_Holder = MainManager.MainCharacter.ItemHolder;
            Rigidbody.isKinematic = true;
            MainManager.MainCharacter.PickItem(this);
        }
    }

    private void FixedUpdate()
    {
        if(m_Picked)
        {
            Rigidbody.MovePosition(m_Holder.position);
        }
    }

    public void ThrowItem(Vector3 force)
    {
        m_Picked = false;
        m_Holder = null;
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce(force, ForceMode.Impulse);
        MainManager.MainCharacter.InteractableRaycaster.EnableInteraction();
    }

    public void LeaveItem()
    {
        m_Picked = false;
        m_Holder = null;
        Rigidbody.isKinematic = false;
        MainManager.MainCharacter.InteractableRaycaster.EnableInteraction();
    }
}
