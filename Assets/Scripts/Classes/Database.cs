using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Database
{

    #region LOCAL_SAVE_METHODS

    public static void LocalSave(PlayerStruct _playerStruct)
    {

        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/player.mango";
        FileStream stream = new(path, FileMode.Create);

        PlayerModel playerModel = new(_playerStruct);
        formatter.Serialize(stream, playerModel);
        stream.Close();

    }

    public static void LocalSave(PLAYER _player)
    {

        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/player.mango";
        FileStream stream = new(path, FileMode.Create);

        PlayerModel playerModel = new(_player);
        formatter.Serialize(stream, playerModel);
        stream.Close();

    }

    #endregion

    #region LOCAL_LOAD_PLAYER_METHOD

    public static PlayerModel LocalLoadPlayer()
    {

        string path = Application.persistentDataPath + "/player.mango";

        if (!File.Exists(path))
        {

            Debug.Log("Savefile Not Found in " + path);
            return null;

        }

        BinaryFormatter formatter = new();
        FileStream stream = new(path, FileMode.Open);

        PlayerModel playerModel = formatter.Deserialize(stream) as PlayerModel;
        stream.Close();
        return playerModel;

    }

    #endregion

}
