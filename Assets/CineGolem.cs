using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineGolem : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool fall = false;
    private Animator AnimCam4;

    private void Start()
    {
        AnimCam4 = GameObject.Find("CamCineGolem").GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && fall == false)
        {
            if (AnimCam4 != null)
            {
                AnimCam4.SetBool("CineGolemActivate", true);
            }
            fall = true;
        }
    }
}
