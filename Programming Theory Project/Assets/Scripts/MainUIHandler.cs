using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIHandler : MonoBehaviour
{
    public GameObject overlay;
    public Button activateOverlay;
    public TextMeshProUGUI scaleText;
    public TextMeshProUGUI sideText;
    private float scale;
    private int sideNumber;
    public Slider scaleSlider;
    public Slider sideSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScale()
    {
        scale = scaleSlider.value;
        scaleText.text = $"Object Scale: " + scale.ToString("F1");
    }

    public void UpdateSideNumber()
    {
        sideNumber = Mathf.RoundToInt(sideSlider.value);
        sideText.text = $"# of Sides: " + sideNumber;
    }

    public void CloseOverlay()
    {
        overlay.SetActive(false);
        activateOverlay.gameObject.SetActive(true);
    }

    public void OpenOverlay()
    {
        overlay.SetActive(true);
        activateOverlay.gameObject.SetActive(false);
    }
}
