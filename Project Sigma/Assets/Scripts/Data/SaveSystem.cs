﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveProgress (PlayerProgress progress)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.oho";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(progress);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadProgress ()
    {
        string path = Application.persistentDataPath + "/progress.oho";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                return data;
            }



        }
        Debug.LogError("Save file not found in " + path);
        return null;
    }
}
