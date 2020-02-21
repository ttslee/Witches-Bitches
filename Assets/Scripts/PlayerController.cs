using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player
    public int player = 1;
    private int health = 3;

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }
    //Animation
    public Animator animator;

    //Movement
    Vector2 movement;
    public float moveSpd = 1f;
    private string horizontal, vertical;
    public Rigidbody2D playerBody;

    //Item Pickup
    private GameObject it;
    private Transform item;
    private bool dropped = false;

    public Transform Item { get { return item; } set { item = value; } }

    private bool isHoldingItem = false;

    public bool IsHoldingItem { get { return isHoldingItem; } set { isHoldingItem = value; } }

    // Element
    private string element;
    private bool hasElement;

    public string Element { get { return element; } set { element = value; } }
    
    public Timer pickupTimer;
    public Timer shootTimer;

    // Weapon
    public GameObject Fire;
    public GameObject Death;
    private bool canShoot = true;
    // Update is called once per frame

    private void Start()
    {
        horizontal = (player == 1) ? "Horizontal" : "Horizontal2";
        vertical = (player == 1) ? "Vertical" : "Vertical2";
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (Input.GetKey(KeyCode.Escape))
            QuitGame();
        if (this.Health > 0) {
            movement.x = Input.GetAxisRaw(horizontal);
            movement.y = Input.GetAxisRaw(vertical);
            movement = movement.normalized; 
            animator.SetFloat("Speed", movement.sqrMagnitude);
            if (movement.x < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            } else if (movement.x > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        

            switch(player)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.E) && IsHoldingItem)
                        Drop();
                    else if (Input.GetKeyDown(KeyCode.Q) && hasElement && canShoot)
                        Shoot();
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.RightShift) && IsHoldingItem)
                        Drop();
                    else if (Input.GetKeyDown(KeyCode.RightControl) && hasElement && canShoot)
                        Shoot();
                    break;
            }
        }
        else {
            if (IsHoldingItem)
                Drop();
            animator.GetComponent<Animator>().enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (this.Health > 0) {
            playerBody.MovePosition(playerBody.position + movement * moveSpd * Time.fixedDeltaTime);
        }
        if(pickupTimer.Done)
            dropped = false;
        if (shootTimer.Done)
            canShoot = true;
        if(IsHoldingItem)
            setItemPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsHoldingItem && !dropped && collision.gameObject.CompareTag("Item") && collision.gameObject.GetComponent<ItemPickup>().Holder == 0)
        {
            switch (collision.gameObject.name)
            {
                case "FireElement":
                case "DeathElement":
                    Element = collision.gameObject.name;
                    hasElement = true;
                    break;
            }
            IsHoldingItem = true;
            it = collision.gameObject;
            it.GetComponent<CircleCollider2D>().enabled = false;
            it.GetComponent<ItemPickup>().Holder = player;
            PickUp(collision);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsHoldingItem && !dropped && other.gameObject.CompareTag("Item") && other.gameObject.GetComponent<ItemPickup>().Holder == 0)
        {
            switch (other.gameObject.name)
            {
                case "FireElement":
                case "DeathElement":
                    Element = other.gameObject.name;
                    hasElement = true;
                    break;
            }
            IsHoldingItem = true;
            it = other.gameObject;
            it.GetComponent<CircleCollider2D>().enabled = false;
            it.GetComponent<ItemPickup>().Holder = player;
            PickUp(other);
        }
    }
    private void Drop()
    {
        Item.parent = null;
        it.GetComponent<ItemPickup>().Drop(calcLocalPos());
        dropped = true;
        IsHoldingItem = false;
        hasElement = false;
        Element = "";
        it = null;
        Item = null;
        if (this.Health > 0)
            TimerStart(pickupTimer, 0.8f);
    }

    private void PickUp(Collider2D collision)
    {
        Item = collision.gameObject.transform;
        Item.parent = transform;
        if (movement.y == 0 && movement.x == 0)
        {
            Item.localPosition = new Vector3(0, -.3f, 1f);
        }
        else
            setItemPosition();
    }

    private void setItemPosition()
    {

        if (movement.y >= .1f && movement.x == 0)
        {
            Item.localPosition = new Vector3(0, .3f, 1f);
        }
        else if (movement.y <= -.1 && movement.x == 0)
        {
            Item.localPosition = new Vector3(0, -.3f, 1f);
        }
        else if ((movement.x <= -.1 || movement.x >= .1f))
        {
            Item.localPosition = new Vector3(.4f, 0, 1f);
        }

        Item.transform.position = new Vector3(Item.position.x, Item.position.y, 1f);
    }

    private void TimerStart(Timer timer, float t)
    {
        timer.SetTime(t, "Player");
    }
    private string calcLocalPos()
    {
        Vector3 localPos = Item.localPosition;

        if (localPos.y > 0)
            return "up";
        else if (localPos.y < 0)
            return "down";
        else if (localPos.z == -1)
            return "left";
        else return "right";
    }
    public void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
        #endif
    }
    public void TakeDamage()
    {
        Health--;
    }
    public void Shoot()
    {
        if (Element == "FireElement")
        {
            GameObject F = Instantiate(Fire, Item.position, Item.rotation);
            F.GetComponent<ProjectileScript>().SetProjectile((player == 1) ? "Player1" : "Player2", calcLocalPos());
        }  
        else
        {
            GameObject D = Instantiate(Death, Item.position, Item.rotation);
            D.GetComponent<ProjectileScript>().SetProjectile((player == 1) ? "Player1" : "Player2", calcLocalPos());
        }
        canShoot = false;
        TimerStart(shootTimer, 1.0f);
    }
}
