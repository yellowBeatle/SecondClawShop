using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    public List<Objects> objectToSell = new List<Objects>();
    public SpawnManager spawnManager;
    public List<GameObject> objects;

    [Multiline]
    public List<string> StrikeTexts;

    public string startText;

    public int MaxStrikesToLose = 5;

    private int m_CurrentStrikes = 0;



    public void AddStrike()
    {
        MainManager.HUDManager.DisplayText(StrikeTexts[m_CurrentStrikes]);
        m_CurrentStrikes++;
        


        if(m_CurrentStrikes >= MaxStrikesToLose)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
