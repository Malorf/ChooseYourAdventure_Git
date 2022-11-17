using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public int maxMessages = 15;
    public GameObject chatPanel, textObject;
    public InputField chatBox;
    public static string PlayerAnswer = "a";
    public Color playerMessage, info;
    [SerializeField]
    public static List<Message> messageList = new List<Message>();
    void Start()
    {
        
    }

    void Update()
    {
        if(chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(Constructeur.NameCharacter +": " + chatBox.text, Message.MessageType.playerMessage);
                chatBox.text = "";
            }
        else
            {
                if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
                    chatBox.ActivateInputField();
            }
        }
        if (chatBox.isFocused)
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = 1;

        if (messageList.Count >= 1)
        {
            PlayerAnswer = messageList[-1 + messageList.Count].text;
        }
        if (PlayerAnswer == "lvlup")
        {
            SendMessageToChat("Félicitations ! Vous êtes niveau " + PlayerInventory.playerLevel + ".Appuyez sur 'TAB'" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "Quest1Done")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + Dialogue2.XpQuêteLoup + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "Quest2Done")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + Dialogue4.XpQuêteSpider + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "Quest3Done")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + Dialogue4.XpQuêteSpiderQueen + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "Quest4Done")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + Dialogue5.XpQuêteJail + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "Quest10ChienvaliersDone")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + DialogueBlackSmith.XpQuêteChienvalier + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "QuestChampionDone")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + DialogueDuchelvau.XpQuêteChampion + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "QuestMayorDone")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + DialogueMayor.XpQuêteMayor + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "QuestFlowersDone")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + DialogueMerlinramix.XpQuêteHerbes + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "QuestPotionDone")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + DialogueMerlinramix.XpQuêtePotion + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "QuestGobelinDone")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + DialogueTavernier.XpQuêteGobelin + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "QuestLieutenantDone")
        {
            SendMessageToChat("Vous venez de terminer une quête et gagner :" + DialogueTavernier.XpQuêteLieutenant + "points d'expérience" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "Quest1Activate")
        {
            SendMessageToChat("La quête a été acceptée." + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
        if (PlayerAnswer == "TutoIsDone")
        {
            SendMessageToChat("Félicitations Soldat ! Parlez aux camarades devant vous et bonne aventure ! Appuyer sur échap sauvegarde la partie" + chatBox.text, Message.MessageType.info);
            chatBox.text = "";
        }
    }
    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        if (messageList.Count >= maxMessages)
        {
            messageList.Remove(messageList[0]);
            Destroy(messageList[0].textObject.gameObject);
        }
        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;

        newMessage.textObject.color = MessageTypeColor(messageType);

        messageList.Add(newMessage);
    }
    Color MessageTypeColor(Message.MessageType messageType)
    {
        Color color = info;

        switch(messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessage;
                break;

            case Message.MessageType.info:
                color = info;
                break;


        }

        return color;
    }
}
[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
    public MessageType messageType;
    public enum MessageType
    {
        playerMessage,
        info,
    }
}
