using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("CompleteLevel", 0.1f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(0);
    }
}
