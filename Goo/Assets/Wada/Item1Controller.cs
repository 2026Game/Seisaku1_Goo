using UnityEngine;

public class Item1Controller : MonoBehaviour
{
    int randomNumber = 0;
    int randomPosition = 0;
    float Item1Time = 10.0f;
    GameObject TimeMinus;
    GameObject playerSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.TimeMinus = GameObject.Find("GameDirector");
        this.playerSpeed = GameObject.Find("PF Player");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PF Player")
        {
            randomNumber = Random.Range(1, 3);
            if (randomNumber == 1)
            {
                this.TimeMinus.GetComponent<GameDirector>().TimerMinus();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                Item1Time = 10.0f;
            }
            if(randomNumber==2)
            {
                this.playerSpeed.GetComponent<PlayerController>().PlayerSpeed();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                Item1Time = 10.0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<SpriteRenderer>().enabled == false)
        {
            randomPosition = Random.Range(1, 3);
        }

        this.Item1Time -= Time.deltaTime;
        if(Item1Time<0)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }

        if(randomPosition==1)
        {
            transform.position = new Vector3(-19.32f, -10.94f, 0.0f);
        }
        else if(randomPosition==2)
        {
            transform.position = new Vector3(27.5f, 3.0f, 0.0f);
        }
    }
}
