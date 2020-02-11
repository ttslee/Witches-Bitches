using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CauldronScript : MonoBehaviour
{
    //Recipe info
    private int currentItem = 0;

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

    private void OnTriggerStay2D(Collider2D collision) // Pick up the correct item
    {
        print(collision.name);
        print(recipe[currentItem]);
        if (!HasRecipe)
            return;
        if (collision.name == recipe[currentItem])
        {
            NumItemsLeft -= 1;
            currentItem++;
            collision.gameObject.GetComponent<ItemPickup>().Kill();
            setFloatingSprite(recipe[currentItem]);
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
    

    private void setFloatingSprite(string name)
    {
        GetComponentInChildren<MonsterSprite>().SetSprite(GetComponentInParent<MonsterManager>().SpriteDictionary[name]);
    }
}
