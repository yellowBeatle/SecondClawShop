using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager m_Instance = null;
    private static MainManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    [Header("Prefabs")]
    [SerializeField] private GameObject m_MainCharacterPrefab = null;
    [SerializeField] private GameObject m_GUICameraPrefab = null;

    public Transform player;

    private void Awake()
    {
        if(m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            m_Instance = this;

            DontDestroyOnLoad(gameObject);

            //Initialize here.
            currentClients = new List<Clients>();
            InstantiateDynamicObjects();
            GetReferences();
            InstantiateMainCharacter();
        }
    }

    private void Start()
    {
        SoundManager.PlayMusic("Music");
    }

    private void InstantiateDynamicObjects()
    {
        Instantiate(m_GUICameraPrefab);
    }

    private void GetReferences()
    {
        m_StoreManager = GetComponent<StoreManager>();
        m_HUDManager = FindObjectOfType<HUDManager>();
        m_SoundManager = GetComponent<SoundManager>();
    }

    public void InstantiateMainCharacter()
    {
        m_MainCharacter = FindObjectOfType<MainCharacter>();

        if(m_MainCharacter != null)
        {
            return;
        }

        m_MainCharacter = Instantiate(m_MainCharacterPrefab).GetComponent<MainCharacter>();
    }

    private int m_Score = 0;
    public int Score { get { return m_Score; } private set { m_Score = value; } }

    public void AddScore(int newScore)
    {
        Score += newScore;
    }

    #region Managers

    private StoreManager m_StoreManager = null;
    public static StoreManager StoreManager
    {
        get
        {
            return Instance.m_StoreManager;
        }
    }

    private HUDManager m_HUDManager = null;
    public static HUDManager HUDManager
    {
        get
        {
            return Instance.m_HUDManager;
        }
    }

    private SoundManager m_SoundManager = null;
    public static SoundManager SoundManager
    {
        get
        {
            return Instance.m_SoundManager;
        }
    }
    
    public static List<Clients> currentClients;
    private MainCharacter m_MainCharacter = null;
    public static MainCharacter MainCharacter
    {
        get
        {
            return Instance.m_MainCharacter;
        }
    }

    #endregion

    #region Scene Loading

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadSceneAdditively(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    #endregion
}
