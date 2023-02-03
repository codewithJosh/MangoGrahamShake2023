using Firebase;
using Firebase.Auth;
using System;
using UnityEngine;

public class FirebaseAuthManager : MonoBehaviour
{

    private FirebaseAuth firebaseAuth;

    private void Awake()
    {

        CheckFirebaseDependencies();

    }

    private void CheckFirebaseDependencies()
    {

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {

            if (task.IsCompleted)
            {

                if (task.Result == DependencyStatus.Available)
                {

                    firebaseAuth = FirebaseAuth.DefaultInstance;

                }
                else
                    FindObjectOfType<DialogManager>().OnDialog(
                        "FAILED",
                        "Could not resolve all Firebase dependencies: " + task.Result.ToString()
                        );

            }
            else
                FindObjectOfType<DialogManager>().OnDialog(
                    "FAILED",
                    "Dependency check was not completed. Error : " + task.Exception.Message
                    );

        });

    }

    private void SignInWithGoogleOnFirebase(string _idToken)
    {

        Credential credential = GoogleAuthProvider.GetCredential(_idToken, null);

        firebaseAuth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {

            AggregateException ex = task.Exception;
            if (ex != null)
            {

                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    FindObjectOfType<DialogManager>().OnDialog(
                        "FAILED",
                        "Error code = " + inner.ErrorCode + " Message = " + inner.Message
                        );

            }
            else
            {

                FindObjectOfType<LoginManager>().OnLoginSuccess();

            }

        });

    }

    public FirebaseAuth Auth
    {

        get { return firebaseAuth; }

    }

    public void OnSignInWithGoogleOnFirebase(string _idToken) { SignInWithGoogleOnFirebase(_idToken); }

}