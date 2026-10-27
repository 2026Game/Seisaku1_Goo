using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
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

    float Timer = 100;
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
        SceneManager.LoadScene("Wada Scene");
    }
    void TitleBack()
    {

    }

    public void CatchPlayer()
    {
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
        if (Timer <= 0)
        {
            Over = true;
            GameOver = 1;
        }

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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
        if (Over == false)
        {
            this.Timer -= Time.deltaTime;
        }
    }
}
