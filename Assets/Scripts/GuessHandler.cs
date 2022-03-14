using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuessHandler : MonoBehaviour
{
    private string guess;
    public string currentGameName;
    public TMP_InputField theUserGuess;
    public TMP_Text correct;
    public TMP_Text wrong;

    // Start is called before the first frame update
    void Start()
    {
        //hides the correct/wrong text when starting the game
        correct.enabled = false;
        wrong.enabled = false;
    }

    public void HandleUserGuess(string s)
    {
        //Debug.Log("user guess was " + theUserGuess.text + " and we thing that's great");

        //again, blanking out correct/wrong when starting a new guess
        correct.enabled = false;
        wrong.enabled = false;

        //compare the guess and the game name
        guess = theUserGuess.text.ToUpper();

        if (guess == currentGameName.ToUpper())
        {
            Debug.Log("Correct");
            correct.enabled = true;
        }
        else if (guess == "")
        {
            //do nothing
        }
        else
        {
            Debug.Log("Wrong, user guessed " + s);
            wrong.enabled = true;
        }

        //blank out the input field and give it focus again after a guess
        theUserGuess.text = "";
        theUserGuess.ActivateInputField();
    }
}
