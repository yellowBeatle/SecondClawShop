using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxEraser : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("MainMenu");
        }
        other.gameObject.SetActive(false);
    }

}
