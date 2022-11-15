using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulledialogue : MonoBehaviour
{
    public Transform LookTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LookTarget != null)
        {
            //rotation de la bulle vers le joueur
            Vector3 targetPosition = new Vector3(LookTarget.position.x, LookTarget.position.y, LookTarget.position.z);
            transform.LookAt(targetPosition);
        }
    }
}
