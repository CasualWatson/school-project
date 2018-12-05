using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float xSpeed;
    private float ySpeed;


    private bool touchFloor;
    public int floorHash;
    Task jumpTask;

	// Use this for initialization
	void Start ()
    {
        xSpeed = 8f;
        ySpeed = 10f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float movementValueX = Input.GetAxis("Horizontal") * xSpeed;
        float movementValueY = 0;

        // Makes it by seconds instead of by frames
        movementValueX *= Time.deltaTime;

        if (!touchFloor)
            movementValueY += ySpeed * Time.deltaTime;

        transform.Translate(movementValueX, movementValueY, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);

        if (!touchFloor)
            ySpeed += Physics2D.gravity.y * Time.deltaTime;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetHashCode() == floorHash)
            touchFloor = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetHashCode() == floorHash)
        {
            touchFloor = true;
            ySpeed = 10f;
            if (Input.GetAxis("Vertical") > 0)
            {
                // Jump Start Here
                touchFloor = false;
            }
        }
    }
}
