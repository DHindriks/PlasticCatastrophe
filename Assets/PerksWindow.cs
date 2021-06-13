using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerksWindow : MonoBehaviour
{

    [SerializeField]
    GameObject PerkBtnPrefab;

    [SerializeField]
    Transform gridObj;

    List<GameObject> PerkBtns = new List<GameObject>();

    void OnEnable()
    {
        RegeneratePerkList();
    }

    void OnDisable()
    {
        foreach(Transform Btn in gridObj)
        {
            Destroy(Btn.gameObject);
        }
    }

    void RegeneratePerkList()
    {
        foreach(Perk perk in GameManager.Instance.player.CurrentChar.PerkList)
        {
            GameObject Btn = Instantiate(PerkBtnPrefab, gridObj);
            Btn.name = perk.ID.ToString();
            Btn.GetComponentInChildren<Image>().sprite = perk.Icon;
            Btn.GetComponentInChildren<TextMeshProUGUI>().text = perk.Desc;
            if (perk.ID == GameManager.Instance.player.CurrentChar.CurrentPerk.ID || GameManager.Instance.PolSystem.PollutionValue > 80 || !perk.Unlocked)
            {
                Btn.GetComponent<Button>().interactable = false;
            }
            int Number;

            if (int.TryParse(Btn.name, out Number))
            {
                Btn.GetComponent<Button>().onClick.AddListener(delegate { GameManager.Instance.player.SetPerk(Number); });
            }
            Btn.GetComponent<Button>().onClick.AddListener(delegate { Refresh(); });
            PerkBtns.Add(Btn);
        }
    }

    public void Refresh()
    {
        foreach(GameObject Btn in PerkBtns)
        {
            Btn.GetComponent<Button>().interactable = true;

            int Number;
            if (int.TryParse(Btn.name, out Number))
            {
                if (Number == GameManager.Instance.player.CurrentChar.CurrentPerk.ID || GameManager.Instance.PolSystem.PollutionValue > 80 || !GameManager.Instance.player.SearchPerkInCurr(Number).Unlocked)
                {
                    Btn.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}
