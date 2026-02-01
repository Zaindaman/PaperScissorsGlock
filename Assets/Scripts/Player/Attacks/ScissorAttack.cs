using System;
using System.Collections;
using UnityEngine;

public class ScissorAttack : PlayerAttack
{

    [Header("DashAttack")]
    private bool canDash;
    private bool isDashing;
    [SerializeField] float dashPower = 10f;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashCooldown = 1f;


    Rigidbody2D rb;




    // fetches component and allows dashing to happen
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
    }

    //overide checks if can dash is true then starts the dash corutine
    public override void Attack()
    {
        if (canDash)
        {
            Debug.Log("dashing");
            StartCoroutine(Dash());
        }
    }


    //tempoary sets gravityscale to 0 and applies a velocity onto the player
    //also has a cooldown to prevent spamming, which sends to false in the meantime untill the dash cooldown is over
    private IEnumerator Dash()
    {
        canDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
