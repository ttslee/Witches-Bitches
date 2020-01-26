using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    // Cauldron
    private bool cauldronDone = false;
    private bool cauldronTimedOut = false;
    // Recipes/ItemList
    private int nRecipes = 6;
    private int recipeSize = 4;
    private int count = 0;
    private int unfinished_recipes = 0;
    private int completed_recipes = 0;

    [System.Serializable]
    public class Recipes
    {
        public List<string> items;
    }

    public List<Recipes> myRecipes;
    
    public List<Recipes> MyRecipes
    {
        get
        {
            return myRecipes;
        }

        set
        {
            myRecipes = value;
        }
    }
    private static List<string> potionList = new List<string>
    {
            "red potion",
            "blue potion",
            "green potion",
            "purple potion",
            "orange potion",
    };
    private static List<string> itemList1 = new List<string> 
    {
            "red ore",
            "burnt rat",
            "burnt meat",
            "burnt skull",
    };
    private static List<string> itemList2 = new List<string>
    {
            "dark ore",
            "rotten rat",
            "rotten meat",
            "rotten skull",
    };

    [System.Serializable]
    public struct NamedImage
    {
        public string name;
        public Sprite image;
    }
    public NamedImage[] sprites;
    private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

    public Dictionary<string, Sprite> SpriteDictionary
    {
        get
        {
            return spriteDictionary;
        }

        set
        {
            SpriteDictionary = value;
        }
    }
    // ManagerTimer
    private Timer timer;
    private float rDelay = 5f;

    // Monster Management
    private static List<string> mList = new List<string>{ "Monster1", "Monster2", "Monster3" };
    private static List<int>    mAvailableList = new List<int> { 0, 1, 2 };
    // Start is called before the first frame update
    public void Start()
    {
        // Fill Dictionary of Items
        foreach (var item in sprites)
        {
            print(item.name);
            spriteDictionary.Add(item.name, item.image);
        }

        timer = gameObject.GetComponent<Timer>();
        // Shuffle list of items for this game.
        for(int n = 0; n < nRecipes; ++n)
        {
            List<string> randomList = GenerateRandomList();
            ShuffleList<string>(randomList);
            for (int j = 0; j < recipeSize; j++)
            {
                MyRecipes[n].items.Add(randomList[j]);
            }
        }
        ShuffleList<string>(potionList);
        ShuffleList<int>(mAvailableList);
        WakeUpMonster(mAvailableList[0]);
        WakeUpCauldron();
    }

    public static void ShuffleList<T>(List<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
    private List<string> GenerateRandomList()
    {
        List<string> randList = new List<string> ();
        for (int i = 0; i < 2; i++)
        {
            int num = Random.Range(0, 4);
            while (randList.Contains(itemList1[num]))
            {
                num = Random.Range(0, 4);
            }
            randList.Add(itemList1[num]);
            num = Random.Range(0, 4);
            while (randList.Contains(itemList2[num]))
            {
                num = Random.Range(0, 4);
            }
            randList.Add(itemList2[num]);
        }
        return randList;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(cauldronDone)
        {
            // Player wins
            QuitGame();
        }
        else if(cauldronTimedOut)
        {
            QuitGame();
        }

        if(unfinished_recipes == nRecipes)
        {
            QuitGame();
            //Player loses
            print("completed" + completed_recipes);
            print("unfinished" + unfinished_recipes);
            print("count" + count);
        }
        else if (timer.Done)
        {
            if(mAvailableList.Count > 0)
            {
                if(mAvailableList.Count > 1)
                    ShuffleList<int>(mAvailableList);
                WakeUpMonster(mAvailableList[0]);
                timer.SetTime(rDelay, "MonsterManager");
            }
        }
    }

    // Wake up monster and pass it a recipe to complete
    private void WakeUpCauldron()
    {
        gameObject.transform.Find("Cauldron").GetComponent<CauldronScript>().WakeUp(potionList);
    }

    private void WakeUpMonster(int monster_num)
    {
        mAvailableList.Remove(monster_num);
        gameObject.transform.Find(mList[monster_num]).GetComponent<Monster>().WakeUp(MyRecipes[count].items, mList[monster_num], monster_num);  // Wakes up a monster and sends it a recipe to complete. 
        count++;
        timer.SetTime(rDelay, "MonsterManager");
    }

    public void AlertManager_RecipeComplete(int monster_num)
    {
        mAvailableList.Add(monster_num);
        completed_recipes++;
    }

    public void AlertManager_TimedOut(int monster_num)
    {
        mAvailableList.Add(monster_num);
        unfinished_recipes++;
    }

    public void AlertManager_CauldronRecipeComplete()
    {
        cauldronDone = true;
    }

    public void AlertManager_CauldronTimedOut()
    {
        cauldronTimedOut = true;
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
}
