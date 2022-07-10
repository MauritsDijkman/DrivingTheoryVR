using UnityEngine;
using UnityEngine.SceneManagement;

public class AnswerInteractible : MonoBehaviour
{
    private Color originalColor;
    public Color newColor;

    private QuestionHandler questionHandler;

    private string buttonName;

    private void Awake()
    {
        questionHandler = FindObjectOfType<QuestionHandler>();

        //Ensure the gameobject is on the "interactable" layer.
        gameObject.layer = 7;
    }

    private void Start()
    {
        originalColor = this.GetComponent<Renderer>().material.color;

        if (this.gameObject.name == "AnswerA")
            buttonName = "A";
        else if (this.gameObject.name == "AnswerB")
            buttonName = "B";
        else if (this.gameObject.name == "AnswerC")
            buttonName = "C";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
            this.GetComponent<Renderer>().material.color = newColor;
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<Renderer>().material.color = originalColor;
    }

    public void CheckAnswer()
    {
        questionHandler.CheckAnswer(buttonName);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            TryAgain();
        else if (Input.GetKeyDown(KeyCode.K))
            NextScene();
        else if (Input.GetKeyDown(KeyCode.J))
            GoToMainMenu();
    }
}
