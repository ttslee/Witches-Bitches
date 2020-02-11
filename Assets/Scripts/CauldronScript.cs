using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CauldronScript : MonoBehaviour
{
    //Recipe info
    public int currentItem = 0;

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
    private float waitTime = 120f;
    // Start is called before the first frame update

    //Animator
    //public Animator animator;
    void Start()
    {
        NumItemsLeft = startNumItems;
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
                SceneManager.LoadScene("Win");
            }
            else if (timer.Done && NumItemsLeft > 0)
            {
                gameObject.GetComponentInParent<MonsterManager>().AlertManager_CauldronTimedOut();
                GetComponentInChildren<MonsterSprite>().RemoveImage();
                SceneManager.LoadScene("Lose");
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
            if (collision.GetComponent<ItemPickup>().Holder != 0)
                return;
            NumItemsLeft -= 1;
            currentItem++;
            collision.gameObject.GetComponent<ItemPickup>().Kill();
            if(numItemsLeft != 0)
                setFloatingSprite(recipe[currentItem]);
        }
    }

    public void WakeUp(List<string> r)
    {
        Recipe = r;
        HasRecipe = true;
        NumItemsLeft = startNumItems;
        currentItem = 0;
        GetComponent<Timer>().SetTime(waitTime, "Cauldron");
        setFloatingSprite(recipe[currentItem]);
    }
    

    private void setFloatingSprite(string name)
    {
        GetComponentInChildren<MonsterSprite>().SetSprite(GetComponentInParent<MonsterManager>().SpriteDictionary[name]);
    }
}
