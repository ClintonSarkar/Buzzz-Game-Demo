using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    public Sprite threeHearts;
    public Sprite twoHearts;
    public Sprite oneHeart;
    public Sprite zeroHeart;

    public PlayerInteractions player;

    void Update()
    {
        if (player.health == 3)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = threeHearts;
        }
        if (player.health == 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = twoHearts;
        }
        if (player.health == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = oneHeart;
        }
        else if (player.health == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = zeroHeart;
        }

    }


}
