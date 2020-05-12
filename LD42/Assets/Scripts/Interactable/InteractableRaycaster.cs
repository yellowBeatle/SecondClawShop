using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRaycaster : MonoBehaviour
{
    [SerializeField] private Camera m_MainCamera = null;
    [SerializeField] private float m_RaycastDistance = 20f;

    public bool AllowInteraction = true;

    private Interactable m_LastInteractable = null;

    public static bool InteractionInThisFrame = false;

    private void Update()
    {
        Raycast();
    }

    private void Raycast()
    {
        InteractionInThisFrame = false;
        if(AllowInteraction)
        {
            Ray ray = new Ray(m_MainCamera.transform.position, m_MainCamera.transform.forward);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo, m_RaycastDistance))
            {
                Interactable target = hitInfo.collider.GetComponent<Interactable>();
                if(target != null)
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        target.OnInteract();
                        InteractionInThisFrame = true;
                    }
                    else
                    {
                        if(m_LastInteractable == null)
                        {
                            target.OnPointerEnter();
                            m_LastInteractable = target;
                        }
                        else
                        {
                            if(m_LastInteractable != target)
                            {
                                m_LastInteractable.OnPointerExit();
                                target.OnPointerEnter();
                                m_LastInteractable = target;
                            }
                        }
                    }

                }
                else
                {
                    if(m_LastInteractable != null)
                    {
                        m_LastInteractable.OnPointerExit();
                        m_LastInteractable = null;
                    }
                }
            }
            else if(m_LastInteractable != null)
            {
                m_LastInteractable.OnPointerExit();
                m_LastInteractable = null;
            }
        }
    }

    public void EnableInteraction()
    {
        AllowInteraction = true;
    }

    public void DisableInteraction()
    {
        AllowInteraction = false;
    }
}
