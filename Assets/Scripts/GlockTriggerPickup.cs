using UnityEngine;

public class GlockTriggerPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Vector2 detectionSize = new Vector2(1.5f, 1.5f); // size of pickup detection
    [SerializeField] LayerMask playerLayer;

    // uses boxoverlap to check what is in the boxcast, then it checks each thing in the hitbox for CharacterSwitcher script
    // first item with that component gets glock ability
    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, detectionSize, 0f, playerLayer);

        foreach (var hit in hits)
        {
            CharacterSwitcher switcher = hit.GetComponentInParent<CharacterSwitcher>();
            if (switcher != null)
            {
                switcher.SpawnCharacter(CharacterSwitcher.CharacterSelected.Glock);

                Destroy(gameObject);

                break;
            }
        }
    }

    // visulising the detection box for debug reasons
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, detectionSize);
    }
}
