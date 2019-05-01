using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGraph : MonoBehaviour
{

    GameObject jumpCharacter;
    Rigidbody2D jumpRigidbody;

    #region Jump Info


    [SerializeField]
    private RaycastHit2D hit;
    public int jumpTimer = 0;
    public bool isAirborne;
    public bool isGrounded;
    public float gravityForce;
    public float upwardForce;
    public float timerLimit;
    //public float jumpArchForce;

    Vector2 jump;
    Vector2 gravity;
    Vector2 unGravity;

    #endregion

    public static AnimationCurve jumpCurve;

    // Start is called before the first frame update
    void Start()
    {
        jumpCharacter = this.gameObject;
        jumpRigidbody = jumpCharacter.GetComponent<Rigidbody2D>();
        jump = new Vector2(0, upwardForce);
        gravity = new Vector2(0, gravityForce);
        unGravity = new Vector2(0, 0);
        isAirborne = false;
        jumpRigidbody.velocity = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f);

        CheckAirborne(isAirborne);
        CheckGrounded();
        if (isAirborne)
        {
            isGrounded = false;
        }
        if (isGrounded)
        {
            jumpRigidbody.velocity = unGravity;
            jumpTimer = 0;
        }
        if (!isAirborne && jumpTimer < timerLimit)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumpRigidbody.velocity = jump;
                Debug.Log("Upward Force is: " + upwardForce.ToString());
            }
        }
        else if (!isGrounded && jumpTimer >= timerLimit)
        {
            jumpRigidbody.velocity = gravity;
            Debug.Log("Gravity is: " + gravityForce.ToString());
        }
        if (isAirborne)
        {
            jumpTimer++;
        }
        //jumpArchForce = playerRigidbody.velocity.y;
    }

    /*void Jump()
    {
        playerRigidbody.velocity = jump;
        jumpTimer++;
    }*/

    bool CheckAirborne(bool airborneBool)
    {
        if (jumpRigidbody.velocity.y != 0)
        {
            isAirborne = true;
        }
        else
        {
            isAirborne = false;
        }
        return isAirborne;
    }

    void CheckGrounded()
    {
        //Debug.Log("Touching ground");
        if (hit.transform.position.y < jumpRigidbody.transform.position.y)
        {
            //Debug.Log("Player is grounded");
            isGrounded = true;
        }
    }
}
