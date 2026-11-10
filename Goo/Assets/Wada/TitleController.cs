using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button StartGame;
    public Button Title;
    public AudioClip SELECT;
    public AudioClip SUCCESS;
    AudioSource aud;

    bool Played = false;
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        StartGame.onClick.AddListener(StartGameScene);
        Title.onClick.AddListener(TitleGameScene);
    }

    void StartGameScene()
    {
        StartCoroutine(StartGameCoroutine());
    }

    void TitleGameScene()
    {
        StartCoroutine(TitleGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        aud.PlayOneShot(SELECT);

        yield return new WaitForSeconds(SELECT.length);

        SceneManager.LoadScene("GameScene1");
    }

    IEnumerator TitleGameCoroutine()
    {
        aud.PlayOneShot(SELECT);

        yield return new WaitForSeconds(SELECT.length);

        SceneManager.LoadScene("Title");
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Title")
        {
            if (Played == false)
            {
                aud.PlayOneShot(SUCCESS);
                Played = true;
            }
        }
    }
}