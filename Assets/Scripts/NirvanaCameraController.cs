using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NirvanaCameraController : MonoBehaviour {

	public GameObject player;
    public GameObject background;
    public PlatformManager platformManager;

	float xCamera = 0;
	float zCamera = -10f;

	float offset = 1f;

	float playerY;
	float cameraMaxY;
	float cameraMinY;
	Vector3 startPos;

	bool intro = true;
	enum CamState {
		Waiting,
		Panning,
		Playing
	};
	CamState camState;

	Vector3 introCameraStart = new Vector3 (0, 46, -10);
	Vector3 introCameraEnd = new Vector3 (0, 0, -10);
	float currentY;
	public float cameraSpeed = 100f;

    ScoreKeeper score;

	void Start()
	{
        score = FindObjectOfType<ScoreKeeper>();
        if (score.state1 == ScoreKeeper.LevelState.Complete)
        {
            introCameraStart = new Vector3(0, 70, -10);
        }
        else
        {
            introCameraStart = new Vector3(0, 20, -10);
        }
        startPos = transform.position;
		cameraMaxY = startPos.y + offset;
		cameraMinY = startPos.y - offset;
		if (intro)
		{
			camState = CamState.Waiting;
			transform.position = introCameraStart;
			currentY = transform.position.y;
            StartCoroutine(Wait());
        }
		else
		{
			camState = CamState.Playing;
		}
	}

	void Update()
	{
		switch (camState)
		{
			case CamState.Waiting:
				break;
			case CamState.Playing:
				playerY = player.transform.position.y;
				if (playerY > cameraMaxY)
				{
					transform.position = new Vector3(xCamera, playerY - offset, zCamera);
					cameraMaxY = transform.position.y + offset;
					cameraMinY = transform.position.y - offset;
				}
				//if the player is below the min line and they're not at the start position
				if (playerY < cameraMinY && cameraMinY > startPos.y - offset)
				{
					transform.position = new Vector3(xCamera, playerY + offset, zCamera);
					cameraMaxY = transform.position.y + offset;
					cameraMinY = transform.position.y - offset;
				}
				break;
			case CamState.Panning:
				if (currentY > introCameraEnd.y)
				{
					transform.position = new Vector3(0, currentY - (cameraSpeed * Time.deltaTime), -10);
					currentY -= cameraSpeed * Time.deltaTime;
				}
				else
				{
                    platformManager.StartCoroutine(platformManager.FlyPlatforms(4.0f));
                    camState = CamState.Playing;
				}
				break;
		}

        //update background position according to camera position
        float backgroundY = transform.position.y - (6.0f * ((transform.position.y - introCameraEnd.y) / (introCameraStart.y - introCameraEnd.y) - 0.5f));
        background.transform.position = new Vector3(0, backgroundY, 10);
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(2f);
		camState = CamState.Panning;
	}

}
