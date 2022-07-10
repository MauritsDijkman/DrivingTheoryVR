using UnityEngine;
using TMPro;

public class FPS_Counter : MonoBehaviour
{
    private TextMeshPro FPS_Text;

    private void Awake()
    {
        FPS_Text = this.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (FPS_Text != null)
        {
            float currentFPS = (int)(1f / Time.deltaTime);

            if (Time.frameCount % 60 == 0)
                FPS_Text.text = "FPS: " + currentFPS.ToString();
        }
        else
            throw new System.Exception("No TextMeshPro found on GameObject where script is located.");
    }
}
