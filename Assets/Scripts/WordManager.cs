using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WordManager : MonoBehaviour {

    public GameObject wordPrefab;
    public GameObject halo;
    public float topSpawn = 12.0f;
    public float leftSpawn = -15.0f;
    public float rightSpawn = 15.0f;
    public float bottomSpawn = -4.0f;
    public float spawnInterval = 3.0f;
    public float minSpawnInterval = 2.0f;
    public int wordSpawnMin = 0;
    public int wordSpawnMax = 3;

    public AudioClip[] positiveClips;
    public AudioClip[] negativeClips;
    public AudioClip endGoodClip;
    public AudioClip endBadClip;
    private AudioSource audio;
    private int posClipIndex = 0;
    private int negClipIndex = 0;

    bool playing = true;
    bool victory = false;
    List<string> goodList;
    List<string> badList;
    //List<string> positiveList = new List<string>();
    public int level = 1; // use this int when we use this script on multiple levels so that the ScoreKeeper and Platform Manangers know which lists to affects
    public int loseState = 3; //the number of negative words that will cause you to lose the level
    int negatives = 0;
    float idealRatio = 0.4f;
    float spawnRatio;
   	ScoreKeeper stored;
    CircleCollider2D collider;
    ParticleSystem ps;

    int goodSpawned = 0;
    int badSpawned = 0;

    Fading fade;

    MetricManager metrics;


    void Awake()
    {
        InitLibrary();

    }

    void Start()
    {
        spawnRatio = idealRatio;

        StartCoroutine(SpawnWords());
        collider = GetComponent<CircleCollider2D>();
        audio = GetComponent<AudioSource>();
        stored = FindObjectOfType<ScoreKeeper>();
        stored.StartMeditation();
        fade = FindObjectOfType<Fading>();
        ps = GetComponent<ParticleSystem>();

        metrics = GameObject.Find("ScoreKeeper").GetComponent<MetricManager>();
        metrics.AddToAttempts(1);
    }

    IEnumerator SpawnWords()
    {
        while (playing)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnWord();
            if (spawnInterval > minSpawnInterval)
            {
                spawnInterval -= 0.02f;
            }
            else
            {
                spawnInterval = minSpawnInterval;
            }
        }
    }
    void SpawnWord()
    {

        int spawnIndex = (int)Random.Range(wordSpawnMin, wordSpawnMax);
        float spawnX = 0;
        float spawnY = 0;
        switch (spawnIndex)
        {
            case 2: //spawn top
                spawnX = Random.Range(leftSpawn, rightSpawn);
                spawnY = topSpawn;
                break;
            case 1: //spawn left
                spawnX = leftSpawn;
                spawnY = Random.Range(bottomSpawn, topSpawn);
                break;
            case 0: //spawn right
                spawnX = rightSpawn;
                spawnY = Random.Range(bottomSpawn, topSpawn);
                break;
        }
        GameObject word = Instantiate(wordPrefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);


        //dynamically balance word spawning

        float r = Random.value;
        int libIndex = 0;
        string wordString = "";
        if(r  < spawnRatio) //spawn good word
        {
            libIndex = (int)Random.Range(0, goodList.Count);
            wordString = goodList[libIndex];
            word.GetComponent<WordController>().InitWord(wordString, true);
            goodSpawned++;

            //adjust ratio
            spawnRatio -= 0.1f;
        }
        else //spawn bad word
        {
            libIndex = (int)Random.Range(0, badList.Count);
            wordString = badList[libIndex];
            word.GetComponent<WordController>().InitWord(wordString, false);
            badSpawned++;

            //adjust ratio
            spawnRatio += 0.1f;
        }
        spawnRatio = Mathf.Clamp(spawnRatio, 0, 1);
    }

    public void PlaySound(bool positive)
    {
        if (playing)
        {
            AudioClip clip;

            if (positive)
            {
                clip = positiveClips[posClipIndex];
                posClipIndex++;
                posClipIndex = posClipIndex % positiveClips.Length;
            }
            else
            {
                clip = negativeClips[negClipIndex];
                negClipIndex++;
                negClipIndex = negClipIndex % negativeClips.Length;

            }

            audio.clip = clip;
            audio.Play();
        }
    }

    void InitLibrary()
    {
        goodList = new List<string>();
        goodList.Add("tranquility");
        goodList.Add("peace");
        goodList.Add("solitude");
        goodList.Add("balance");
        goodList.Add("mindfulness");
        goodList.Add("focus");
        goodList.Add("purity");
        goodList.Add("emptiness");
        goodList.Add("truth");
        goodList.Add("nature");
        goodList.Add("honesty");
        goodList.Add("calm");
        goodList.Add("meditation");
        goodList.Add("air");
        goodList.Add("river");


        badList = new List<string>();
        badList.Add("lunch");
        badList.Add("pain");
        badList.Add("fear");
        badList.Add("politics");
        badList.Add("reddit");
        badList.Add("boobs");
        badList.Add("tacos");
        badList.Add("cats");
        badList.Add("money");
        badList.Add("memes");
        badList.Add("failure");
        badList.Add("debt");
        badList.Add("cars");
        badList.Add("salary");
        badList.Add("interviews");
        badList.Add("depression");
        badList.Add("hatred");
        badList.Add("wifi");
        badList.Add("lawyers");
        badList.Add("nickelback");
        badList.Add("instagram");
        badList.Add("anxiety");
        badList.Add("diamonds");
    }

    /* impact with word */
    public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Word"))
		{
			if (other.gameObject.GetComponent<WordController>().good)
			{
                PlaySound(true);
                //metrics.AddGoodWords(1);
                
				//adds the positives to the list
				stored.AddPositiveToList(level, other.gameObject.GetComponentInChildren<Text>());
                //expand halo & hit box
                halo.transform.localScale = new Vector3(halo.transform.localScale.x + 0.5f, halo.transform.localScale.y + 0.5f, 1);
                collider.radius += 0.1f;

				//using the number of positives to determine win state
				if ((level == 1 && stored.levelOnePositiveList.Count >= stored.levelOneThreshold)
                    || (level == 2 && stored.levelTwoPositiveList.Count >= stored.levelTwoThreshold))
				{
					playing = false;
                    audio.clip = endGoodClip;
                    audio.Play();
                    victory = true;
				}

			}
			else
			{
                PlaySound(false);
                ps.Play();
				//increase the negative word count
				negatives++;

				//using the negative counter to determine lose state
				if (negatives >= loseState)
				{
					playing = false;
                    audio.clip = endBadClip;
                    audio.Play();
				}

			}

			Destroy(other.gameObject);

            if (!playing)
            {
            	stored.SetHighScore(level);
                StartCoroutine(SwitchScene());
                
            }
            //this is how it was before
            /*else
            {
                Destroy(other.gameObject);
            }*/
        }
    }

	private IEnumerator SwitchScene()
	{
		GameObject[] words = GameObject.FindGameObjectsWithTag("Word");
		foreach (GameObject word in words)
		{
			word.GetComponent<WordController>().speed = -10.0f;
			//Destroy(word);
		}
		float timeElapsed = 0.0f;
		while (timeElapsed < 5.0f)
		{
			if (victory)
			{
				halo.transform.localScale = new Vector3(halo.transform.localScale.x + 8 * Time.deltaTime, halo.transform.localScale.y + 8 * Time.deltaTime, 1);
			}
            timeElapsed += Time.deltaTime;
            if (timeElapsed > 3.0f && 3.3f > timeElapsed)
            {
                fade.BeginFade(1);
            }
            yield return new WaitForSeconds(0);
        }
        //yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Nirvana", LoadSceneMode.Single);
    }
}
