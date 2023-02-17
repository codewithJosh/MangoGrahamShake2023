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

        LOCATION_DOUBLE = new double[11, 3]
        {

            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },

        };

        ADVERTISEMENT_DOUBLE = new double[11, 2]
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

        LOCATION_DOUBLE[0, 0] = 100;
        LOCATION_DOUBLE[0, 1] = 0;
        LOCATION_DOUBLE[0, 2] = 0.001;

        LOCATION_DOUBLE[1, 0] = 200;
        LOCATION_DOUBLE[1, 1] = 5000;
        LOCATION_DOUBLE[1, 2] = 0.0011;

        LOCATION_DOUBLE[2, 0] = 500;
        LOCATION_DOUBLE[2, 1] = 10000;
        LOCATION_DOUBLE[2, 2] = 0.0013;

        LOCATION_DOUBLE[3, 0] = 1000;
        LOCATION_DOUBLE[3, 1] = 20000;
        LOCATION_DOUBLE[3, 2] = 0.0016;

        LOCATION_DOUBLE[4, 0] = 3000;
        LOCATION_DOUBLE[4, 1] = 50000;
        LOCATION_DOUBLE[4, 2] = 0.002;

        LOCATION_DOUBLE[5, 0] = 5000;
        LOCATION_DOUBLE[5, 1] = 100000;
        LOCATION_DOUBLE[5, 2] = 0.0025;

        LOCATION_DOUBLE[6, 0] = 10000;
        LOCATION_DOUBLE[6, 1] = 280000;
        LOCATION_DOUBLE[6, 2] = 0.003;

        LOCATION_DOUBLE[7, 0] = 15000;
        LOCATION_DOUBLE[7, 1] = 460000;
        LOCATION_DOUBLE[7, 2] = 0.0035;

        LOCATION_DOUBLE[8, 0] = 20000;
        LOCATION_DOUBLE[8, 1] = 640000;
        LOCATION_DOUBLE[8, 2] = 0.004;

        LOCATION_DOUBLE[9, 0] = 25000;
        LOCATION_DOUBLE[9, 1] = 820000;
        LOCATION_DOUBLE[9, 2] = 0.0045;

        LOCATION_DOUBLE[10, 0] = 30000;
        LOCATION_DOUBLE[10, 1] = 1000000;
        LOCATION_DOUBLE[10, 2] = 0.005;

        ADVERTISEMENT_DOUBLE[0, 0] = 0;
        ADVERTISEMENT_DOUBLE[0, 1] = 0;

        ADVERTISEMENT_DOUBLE[1, 0] = 20;
        ADVERTISEMENT_DOUBLE[1, 1] = 00.005;

        ADVERTISEMENT_DOUBLE[2, 0] = 30;
        ADVERTISEMENT_DOUBLE[2, 1] = 0.0075;

        ADVERTISEMENT_DOUBLE[3, 0] = 40;
        ADVERTISEMENT_DOUBLE[3, 1] = 0.01;

        ADVERTISEMENT_DOUBLE[4, 0] = 60;
        ADVERTISEMENT_DOUBLE[4, 1] = 0.015;

        ADVERTISEMENT_DOUBLE[5, 0] = 70;
        ADVERTISEMENT_DOUBLE[5, 1] = 0.02;

        ADVERTISEMENT_DOUBLE[6, 0] = 130;
        ADVERTISEMENT_DOUBLE[6, 1] = 0.04;

        ADVERTISEMENT_DOUBLE[7, 0] = 190;
        ADVERTISEMENT_DOUBLE[7, 1] = 0.06;

        ADVERTISEMENT_DOUBLE[8, 0] = 250;
        ADVERTISEMENT_DOUBLE[8, 1] = 0.08;

        ADVERTISEMENT_DOUBLE[9, 0] = 310;
        ADVERTISEMENT_DOUBLE[9, 1] = 0.1;

        ADVERTISEMENT_DOUBLE[10, 0] = 600;
        ADVERTISEMENT_DOUBLE[10, 1] = 0.2;

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

    public double[,] LOCATION_DOUBLE { get; private set; }

    public double[,] ADVERTISEMENT_DOUBLE { get; private set; }

}
