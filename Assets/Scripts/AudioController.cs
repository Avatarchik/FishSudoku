using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider musicSlider;
    public Slider volumeSlider;

    public void OnChangeMusicVolume()
    {
        this.GetComponent<AudioSource>().volume = musicSlider.value;
    }

    public void OnChangeSoundVolume()
    {
        this.GetComponent<AudioSource>().volume = volumeSlider.value;
    }
}
