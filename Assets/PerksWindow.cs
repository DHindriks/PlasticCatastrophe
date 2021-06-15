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
        ResetPerkList();
    }

    void ResetPerkList()
    {
        foreach (Transform Btn in gridObj)
        {
            Destroy(Btn.gameObject);
        }
    }

    void RegeneratePerkList()
    {
        ResetPerkList();

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
                Btn.GetComponent<Button>().onClick.AddListener(delegate { RegeneratePerkList(); });
            }
            PerkBtns.Add(Btn);
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
            ItemDisplay.GetComponent<PromptItemScript>().Name.text = + item.amount + " " + item.item.name;
            ItemDisplay.GetComponent<PromptItemScript>().Icon.sprite = item.item.sprite;
            ItemDisplay.GetComponent<PromptItemScript>().Icon.preserveAspect = true;
            if (GameManager.Instance.inventory.GetItem(item.item, item.amount) == null)
            {
                ItemDisplay.GetComponent<PromptItemScript>().Icon.color = Color.red;
                bool collected = false;
                ItemsCollected = collected;
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
        promptScript.Confirm.onClick.AddListener(delegate { RegeneratePerkList(); });
        promptScript.Cancel.onClick.AddListener(delegate { Destroy(prompt); });
        promptScript.Cancel.onClick.AddListener(delegate { RegeneratePerkList(); });

    }

    void UnlockPerk(Perk perk)
    {
        GameManager.Instance.player.SetPerk(perk.ID);
        GameManager.Instance.player.CurrentChar.CurrentPerk.Unlocked = true;

        foreach (InventorySlot item in perk.Cost.Container)
        {
            GameManager.Instance.inventory.RemoveItem(item.item, item.amount);
        }
    }

}
