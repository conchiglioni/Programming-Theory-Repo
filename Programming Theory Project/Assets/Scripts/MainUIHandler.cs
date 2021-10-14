using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIHandler : MonoBehaviour
{
    public GameObject overlay;
    public GameObject controlsOverlay;
    public Button activateOverlay;
    public Button activateControlsOverlay;
    public TextMeshProUGUI greetingText;
    public TextMeshProUGUI scaleText;
    public TextMeshProUGUI sideText;
    public float scale;
    public int sideNumber;
    public Slider scaleSlider;
    public Slider sideSlider;
    public GameObject meshGeneratorPrefab;
    public float gravityModifier;

    // Start is called before the first frame update
    void Start()
    {
        // If we have a name from the MainManager, greet the player in the overlay
        if(MainManager.Instance != null)
        {
            greetingText.text = $"User: " + MainManager.Instance.playerName;
        }
        Physics.gravity *= gravityModifier;
        // ABSTRACTION
        UpdateScale();
        UpdateSideNumber();
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

    public void OpenControlsOverlay()
    {
        controlsOverlay.SetActive(true);
        activateControlsOverlay.gameObject.SetActive(false);
    }

    public void CloseControlsOverlay()
    {
        controlsOverlay.SetActive(false);
        activateControlsOverlay.gameObject.SetActive(true);
    }

    public void CreateMeshGenerator()
    {
        // Create a new base Mesh Generator prefab
        GameObject meshGenerator = Instantiate(meshGeneratorPrefab, new Vector3(0, 5, 0), meshGeneratorPrefab.transform.rotation);
        // Call the CreateObject method, which generates a mesh based on sideNumber
        meshGenerator.GetComponent<MeshGenerator>().CreateObject(sideNumber, scale);
        // Add a drag handler so the user can drag the generated object
        meshGenerator.AddComponent<DragHandler>();
    }
}
