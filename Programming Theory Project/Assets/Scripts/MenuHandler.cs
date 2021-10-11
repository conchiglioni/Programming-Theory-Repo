using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    private TMP_InputField nameInputField;
    public TextMeshProUGUI errorText;
    string playerName;

    // Start is called before the first frame update
    void Start()
    {
        nameInputField = GameObject.Find("Name Input").GetComponent<TMP_InputField>();
    }

    public void UpdatePlayerName()
    {
        errorText.gameObject.SetActive(false);
        playerName = nameInputField.text;
        MainManager.Instance.playerName = playerName;
    }

    public void SwitchScene()
    {
        if(playerName != null)
        {
            // if player has entered their name, switch scenes
            SceneManager.LoadScene(1);
        }
        else
        {
            // display error
            errorText.gameObject.SetActive(true);
        }
    }

}
