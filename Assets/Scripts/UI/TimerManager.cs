using System.Collections;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public int countdownTime = 120;
    [SerializeField] TextMeshProUGUI countdownText;

    [HideInInspector] public float currentTime;

    ScoreManager scoreManager;
    GameStartStopper gameStartStopper;

    // fetches scoreManager and gameStartStopper scripts
    //Starts the game timer.
    void Start()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        gameStartStopper = FindAnyObjectByType<GameStartStopper>();
        StartCoroutine(CountdownTimer());
    }

    // ticks the timer down every second and then when timer ends, it checks each players points and set wincase based on who has more
    IEnumerator CountdownTimer()
    {
        currentTime = countdownTime;

        while (currentTime > 0)
        {
            countdownText.text = currentTime.ToString("0");
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        
        yield return new WaitForSeconds(1f);

        if (currentTime <= 0)
        {
            if (scoreManager.player1Score > scoreManager.player2Score)
                gameStartStopper.RedWins();

            else if (scoreManager.player1Score < scoreManager.player2Score)
                gameStartStopper.BlueWins();

            else
                gameStartStopper.Tied();
        }
    }
}
