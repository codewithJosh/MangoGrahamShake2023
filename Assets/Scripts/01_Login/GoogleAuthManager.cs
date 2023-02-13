using Google;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAuthManager : MonoBehaviour
{

    [SerializeField]
    private string webClientId;

    private GoogleSignInConfiguration configuration;

    void Awake()
    {

        configuration = new GoogleSignInConfiguration
        {

            WebClientId = webClientId,
            RequestEmail = true,
            RequestIdToken = true

        };

        GoogleSignIn.Configuration = configuration;
        GoogleSignIn
            .Configuration
            .UseGameSignIn = false;
        GoogleSignIn
            .Configuration
            .RequestIdToken = true;
        GoogleAuth = GoogleSignIn.DefaultInstance;

    }

    private void SignIn()
    {

        GoogleAuth
            .SignIn()
            .ContinueWith(task =>
            {

                if (task.IsFaulted)
                {

                    using IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator();

                    if (enumerator.MoveNext())
                    {

                        FindObjectOfType<SoundsManager>().OnError();
                        GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                        FindObjectOfType<DialogManager>().OnDialog(
                            "FAILED",
                            "Got Error: " + error.Status + " " + error.Message,
                            "dialog");

                    }
                    else
                    {

                        FindObjectOfType<SoundsManager>().OnError();
                        FindObjectOfType<DialogManager>().OnDialog(
                            "FAILED",
                            "Got Unexpected Exception?!?" + task.Exception,
                            "dialog");

                    }

                }
                else if (task.IsCanceled)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "FAILED",
                        "Canceled",
                        "dialog");

                }
                else
                {

                    string idToken = task.Result.IdToken;

                    if (idToken != null)

                        FindObjectOfType<FirebaseAuthManager>().OnSignInWithGoogleOnFirebase(idToken);

                }

            });

    }

    public GoogleSignIn GoogleAuth { get; private set; }

    public void OnLogin() { SignIn(); }

}
