using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameDirector : MonoBehaviour
{
    public AudioClip SELECT;
    public AudioClip DAMAGE;
    AudioSource aud;

    public static int GameOver = 0;
    public GameObject OverUI;
    public GameObject HPUI1;
    public GameObject HPUI2;
    public GameObject HPUI3;
    GameObject TimerUI;
    GameObject OniUI;
    GameObject onistop;
    public Button ReStart;
    public Button Title;

    float Timer = 60.0f;
    int OniCount = 1;
    int Damage = 0;
    bool Over;

    // UIを表示する
    public void ShowUI()
    {
        OverUI.SetActive(true);
    }

    // UIを非表示にする
    public void HideUI()
    {
        OverUI.SetActive(false);
    }

    // UIの表示・非表示を切り替える
    public void ToggleUI()
    {
        OverUI.SetActive(!OverUI.activeSelf);
        HPUI1.SetActive(!HPUI1.activeSelf);
        HPUI2.SetActive(!HPUI2.activeSelf);
        HPUI3.SetActive(!HPUI3.activeSelf);
    }

    public void OniPlus()
    {
        OniCount++;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        this.onistop = GameObject.Find("Oni");
        OverUI.SetActive(false);
        HPUI1.SetActive(true);
        HPUI2.SetActive(true);
        HPUI3.SetActive(true);
        Over = false;
        this.TimerUI = GameObject.Find("Timer");
        this.OniUI = GameObject.Find("OniCount");
        GameOver = 0;
        ReStart.onClick.AddListener(ReStartGame);
        Title.onClick.AddListener(TitleBack);
    }

    void ReStartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }
    IEnumerator StartGameCoroutine()
    {
        aud.PlayOneShot(SELECT);

        yield return new WaitForSeconds(SELECT.length);

        SceneManager.LoadScene("GameScene1");
    }

    void TitleBack()
    {
        StartCoroutine(TitleGameCoroutine());
    }
    IEnumerator TitleGameCoroutine()
    {
        aud.PlayOneShot(SELECT);

        yield return new WaitForSeconds(SELECT.length);

        SceneManager.LoadScene("Title");
    }


    public void CatchPlayer()
    {
        if (Damage <= 2)
        {
            aud.PlayOneShot(DAMAGE);
        }
        this.onistop.GetComponent<OniController>().OniStop();
        Damage++;
        Debug.Log(Damage + "ディレクタ");
        if (Damage >= 3)
        {
            Over = true;

        }
    }
    // Update is called once per frame
    void Update()
    {
        this.TimerUI.GetComponent<TextMeshProUGUI>().text = this.Timer.ToString("F1");
        this.OniUI.GetComponent<TextMeshProUGUI>().text = this.OniCount.ToString("F0");

        if (Damage >= 1)
        {
            HPUI3.SetActive(false);
        }
        if (Damage >= 2)
        {
            HPUI2.SetActive(false);
        }
        if (Damage >= 3)
        {
            HPUI1.SetActive(false);
        }


        if (Over == true)
        {
            OverUI.SetActive(true);
        }
        if (Over == false)
        {
            this.Timer -= Time.deltaTime;
        }

        if(Timer <= 0)
        {
            SceneManager.LoadScene("GameClear");
        }
    }
}
