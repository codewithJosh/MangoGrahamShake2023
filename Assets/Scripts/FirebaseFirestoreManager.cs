using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseFirestoreManager : MonoBehaviour
{

    private FirebaseFirestore firebaseFiretore;

    // Start is called before the first frame update
    void Start()
    {

        firebaseFiretore = FirebaseFirestore.DefaultInstance;

        DocumentReference documentRef = firebaseFiretore.Collection("Players").Document("Player1");

        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary.Add("Test", 1);

        //documentRef.SetAsync(dictionary).ContinueWithOnMainThread(task => { FindObjectOfType<DialogManager>().OnDialog("SUCCESS", "Test"); });

        documentRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {

            DocumentSnapshot doc = task.Result;

            if (doc != null)
            {

                if (doc.Exists)
                {
                    FindObjectOfType<DialogManager>().OnDialog("SUCCESS", "Document Exists");
                }
                else
                {
                    FindObjectOfType<DialogManager>().OnDialog("SUCCESS", "Document Does not Exists");
                }

            }
            else
            {
                FindObjectOfType<DialogManager>().OnDialog("SUCCESS", "Collection Does not Exists");
            }

        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}
