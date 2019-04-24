using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private PlayerMovement player;
    private Timer stuckTimer = new Timer(4);
    public bool stuck = false;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player.wallHash = gameObject.GetHashCode();
        Debug.Log(gameObject.GetHashCode());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (stuck)
            stuckTimer.UpdateTimer();
        else
            stuckTimer.ResetTimer();

        if (stuckTimer.completion)
            player.death = true;
	}
}
