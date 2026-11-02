using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button StartGame;

    void Start()
    {
        StartGame.onClick.AddListener(StartGameScene);
    }

    void StartGameScene()
    {
        SceneManager.LoadScene("GameScene1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
