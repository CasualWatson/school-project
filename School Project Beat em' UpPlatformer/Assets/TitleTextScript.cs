using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ColorChange()
    {
        gameObject.GetComponent<Image>().color = Color.red;
    }
    public void ResetColor()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }
}
