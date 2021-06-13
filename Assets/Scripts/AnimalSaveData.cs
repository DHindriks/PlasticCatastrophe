using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimalSaveData {
    public string Name;
    public int Level;
    public int Exp;
    public int NextLevelReq;
    public float Energy;
    public int TimeStamp;
    public int Perk;

    public bool[] PerksUnlocked;

    public AnimalSaveData (character CharStats)
    {
        Name = CharStats.Name;
        Level = CharStats.Level;
        Exp = CharStats.Exp;
        NextLevelReq = CharStats.NextLevelReq;
        Energy = CharStats.Energy;
        TimeStamp = CharStats.LastUsed;
        Perk = CharStats.CurrentPerk.ID;
        PerksUnlocked = new bool[CharStats.PerkList.Count];
        for(int i = 0; i < CharStats.PerkList.Count; i++)
        {
            PerksUnlocked[i] = CharStats.PerkList[i].Unlocked;
        }
    }
}

[System.Serializable]
public class PollutionSaveData
{
    public float Value;
    public int TimeStamp;
    public PollutionSaveData(PollutionSystem pollution)
    {
        Value = pollution.PollutionValue;
        TimeStamp = pollution.TimeStamp;
    }
}

