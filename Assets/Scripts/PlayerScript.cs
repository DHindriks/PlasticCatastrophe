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
    public int currentHealth;
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

        //Setting the health at the beginning 
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //Setting the xp at the beginning
        currentXP = CurrentChar.Exp;
    }


    public void AddExp(int amount)
    {
        CurrentChar.Exp += amount;
        xp.SetXP(CurrentChar.Exp);

        //if exp >= than than lvl requirement, level up.
        if (CurrentChar.Exp >= CurrentChar.NextLevelReq)
        {
            CurrentChar.Level += 1;
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


    //sets the selected character based on character's ID value;
    public void SetChar(int CharID){

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
            CurrentChar.Name = data.Name;
            CurrentChar.Level = data.Level;
            CurrentChar.Exp = data.Exp;
            CurrentChar.NextLevelReq = data.NextLevelReq;
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
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
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
