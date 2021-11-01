using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SaveData(bool[] planetTally)
    {
        //Creates the binary formatter and opens the file stream, path is relative to the project
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/sav.toast";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveScript SaveData = new SaveScript(planetTally);

        formatter.Serialize(stream, SaveData);
        stream.Close();
    }
    public static SaveScript LoadData()
    {
        string path = Application.persistentDataPath + "/sav.toast";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveScript SaveData = formatter.Deserialize(stream) as SaveScript;
            stream.Close();

            return SaveData;
        }
        else
        {
            Debug.LogError("FILE NOT FOUND:" + path);
            return null;
        }
    }

}
