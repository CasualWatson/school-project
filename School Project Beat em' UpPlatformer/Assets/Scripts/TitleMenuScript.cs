using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuScript : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject textPrefab;
    private List<GameObject> textList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        var spacing = 25;
		// create three of the text objects and align them properly
        for (int i = 0; i < 3; i++)
        {
            var newText = Instantiate(textPrefab);
            newText.transform.position = new Vector2(mainCamera.scaledPixelWidth / 2, mainCamera.scaledPixelHeight + (spacing * i));
            newText.GetComponent<Text>().text = "This is Object #: " + i;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
