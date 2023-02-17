using Google;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAuthManager : MonoBehaviour
{

    /*
     * Let's privately declare a STRING field
     * where we can place our WEB CLIENT ID later.
     * Also, let's set it in a SERIALIZED FIELD manner
     * so that, we can be able to access it in the INSPECTOR PANEL.
     */
    [SerializeField]
    private string webClientId;

    /*
     * Let's privately declare an OBJECT field
     * where we can store our Google Sign In CONFIGURATION later.
     */
    private GoogleSignInConfiguration googleSignInConfiguration;

    /*
     * Let's privately declare an OBJECT field
     * where we can store our Google Sign In INSTANCE later.
     */
    private GoogleSignIn googleSignIn;

    /*
     * A predefined (built-in) method in UNITY
     * where is called when the script instance is being loaded.
     */
    void Awake()
    {

        /*
         * As the SUMMARY of all the things that we have done here.
         * We basically configure an OBJECT's properties.
         * As an important highlight is giving our OBJECT's WEB CLIENT ID value.
         * Therefore, we can now create an INSTANCE of that OBJECT.
         */

        /*
         * First, let's define our OBJECT's configuration properties
         * such as its WEB CLIENT ID and some BOOLEAN fields value.
         */
        googleSignInConfiguration = new GoogleSignInConfiguration
        {

            RequestEmail = true,
            RequestIdToken = true,
            UseGameSignIn = false,
            WebClientId = webClientId,

        };

        /*
         * Then, let's place it in our OBJECT's configuration property
         */
        GoogleSignIn.Configuration = googleSignInConfiguration;

        /*
         * Finally, let's now initialize the value of an OBJECT by creating an INSTANCE of that OBJECT.
         */
        googleSignIn = GoogleSignIn.DefaultInstance;

    }

    /*
     * Upon calling this method the system must ask the user to login his/ her Google account.
     * If done successfully.
     * Then, the user has been successfully login on Google.
     * Else, the system must prompt the user that something went wrong.
     */
    private void SignIn()
    {

        /*
         * First, let's ask the user to login his/ her Google account.
         */
        googleSignIn
            .SignIn()
            .ContinueWith(task =>
            {

                /*
                 * If done failed.
                 * Then, proceed on identifying something went wrong.
                 */
                if (task.IsFaulted)
                {

                    using IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator();

                    if (!enumerator.MoveNext())
                    {

                        /*
                         * First, let's locally declare a STRING field.
                         * Also, let's initialize it with a more detailed message for our exception.
                         */
                        string description = string.Format("Got Unexpected Exception?!? {0}", task.Exception);

                        /*
                         * Finally, let's call our user-defined method on the other class
                         * where we can pass our message and prompt the user that something went wrong.
                         */
                        FindObjectOfType<GameManager>().OnFailed(description);

                    }

                }

                /*
                 * If canceled.
                 * Then, prompt the user that the process is canceled.
                 */
                else if (task.IsCanceled)

                    /*
                     * Let's call our user-defined method on the other class
                     * where we can prompt the user that the process is canceled.
                     */
                    FindObjectOfType<GameManager>().OnFailed("Canceled");

                /*
                 * If done successfully.
                 * Then, the user has been successfully Sign In on Google.
                 */
                else
                {

                    /*
                     * Let's locally declare a STRING field.
                     * Also, let's initialize it with our user's ID TOKEN.
                     */
                    string idToken = task.Result.IdToken;

                    /*
                     * If ID TOKEN contains a value.
                     * Then, proceed on calling and passing this value to that OBJECT.
                     */
                    if (idToken != null)

                        /*
                         * Then, let's call our user-defined method on the other class
                         * where we can pass our ID TOKEN to proceed.
                         * Also, let's ID TOKEN serve as our primary key for PLAYERS collection also known as PLAYER ID.
                         */
                        FindObjectOfType<FirebaseAuthManager>().OnSignInWithGoogleOnFirebase(idToken);

                }

            });

    }

    /*
     * Let's publicly declare a Google LOGIN method
     * where we can only allow other classes to used.
     */
    public void OnLogin() => SignIn();

    /*
     * Let's publicly declare a Google SIGNOUT method
     * where we can only allow other classes to used.
     */
    public void OnSignout() => googleSignIn.SignOut();

}
