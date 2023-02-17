using UnityEngine;

public class ENV : MonoBehaviour
{

    void Awake()
    {

        DontDestroy();

        ADVERTISEMENT = new double[11, 2]
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

        LOCATION = new double[11, 3]
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

        SUPPLIES = new double[5, 3, 3]
        {

            { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }

        };

        ADVERTISEMENT[0, 0] = 0;
        ADVERTISEMENT[0, 1] = 0;

        ADVERTISEMENT[1, 0] = 20;
        ADVERTISEMENT[1, 1] = 00.005;

        ADVERTISEMENT[2, 0] = 30;
        ADVERTISEMENT[2, 1] = 0.0075;

        ADVERTISEMENT[3, 0] = 40;
        ADVERTISEMENT[3, 1] = 0.01;

        ADVERTISEMENT[4, 0] = 60;
        ADVERTISEMENT[4, 1] = 0.015;

        ADVERTISEMENT[5, 0] = 70;
        ADVERTISEMENT[5, 1] = 0.02;

        ADVERTISEMENT[6, 0] = 130;
        ADVERTISEMENT[6, 1] = 0.04;

        ADVERTISEMENT[7, 0] = 190;
        ADVERTISEMENT[7, 1] = 0.06;

        ADVERTISEMENT[8, 0] = 250;
        ADVERTISEMENT[8, 1] = 0.08;

        ADVERTISEMENT[9, 0] = 310;
        ADVERTISEMENT[9, 1] = 0.1;

        ADVERTISEMENT[10, 0] = 600;
        ADVERTISEMENT[10, 1] = 0.2;

        LOCATION[0, 0] = 100;
        LOCATION[0, 1] = 0;
        LOCATION[0, 2] = 0.001;

        LOCATION[1, 0] = 200;
        LOCATION[1, 1] = 5000;
        LOCATION[1, 2] = 0.0011;

        LOCATION[2, 0] = 500;
        LOCATION[2, 1] = 10000;
        LOCATION[2, 2] = 0.0013;

        LOCATION[3, 0] = 1000;
        LOCATION[3, 1] = 20000;
        LOCATION[3, 2] = 0.0016;

        LOCATION[4, 0] = 3000;
        LOCATION[4, 1] = 50000;
        LOCATION[4, 2] = 0.002;

        LOCATION[5, 0] = 5000;
        LOCATION[5, 1] = 100000;
        LOCATION[5, 2] = 0.0025;

        LOCATION[6, 0] = 10000;
        LOCATION[6, 1] = 280000;
        LOCATION[6, 2] = 0.003;

        LOCATION[7, 0] = 15000;
        LOCATION[7, 1] = 460000;
        LOCATION[7, 2] = 0.0035;

        LOCATION[8, 0] = 20000;
        LOCATION[8, 1] = 640000;
        LOCATION[8, 2] = 0.004;

        LOCATION[9, 0] = 25000;
        LOCATION[9, 1] = 820000;
        LOCATION[9, 2] = 0.0045;

        LOCATION[10, 0] = 30000;
        LOCATION[10, 1] = 1000000;
        LOCATION[10, 2] = 0.005;

        SUPPLIES[0, 2, 0] = 260;
        SUPPLIES[0, 2, 1] = 500;
        SUPPLIES[0, 2, 2] = 970;

        SUPPLIES[1, 2, 0] = 86;
        SUPPLIES[1, 2, 1] = 330;
        SUPPLIES[1, 2, 2] = 800;

        SUPPLIES[2, 2, 0] = 300;
        SUPPLIES[2, 2, 1] = 480;
        SUPPLIES[2, 2, 2] = 1150;

        SUPPLIES[3, 2, 0] = 30;
        SUPPLIES[3, 2, 1] = 60;
        SUPPLIES[3, 2, 2] = 120;

        SUPPLIES[4, 2, 0] = 70;
        SUPPLIES[4, 2, 1] = 200;
        SUPPLIES[4, 2, 2] = 420;

        SUPPLIES[0, 1, 0] = 12;
        SUPPLIES[0, 1, 1] = 24;
        SUPPLIES[0, 1, 2] = 48;

        SUPPLIES[1, 1, 0] = 50;
        SUPPLIES[1, 1, 1] = 200;
        SUPPLIES[1, 1, 2] = 500;

        SUPPLIES[2, 1, 0] = 12;
        SUPPLIES[2, 1, 1] = 20;
        SUPPLIES[2, 1, 2] = 50;

        SUPPLIES[3, 1, 0] = 20;
        SUPPLIES[3, 1, 1] = 50;
        SUPPLIES[3, 1, 2] = 120;

        SUPPLIES[4, 1, 0] = 75;
        SUPPLIES[4, 1, 1] = 225;
        SUPPLIES[4, 1, 2] = 500;

    }

    private void DontDestroy()
    {

        if (FindObjectsOfType(GetType()).Length > 1)

            Destroy(gameObject);

        else

            DontDestroyOnLoad(gameObject);

    }

    public double[,] ADVERTISEMENT { get; private set; }

    public double[,] LOCATION { get; private set; }

    public double[,,] SUPPLIES { get; private set; }

}
