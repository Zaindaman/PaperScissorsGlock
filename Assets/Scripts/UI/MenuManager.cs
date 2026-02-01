using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject titleScreenPanel;
    [SerializeField] private GameObject characterScreenPanel;

    private RPSManager rpsManager;

    //sets timescale to 1 to allow UI interaction and allows title screen to show and hides characterscreen
    void Start()
    {
        Time.timeScale = 1f;

        titleScreenPanel.SetActive(true);
        characterScreenPanel.SetActive(false);

        rpsManager = RPSManager.instance;
    }

    //hides titlescreen, shows character screen, and starts countdown.
    public void ShowCharacterScreen()
    {
        titleScreenPanel.SetActive(false);
        characterScreenPanel.SetActive(true);

        if (RPSManager.instance != null)
            RPSManager.instance.StartCountdownIfActive();
    }

}
