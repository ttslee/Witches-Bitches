using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronScript : MonoBehaviour
{
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

    private int numItemsLeft = 5;

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
    private float waitTime = 120f;
    // Start is called before the first frame update

    //Animator
    //public Animator animator;
    void Start()
    {
        timer = gameObject.GetComponent<Timer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HasRecipe)
        {
            if (NumItemsLeft == 0)
            {
                gameObject.GetComponentInParent<MonsterManager>().AlertManager_CauldronRecipeComplete();
                GetComponentInChildren<MonsterSprite>().RemoveImage();
            }
            else if (timer.Done && NumItemsLeft > 0)
            {
                gameObject.GetComponentInParent<MonsterManager>().AlertManager_CauldronTimedOut();
                GetComponentInChildren<MonsterSprite>().RemoveImage();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) // Pick up the correct item
    {
        if (!HasRecipe)
            return;
        if (collision.name == recipe[currentItem])
        {
            NumItemsLeft -= 1;
            currentItem++;
            collision.gameObject.GetComponent<ItemPickup>().Kill();
            setFloatingSprite(recipe[currentItem]);
        }
        else
        {
            Spit(collision);
        }
    }

    public void WakeUp(List<string> r)
    {
        Recipe = r;
        HasRecipe = true;
        currentItem = 0;
        GetComponent<Timer>().SetTime(waitTime, "Cauldron");
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
