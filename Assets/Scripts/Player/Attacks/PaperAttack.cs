using UnityEngine;

public class PaperAttack : PlayerAttack
{
    public Animator animator;

    [Header("KnockBackAttack")]
    [SerializeField] float attackForce = 10f;



    // creates hitbox,checks everthing in it, and plays animation
    // if its a player, it deals knockback damange
    // if it is a projectile, it will parry it and add an extra force onto it.

    public override void Attack()
    {
        Vector2 boxCenter = weaponEmpty.transform.position;
        Vector2 boxSize = new Vector2(2f, 2f);
        float angle = transform.rotation.eulerAngles.z;

        DrawBox(boxCenter, boxSize, angle, Color.red);

        animator.SetTrigger("Attack");

        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, angle);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject) continue; // ignore self

            int hitLayer = hit.gameObject.layer;

            if (hitLayer == LayerMask.NameToLayer("Projectile"))
            {
                Rigidbody2D rb = hit.attachedRigidbody;
                if (rb != null)
                {
                    rb.linearVelocity = -rb.linearVelocity;
                    rb.AddForce(transform.right * transform.localScale.x * 3f, ForceMode2D.Impulse);
                }
            }
            else if (hitLayer == LayerMask.NameToLayer("Player"))
            {
                Rigidbody2D rb = hit.attachedRigidbody;
                if (rb != null)
                {
                    Vector2 direction = hit.transform.position - transform.position;
                    rb.AddForce(direction.normalized * attackForce, ForceMode2D.Impulse);
                }
            }
        }
    }



    // Draw the hitbox for debugging

    void DrawBox(Vector2 center, Vector2 size, float angle, Color color)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector2 halfSize = size * 0.5f;

        Vector2 topLeft = center + (Vector2)(rotation * new Vector3(-halfSize.x, halfSize.y));
        Vector2 topRight = center + (Vector2)(rotation * new Vector3(halfSize.x, halfSize.y));
        Vector2 bottomRight = center + (Vector2)(rotation * new Vector3(halfSize.x, -halfSize.y));
        Vector2 bottomLeft = center + (Vector2)(rotation * new Vector3(-halfSize.x, -halfSize.y));

        Debug.DrawLine(topLeft, topRight, color, 0.02f);
        Debug.DrawLine(topRight, bottomRight, color, 0.02f);
        Debug.DrawLine(bottomRight, bottomLeft, color, 0.02f);
        Debug.DrawLine(bottomLeft, topLeft, color, 0.02f);
    }





}
