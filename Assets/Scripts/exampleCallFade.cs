using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exampleCallFade : MonoBehaviour {

    Fading fadeScript;

	// Use this for initialization
	void Start () {
        fadeScript = FindObjectOfType<Fading>();
        if (fadeScript) {
            fadeScript.BeginFade(-1);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
