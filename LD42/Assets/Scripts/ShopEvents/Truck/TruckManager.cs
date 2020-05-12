using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour {

    public float OpenWindow;
    public GameObject Eraser;

    public bool ok = false;

    Vector3 openPos;
    Vector3 closedPos;

    private void Start()
    {
        openPos = transform.position + (transform.up * 3);
        closedPos = transform.position;
    }
    

    public void StartTruck()
    {
        StartCoroutine(WindowCO());
    }

    IEnumerator WindowCO ()
    {
        while (transform.position.y < openPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position,openPos,2f * Time.deltaTime);
            MainManager.SoundManager.PlaySound("Trampilla", transform.position);
            yield return null;
        }
        ok = true;
        yield return new WaitForSeconds(OpenWindow);

        while (transform.position.y > closedPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPos, 2f * Time.deltaTime);
            yield return null;
        }
        Eraser.SetActive(true);
        yield return new WaitForSeconds(1);
        Eraser.SetActive(false);
        ok = false;

    }


}
