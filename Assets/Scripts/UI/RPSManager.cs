using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPSManager : MonoBehaviour
{
    public enum RPSChoice { None, Rock, Paper, Scissors }

    public static RPSManager instance;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI player1ChoiceText;
    [SerializeField] TextMeshProUGUI player2ChoiceText;
    [SerializeField] float countdownTime = 10f;
    [SerializeField] GameObject panel;

    [Header("Player Choices")]
    public RPSChoice player1Choice = RPSChoice.None;
    public RPSChoice player2Choice = RPSChoice.None;

    [Header("Final Result")]
    public CharacterSwitcher.CharacterSelected player1Character;
    public CharacterSwitcher.CharacterSelected player2Character;

    private bool selectionLocked = false;

    //sets instance to this and sets as dontdestroy on load.
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // starts the character choosing countdown timer.
    public void StartCountdownIfActive()
    {
        if (countdownText != null && countdownText.gameObject.activeInHierarchy)
            StartCoroutine(Countdown());
    }   

    //sets players 1 choice when they press relevant buttons
    public void SetPlayer1Choice(string choice)
    {
        if (selectionLocked) return;
        player1Choice = ParseChoice(choice);
        player1ChoiceText.text = choice;
    }

    // sets players 2 choice when they press relevant buttons
    public void SetPlayer2Choice(string choice)
    {
        if (selectionLocked) return;
        player2Choice = ParseChoice(choice);
        player2ChoiceText.text = choice;
    }

    // converts string input from buttons into the enum 
    RPSChoice ParseChoice(string choice)
    {
        return choice switch
        {
            "Rock" => RPSChoice.Rock,
            "Paper" => RPSChoice.Paper,
            "Scissors" => RPSChoice.Scissors,
            _ => RPSChoice.None
        };
    }

    //carries out the cowndown, calls decidecharacters when its over and locks in the choices
    IEnumerator Countdown()
    {
        float time = countdownTime;
        while (time > 0)
        {
            countdownText.text = time.ToString("0");
            yield return new WaitForSeconds(1f);
            time--;
        }

        selectionLocked = true;
        DecideCharacters();

        SceneManager.LoadScene("MainLevel");
    }

    // when timer goes out, it passes the final choices to characterswither.
    public void DecideCharacters()
    {
        player1Character = player1Choice switch
        {
            RPSChoice.Paper => CharacterSwitcher.CharacterSelected.Paper,
            RPSChoice.Scissors => CharacterSwitcher.CharacterSelected.Scissors,
            RPSChoice.Rock => CharacterSwitcher.CharacterSelected.Rock,
            _ => CharacterSwitcher.CharacterSelected.Paper
        };

        player2Character = player2Choice switch
        {
            RPSChoice.Paper => CharacterSwitcher.CharacterSelected.Paper,
            RPSChoice.Scissors => CharacterSwitcher.CharacterSelected.Scissors,
            RPSChoice.Rock => CharacterSwitcher.CharacterSelected.Rock,
            _ => CharacterSwitcher.CharacterSelected.Paper
        };
    }


}
