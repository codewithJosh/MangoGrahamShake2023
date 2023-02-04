using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Database
{

    public static void SavePlayer(FirebasePlayerModel _firebasePlayerModel)
    {

        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/player.mango";
        FileStream stream = new(path, FileMode.Create);

        PlayerModel playerModel = new(_firebasePlayerModel);
        formatter.Serialize(stream, playerModel);
        stream.Close();

    }

    public static void SavePlayer(Player _player)
    {

        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/player.mango";
        FileStream stream = new(path, FileMode.Create);

        PlayerModel playerModel = new(_player);
        formatter.Serialize(stream, playerModel);
        stream.Close();

    }

    public static PlayerModel LoadPlayer()
    {

        string path = Application.persistentDataPath + "/player.mango";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            PlayerModel playerModel = formatter.Deserialize(stream) as PlayerModel;
            stream.Close();

            return playerModel;

        }
        else
        {

            Debug.Log("Savefile Not Found in " + path);
            return null;

        }

    }

}
