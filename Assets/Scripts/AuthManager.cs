using Firebase;
using Firebase.Auth;
using Google;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{

    [SerializeField] private string webClientId;

    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;

    public void OnLogin() { OnSignIn(); }

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

    private void CheckFirebaseDependencies()
    {

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {

            if (task.IsCompleted)
            {

                if (task.Result == DependencyStatus.Available)
                {

                    auth = FirebaseAuth.DefaultInstance;
                    CheckCurrentAuthState();

                }
                else
                    FindObjectOfType<Toast>().OnToast("Could not resolve all Firebase dependencies: " + task.Result.ToString());

            }
            else
                FindObjectOfType<Toast>().OnToast("Dependency check was not completed. Error : " + task.Exception.Message);

        });

    }

    private void CheckCurrentAuthState()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0 && auth.CurrentUser != null)
        {

            PlayerPrefs.SetString("user_id", auth.CurrentUser.UserId);
            SceneManager.LoadScene(1);

        }

    }

    private void OnSignIn()
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
            {

                FindObjectOfType<Toast>().OnToast("Sign In Successful");
                Invoke("PreparationPhaseToStart", 3f);

            }

        });

    }

    private void PreparationPhaseToStart()
    {

        SceneManager.LoadScene(1);

    }

}
