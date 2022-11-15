using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChienvalierAmbiance : MonoBehaviour
{
    public static AudioSource audios;
    public AudioClip ambianceVille;
    public AudioClip ambianceGeneral;
    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
        audios.clip = ambianceGeneral;
        audios.Play();
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider other)
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
