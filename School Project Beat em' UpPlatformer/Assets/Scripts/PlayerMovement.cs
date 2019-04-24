using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float originalTime;
    public float currentTime;
    public bool completion = false;

    public Timer (float time)
    {
        originalTime = time;
        currentTime = time;
    }

    public bool UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
            completion = true;
        else
            completion = false;
        return completion;
    }

    public void ResetTimer()
    {
        currentTime = originalTime;
        completion = false;
    }
}

public class PlayerMovement : MonoBehaviour
{
    private float xSpeed;
    private float ySpeed;
    public Vector2 respawnPosition;
    public GameObject startLocationObj;

    private bool touchFloor;
    public bool death;
    public int floorHash;
    public int hazardHash;
    public int doorHash;
    public int wallHash;
    private Timer dashTimer;
    private int dashWait = 3;
    private float dashMulti = 1;

    // Use this for initialization
    private void OnGUI()
    {
        int roundTime = (int) Math.Ceiling(dashTimer.currentTime);
        if (roundTime < 0)
            roundTime = 0;
        var point = Camera.main.WorldToScreenPoint(transform.position);
        var rect = new Rect(0, 0, 300, 100);
        rect.x = point.x;
        rect.y = Screen.height - point.y;
        GUI.Label(rect, roundTime.ToString());
    }

    void Start ()
    {
        xSpeed = 8f;
        ySpeed = 10f;
        if (startLocationObj == null)
            respawnPosition = transform.position;
        else
        {
            respawnPosition = startLocationObj.transform.position;
            transform.position = respawnPosition;
        }
        dashTimer = new Timer(dashWait);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (death)
        {
            transform.position = respawnPosition;
            death = false;
        }
        float movementValueX = Input.GetAxis("Horizontal") * xSpeed;
        float movementValueY = 0;

        if (Input.GetAxis("Dash") > 0 && dashTimer.completion)
        {
            dashMulti = 2;
            dashTimer.ResetTimer();
        }

        // Makes it by seconds instead of by frames
        movementValueX *= Time.deltaTime * dashMulti;

        if (dashMulti > 1)
            dashMulti -= Time.deltaTime;

        if (!touchFloor)
            movementValueY += ySpeed * Time.deltaTime;

        transform.Translate(movementValueX, movementValueY, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);

        if (!touchFloor)
            ySpeed += Physics2D.gravity.y * Time.deltaTime;

        dashTimer.UpdateTimer();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.GetHashCode() == floorHash)
            touchFloor = true;
        if (col.GetHashCode() == hazardHash)
            death = true;
        if (col.GetHashCode() == wallHash)
            col.GetComponent<WallScript>().stuck = true;
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.GetHashCode() == wallHash)
            col.GetComponent<WallScript>().stuck = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetHashCode() == doorHash)
        {
            // Load Scene
            Debug.Log("Next Level!");
            int next = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;

            if (UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings < next + 1)
                UnityEngine.SceneManagement.SceneManager.LoadScene("title");
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene(next);
        }
    }
}
