using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class QuestionHandler : MonoBehaviour
{
    [SerializeField] private QuestionHolder question;

    [SerializeField] private QuestionObjectsHolder questionsHolderPlayer;
    [SerializeField] private QuestionObjectsHolder questionsHolderCar;

    [SerializeField] private Transform playerInCarPosition;

    private PlayerCarChecker playerCarChecker;
    private TimelineHandler animations;
    private Transform player;

    private string givenAnswer;
    private float countdownValue = 5f;
    private bool animationHasBeenCalled = false;

    private PlayableDirector playableDirector;

    [Header("Audio")]
    [SerializeField] private AudioClip correctAnswerClip;
    [SerializeField] private AudioClip wrongAnswerClip;
    [SerializeField] private AudioClip clickSoundClip;

    private AudioSource clickSoundSource;


    private void Awake()
    {
        animations = FindObjectOfType<TimelineHandler>();
        player = FindObjectOfType<PlayerCore>().transform;
        playerCarChecker = FindObjectOfType<PlayerCarChecker>();
        playableDirector = FindObjectOfType<PlayableDirector>();
        clickSoundSource = GetComponent<AudioSource>();

        //if (question == null)
        //    throw new System.Exception("question");

        //else if (questionsHolderPlayer == null)
        //    throw new System.Exception("questionsHolderPlayer");

        //else if (questionsHolderCar == null)
        //    throw new System.Exception("questionsHolderCar");

        //else if (playerInCarPosition == null)
        //    throw new System.Exception("playerInCarPosition");

        //else if (playerCarChecker == null)
        //    throw new System.Exception("playerCarChecker");

        //else if (animations == null)
        //    throw new System.Exception("animations");

        //else if (player == null)
        //    throw new System.Exception("player");

        //else if (givenAnswer == null)
        //    throw new System.Exception("givenAnswer");
    }

    private void Start()
    {
        questionsHolderPlayer.SetQuestion(question);
        questionsHolderCar.SetQuestion(question);
    }

    public void CheckAnswer(string pGivenAnswer)
    {
        givenAnswer = pGivenAnswer;

        if (!playerCarChecker.playerIsInCar)
            TeleportToTPPos();

        questionsHolderPlayer.ShowAnimationScreen();
        questionsHolderCar.ShowAnimationScreen();

        StartCoroutine(StartCountdown());
    }

    private void TeleportToTPPos()
    {
        player.position = playerInCarPosition.position;
        player.rotation = playerInCarPosition.rotation;
    }

    private void StartAnimation()
    {
        animations.StartTimeline(givenAnswer);
        StartCoroutine(PlayTimelineRoutine(playableDirector, ShowAnswer));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            CheckAnswer("A");
        else if (Input.GetKeyDown(KeyCode.O))
            CheckAnswer("B");
        else if (Input.GetKeyDown(KeyCode.P))
            CheckAnswer("C");

        if (countdownValue <= 0 && !animationHasBeenCalled)
        {
            StartAnimation();
            animationHasBeenCalled = true;
        }
    }

    private void ShowAnswer()
    {
        questionsHolderPlayer.ShowFeedbackScreen();
        questionsHolderCar.ShowFeedbackScreen();

        if (givenAnswer == question.correctAnswer)
        {
            questionsHolderPlayer.CorrectAnswer();
            questionsHolderCar.CorrectAnswer();
            clickSoundSource.PlayOneShot(correctAnswerClip);
        }
        else
        {
            questionsHolderPlayer.WrongAnswer();
            questionsHolderCar.WrongAnswer();
            clickSoundSource.PlayOneShot(wrongAnswerClip);
        }
    }

    private IEnumerator PlayTimelineRoutine(PlayableDirector playableDirector, Action onComplete)
    {
        playableDirector.Play();
        yield return new WaitForSeconds((float)playableDirector.duration);
        onComplete();
    }

    private IEnumerator StartCountdown()
    {
        while (countdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            countdownValue--;
            questionsHolderPlayer.countdown.text = countdownValue.ToString();
            questionsHolderCar.countdown.text = countdownValue.ToString();
        }
    }
}
