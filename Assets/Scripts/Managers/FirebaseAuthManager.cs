using Firebase;
using Firebase.Auth;
using System;
using UnityEngine;

public class FirebaseAuthManager : MonoBehaviour
{

    /*
     * Let's privately declare an OBJECT field
     * where we can store our Firebase Auth INSTANCE later.
     */
    private FirebaseAuth firebaseAuth;

    /*
     * A predefined (built-in) method in UNITY
     * where is called when the script instance is being loaded.
     */
    void Awake()
    {

        /*
         * Let's initialize the value of an OBJECT by creating an INSTANCE of that OBJECT.
         * In order to do that, first let's check the Firebase dependencies of our system.
         */
        CheckFirebaseDependencies();

    }

    /*
     * Upon calling this method the system must check the Firebase dependencies first.
     * If done successfully.
     * Then, let's initialize the value of an OBJECT by creating an INSTANCE of that OBJECT.
     * Else, the system must prompt the user that something went wrong.
     */
    private void CheckFirebaseDependencies()
    {

        /*
         * First, let's check and fix the Firebase dependencies.
         */
        FirebaseApp
            .CheckAndFixDependenciesAsync()
            .ContinueWith(task =>
            {

                /*
                 * If done failed.
                 * Then, proceed on identifying something went wrong.
                 */
                if (!task.IsCompleted)
                {

                    /*
                     * First, let's locally declare a STRING field.
                     * Also, let's initialize it with a more detailed message for our exception.
                     */
                    string description = string.Format("Dependency check was not completed. Error : {0}", task.Exception.Message);

                    /*
                     * Finally, let's call our user-defined method on the other class
                     * where we can pass our message and prompt the user that something went wrong.
                     */
                    FindObjectOfType<GameManager>().OnFailed(description);

                }

                /*
                 * If status is unavailable.
                 * Then, prompt the user that the process is resulting to unavailable status.
                 */
                else if (task.Result != DependencyStatus.Available)
                {

                    /*
                     * First, let's locally declare a STRING field.
                     * Also, let's initialize it with a more detailed message for our exception.
                     */
                    string description = string.Format("Could not resolve all Firebase dependencies: {0}", task.Result.ToString());

                    /*
                     * Finally, let's call our user-defined method on the other class
                     * where we can pass our message and prompt the user that something went wrong.
                     */
                    FindObjectOfType<GameManager>().OnFailed(description);

                }

                /*
                 * If done successfully.
                 * Then, an INSTANCE of an OBJECT must be created.
                 */
                else

                    /*
                     * Let's now initialize the value of an OBJECT by creating an INSTANCE of that OBJECT.
                     */
                    firebaseAuth = FirebaseAuth.DefaultInstance;

            });

    }

    /*
     * Upon calling this method the system must link our user's Google account in our Firebase database.
     * Else, the system must prompt the user that something went wrong.
     */
    private void SignInWithGoogleOnFirebase(string _idToken)
    {

        /*
         * First, let's locally declare an OBJECT field.
         * Also, let's initialize it by getting the returned value of an OBJECT's function/ method.
         * Along with that, the OBJECT's function/ method requires a parameter.
         * So that, let's pass a STRING value of ID TOKEN
         * provided by the one who called/ referenced this method.
         */
        Credential credential = GoogleAuthProvider.GetCredential(_idToken, null);

        /*
         * Then, let's sign in the user's credential in Firebase.
         * Moreover, it basically links our user's Google account in our Firebase database.
         */
        firebaseAuth
            .SignInWithCredentialAsync(credential)
            .ContinueWith(task =>
            {

                /*
                 * Then, let's locally declare an OBJECT field.
                 * Also, let's initialize it by getting the value of our exception.
                 */
                AggregateException aggregateException = task.Exception;

                /*
                 * If exception contains a value.
                 * Then, proceed on identifying something went wrong.
                 */
                if (aggregateException != null)
                {

                    if (aggregateException.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    {

                        /*
                         * First, let's locally declare a STRING field.
                         * Also, let's initialize it with a more detailed message for our exception.
                         */
                        string description = string.Format("Error code = {0} Message = {1}", inner.ErrorCode, inner.Message);

                        /*
                         * Finally, let's call our user-defined method on the other class
                         * where we can pass our message and prompt the user that something went wrong.
                         */
                        FindObjectOfType<GameManager>().OnFailed(description);

                    }

                }

                /*
                 * If done successfully.
                 * Then, the user has been successfully link his/ her Google account in our Firebase database.
                 */
                else

                    /*
                     * Then, let's call our user-defined method on the other class
                     * where successful login takes place.
                     */
                    FindObjectOfType<LoginManager>().OnSignInSuccess();

            });

    }

    /*
     * Let's publicly declare an OBJECT property
     * where we can only allow other classes to get the value/ referenced.
     */
    public FirebaseUser FirebaseUser => firebaseAuth.CurrentUser;

    /*
     * Let's publicly declare a Google on Firebase LOGIN method
     * where we can only allow other classes to used.
     */
    public void OnSignInWithGoogleOnFirebase(string _idToken) => SignInWithGoogleOnFirebase(_idToken);

    /*
     * Let's publicly declare a Firebase Auth SIGNOUT method
     * where we can only allow other classes to used.
     */
    public void OnSignout() => firebaseAuth.SignOut();

}
