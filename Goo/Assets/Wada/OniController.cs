using UnityEngine;

public class OniController : MonoBehaviour
{
    GameObject PlayerCatch;
    GameObject director;

    public Transform player;
    static float speed = 0.0f;
    float Pause = 3.0f;
    static int PlayerDamage = 0;

    public float checkDistance = 0.6f;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private Collider2D enemyCollider;
    int idx = 0;
    int State = 0;
    private int avoidSide = 1;

    private Vector3 previousPosition;

    private Vector2 move;
    public Sprite[] RightwalkSprites;
    public Sprite[] LeftwalkSprites;
    public Sprite[] UpwalkSprites;
    public Sprite[] DownwalkSprites;
    public Sprite[] RightStopSprites;
    public Sprite[] LeftStopSprites;
    public Sprite[] UpStopSprites;
    public Sprite[] DownStopSprites;


    float animationInterval = 0.2f;
    private float animationTimer = 0f;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        previousPosition = transform.position;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.director = GameObject.Find("GameDirector");
        this.PlayerCatch = GameObject.Find("PF Player");
        this.player = GameObject.Find("PF Player").transform;
        enemyCollider.enabled = true;
        if (PlayerDamage >= 3)
        {
            PlayerDamage = 0;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Transform detector = transform.Find("Collision");

        if (detector != null)
        {
            enemyCollider = detector.GetComponent<Collider2D>();
        }
    }

    //当たり判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PF Player")
        {
            this.PlayerCatch.GetComponent<PlayerController>().Catch();
            this.director.GetComponent<GameDirector>().CatchPlayer();



            transform.localScale = new Vector3(1f, 1f, 1f);
            if (State == 3)
            {
                this.spriteRenderer.sprite = this.RightStopSprites[this.idx];
            }
            else if (State == 2)
            {
                this.spriteRenderer.sprite = this.LeftStopSprites[this.idx];
            }
            else if (State == 1)
            {
                this.spriteRenderer.sprite = this.UpStopSprites[this.idx];
            }
            else
            {
                this.spriteRenderer.sprite = this.DownStopSprites[this.idx];
            }
        }
    }

    public void OniStop()
    {
        Pause = 3;
        speed = 0;
        PlayerDamage++;
    }

    void FixedUpdate()
    {
        if (player == null || rb == null)
            return;

        Vector2 toPlayer =
            (Vector2)player.position - rb.position;

        if (toPlayer.sqrMagnitude < 0.001f)
            return;

        toPlayer.Normalize();

        Vector2 moveDirection = toPlayer;


        if (IsBlocked(toPlayer))
        {
            Vector2 left =
                new Vector2(-toPlayer.y, toPlayer.x);

            Vector2 right =
                new Vector2(toPlayer.y, -toPlayer.x);

            bool leftBlocked = IsBlocked(left);
            bool rightBlocked = IsBlocked(right);

            if (!leftBlocked && !rightBlocked)
            {

                moveDirection = avoidSide == 1 ? left : right;


                if (IsBlocked(moveDirection))
                {
                    avoidSide *= -1;
                    moveDirection =
                        avoidSide == 1 ? left : right;
                }
            }
            else if (!leftBlocked)
            {
                moveDirection = left;
                avoidSide = 1;
            }
            else if (!rightBlocked)
            {
                moveDirection = right;
                avoidSide = -1;
            }
            else
            {

                return;
            }
        }

        Vector2 nextPosition =
            rb.position +
            moveDirection * speed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);

        //タイムアップ
        if (GameDirector.GameOver != 0)
        {
            PlayerDamage = 3;
            speed = 0;
        }

        //方向測定

        Vector3 difference =
            transform.position - previousPosition;

        previousPosition = transform.position;

        //向きに応じて走りを変える

        if (speed == 0)
        {
            if (Pause <= 0)
            {
                Pause = 3;
            }
            this.Pause -= Time.deltaTime;
            enemyCollider.enabled = false;

            if (PlayerDamage >= 3)
            {
                speed = 0;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    PlayerDamage = 0;
                }
            }
            else if (Pause <= 0)
            {
                enemyCollider.enabled = true;
                speed = 4;
            }
        }
        else
        {
            speed = 4;
            Pause = 3;

            enemyCollider.enabled = true;

            if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
            {
                if (difference.x > 0.0)
                {
                    //Debug.Log("右向き！");
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
                else if (difference.x < 0.0)
                {
                    //Debug.Log("左向き！");
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
                else
                {
                    if (State == 2)
                    {
                        this.spriteRenderer.sprite = this.RightStopSprites[this.idx];
                    }
                    else if (State == 3)
                    {
                        this.spriteRenderer.sprite = this.LeftStopSprites[this.idx];
                    }
                }
            }
            else
            {
                if (difference.y > 0.0)
                {
                    //Debug.Log("上向き！");
                    move.x = 1;
                    this.spriteRenderer.sprite = this.UpwalkSprites[this.idx];
                    State = 1;
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else if (difference.y < 0.0)
                {
                    //Debug.Log("下向き！");
                    move.x = -1;
                    this.spriteRenderer.sprite = this.DownwalkSprites[this.idx];
                    State = 0;
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    if (State == 1)
                    {
                        this.spriteRenderer.sprite = this.UpStopSprites[this.idx];
                    }
                    else if (State == 0)
                    {
                        this.spriteRenderer.sprite = this.DownStopSprites[this.idx];
                    }
                }
            }

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


    }


    bool IsBlocked(Vector2 direction)
    {
        if (enemyCollider == null)
        {
            return Physics2D.Raycast(
                rb.position,
                direction,
                checkDistance,
                wallLayer
            ).collider != null;
        }

        Bounds bounds = enemyCollider.bounds;

        float radius = Mathf.Min(
            bounds.extents.x,
            bounds.extents.y
        ) * 0.8f;

        RaycastHit2D hit = Physics2D.CircleCast(
            bounds.center,
            radius,
            direction,
            checkDistance,
            wallLayer
        );

        return hit.collider != null;
    }
}
