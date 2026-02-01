using UnityEngine;
using static UnityEngine.LightAnchor;

public class PlayerPhysics : MonoBehaviour
{
    [Header("Rotation")]
    float playerRotation = 0f;

    [SerializeField] float minAngle = 178f;
    [SerializeField] float maxAngle = 182f;
    [SerializeField] float rotation = 10f;

    [Header("Jumping")]
    [SerializeField] float jumpForce = -10f;
    public KeyCode jumpKey;
    [SerializeField] GameObject groundEmpty;
    public LayerMask Ground;
    public bool isGrounded;


    [Header("Enemy Player")]
    public CharacterSwitcher otherPlayerSwitcher;


    Rigidbody2D rb;



    // just fetches the rigidbody component
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    // checks everyframe for if the key is pressed and is it is grounded, it gets transform.up and stores it to use for calling the jump method
    private void Update()
    {
        if (!gameObject.activeInHierarchy) 
            return;


        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {

            Vector2 jumpDirection = transform.up; 

            Jump(jumpDirection);

        }
    }

    //called the method in late update so it only refreshes after character movement, preventing any sort of flikering or glitches
    private void LateUpdate()
    {
        FaceOtherPlayer();
    }

    // fetches the current position of itself and the other player and flips localscale if the x of itself is greater than the other player
    private void FaceOtherPlayer()
    {
        if (otherPlayerSwitcher == null || otherPlayerSwitcher.CurrentCharacter == null)
            return;

        Vector3 otherPos = otherPlayerSwitcher.CurrentCharacter.transform.position;
        Vector3 myPos = transform.position;

        if (myPos.x > otherPos.x && transform.localScale.x > 0)
        {
            // Face left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (myPos.x < otherPos.x && transform.localScale.x < 0)
        {
            // Face right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }


    //checks grounding with boxcast
    //checks rotation and applies rotation force either left of right depending on angle untill it reaches threshold.
    void FixedUpdate()
    {

        playerRotation = transform.eulerAngles.z;

        RaycastHit2D boxCast = Physics2D.BoxCast(groundEmpty.transform.position, new Vector2(1.5f, 0.5f), transform.eulerAngles.z, Vector2.down, 0.1f, Ground);

        isGrounded = (boxCast.collider != null && boxCast.collider.CompareTag("Ground"));



        //checking if the rotation is greater or lower than the threshold, if so it will rotate accordingly to self-correct

        if (isGrounded)
        {
            if (playerRotation > maxAngle)
            {
                rb.AddTorque(-rotation, ForceMode2D.Force);
            }

            if (playerRotation < minAngle)
            {
                rb.AddTorque(rotation, ForceMode2D.Force);
            }
        }

    }

    // doing the math for the angle of jump and then jumping
    void Jump(Vector2 jumpDirection)
    {
        rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);

    }



    // Generic ray to see transform.up

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)transform.up * 2f);
    }


}
