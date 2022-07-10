using UnityEngine;
using TMPro;
using System.Collections;

public class QuestionObjectsHolder : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject questionScreen;
    [SerializeField] private GameObject feedbackScreen;
    [SerializeField] private GameObject correctAnswerScreen;
    [SerializeField] private GameObject wrongAnswerScreen;
    [SerializeField] private GameObject animationScreen;

    [Header("QuestionHolders")]
    [SerializeField] private TMP_Text questionTextHolder;
    [SerializeField] private TMP_Text answerAHolder;
    [SerializeField] private TMP_Text answerBHolder;
    [SerializeField] private TMP_Text answerCHolder;
    public TMP_Text countdown;

    //[Header("Audio")]
    //[SerializeField] private AudioClip correctAnswerClip;
    //[SerializeField] private AudioClip wrongAnswerClip;
    //[SerializeField] private AudioClip clickSoundClip;

    //private AudioSource clickSoundSource;

    private void Awake()
    {
       // clickSoundSource = GetComponent<AudioSource>();
    }

    public void SetQuestion(QuestionHolder question)
    {
        questionTextHolder.text = question.questionText;
        answerAHolder.text = question.answerA;
        answerBHolder.text = question.answerB;
        answerCHolder.text = question.answerC;
    }

    public void ShowAnimationScreen()
    {
        questionScreen.SetActive(false);
        animationScreen.SetActive(true);
        feedbackScreen.SetActive(false);
    }

    public void ShowFeedbackScreen()
    {
        questionScreen.SetActive(false);
        animationScreen.SetActive(false);
        feedbackScreen.SetActive(true);
    }

    public void CorrectAnswer()
    {
        correctAnswerScreen.SetActive(true);
        wrongAnswerScreen.SetActive(false);

        Debug.Log($"Correct Answer: {correctAnswerScreen.activeSelf} || Wrong Answer: {wrongAnswerScreen.activeSelf}");

        //clickSoundSource.PlayOneShot(correctAnswerClip);
    }

    public void WrongAnswer()
    {
        correctAnswerScreen.SetActive(false);
        wrongAnswerScreen.SetActive(true);
        Debug.Log($"Correct Answer: {correctAnswerScreen.activeSelf} || Wrong Answer: {wrongAnswerScreen.activeSelf}");

       // clickSoundSource.PlayOneShot(wrongAnswerClip);
    }

    //public void PlayClickSound()
    //{
    //    clickSoundSource.PlayOneShot(clickSoundClip);
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ShowFeedbackScreen();
            wrongAnswerScreen.SetActive(true);
        }
    }
}
