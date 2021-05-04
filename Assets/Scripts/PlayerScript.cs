using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    character CurrentChar;

    [SerializeField]
    GameObject CharContainer;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.player = this;
        SetChar(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetChar(int CharID){
        CurrentChar = GameManager.Instance.CharList[CharID];
        SpawnCharacter();
    }

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
}
