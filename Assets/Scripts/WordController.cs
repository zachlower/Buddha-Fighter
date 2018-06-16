using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordController : MonoBehaviour {

    public float speed = 1.0f;
    public bool good;


    GameObject center;
    Vector3 direction;
    int health = 100;
    ParticleSystem particles;
    WordManager manager;


    void Start()
    {
        center = GameObject.Find("Center");

        direction = center.transform.position + new Vector3(0,0.5f,0) - transform.position;
        direction.Normalize();

        speed = Random.Range(0.7f, 1.2f);

        particles = GetComponent<ParticleSystem>();
        manager = FindObjectOfType<WordManager>();
    }
    public void InitWord(string word, bool good)
    {
        Text wordText = GetComponentInChildren<Text>();
        this.good = good;
        if (good)
        {
            wordText.color = new Color(1.0f, 1.0f, 0.4f);
        }else
        {
            wordText.color = new Color(1.0f, 0.2f, 0.3f);
        }
        wordText.text = word;
    }

    void Update()
    {
        transform.position = transform.position + direction * speed * Time.deltaTime;
        
    }

    public void Damage(int damage)
    {
        manager.PlaySound(!good);

        particles.Play();
        StartCoroutine(KnockBack(-10.0f));
    }

    IEnumerator KnockBack(float impactSpeed)
    {
        float prevSpeed = speed;
        speed = impactSpeed;
        float acceleration = 4.5f;
        while(speed < prevSpeed)
        {
            speed += acceleration * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
