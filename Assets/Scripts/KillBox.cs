using UnityEngine;

public class KillBox : MonoBehaviour
{

    // just checks whichplayer hits killbox and awards points accordingly
    private void OnTriggerEnter2D(Collider2D other)
    {
        var switcher = other.GetComponentInParent<CharacterSwitcher>();
        if (switcher == null)
            return;

        // Check the tag of the root player object instead of the prefab
        if (switcher.transform.root.CompareTag("Player1"))
        {
            ScoreManager.instance.AddPointToPlayer2();
            Debug.Log("calling for P2 point");
        }
        else if (switcher.transform.root.CompareTag("Player2"))
        {
            ScoreManager.instance.AddPointToPlayer1();
            Debug.Log("calling for P1 point");
        }

        switcher.Respawn();
    }
}
