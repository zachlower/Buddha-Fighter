using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyScoreKeeperCheck : MonoBehaviour {

	ScoreKeeper score;
    public GameObject level1wall;
    public GameObject level2wall;

	void Start()
	{
		score = FindObjectOfType <ScoreKeeper>();

        //for this build, let's just always clear the list after you play
        //score.ClearList(1);
        if (score.state1 == ScoreKeeper.LevelState.Complete)
        {
            level1wall.SetActive(false);
        }
        if (score.state2 == ScoreKeeper.LevelState.Complete)
        {
            level2wall.SetActive(false);
        }

        //reset player position in overworld
        if (score.overworldPosition != Vector3.zero)
        {
            GameObject.Find("Player").transform.position = score.overworldPosition;
        }


	}

}
