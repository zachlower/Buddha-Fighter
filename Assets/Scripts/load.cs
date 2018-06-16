using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load : MonoBehaviour {

    Fading fade;
    public string levelToLoad;
    public float waitTimeBeforeFade;

	void Start () {
        fade = FindObjectOfType<Fading>();
        StartCoroutine(LoadNextScene());
	}


    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(waitTimeBeforeFade);
        fade.BeginFade(1);
        yield return new WaitForSeconds(2f);
        if (levelToLoad == "quit")
        {
            Application.Quit();
        } else
        {
            SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
        }
    }
}
