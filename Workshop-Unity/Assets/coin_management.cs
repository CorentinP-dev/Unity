using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_management : MonoBehaviour
{
     void OnCollisionEnter2D(Collision2D col)
    {
        Destroy (gameObject);
    } 
}
