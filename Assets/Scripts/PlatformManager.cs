using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour {

	ScoreKeeper score;

    public Transform playerTrans;
    Vector3 playerStartPos;

    //level 1 variables
	public GameObject[] platforms1;
	public GameObject[] clouds1;
	public int positives1;

    //level 2 variables
    public GameObject[] platforms2;
    public GameObject[] clouds2;
    public int positives2;

    void Start()
	{
		score = FindObjectOfType<ScoreKeeper>();
        if (score == null)
        {
            score = new ScoreKeeper();
        }
		positives1 = score.levelOneHighScore;
		for (int i = 0; i < positives1; i++)
		{
			
			//platforms1[i].SetActive(true);
            platforms1[i].GetComponent<BoxCollider2D>().enabled = false;
            TextMesh[] platformText = platforms1[i].GetComponentsInChildren<TextMesh>();  
			for (int j = 0; j < platformText.Length; j++)
			{
				platformText[j].text = score.levelOnePositiveList[i];
			}

			//WaitForNextPlatform();
		}
        positives2 = score.levelTwoHighScore;
        for (int i = 0; i < positives2; i++)
        {
            
            //platforms2[i].SetActive(true);
            BoxCollider2D collider = platforms2[i].GetComponent<BoxCollider2D>();
            if(collider == null) //part of group
            {
                BoxCollider2D[] colliders = platforms2[i].GetComponentsInChildren<BoxCollider2D>();
                for (int j = 0; j < colliders.Length; j++)
                    colliders[j].enabled = false;
            }
            else
            {
                collider.enabled = false;
            }
            
            TextMesh[] platformText = platforms2[i].GetComponentsInChildren<TextMesh>();
            for (int j = 0; j < platformText.Length; j++)
            {
                platformText[j].text = score.levelTwoPositiveList[i];
            }

            //WaitForNextPlatform();
        }

        playerStartPos = playerTrans.position;
        //StartCoroutine(FlyPlatforms(5.0f));
    }

	void Update() {
		/*if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("overworld", LoadSceneMode.Single);
        }*/

		//for testing
		if (Input.GetKeyDown(KeyCode.P) && positives1 < platforms1.Length)
		{
			positives1++;
			clouds1[positives1 - 1].SetActive(false);
			platforms1[positives1 - 1].SetActive(true);
		}
	}

	IEnumerator WaitForNextPlatform() {
		yield return new WaitForSeconds(0.5f);
	}

    public IEnumerator FlyPlatforms(float duration)
    {
        //TODO: moving platform not quite right!!

        float currentTime = 0.0f;
        Vector3[] endPos1 = new Vector3[positives1];
        Vector3[] endPos2 = new Vector3[positives2];

        for (int i = 0; i < positives1; i++)
        {
            platforms1[i].SetActive(true);
            endPos1[i] = platforms1[i].transform.position;
        }
        for (int i = 0; i < positives2; i++)
        {
            platforms2[i].SetActive(true);
            endPos2[i] = platforms2[i].transform.position;
        }

        while(currentTime <= duration)
        {
            for(int i=0; i<positives1; i++)
                platforms1[i].transform.position = Vector3.Lerp(playerStartPos, endPos1[i], currentTime / duration);
            for (int i = 0; i < positives2; i++)
                platforms2[i].transform.position = Vector3.Lerp(playerStartPos, endPos2[i], currentTime / duration);

            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        }

        for (int i = 0; i < positives1; i++)
        {
            clouds1[i].SetActive(false);
            platforms1[i].GetComponent<BoxCollider2D>().enabled = true;
            Debug.Log("enabling platform collider: " + platforms1[i].name);
            PC2D.SimpleUpDown moving = platforms1[i].GetComponent<PC2D.SimpleUpDown>();
            if (moving != null)
                moving.enabled = true;
        }
        for (int i = 0; i < positives2; i++)
        {
            clouds2[i].SetActive(false);
            BoxCollider2D collider = platforms2[i].GetComponent<BoxCollider2D>();
            if (collider == null) //part of group
            {
                BoxCollider2D[] colliders = platforms2[i].GetComponentsInChildren<BoxCollider2D>();
                for (int j = 0; j < colliders.Length; j++)
                    colliders[j].enabled = true;
            }
            else
            {
                collider.enabled = true;
            }
        }
    }
}
