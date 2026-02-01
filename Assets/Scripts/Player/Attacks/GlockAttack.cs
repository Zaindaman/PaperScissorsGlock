using UnityEngine;
using System.Collections;

public class GlockAttack : PlayerAttack
{
    [Header("Gun Settings")]
    [SerializeField] float range = 15f;
    [SerializeField] float knockbackForce = 10f;
    [SerializeField] float lineDuration = 0.05f;
    [SerializeField] float fireRate = 0.5f;

    [Header("References")]
    [SerializeField] Transform gunTip;
    [SerializeField] LineRenderer lineRenderer;


    // nullcheck for line renderer and guntip
    //starts attack corutine
    public override void Attack()
    {
        if (gunTip == null || lineRenderer == null) return;

        StartCoroutine(ShootGlock());
    }

    //draws a long hitbox and applies force to players on it
    //also starts a corutine for the "bullet"
    private IEnumerator ShootGlock()
    {
        // Shoot in the direction the Glock is facing
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(gunTip.position, direction, range);

        StartCoroutine(ShowBulletLine(gunTip.position, hit ? (Vector2)hit.point : (Vector2)gunTip.position + direction * range));


        if (hit.collider != null)
        {
            Rigidbody2D rb = hit.collider.attachedRigidbody;

            if (rb != null)
            {
                int hitLayer = hit.collider.gameObject.layer;

                if (hitLayer == LayerMask.NameToLayer("Player"))
                {
                    Vector2 knockDir = (hit.collider.transform.position - transform.position).normalized;
                    rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }

        yield return new WaitForSeconds(fireRate);
    }


    // just draws the line renderer from the tip to the end point depending if the boxcast hit something
    private IEnumerator ShowBulletLine(Vector2 start, Vector2 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.enabled = false;
    }
}
