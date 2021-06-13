using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PollutionSystem : MonoBehaviour
{
    [SerializeField]
    Gradient SkyColor;

    [SerializeField]
    Material SkyBox;

    public float PollutionValue;

    public int TimeStamp;

    [SerializeField]
    TextMeshProUGUI amountTxt;

    [SerializeField]
    Image IMG;

    [SerializeField]
    Sprite PollutionEmergency;

    // Start is called before the first frame update
    void Start()
    {
        //setup
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.PolSystem = this;

        //load data
        PollutionSaveData Data = SaveSystem.LoadPollutionData();
        if (Data != null)
        {
            PollutionValue = Data.Value;

            //adds pollution added since player last played;
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            AddPollution(((((float)(System.DateTime.UtcNow - epochStart).TotalSeconds - Data.TimeStamp) / 60) / 60) * 1.3f);
        }else
        {
            AddPollution(0);
        }



    }

    public void AddPollution(float amount = 0)
    {
        PollutionValue += amount;
        if (PollutionValue < 0)
        {
            PollutionValue = 0;
        }else if (PollutionValue > 100)
        {
            PollutionValue = 100;
        }else if (PollutionValue > 80 && amount > 0)
        {
            GameManager.Instance.GenPopup("Pollution!", "Too much plastic will make animals weaker.", PollutionEmergency);
            GameManager.Instance.player.SetPerk(-1);
            amountTxt.GetComponent<Animator>().SetBool("Emergency", true);
        }else if (PollutionValue <= 80)
        {
            amountTxt.GetComponent<Animator>().SetBool("Emergency", false);
        }

        amountTxt.text = Mathf.RoundToInt(PollutionValue).ToString();
        IMG.fillAmount = PollutionValue / 100;


        RenderSettings.fogColor = SkyColor.Evaluate(PollutionValue / 100);
        RenderSettings.fogStartDistance = 100 - PollutionValue;
        SkyBox.SetColor("_SkyTint", SkyColor.Evaluate(PollutionValue / 100)); ;
    }

    private void OnApplicationQuit()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        TimeStamp = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        SaveSystem.SavePollutionData(this);
    }
}