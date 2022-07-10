using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using OVR;
using UnityEngine;

public class WorldUIButton : MonoBehaviour
{
    Image image = null;

    Color originalColor = Color.white;
    [SerializeField] Color hoverColor = Color.black;
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] UnityEvent onPress = null;

    void Awake()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    private void Start()
    {
        if (text == null)
            return;

        if (text.text == "Teleport")
            PlayerPrefs.SetInt("Movement", 0);
        else if (text.text == "Stick")
            PlayerPrefs.SetInt("Movement", 1);
    }

    public void OnHoverEnter()
    {
        image.color = hoverColor;
    }
    public void OnHoverExit()
    {
        image.color = originalColor;
    }

    //HARDCODE
    public void SetText()
    {
        if (text.text == "Teleport")
        {
            text.text = "Stick";
            PlayerPrefs.SetInt("Movement", 1);
        }
        else if (text.text == "Stick")
        {
            text.text = "Teleport";
            PlayerPrefs.SetInt("Movement", 0);
        }
    }

    public void OnPressed()
    {
        onPress?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }
}
