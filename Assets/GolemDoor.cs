using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDoor : MonoBehaviour
{
    private Animator Door;
    public static bool golemDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        Door = GameObject.Find("Door_Quest").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (golemDoor == true)
        {
            Door.SetBool("OpenDoor", true);
        }
    }
}
