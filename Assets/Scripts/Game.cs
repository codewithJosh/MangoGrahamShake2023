using UnityEngine;

public class Game : MonoBehaviour
{

    void Awake()
    {

        DontDestroy();

        SUPPLIES_INT = new int[5, 2, 3]
        {

            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } }

        };

        SUPPLIES_DOUBLE = new double[5, 3]
        {

            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }

        };

        RECIPE_INT = new int[4, 2]
        {

            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 }

        };

        LOCATION_INT = new int[]
        {

            100,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,

        };

        LOCATION_DOUBLE = new double[]
        {

            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,

        };

        ADVERTISEMENT_DOUBLE = new double[10, 2]
        {

            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },

        };

        SUPPLIES_INT[0, 1, 0] = 12;
        SUPPLIES_INT[0, 1, 1] = 24;
        SUPPLIES_INT[0, 1, 2] = 48;

        SUPPLIES_INT[1, 1, 0] = 50;
        SUPPLIES_INT[1, 1, 1] = 200;
        SUPPLIES_INT[1, 1, 2] = 500;

        SUPPLIES_INT[2, 1, 0] = 12;
        SUPPLIES_INT[2, 1, 1] = 20;
        SUPPLIES_INT[2, 1, 2] = 50;

        SUPPLIES_INT[3, 1, 0] = 20;
        SUPPLIES_INT[3, 1, 1] = 50;
        SUPPLIES_INT[3, 1, 2] = 120;

        SUPPLIES_INT[4, 1, 0] = 75;
        SUPPLIES_INT[4, 1, 1] = 225;
        SUPPLIES_INT[4, 1, 2] = 500;

        SUPPLIES_DOUBLE[0, 0] = 260;
        SUPPLIES_DOUBLE[0, 1] = 500;
        SUPPLIES_DOUBLE[0, 2] = 970;

        SUPPLIES_DOUBLE[1, 0] = 86;
        SUPPLIES_DOUBLE[1, 1] = 330;
        SUPPLIES_DOUBLE[1, 2] = 800;

        SUPPLIES_DOUBLE[2, 0] = 300;
        SUPPLIES_DOUBLE[2, 1] = 480;
        SUPPLIES_DOUBLE[2, 2] = 1150;

        SUPPLIES_DOUBLE[3, 0] = 30;
        SUPPLIES_DOUBLE[3, 1] = 60;
        SUPPLIES_DOUBLE[3, 2] = 120;

        SUPPLIES_DOUBLE[4, 0] = 70;
        SUPPLIES_DOUBLE[4, 1] = 200;
        SUPPLIES_DOUBLE[4, 2] = 420;

        RECIPE_INT[0, 1] = 20;
        RECIPE_INT[1, 1] = 10;
        RECIPE_INT[2, 1] = 10;
        RECIPE_INT[3, 1] = 7;

        ADVERTISEMENT_DOUBLE[0, 0] = 0;
        ADVERTISEMENT_DOUBLE[0, 1] = 0;

        ADVERTISEMENT_DOUBLE[1, 0] = 10;
        ADVERTISEMENT_DOUBLE[1, 1] = 0;

        ADVERTISEMENT_DOUBLE[2, 0] = 20;
        ADVERTISEMENT_DOUBLE[2, 1] = 0;

        ADVERTISEMENT_DOUBLE[3, 0] = 50;
        ADVERTISEMENT_DOUBLE[3, 1] = 0;

        ADVERTISEMENT_DOUBLE[4, 0] = 100;
        ADVERTISEMENT_DOUBLE[4, 1] = 0;

        ADVERTISEMENT_DOUBLE[5, 0] = 200;
        ADVERTISEMENT_DOUBLE[5, 1] = 0;

        ADVERTISEMENT_DOUBLE[6, 0] = 500;
        ADVERTISEMENT_DOUBLE[6, 1] = 0;

        ADVERTISEMENT_DOUBLE[7, 0] = 1000;
        ADVERTISEMENT_DOUBLE[7, 1] = 0;

        ADVERTISEMENT_DOUBLE[8, 0] = 2000;
        ADVERTISEMENT_DOUBLE[8, 1] = 0;

        ADVERTISEMENT_DOUBLE[9, 0] = 5000;
        ADVERTISEMENT_DOUBLE[9, 1] = 0;

    }

    private void DontDestroy()
    {

        if (FindObjectsOfType(GetType()).Length > 1)

            Destroy(gameObject);

        else

            DontDestroyOnLoad(gameObject);

    }

    public double[,] SUPPLIES_DOUBLE { get; private set; }

    public int[,] RECIPE_INT { get; private set; }

    public int[,,] SUPPLIES_INT { get; private set; }

    public int[] LOCATION_INT { get; private set; }

    public double[] LOCATION_DOUBLE { get; private set; }

    public double[,] ADVERTISEMENT_DOUBLE { get; private set; }

}
