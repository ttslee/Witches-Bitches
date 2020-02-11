using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject potionSpawn;
    public GameObject potion;
    private Sprite potionSprite;
    private string pName;
    //Monster info
    private int monster_num;

    //Recipe info
    private int currentItem;
    
    private bool hasRecipe = false;

    public bool HasRecipe
    {
        get
        {
            return hasRecipe;
        }

        set
        {
            hasRecipe = value;
        }
    }

    public List<string> recipe;

    public List<string> Recipe
    {
        get
        {
            return recipe;
        }

        set
        {
            recipe = value;
        }
    }
    private int startNumItems = 4;
    private int numItemsLeft;

    public int NumItemsLeft
    {
        get
        {
            return numItemsLeft;
        }

        set
        {
            numItemsLeft = value;
        }
    }

    //Timer
    private Timer timer;
    private float waitTime = 50f;
    // Start is called before the first frame update

    //Animator
    public Animator animator;
    void Start()
    {
        NumItemsLeft = startNumItems;
        timer = gameObject.GetComponent<Timer>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(HasRecipe)
        {
            if (NumItemsLeft == 0)
            {
                GameObject pot = Instantiate(potion, potionSpawn.transform.position, potionSpawn.transform.rotation);
                pot.GetComponent<SpriteRenderer>().sprite = potionSprite;
                pot.name = pName;
                gameObject.GetComponentInParent<MonsterManager>().AlertManager_RecipeComplete(monster_num);
                GetComponentInChildren<MonsterSprite>().RemoveImage();
                HasRecipe = false;
                timer.Done = true;
                return;
            }
            if(timer.Done && NumItemsLeft > 0)
            {
                gameObject.GetComponentInParent<MonsterManager>().AlertManager_TimedOut(monster_num);
                GetComponentInChildren<MonsterSprite>().RemoveImage();
                HasRecipe = false;
            }
                
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Pick up the correct item
    {
        if (!HasRecipe)
            return;
        if (currentItem >= recipe.Count)
            return;
        if (collision.name == recipe[currentItem])
        {
            //if (collision.GetComponent<ItemPickup>().Holder != 0)
            //    return;
            NumItemsLeft -= 1;
            currentItem++;
            animator.SetTrigger("Chomp");
            collision.gameObject.GetComponent<ItemPickup>().Kill();
            if(numItemsLeft != 0)
                setFloatingSprite(recipe[currentItem]);
        }
    }

    public void WakeUp(List<string> r, string nm, int mNum, string potionName, Dictionary<string, Sprite> spriteDictionary)
    {
        numItemsLeft = startNumItems;
        pName = potionName;
        potionSprite = spriteDictionary[potionName];
        monster_num = mNum;
        recipe = r;
        HasRecipe = true;
        currentItem = 0;
        GetComponent<Timer>().SetTime(waitTime, nm);
        setFloatingSprite(recipe[currentItem]);
    }

    private void setFloatingSprite(string name)
    {
        GetComponentInChildren<MonsterSprite>().SetSprite(GetComponentInParent<MonsterManager>().SpriteDictionary[name]);
    }
}
