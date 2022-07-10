using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    private Color originalColor;
    public Color newColor;

    private QuestionHandler questionHandler;

    private string buttonName;

    private void Awake()
    {
        questionHandler = FindObjectOfType<QuestionHandler>();
    }

    private void Start()
    {
        originalColor = this.GetComponent<Renderer>().material.color;

        if (this.gameObject.name == "AnswerA")
        {
            Debug.Log($"{this.gameObject.name} = {buttonName}");
            buttonName = "A";
        }
        else if (this.gameObject.name == "AnswerB")
        {
            Debug.Log($"{this.gameObject.name} = {buttonName}");
            buttonName = "B";
        }
        else if (this.gameObject.name == "AnswerC")
        {
            Debug.Log($"{this.gameObject.name} = {buttonName}");
            buttonName = "C";
        }
    }

    public void ChangeColor()
    {
        this.GetComponent<Renderer>().material.color = newColor;
    }

    public void OriginalColor()
    {
        this.GetComponent<Renderer>().material.color = originalColor;
    }

    public void Action()
    {
        questionHandler.CheckAnswer(buttonName);
    }
}
