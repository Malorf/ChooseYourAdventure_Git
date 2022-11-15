using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowers : MonoBehaviour
{
    public static int NumberFlowers;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && DialogueMerlinramix.QuestHerbes == true)
        {
            NumberFlowers += 1;
            Destroy(gameObject);
            Debug.Log(NumberFlowers);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
