using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
public static class SaveLoad
{
    public static Game savedProgress;

    public static void Save()
    {
        savedProgress = Game.current;
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedProgress.gd";
        FileStream stream = new FileStream(path, FileMode.Create);
        bf.Serialize(stream, Game.current);
        stream.Close();

    }

    public static Game LoadGame()
    {
        string path = Application.persistentDataPath + "/savedProgress.gd";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Game loadedGame = formatter.Deserialize(stream) as Game;
            stream.Close();
            return loadedGame;
        }
        else
        {
            Debug.LogError("Saved file not found in " + path);
            return null;
        }
    }
}
