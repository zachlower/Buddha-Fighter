using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBox : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("overworld", LoadSceneMode.Single);
    }
}
