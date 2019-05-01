using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] int jumpTime;
    [SerializeField] bool isAirborne;
    [SerializeField] float jumpDistance;

    GameObject playerCharacter;
    Rigidbody2D playerRB;
    RaycastHit2D groundCast;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = gameObject;
        playerRB = GetComponent<Rigidbody2D>();
        jumpTime = 0;
        isAirborne = false;
    }

    // Update is called once per frame
    void Update()
    {
        groundCast = Physics2D.Raycast(transform.position - new Vector3(0, 0.51f, 0), Vector2.down, 0.0001f);
        //jumpDistance = Vector2.Distance(groundCast.transform.position, this.transform.position);
        moveHorizontal();
        Jump();
        Debug.Log("Distance is: " + jumpDistance);
        Debug.Log("Groundcast: " + groundCast.transform.tag);
    }

    void moveHorizontal()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.D))
            {
                playerRB.velocity = Vector2.right;
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerRB.velocity = Vector2.left;
            }
        }
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTime <= 30)
            {
                playerRB.velocity = Vector2.up;
                jumpTime++;
            }
        }
        if (groundCast.transform.tag == "Platform")
        {
            isAirborne = false;
            jumpTime = 0;
        }
        if (groundCast.transform.tag == null)
        {
            isAirborne = true;
        }
        /*if (groundCast.distance < 1.0f) //jumpDistance < 1.0f, groundCast.distance < 1.0f
        {
            Debug.Log("Groundcast Hit!");
            isAirborne = false;
            jumpTime = 0;
        }
        else
        {
            isAirborne = true;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Victory")
        {
            Debug.Log("You Win!");
        }
    }
}
