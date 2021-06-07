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

    public AnimalSaveData (character CharStats)
    {
        Name = CharStats.Name;
        Level = CharStats.Level;
        Exp = CharStats.Exp;
        NextLevelReq = CharStats.NextLevelReq;
        Energy = CharStats.Energy;
        TimeStamp = CharStats.LastUsed;
    }
}


