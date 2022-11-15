using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPos : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Respawn")
        {
            Debug.Log("toto");
            Vector3 up = transform.TransformDirection(Vector3.up);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, up, out hit, 100))
            {
                transform.position = new Vector3(transform.position.x, hit.transform.position.y + 2, transform.position.z);
            }
        }
    }
}