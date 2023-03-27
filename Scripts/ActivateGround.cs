using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGround : MonoBehaviour
{
    public Sprite ActivatedGround;
    public LayerMask changeLayer; 
    public Transform groundCheck;


    void Update()
    {
        if (IsActivated())
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ActivatedGround;
        }
    }

    private bool IsActivated()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, changeLayer);
    }
}
