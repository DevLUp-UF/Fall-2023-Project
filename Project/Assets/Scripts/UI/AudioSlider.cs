using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField] 
    private TextMeshProUGUI valueText;
    [Header("Only input 'master', 'sfx', or 'music'")]
    [SerializeField]
    private string sourceName;

    // Start is called before the first frame update
    public void OnChangeSlider(float value)
    {
        valueText.SetText($"{value.ToString("N1")}");
        mixer.SetFloat(sourceName, (Mathf.Log10(value/100) * 20));
    }
}
