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

    [SerializeField]
    GameObject PromptPrefab;

    [SerializeField]
    GameObject PromptItemPrefab;

    void OnEnable()
    {
        PerkBtns = new List<GameObject>();
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
            if (perk.ID == GameManager.Instance.player.CurrentChar.CurrentPerk.ID || GameManager.Instance.PolSystem.PollutionValue > 80)
            {
                Btn.GetComponent<Button>().interactable = false;
            } else if (!perk.Unlocked)
            {
                Btn.GetComponent<Button>().onClick.AddListener(delegate { ShowCosts(perk); });
            }
            else
            {
                int Number;
                if (int.TryParse(Btn.name, out Number))
                {
                    Btn.GetComponent<Button>().onClick.AddListener(delegate { GameManager.Instance.player.SetPerk(Number); });
                }
                Btn.GetComponent<Button>().onClick.AddListener(delegate { Refresh(); });
            }
            PerkBtns.Add(Btn);
        }
    }

    public void Refresh()
    {
        foreach(GameObject Btn in PerkBtns)
        {
            Btn.GetComponent<Button>().interactable = true;
            Btn.GetComponent<Button>().onClick.RemoveAllListeners();
            Btn.GetComponent<Button>().onClick.AddListener(delegate { Refresh(); });
            int Number;
            if (int.TryParse(Btn.name, out Number))
            {
                if (Number == GameManager.Instance.player.CurrentChar.CurrentPerk.ID || GameManager.Instance.PolSystem.PollutionValue > 80)
                {
                    Btn.GetComponent<Button>().interactable = false;
                }else if (!GameManager.Instance.player.SearchPerkInCurr(Number).Unlocked)
                {
                    Btn.GetComponent<Button>().onClick.AddListener(delegate { ShowCosts(GameManager.Instance.player.SearchPerkInCurr(Number)); });
                }
            }
        }
    }

    void ShowCosts(Perk perk)
    {
        GameObject prompt = Instantiate(PromptPrefab, transform);
        PromptScript promptScript = prompt.GetComponent<PromptScript>();
        bool ItemsCollected = true;

        foreach(InventorySlot item in perk.Cost.Container)
        {
            GameObject ItemDisplay = Instantiate(PromptItemPrefab, promptScript.Grid);
            ItemDisplay.GetComponent<PromptItemScript>().Name.text = item.item.name;
            ItemDisplay.GetComponent<PromptItemScript>().Icon.sprite = item.item.sprite;
            ItemDisplay.GetComponent<PromptItemScript>().Icon.preserveAspect = true;
            if (GameManager.Instance.inventory.GetItem(item.item) == null)
            {
                bool collected = false;
                ItemsCollected = collected;
                break;
            }
        }

        if (!ItemsCollected)
        {
            promptScript.Confirm.interactable = false;
        }else
        {
            promptScript.Confirm.onClick.AddListener(delegate { UnlockPerk(perk); });
        }
        promptScript.Confirm.onClick.AddListener(delegate { Destroy(prompt); });
        promptScript.Confirm.onClick.AddListener(delegate { Refresh(); });
        promptScript.Cancel.onClick.AddListener(delegate { Destroy(prompt); });
        promptScript.Cancel.onClick.AddListener(delegate { Refresh(); });

    }

    void UnlockPerk(Perk perk)
    {
        GameManager.Instance.player.SetPerk(perk.ID);
        GameManager.Instance.player.CurrentChar.CurrentPerk.Unlocked = true;

        foreach (InventorySlot item in perk.Cost.Container)
        {
            GameManager.Instance.inventory.RemoveItem(item.item, 1);
        }
    }

}
