using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Scores")]
    public int player1Score = 0;
    public int player2Score = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI player1ScoreText;
    [SerializeField] TextMeshProUGUI player2ScoreText;

    // sets instance as a singleton
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    //ticks one point to player 1 if player 2 "dies"
    public void AddPointToPlayer1()
    {
        player1Score++;
        Debug.Log("added point to P1");
        UpdateScoreUI();
    }

    //ticks one point to player 2 if player 1 "dies"
    public void AddPointToPlayer2()
    {
        player2Score++;
        UpdateScoreUI();
        Debug.Log("added point to P2");

    }

    //updates score UI for both players
    void UpdateScoreUI()
    {
        player1ScoreText.text = player1Score.ToString();

        player2ScoreText.text = player2Score.ToString();
    }
}
