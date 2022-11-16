using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tuto : MonoBehaviour
{
    public static bool Conversation = false;
    public bool inTuto = true;
    public string lastAnswer;
    public GameObject panelTuto;
    // Start is called before the first frame update
    void Start()
    {
        if (inTuto == true)
        {
            Conversation = true;
            panelTuto.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Conversation)
        {
            lastAnswer = GameManager.PlayerAnswer;
            if (lastAnswer == Constructeur.NameCharacter + ": améliorer")
            {
                panelTuto.SetActive(false);
                GameManager.messageList.Clear();
                GameManager.PlayerAnswer = "TutoIsDone";
                inTuto = false;
            }
        }
    }
}
