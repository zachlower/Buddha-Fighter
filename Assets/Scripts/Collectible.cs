using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour {

	public GameObject victory;

    SpriteRenderer sprite;
    ScoreKeeper score;

    public bool isDoubleJump = false;

    public int level;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        score = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        if (isDoubleJump && score.state1 == ScoreKeeper.LevelState.Complete)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("overworld", LoadSceneMode.Single);
    }

    void OnTriggerEnter2D(Collider2D col) {
		victory.SetActive(true);
        GetComponent<AudioSource>().Play();
        score.canDoubleJump = true;
        if (level == 1)
        {
            score.state1 = ScoreKeeper.LevelState.Complete;
        }
        if (level == 2)
        {
            score.state2 = ScoreKeeper.LevelState.Complete;
        }
        sprite.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        

        StartCoroutine(LoadNextScene());
	}
}
