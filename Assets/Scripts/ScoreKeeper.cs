using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public bool canDoubleJump = false;

	public bool newLevel = false;
	public bool emptyList = true;
    public Vector3 overworldPosition = Vector3.zero;

    public enum LevelState
    {
        Incomplete, 
        Meditated, 
        Complete
    };

    public int currentScore = 0;

    //level one variables
    public List<string> levelOnePositiveList = new List<string>();
    public LevelState state1 = LevelState.Incomplete;
	public int levelOneHighScore = 0;
    public int levelOneThreshold = 8;

    //level two variables
    public List<string> levelTwoPositiveList = new List<string>();
    public LevelState state2 = LevelState.Incomplete;
    public int levelTwoHighScore = 0;
    public int levelTwoThreshold = 8;

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject quitButton = gameObject.transform.Find("Pause Canvas").gameObject;
            quitButton.SetActive(!quitButton.activeInHierarchy);
        }
    }

    public void StartMeditation()
    {
        currentScore = 0;
    }

	public void AddPositiveToList(int level, Text textComponent) {
        currentScore++;

        switch (level)
        {
            case 1:
                if (currentScore > levelOneHighScore)
                    levelOnePositiveList.Add(textComponent.text);
                break;
            case 2:
                if(currentScore > levelTwoHighScore)
                    levelTwoPositiveList.Add(textComponent.text);
                break;
        }
	}

	public void SetHighScore(int level)
	{
        switch (level)
        {
            case 1:
                if(levelOnePositiveList.Count > levelOneHighScore) //new high score
                {
                    levelOneHighScore = levelOnePositiveList.Count;
                }
                break;
            case 2:
                if (levelTwoPositiveList.Count > levelTwoHighScore)
                {
                    levelTwoHighScore = levelTwoPositiveList.Count;
                };
                break;
        }
	}

    public bool CheckIfLevelWon (int level)
    {
        if (level == 1)
        {
            return state1 == LevelState.Complete;
        }
        if (level == 2)
        {
            return state2 == LevelState.Complete;
        } else
        {
            return false;
        }
    }

    internal bool CheckIfLevelWon()
    {
        throw new NotImplementedException();
    }
}
