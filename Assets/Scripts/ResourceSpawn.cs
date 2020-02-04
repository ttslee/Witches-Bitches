using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    public GameObject Bat;
    public GameObject Rat;
    public GameObject Skeleton;
    public GameObject Rock;
    public GameObject[] Bat_a;
    public GameObject[] Rat_a;
    public GameObject[] Skeleton_a;
    public GameObject[] Rock_a;
    public Transform[] spawnPositions;
    public static int num_bat;
    public static int num_rat;
    public static int num_skeleton;
    public static int num_rock;

    // Start is called before the first frame update
    void Start()
    {
        Bat_a = GameObject.FindGameObjectsWithTag("Bat");
        Rat_a = GameObject.FindGameObjectsWithTag("Rat");
        Skeleton_a = GameObject.FindGameObjectsWithTag("Skeleton");
        Rock_a = GameObject.FindGameObjectsWithTag("Rock");
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        Bat_a = GameObject.FindGameObjectsWithTag("Bat");
        Rat_a = GameObject.FindGameObjectsWithTag("Rat");
        Skeleton_a = GameObject.FindGameObjectsWithTag("Skeleton");
        Rock_a = GameObject.FindGameObjectsWithTag("Rock");
    }

    IEnumerator Spawn()
    {
        num_bat = Bat_a.Length;
        num_rat = Rat_a.Length;
        num_skeleton = Skeleton_a.Length;
        num_rock = Rock_a.Length;
        while(num_rat < 3)
        {
            int rand = Random.Range(0, 5);
            if (num_bat < 1)
            {
                GameObject b = Instantiate(Bat, spawnPositions[rand].transform.position, Quaternion.identity);
                num_bat += 1;
            }
            else if (num_rat < 1)
            {
                GameObject r = Instantiate(Rat, spawnPositions[rand].transform.position, Quaternion.identity);
                num_rat += 1;
            }
             else if (num_skeleton < 1)
            {
                GameObject s = Instantiate(Skeleton, spawnPositions[rand].transform.position, Quaternion.identity);
                num_skeleton += 1;
            }
            else if (num_rock < 1)
            {
                GameObject R = Instantiate(Rock, spawnPositions[rand].transform.position, Quaternion.identity);
                num_rock += 1;
            }
            yield return new WaitForSeconds(2);
        }
    }
}
