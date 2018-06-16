using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HintToExit : MonoBehaviour {

	public GameObject keyHint;


	void OnTriggerEnter2D(Collider2D col)
	{
		keyHint.SetActive(true);
	}

	void OnTriggerExit2D(Collider2D col)
	{
		keyHint.SetActive(false);
	}
}
