using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public character CurrentChar;

    [SerializeField]
    GameObject CharContainer;

    //Health bar & XP bar
    public int maxHealth = 100;
    public int maxXP = 100;
    public int currentXP;
    public Image Portrait;
    public HealthBar healthBar;
    public XPScript xp;
    //Health bar sprites
    public Sprite HappyTurtle;
    public Sprite NormalTurtle;
    public Sprite SadgeTurtle;
    public Image HealthBarSprite;
    //Inventory
    public InventoryObject inventory;
    //Level text
    public string textValue;
    public Text textElement;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.player = this;
        SetChar(0);
    }


    public void AddExp(int amount)
    {
        if (CurrentChar.CurrentPerk != null && CurrentChar.CurrentPerk.modifier == PerkModifiers.XPModifier)
        {
            float Modified = amount * CurrentChar.CurrentPerk.value;
            amount = Mathf.CeilToInt(Modified);
        }
        Debug.Log("xppp " + amount);
        CurrentChar.Exp += amount;
        xp.SetXP(CurrentChar.Exp);

        //if exp >= than than lvl requirement, level up.
        if (CurrentChar.Exp >= CurrentChar.NextLevelReq)
        {
            CurrentChar.Level += 1;
            maxHealth = 100 + (CurrentChar.Level * 5);
            UpdateMaxhealth();
            Debug.Log(CurrentChar.Name + " leveled up! Level " + CurrentChar.Level);
            //calculate xp needed for next level
            int prevReq = CurrentChar.NextLevelReq;
            //CurrentChar.NextLevelReq = prevReq + (CurrentChar.GrowthRate * (CurrentChar.Level + 1));
            CurrentChar.NextLevelReq = (CurrentChar.GrowthRate * (CurrentChar.Level + 1));
            xp.SetMaxXP(CurrentChar.NextLevelReq);
            //after getting level up, adds 0 xp just to check if player skipped a level(), also resets xp back to 0
            CurrentChar.Exp -= prevReq;
            AddExp(0);
        }

        //after level up check, save character.
        SaveSystem.SaveAnimalData(CurrentChar);

    }
    // assigns a perk to the player(Invalid IDs have no effect)
    public void SetPerk(int ID, bool Manual = true)
    {
        foreach (Perk perk in CurrentChar.PerkList)
        {
            if (perk.ID == ID)
            {
                CurrentChar.CurrentPerk = perk;
                if (Manual)
                {
                    SetChar(CurrentChar.ID);
                }
                break;
            }
        }
    }

    public Perk SearchPerkInCurr(int ID)
    {
        foreach (Perk perk in CurrentChar.PerkList)
        {
            if (perk.ID == ID)
            {
                return perk;
            }
        }
        return null;
    }

    //sets the selected character based on character's ID value;
    public void SetChar(int CharID){

        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

        if (CurrentChar.Name != "")
        {
            CurrentChar.LastUsed = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            SaveSystem.SaveAnimalData(CurrentChar);
        }

        CurrentChar = GameManager.Instance.CharList[CharID];

        //looks for a save for the animal, if not found, creates a new, default one.
        if (SaveSystem.LoadAnimalData(CurrentChar) == null)
        {
            Debug.Log("<color=green>Creating new save for character ID: " + CurrentChar.ID + " Name: " + CurrentChar.Name + "</color>");
            CurrentChar.NextLevelReq = CurrentChar.BaseLevelReq;
            SaveSystem.SaveAnimalData(CurrentChar);
        }else
        {
            //loads save data
            AnimalSaveData data = SaveSystem.LoadAnimalData(CurrentChar);

            //sets loaded values
            SetPerk(data.Perk, false);
            CurrentChar.Name = data.Name;
            CurrentChar.Level = data.Level;
            CurrentChar.Exp = data.Exp;
            CurrentChar.NextLevelReq = data.NextLevelReq;
            CurrentChar.Energy = data.Energy;
            CurrentChar.LastUsed = data.TimeStamp;

            for (int i = 0; i < data.PerksUnlocked.Length; i++)
            {
                CurrentChar.PerkList[i].Unlocked = data.PerksUnlocked[i];
            }

            if (CurrentChar.CurrentPerk != null && CurrentChar.CurrentPerk.modifier == PerkModifiers.EnergyModifier)
            {
                maxHealth = Mathf.CeilToInt((100 + (CurrentChar.Level * 5)) * CurrentChar.CurrentPerk.value);
            }
            else
            {
                maxHealth = 100 + (CurrentChar.Level * 5);
            }

            //adds more energy that recharged over time
            if (data.TimeStamp != 0)
            {
                Debug.Log(((((float)(System.DateTime.UtcNow - epochStart).TotalSeconds - data.TimeStamp) / 60) / 60) * 8);
                if (CurrentChar.CurrentPerk != null && CurrentChar.CurrentPerk.modifier == PerkModifiers.EnergyRecoveryModifier)
                {
                    CurrentChar.Energy += (((((float)(System.DateTime.UtcNow - epochStart).TotalSeconds - data.TimeStamp) / 60) / 60) * 8) * CurrentChar.CurrentPerk.value;
                }
                else
                {
                    CurrentChar.Energy += ((((float)(System.DateTime.UtcNow - epochStart).TotalSeconds - data.TimeStamp) / 60) /60) * 8;
                }

                if (CurrentChar.Energy > maxHealth)
                {
                    CurrentChar.Energy = maxHealth;
                }
            }
            UpdateMaxhealth();
            Updatehealth();
            currentXP = CurrentChar.Exp;
        }
        xp.SetMaxXP(CurrentChar.NextLevelReq);
        xp.SetXP(CurrentChar.Exp);
        Portrait.sprite = CurrentChar.Portrait;
        SpawnCharacter();
    }

    //Spawns character and removes the previous one.
    void SpawnCharacter(bool ClearPrevious = true)
    {
        if (ClearPrevious)
        {
            foreach (Transform child in CharContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }

        Instantiate(CurrentChar.Prefab, CharContainer.transform);
    }

    private void Update()
    {
        //To be changed when we know how to decrase/increase the health
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    TakeDamage(20);
        //}
        // Turtle sprites
        //if (currentHealth == 100)
        //  {
        // HealthBarSprite.sprite = HappyTurtle;
        // }
        //if (currentHealth == 60)
        // {
        // HealthBarSprite.sprite = NormalTurtle;
        // }
        // if (currentHealth == 20)
        // {
        // HealthBarSprite.sprite = SadgeTurtle;
        //}
        //Level text stuff
        textElement.text = textValue;
        textValue = CurrentChar.Level.ToString();

    }
    //TakeDamage
    public void TakeDamage(float damage)
    {
        CurrentChar.Energy -= damage;
        healthBar.SetHealth(CurrentChar.Energy);
    }

    //TakeDamage
    public void Updatehealth()
    {
        healthBar.SetHealth(CurrentChar.Energy);
    }

    //TakeDamage
    public void UpdateMaxhealth()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeXP(int damage)
    {
        currentXP -= damage;

        xp.SetXP(currentXP);
    }
    //Invetory
    public void OnMouseUp()
    {
        var item = GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
        }
    }
    public void OnApplicationQuit()
    {
        //TODO: properly save inventory
        //inventory.Container.Clear();
    }
}
