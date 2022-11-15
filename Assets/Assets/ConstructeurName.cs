using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constructeur;

public class ConstructeurName : MonoBehaviour
{
    public Text NameInGame ; //Pseudo

    private void Start()
    {
        NameInGame.text = NameCharacter;
    }

}
