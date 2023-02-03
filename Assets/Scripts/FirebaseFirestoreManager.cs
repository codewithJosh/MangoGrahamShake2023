using Firebase.Firestore;
using UnityEngine;

public class FirebaseFirestoreManager : MonoBehaviour
{

    private FirebaseFirestore firebaseFirestore;

    private void Awake()
    {

        firebaseFirestore = FirebaseFirestore.DefaultInstance;

    }

    public FirebaseFirestore Firestore
    {

        get { return firebaseFirestore; }

    }

}
