using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;

    private bool movingLeft = true;

    public Transform edgeCheck;
    public LayerMask groundLayer;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (IsEdge() == false)
        {
            if (movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft= false;
            }  
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
    }

    private bool IsEdge()
    {
        return Physics2D.OverlapCircle(edgeCheck.position, 0.2f, groundLayer);
    }
}
