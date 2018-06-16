using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    public float scrollSpeed;

	void Update () {
        transform.Translate(0, scrollSpeed/50, 0);
	}
}
