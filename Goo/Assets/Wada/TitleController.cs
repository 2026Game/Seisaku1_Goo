using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button StartGame;
    public Button Title;
    void Start()
    {
        StartGame.onClick.AddListener(StartGameScene);
        Title.onClick.AddListener(TitleGameScene);
    }

    void StartGameScene()
    {
        SceneManager.LoadScene("GameScene1");
    }

    void TitleGameScene()
    {
        SceneManager.LoadScene("Title");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
