using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class FirebaseFirestoreManager : MonoBehaviour
{

    #region DECLARATION

    private static DocumentReference documentRef;

    #endregion

    #region AWAKE_METHOD

    void Awake()
    {

        STATUS.FIREBASE_FIRESTORE = FirebaseFirestore.DefaultInstance;

    }

    #endregion

    #region  GLOBAL_SAVE_METHOD

    private static void GlobalSave(PlayerStruct _playerStruct)
    {

        string playerId = PlayerPrefs.GetString("player_id", "");

        if (playerId.Equals(""))

            return;

        documentRef = STATUS.FIREBASE_FIRESTORE
            .Collection("Players")
            .Document(playerId);

        documentRef
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {

                DocumentSnapshot doc = task.Result;

                if (doc != null 
                && doc.Exists)

                    documentRef
                    .SetAsync(_playerStruct);

            });

    }

    #endregion

    #region AUTOMATED_PROPERTY

    public static void OnGlobalSave(PlayerStruct _playerStruct) => GlobalSave(_playerStruct);

    #endregion

}
