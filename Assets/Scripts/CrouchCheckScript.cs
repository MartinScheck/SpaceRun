using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchCheckScript : MonoBehaviour
{
   
    public LayerMask groundLayer;
    public bool CanNotStand()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);
        return hit != null;
    }

}
