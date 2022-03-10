using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class ChatAPIController : MonoBehaviour
{
    public TextMeshProUGUI messageToUsers;
    public GameObject myGuessHandler;
    public GuessHandler guessHandlerScript;
    public ChatPanelManager myChatPanelManager;

   // private readonly string baseChatURL = "https://run.mocky.io/v3/";
    private readonly string baseChatURL = "localhost:8080/";
   // private readonly string restOfURL = "db03d3a7-b904-44a1-96f6-bb5d8966ba64";
    private readonly string restOfURL = "chat/play";

    //these are the text-on-screen that tell the user if they're right/wrong
    public TMP_Text correct;
    public TMP_Text wrong;

    string[] commentsAndNames;
    JSONNode theWholeJSON, theCommentsJSON;

    private void Start()
    {
        messageToUsers.text = "Press the Next Chat Log button to see the next chat log";
    }

    public void OnButtonRandomChatLog()
    {
        messageToUsers.text = "Loading...";

        //clear any messages from previous chatlogs
        myChatPanelManager.ClearTheMessageList();

        //make sure the correct/wrong texts are hidden
        correct.enabled = false;
        wrong.enabled = false;

        StartCoroutine(GetChatLog(restOfURL));

        messageToUsers.text = "";
    }

    //this grabs the chat log from the API and then starts the Coroutine that prints them on the screen
    IEnumerator GetChatLog(string theRestOfTheURL)
    {
        //assemble the full URL for the REST call
        string theWholeAPIURL = baseChatURL + restOfURL;

        UnityWebRequest chatInfoRequest = UnityWebRequest.Get(theWholeAPIURL);

        yield return chatInfoRequest.SendWebRequest();

        if (chatInfoRequest.isNetworkError || chatInfoRequest.isHttpError)
        {
            Debug.LogError(chatInfoRequest.error);
            yield break;
        }

        //breaking down the JSON into the whole thing and then a subsection that just includes the comments nodes
        theWholeJSON = JSON.Parse(chatInfoRequest.downloadHandler.text);
        theCommentsJSON = theWholeJSON["comments"];
        commentsAndNames = new string[theCommentsJSON.Count];

        //Store name of correct game
        guessHandlerScript.currentGameName = theWholeJSON["gameName"];

        //print all the comments to the screen
        StartCoroutine(PrintAllTheChats());

    }

    //this looks at the comments JSON and prints each one to the screen after a random delay
    IEnumerator PrintAllTheChats()
    {

        for (int i = 0; i < theCommentsJSON.Count; i++)
        {
            myChatPanelManager.SendMessageToChat("<color=#E0E300>" + theCommentsJSON[i]["name"] + "</color>" + ": " + theCommentsJSON[i]["comment"]);
            yield return new WaitForSeconds(Random.Range(0.1f, 1.2f));
        }
    }

    
}
