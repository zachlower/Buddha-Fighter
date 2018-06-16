using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

	//fade out variables
	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.5f;
	int drawDepth = -1000;
	float alpha = 1f; //alpha of the texture, between 1 and 0
	int fadeDir = -1; //value that determines fade in or out

	void OnGUI()
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01(alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	public float BeginFade (int direction) {
		fadeDir = direction;
		return (fadeSpeed);
	}

	void OnLevelWasLoaded () {
		BeginFade(-1);
	}
}
