using Firebase;
using Firebase.Auth;
using System;
using UnityEngine;

public class FirebaseAuthManager : MonoBehaviour
{

    #region AWAKE_METHOD

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

    #endregion

    #region CHECK_FIREBASE_DEPENDENCIES

    /*
     * Upon calling this method the system must check the Firebase dependencies first.
     * If done successfully.
     * Then, let's initialize the value of an OBJECT by creating an INSTANCE of that OBJECT.
     * Else, the system must prompt the user that something went wrong.
     */
    private static void CheckFirebaseDependencies()
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
                    string description = $"Dependency check was not completed. Error : {task.Exception.Message}";

                    /*
                     * Finally, let's call our user-defined method on the other class
                     * where we can pass our message and prompt the user that something went wrong.
                     */
                    DialogManager.OnFailed(description);

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
                    string description = $"Could not resolve all Firebase dependencies: {task.Result}";

                    /*
                     * Finally, let's call our user-defined method on the other class
                     * where we can pass our message and prompt the user that something went wrong.
                     */
                    DialogManager.OnFailed(description);

                }

                /*
                 * If done successfully.
                 * Then, an INSTANCE of an OBJECT must be created.
                 */
                else

                    /*
                     * Let's now initialize the value of an OBJECT by creating an INSTANCE of that OBJECT.
                     */
                    STATUS.FIREBASE_AUTH = FirebaseAuth.DefaultInstance;

            });

    }

    #endregion

    #region SIGN_IN_WITH_GOOGLE_ON_FIREBASE

    /*
     * Upon calling this method the system must link our user's Google account in our Firebase database.
     * Else, the system must prompt the user that something went wrong.
     */
    private static void SignInWithGoogleOnFirebase(string _idToken)
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
        STATUS.FIREBASE_AUTH
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
                        string description = $"Error code = {inner.ErrorCode} Message = {inner.Message}";

                        /*
                         * Finally, let's call our user-defined method on the other class
                         * where we can pass our message and prompt the user that something went wrong.
                         */
                        DialogManager.OnFailed(description);

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

    #endregion

    #region AUTOMATED_PRIORITIES

    /*
     * Let's publicly declare a Google on Firebase LOGIN method
     * where we can only allow other classes to used.
     */
    public static void OnSignInWithGoogleOnFirebase(string _idToken) => SignInWithGoogleOnFirebase(_idToken);

    /*
     * Let's publicly declare a Firebase Auth SIGNOUT method
     * where we can only allow other classes to used.
     */
    public static void OnSignout() => STATUS.FIREBASE_AUTH.SignOut();

    #endregion

}
