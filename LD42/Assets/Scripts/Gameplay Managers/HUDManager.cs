using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //References
    public GameObject DialoguePanel = null;
    public Text DialogueText = null;
    public Text ScoreText = null;
    public Text HelpText;
    public Font myFont;

    public GameObject Tuto;
    public GameObject ExitButton;
    bool paused;

    private bool m_TextDisplaying = false;

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        Tuto.SetActive(false);
        DialogueText.font = myFont;
        HelpText.font = myFont;
        ScoreText.font = myFont;
        DisplayText(MainManager.StoreManager.startText);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                ExitButton.SetActive(true);
                Tuto.SetActive(true);
                Time.timeScale = 0;
                paused = true;
            }
            else
            {
                ExitButton.SetActive(false);
                Tuto.SetActive(false);
                Time.timeScale = 1;
                paused = false;
            }

        }

        if (m_TextDisplaying)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                HideText();
            }
        }
    }

    public void DisplayText(string text)
    {
        m_TextDisplaying = true;        
        DialogueText.text = text;        
        DialoguePanel.SetActive(true);
    }

    public void DisplayHelp(string text)
    {
        HelpText.text = text;        
        HelpText.gameObject.SetActive(true);
    }
    public void EraseHelp ()
    {
        HelpText.gameObject.SetActive(false);
    }

    public void HideText()
    {
        DialoguePanel.SetActive(false);
        m_TextDisplaying = true;
    }

    public void DisplayScore(int currentScore)
    {       
        ScoreText.text = currentScore.ToString();
        
    }
}
