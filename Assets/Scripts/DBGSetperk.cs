using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DBGSetperk : MonoBehaviour
{
    [SerializeField]
    Button BTN;

    [SerializeField]
    TMP_InputField TextField;


    public void SetID()
    {
        int Number;
        if (int.TryParse(TextField.text, out Number)) {
            BTN.onClick.RemoveAllListeners();
            BTN.onClick.AddListener(delegate { GameManager.Instance.player.SetPerk(Number); });
        }
    }
}
