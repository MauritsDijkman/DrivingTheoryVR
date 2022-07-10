using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private AudioMixer audioMixer = null;

    private void Start()
    {
        SetUIValues(getVolume());
    }

    private float getVolume()
    {
        return PlayerPrefs.GetFloat("Volume", 0.0f);
    }

    public void ChangeVolume(float addedVolume)
    {
        float currentVolume = getVolume();
        float newVolume = Mathf.Clamp(currentVolume += addedVolume, -80, 0);
        PlayerPrefs.SetFloat("Volume", newVolume);

        SetUIValues(newVolume);
    }

    private void SetUIValues(float volume)
    {
        slider.value = volume;
        audioMixer.SetFloat("volume", volume);

        float volumeTextPercentage = 100;

        if (volume == 0)
            volumeTextPercentage = 100;
        else if (volume == -8)
            volumeTextPercentage = 90;
        else if (volume == -16)
            volumeTextPercentage = 80;
        else if (volume == -24)
            volumeTextPercentage = 70;
        else if (volume == -32)
            volumeTextPercentage = 60;
        else if (volume == -40)
            volumeTextPercentage = 50;
        else if (volume == -48)
            volumeTextPercentage = 40;
        else if (volume == -56)
            volumeTextPercentage = 30;
        else if (volume == -64)
            volumeTextPercentage = 20;
        else if (volume == -72)
            volumeTextPercentage = 10;
        else if (volume == -80)
            volumeTextPercentage = 0;

        text.text = volumeTextPercentage.ToString() + "%";
    }
}
