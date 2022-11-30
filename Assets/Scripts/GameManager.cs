using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] gamePlayObjs;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject howToPlayScreen;
    [SerializeField] TextMeshProUGUI problemText;
    [SerializeField] Button okButton;
    [SerializeField] GameObject overlapSensor;
    [SerializeField] GameObject resultsScreen;
    [SerializeField] TextMeshProUGUI resultsText;
    [SerializeField] AudioClip correctAnswerSoundEffect;
    [SerializeField] AudioClip wrongAnswerSoundEffect;
    private string[] questions = new string[10];
    private int[] answersDollarParts = new int[10];
    private int[] answersCentParts = new int[10];
    private bool readyForNextQuestion = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeQuestionsAndAnswers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        for (int i = 0; i < gamePlayObjs.Length; i++)
        {
            gamePlayObjs[i].SetActive(true);
        }
        titleScreen.SetActive(false);
        StartCoroutine(BeginGameplay());
    }

    IEnumerator BeginGameplay()
    {
        int questionsCorrectlyAnswered = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            problemText.text = questions[i];
            yield return new WaitUntil(() => readyForNextQuestion == true);
            int dollarsInputted = overlapSensor.GetComponent<OverlapSensor>().GetDollarsInPiggyBank();
            int centsInputted = overlapSensor.GetComponent<OverlapSensor>().GetCentsInPiggyBank();
            if (dollarsInputted == answersDollarParts[i] && centsInputted == answersCentParts[i])
            {
                questionsCorrectlyAnswered++;
                audioSource.PlayOneShot(correctAnswerSoundEffect, 1);
            }
            else
            {
                audioSource.PlayOneShot(wrongAnswerSoundEffect, 1);
            }
            overlapSensor.GetComponent<OverlapSensor>().ResetAmountInputted(false);
            ToggleReadyForNextQuestion();
        }
        for (int i = 0; i < gamePlayObjs.Length; i++)
        {
            gamePlayObjs[i].SetActive(false);
        }
        if (questionsCorrectlyAnswered >= 8)
        {
            resultsText.text = "You answered " + questionsCorrectlyAnswered.ToString() + " out of 10 questions correctly. Well done!";
        }
        else
        {
            resultsText.text = "You answered " + questionsCorrectlyAnswered.ToString() + " out of 10 questions correctly. Maybe next time!";
        }
        resultsScreen.SetActive(true);
    }

    private void InitializeQuestionsAndAnswers()
    {
        questions[0] = "Simon initially had $65.49 in his bank account. After house-sitting for his neighbor, he received $84.22, which he then deposited into his bank account. How much does he have in his bank account now?";
        answersDollarParts[0] = 149;
        answersCentParts[0] = 71;

        questions[1] = "Walter bought a pack of gum that cost him $3.80. If he used a five-dollar bill to purchase it, how much did he receive in change?";
        answersDollarParts[1] = 1;
        answersCentParts[1] = 20;

        questions[2] = "In a food-eating competition, Boris ate 68 hot dogs, and he won $1.16 for every hot dog he ate. How much money in total did Boris win from the competition?";
        answersDollarParts[2] = 78;
        answersCentParts[2] = 88;

        questions[3] = "If a pack of 15 pens costs $2.25, what is the unit price of one of those pens?";
        answersDollarParts[3] = 0;
        answersCentParts[3] = 15;

        questions[4] = "Naomi bought the following from King Soopers: 2 banana bunches that were each $2.50, 10 apples that were each $0.71, and 10 oranges that were each $0.90. If she paid $25, how much change did Naomi receive?";
        answersDollarParts[4] = 3;
        answersCentParts[4] = 90;

        questions[5] = "From a contest, Logan won a cash prize of $200. He later used 3/8 of it to purchase new accessories for his computer. How much did Logan pay for those computer accessories?";
        answersDollarParts[5] = 75;
        answersCentParts[5] = 0;

        questions[6] = "On her birthday, Miranda earned $150 as a gift. She later spent 40% of it on some new clothes. How much did Miranda pay for those clothes?";
        answersDollarParts[6] = 60;
        answersCentParts[6] = 0;

        questions[7] = "At Fashion Flair, a pair of jeans normally costs $34. On Black Friday, the same pair of jeans is 15% off. What is the price of that pair of jeans on Black Friday?";
        answersDollarParts[7] = 28;
        answersCentParts[7] = 90;

        questions[8] = "Emanuel bought three novels: “The Great Gatsby”, “The Lost Hero”, and “Harry Potter and the Chamber of Secrets”. Their prices were $12.27, $15.32, and $20.50, respectively, and a sales tax of 7% was applied. What was Emanuel’s grand total?";
        answersDollarParts[8] = 51;
        answersCentParts[8] = 46;

        questions[9] = "Mabel won a cash prize of $1,000 on Thursday. On Friday, she purchased a Nintendo Switch that cost her 13/20 of her prize, and a sales tax of 9% was applied to that purchase. Then on Saturday, she deposited 90% of what she had left at that time into her bank account. How much of the cash prize does Mabel have remaining after those events?";
        answersDollarParts[9] = 29;
        answersCentParts[9] = 15;
    }

    public void ToggleReadyForNextQuestion()
    {
        readyForNextQuestion = !readyForNextQuestion;
        okButton.enabled = false;
        okButton.enabled = true;
    }

    public void ShowHowToPlayScreen()
    {
        titleScreen.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void GoBackToTitleScreen()
    {
        howToPlayScreen.SetActive(false);
        resultsScreen.SetActive(false);
        titleScreen.SetActive(true);
    }
}
