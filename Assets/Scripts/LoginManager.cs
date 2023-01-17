using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Google;
using Firebase;
using System.Collections.Generic;
using System;

public class LoginManager : MonoBehaviour
{

    [SerializeField] private Button loginUIButton;
    [SerializeField] private TextMeshProUGUI statusUIText;
    [SerializeField] private string webClientId;

    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;

    private void Awake()
    {

        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestEmail = true,
            RequestIdToken = true
        };

        CheckFirebaseDependencies();

    }

    void Update()
    {

        loginUIButton.interactable = Application.internetReachability != NetworkReachability.NotReachable;

        statusUIText.text = 
            loginUIButton.IsInteractable()
            ? "Let's Get Started!"
            : "No Internet Connection!";

        if (SimpleInput.GetButtonDown("OnLogin") && loginUIButton.IsInteractable()) 
            OnLogin();

    }

    private void CheckFirebaseDependencies()
    {

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {

            if (task.IsCompleted)
            {

                if (task.Result == DependencyStatus.Available)
                    auth = FirebaseAuth.DefaultInstance;
                else
                    FindObjectOfType<Toast>().OnToast("Could not resolve all Firebase dependencies: " + task.Result.ToString());

            }
            else
                FindObjectOfType<Toast>().OnToast("Dependency check was not completed. Error : " + task.Exception.Message);
        
        });

    }

    private void OnLogin()
    {

        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(task =>

        {

            if (task.IsFaulted)
            {

                using IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator();

                if (enumerator.MoveNext())
                {

                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    FindObjectOfType<Toast>().OnToast("Got Error: " + error.Status + " " + error.Message);

                }
                else
                    FindObjectOfType<Toast>().OnToast("Got Unexpected Exception?!?" + task.Exception);

            }
            else if (task.IsCanceled)
            {

                FindObjectOfType<Toast>().OnToast("Canceled");

            }
            else
            {

                string idToken = task.Result.IdToken;

                if (idToken != null) 
                    SignInWithGoogleOnFirebase(idToken);

            }

        });

    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {

        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {

            AggregateException ex = task.Exception;
            if (ex != null)
            {

                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    FindObjectOfType<Toast>().OnToast("Error code = " + inner.ErrorCode + " Message = " + inner.Message);
            
            }
            else
                FindObjectOfType<Toast>().OnToast("Sign In Successful");

        });

    }

}
