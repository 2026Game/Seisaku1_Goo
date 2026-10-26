using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool Stop;
    float speed = 0.0f;
    private Rigidbody2D rb;
    private Vector2 move;

    public Sprite[] RightwalkSprites;
    public Sprite[] LeftwalkSprites;
    public Sprite[] UpwalkSprites;
    public Sprite[] DownwalkSprites;
    public Sprite[] RightStopSprites;
    public Sprite[] LeftStopSprites;
    public Sprite[] UpStopSprites;
    public Sprite[] DownStopSprites;
    float time = 0;
    int idx = 0;
    int State = 0;
    SpriteRenderer spriteRenderer;
    float animationInterval = 0.2f;
    private float animationTimer = 0f;
    int Damage = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        Stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        move = Vector2.zero;
        speed = 0.0f;
        if (Stop == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                speed = 5.0f;
                move.y = 1;
                this.spriteRenderer.sprite = this.UpwalkSprites[this.idx];
                State = 1;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                speed = 5.0f;
                move.y = -1;
                this.spriteRenderer.sprite = this.DownwalkSprites[this.idx];
                State = 0;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                speed = 5.0f;
                move.x = -1;
                this.spriteRenderer.sprite = this.LeftwalkSprites[this.idx];
                State = 2;
                if (idx % 2 == 0)
                {
                    transform.localScale = new Vector3(1.4f, 1f, 1f);
                }
                else
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                speed = 5.0f;
                move.x = 1;
                this.spriteRenderer.sprite = this.RightwalkSprites[this.idx];
                State = 3;
                if (idx % 2 == 0)
                {
                    transform.localScale = new Vector3(1.4f, 1f, 1f);
                }
                else
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }

        }
        else
        {
            speed = 0;
        }

        if (move.x != 0 || move.y != 0)
        {
            animationTimer += Time.deltaTime;

            if (animationTimer >= animationInterval)
            {
                animationTimer = 0f;
                idx = (idx + 1) % 4;
            }
        }
        else
        {
            animationTimer = 0f;
            idx = 0;
        }



        if (speed < 5)
        {
            //Œü‚¢‚Ä‚¢‚é•ûŒü‚É‚æ‚Á‚Ä—§‚¿Žp‚ð•Ï‚¦‚é
            if (State == 0)
            {
                this.spriteRenderer.sprite = this.DownStopSprites[this.idx];
            }
            else if (State == 1)
            {
                this.spriteRenderer.sprite = this.UpStopSprites[this.idx];
            }
            else if (State == 2)
            {
                this.spriteRenderer.sprite = this.LeftStopSprites[this.idx];
            }
            else if (State == 3)
            {
                this.spriteRenderer.sprite = this.RightStopSprites[this.idx];
            }
        }
        move = move.normalized;

        if (GameDirector.GameOver != 0)
        {
            Stop = true;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = move * speed;
    }

    public void Catch()
    {
        Damage++;
        Debug.Log(Damage);
        if (Damage >= 3)
        {
            Stop = true;
        }
    }
}
