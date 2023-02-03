using Google;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAuthManager : MonoBehaviour
{

    [SerializeField]
    private string webClientId;

    private GoogleSignInConfiguration configuration;

    private void Awake()
    {

        configuration = new GoogleSignInConfiguration
        {

            WebClientId = webClientId,
            RequestEmail = true,
            RequestIdToken = true

        };

    }

    private void SignIn()
    {

        GoogleSignIn.Configuration = configuration;
        GoogleSignIn
            .Configuration
            .UseGameSignIn = false;
        GoogleSignIn
            .Configuration
            .RequestIdToken = true;
        GoogleSignIn
            .DefaultInstance
            .SignIn()
            .ContinueWith(task =>
            {

                if (task.IsFaulted)
                {

                    using IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator();

                    if (enumerator.MoveNext())
                    {

                        GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                        FindObjectOfType<DialogManager>().OnDialog(
                            "FAILED",
                            "Got Error: " + error.Status + " " + error.Message,
                            "dialog"
                            );

                    }
                    else
                        FindObjectOfType<DialogManager>().OnDialog(
                            "FAILED",
                            "Got Unexpected Exception?!?" + task.Exception,
                            "dialog"
                            );

                }
                else if (task.IsCanceled)
                {

                    FindObjectOfType<DialogManager>().OnDialog(
                        "FAILED",
                        "Canceled",
                        "dialog"
                        );

                }
                else
                {

                    string idToken = task.Result.IdToken;

                    if (idToken != null)

                        FindObjectOfType<FirebaseAuthManager>().OnSignInWithGoogleOnFirebase(idToken);

                }

            });

    }

    public void OnLogin() { SignIn(); }

}
