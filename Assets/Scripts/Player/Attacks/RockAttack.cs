using System.Collections;
using UnityEngine;

public class RockAttack : PlayerAttack
{
    [SerializeField] GameObject rockProjectilePrefab;   
    [SerializeField] float launchForce = 30f;
    [SerializeField] float throwCooldown = 0.5f;
    [SerializeField] float deleteTimer = 2f;

    private bool isThrowing;


    // sets throwing to false
    void Start()
    {
        isThrowing = false;
    }


    //checks if it is allowed to throw, then starts corutine if it can
    public override void Attack()
    {
        if (!isThrowing)
        {
            StartCoroutine(ThrowRock());
        }

    }

    // sets throwing to true, preventing spam, then spawns a rock prefab, gets its rigidbody, and applies a force onto it
    //sets can throw to true after cooldown is over, also deleates the projectile prefab after a certain amount of time to prevent performance issues
    private IEnumerator ThrowRock()
    {
        isThrowing = true;

        GameObject projectile = Instantiate(rockProjectilePrefab, weaponEmpty.transform.position, weaponEmpty.transform.rotation);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        Vector2 direction = transform.right * transform.localScale.x;
        rb.AddForce(-direction * launchForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(throwCooldown);
        isThrowing = false;
        
        yield return new WaitForSeconds(deleteTimer);
        Destroy(projectile);
    }

}
