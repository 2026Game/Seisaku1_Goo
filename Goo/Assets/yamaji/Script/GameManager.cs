using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManagerをほかのスクリプトから使えるようにする
    public static GameManager Instance;

    [Header("ライフ設定")]
    public int initialLives = 3;

    [Header("鬼の停止時間（秒）")]
    public float freezeSeconds = 3.0f;

    private int currentLives;

    //isProcessingCaptureは捕獲処理を実行中かどうかを記録する変数。鬼がプレイヤーに触れ続けても、ライフが連続して減らないようにするため
    private bool isProcessingCapture = false;

    //ゲームオーバーになっているか
    private bool isGameOver = false;

    // trueの間は鬼が動かない
    public static bool EnemiesPaused { get; private set; }

    void Awake()
    {
        Instance = this;
        EnemiesPaused = false;
    }

    void Start()
    {
        currentLives = Mathf.Max(1, initialLives);

        Debug.Log("ライフ：" + currentLives);
    }

    public void CapturePlayer()
    {
        // 捕獲処理中やゲームオーバー中は無視
        if (isProcessingCapture || isGameOver)
            return;

        StartCoroutine(CaptureRoutine());
    }

    IEnumerator CaptureRoutine()
    {
        isProcessingCapture = true;

        currentLives--;

        Debug.Log("捕まった！ 残りライフ：" + currentLives);

        // 鬼だけ停止する
        EnemiesPaused = true;

        if (currentLives <= 0)
        {
            isGameOver = true;
            Debug.Log("GAME OVER");
            yield break;
        }

        // 指定した秒数だけ待つ
        yield return new WaitForSeconds(freezeSeconds);

        // 鬼の停止を解除する
        EnemiesPaused = false;

        isProcessingCapture = false;
    }
}