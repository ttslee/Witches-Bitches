using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSprite : MonoBehaviour
{
    public float speed;
    public float height;
    public float startY;
    // Start is called before the first frame update
    void Start()
    {
        //transform.localPosition = new Vector3(0f, startY, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        //set the object's Y to the new calculated Y
        transform.localPosition = new Vector3(0, startY + newY, 0) ;
    }

    public void SetSprite(Sprite image)
    {
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void RemoveImage()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }
}
