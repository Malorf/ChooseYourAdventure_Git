using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenFire : MonoBehaviour
{
    public static float fireRegen;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fireRegen = 5f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fireRegen = 0f;
        }
    }

}
