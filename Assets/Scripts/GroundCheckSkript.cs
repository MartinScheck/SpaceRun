using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckSkript : MonoBehaviour
{

    public LayerMask groundLayer;
    public bool IsGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);
        return hit != null;
    }
   
}
