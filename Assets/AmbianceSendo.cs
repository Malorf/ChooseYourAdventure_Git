using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceSendo : ChienvalierAmbiance
{
    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audios.clip = ambianceVille;
            audios.Play();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audios.clip = ambianceGeneral;
            audios.Play();
        }
    }
}
