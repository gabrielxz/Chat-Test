using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanelManager : MonoBehaviour
{
    public int maxMessages = 50;

    public GameObject chatPanel, textObject;

    [SerializeField]
    List<Message> messageList = new List<Message>();

    //this adds a message to the list & creates the text message object
    public void SendMessageToChat(string text)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newText.gameObject.tag = "chatMessage";

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;

        messageList.Add(newMessage);
    }

    //this clears out the message list and deletes all the textObjects
    public void ClearTheMessageList()
    {
        GameObject[] chatTexts = GameObject.FindGameObjectsWithTag("chatMessage");

        for (int i = 0; i < chatTexts.Length; i++)
        {
            Destroy(chatTexts[i]);
        }

        messageList.Clear();
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;

}