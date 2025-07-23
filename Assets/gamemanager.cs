using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public scoremanager scoreManager;
    public Button[] colorButtons;
    public Button startButton;
    public Text statusText;
    private List<int> sequence = new List<int>();
    private List<int> playerinput = new List<int>();
    private int currentstep = 0;
    private bool inputEnabled = false;
    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i;
            colorButtons[i].onClick.AddListener(() => StartCoroutine(oncolorpressed(index)));
        }
        scoreManager.ResetScore();
    }
    void StartGame()
    {
        statusText.text = "Watch the pattern!";
        sequence.Clear();
        playerinput.Clear();
        StartCoroutine(NextRound());
    }
    IEnumerator NextRound()
    {
        inputEnabled = false;
        playerinput.Clear();
        sequence.Add(Random.Range(0, 4));
        yield return showSequence();
        inputEnabled = true;
        statusText.text = "Your turn!";
    }
    IEnumerator showSequence()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            int index = sequence[i];
            highlightbutton(colorButtons[index]);
            yield return new WaitForSeconds(1f);
        }
    }
    void highlightbutton(Button btn)
    {
        Color original = btn.image.color;
        btn.image.color = Color.white;
        StartCoroutine(resetButtonColor(btn, original));
    }
    IEnumerator resetButtonColor(Button btn, Color original)
    {
        yield return new WaitForSeconds(0.5f);
        btn.image.color = original;
    }
    IEnumerator oncolorpressed(int index)
    {
        if (!inputEnabled) yield break;
        playerinput.Add(index);
        highlightbutton(colorButtons[index]);
        if (playerinput[playerinput.Count - 1] != sequence[playerinput.Count - 1])
        {
            statusText.text = "Wrong! Try again.";
            scoreManager.SetScoreZero(); 
            inputEnabled = false;
            yield break;
        }
        else if (playerinput.Count == sequence.Count)
        {
            scoreManager.Addpoints();
            statusText.text = "Good job! Next round!";
            inputEnabled = false;
            yield return new WaitForSeconds(2f);
            StartCoroutine(NextRound());
        }
        else
        {
            statusText.text = "Keep going!";
        }
    }
    

}
