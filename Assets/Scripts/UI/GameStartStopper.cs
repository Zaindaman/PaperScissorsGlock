using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartStopper : MonoBehaviour
{
    public static GameStartStopper instance;
    
    // just initialises this as a static instance 
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("UI Features")]
    [SerializeField] GameObject controlsPanel;

    [SerializeField] GameObject redWinPanel;
    [SerializeField] GameObject blueWinPanel;
    [SerializeField] GameObject tiePanel;


    [SerializeField] KeyCode startKey = KeyCode.Space;
    public bool gameStarted;

    // sets timescale to 0 to frezegame and sets the controlls screen on while setting the others off
    void Start()
    {
        gameStarted = false;
        Time.timeScale = 0f;
        controlsPanel.SetActive(true);
        blueWinPanel.SetActive(false);
        redWinPanel.SetActive(false);
        tiePanel.SetActive(false);
    }
    
    //checks for start button to be pressed to turn off controls pannel and start game
    void Update()
    {
        if (Time.timeScale == 0f && Input.GetKeyDown(startKey))
        {
            Time.timeScale = 1f;
            controlsPanel.SetActive(false);
            gameStarted = true;
        }
    }
    
    //freeses game and shows red win pannel
    public void RedWins()
    {
        Time.timeScale = 0f;
        redWinPanel.SetActive(true);

        StartCoroutine(SendToMenu());
    }

    //freeses game and shows blue win pannel

    public void BlueWins()
    {
        Time.timeScale = 0f;
        blueWinPanel.SetActive(true);

        StartCoroutine(SendToMenu());
    }

    //freeses game and shows tied pannel
    public void Tied()
    {
        Time.timeScale = 0f;
        tiePanel.SetActive(true);

        StartCoroutine(SendToMenu());
    }

    // unfreezes timesccale and loads the titlescreen after 3 seconds.
    IEnumerator SendToMenu()
    {
        Time.timeScale = 1f; // reset before loading
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene("TitleScreen");
    }
}
