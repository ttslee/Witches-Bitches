using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
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

    private int numItemsLeft = 4;

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
    private float waitTime = 10f;
    // Start is called before the first frame update

    //Animator
    public Animator animator;
    void Start()
    {
        timer = gameObject.GetComponent<Timer>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(HasRecipe)
        {
            if (NumItemsLeft == 0)
            {
                gameObject.GetComponentInParent<MonsterManager>().AlertManager_RecipeComplete(monster_num);
                GetComponentInChildren<MonsterSprite>().RemoveImage();
                HasRecipe = false;
            }
            if(timer.Done && NumItemsLeft > 0)
            {
                gameObject.GetComponentInParent<MonsterManager>().AlertManager_TimedOut(monster_num);
                GetComponentInChildren<MonsterSprite>().RemoveImage();
                HasRecipe = false;
            }
                
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision) // Pick up the correct item
    {
        if (!HasRecipe)
            return;
        if(collision.name == recipe[currentItem])
        {
            NumItemsLeft -= 1;
            currentItem++;
            animator.SetTrigger("Chomp");
            collision.gameObject.GetComponent<ItemPickup>().Kill();
            setFloatingSprite(recipe[currentItem]);
        }
        else
        {
            Spit(collision);
        }
    }

    public void WakeUp(List<string> r, string nm, int mNum)
    {
        monster_num = mNum;
        recipe = r;
        HasRecipe = true;
        currentItem = 0;
        GetComponent<Timer>().SetTime(waitTime, nm);
        setFloatingSprite(recipe[currentItem]);
    }

    private void Spit(Collider2D collision)
    {

    }

    private void setFloatingSprite(string name)
    {
        GetComponentInChildren<MonsterSprite>().SetSprite(GetComponentInParent<MonsterManager>().SpriteDictionary[name]);
    }
}
