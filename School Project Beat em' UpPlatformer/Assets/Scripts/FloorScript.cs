using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player.floorHash = gameObject.GetHashCode();
        Debug.Log(gameObject.GetHashCode());
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
}
