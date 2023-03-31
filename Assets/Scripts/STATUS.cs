using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

public class STATUS : MonoBehaviour
{

    #region DECLARATION

    public enum STATES { IDLE, LOG_OUT, CONFIRMATION, SKIPPING, STARTING_OVER, CANCELING, BUYING, RENTING, UPGRADING, }

    #endregion

    #region UPDATE_METHOD

    void Update()
    {

        /*
         * A field that continuously holds a boolean value.
         * If it's value is TRUE, then the system is connected to the internet. Else, FALSE.
         */
        IS_CONNECTED = Application.internetReachability != NetworkReachability.NotReachable;

    }

    #endregion

    #region AUTOMATED_PROPERTIES

    public static bool IS_LOADING { get; set; }

    public static bool IS_CONNECTED { get; set; }

    public static STATES STATE { get; set; }

    public static float IS_SOUNDS_ON { get; set; }

    public static float IS_AUDIO_ON { get; set; }

    public static FirebaseAuth FIREBASE_AUTH { get; set; }

    public static FirebaseFirestore FIREBASE_FIRESTORE { get; set; }

    public static FirebaseUser FIREBASE_USER => FIREBASE_AUTH.CurrentUser;

    public static bool IS_SOUNDS_MUTED => IS_SOUNDS_ON == 0;

    public static bool IS_AUDIO_MUTED => IS_AUDIO_ON == 0;

    public static bool IS_PLAYER_LOADING { get; set; }

    #endregion

}
