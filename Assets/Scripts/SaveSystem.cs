using UnityEngine;
using UnityEngine.Android;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SaveAnimalData(character savedata)
    {
        #if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
        #endif

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/"+savedata.ID + "Data.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        AnimalSaveData data = new AnimalSaveData(savedata);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static AnimalSaveData LoadAnimalData(character loaddata)
    {

        #if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
            }
        #endif

        string path = Application.persistentDataPath + "/" + loaddata.ID + "Data.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //formatter.Deserialize(stream);
            object DeserializedStream = formatter.Deserialize(stream);
            stream.Close();
            AnimalSaveData data = (AnimalSaveData)DeserializedStream;
            return data;
        }else
        {
            Debug.LogWarning("<color=orange>Save not found at " + path + "</color>");
            return null;
        }
    }

    public static void ResetSaves()
    {
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filePath in filePaths)
        {
            Debug.Log("REMOVED: " + Path.GetFileName(filePath));
            File.Delete(filePath);
        }
            GameManager.Instance.player.SetChar(GameManager.Instance.player.CurrentChar.ID);
        return;
    }

    public static void SavePollutionData(PollutionSystem savedata)
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
#endif

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PollutionData.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        PollutionSaveData data = new PollutionSaveData(savedata);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PollutionSaveData LoadPollutionData()
    {

#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
#endif

        string path = Application.persistentDataPath + "/PollutionData.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //formatter.Deserialize(stream);
            object DeserializedStream = formatter.Deserialize(stream);
            stream.Close();
            PollutionSaveData data = (PollutionSaveData)DeserializedStream;
            return data;
        }
        else
        {
            Debug.LogWarning("<color=orange>Save not found at " + path + "</color>");
            return null;
        }
    }
}
