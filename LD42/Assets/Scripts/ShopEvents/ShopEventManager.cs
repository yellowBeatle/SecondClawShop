using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEventManager : MonoBehaviour {

    public HoleSPawner holeSpawnerSan;

    public float holeTimer;

    public IEnumerator HoleCoroutine;

    private void Start()
    {
        holeSpawnerSan = GameObject.Find("THAGLORYHOLE").gameObject.GetComponent<HoleSPawner>();

        HoleCoroutine = HoleCO();
        StartCoroutine(HoleCoroutine);
    }

    IEnumerator HoleCO ()
    {
        yield return new WaitForSeconds(holeTimer);
        holeSpawnerSan.StartSpawn();
        HoleCoroutine = HoleCO();
        StartCoroutine(HoleCoroutine);
    }



}
