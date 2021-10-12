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
    public float scale;
    public int sideNumber;
    public Slider scaleSlider;
    public Slider sideSlider;
    public GameObject meshGeneratorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScale();
        UpdateSideNumber();
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

    public void CreateMeshGenerator()
    {
        GameObject meshGenerator = Instantiate(meshGeneratorPrefab, new Vector3(0, 5, 0), meshGeneratorPrefab.transform.rotation);
        meshGenerator.GetComponent<MeshGenerator>().CreateObject(sideNumber, scale);
        meshGenerator.AddComponent<DragHandler>();
    }
}
