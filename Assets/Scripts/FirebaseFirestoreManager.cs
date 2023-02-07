using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class FirebaseFirestoreManager : MonoBehaviour
{

    private DocumentReference documentRef;
    private FirebaseFirestore firebaseFirestore;

    void Awake()
    {

        firebaseFirestore = FirebaseFirestore.DefaultInstance;

    }

    private void GlobalSave(PlayerStruct _playerStruct)
    {

        string playerId = PlayerPrefs.GetString("player_id", "");

        if (!playerId.Equals(""))
        {

            documentRef = FirebaseFirestore
            .Collection("Players")
            .Document(playerId);

            documentRef
                .GetSnapshotAsync()
                .ContinueWithOnMainThread(task =>
                {

                    DocumentSnapshot doc = task.Result;

                    if (doc != null && doc.Exists)

                        documentRef
                        .SetAsync(_playerStruct)
                        .ContinueWithOnMainThread(task =>
                        {

                            FindObjectOfType<DialogManager>().OnDialog(
                                "SUCCESS",
                                "Saved!",
                                "dialog");

                        });

                });

        }

    }

    public FirebaseFirestore FirebaseFirestore { get => firebaseFirestore; }

    public void OnGlobalSave(PlayerStruct _playerStruct) { GlobalSave(_playerStruct); }

}
