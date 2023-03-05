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
            { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } },

        };

        TEMPERATURE = new double[5, 2]
        {

            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },

        };

        LOCATION_TEXT = new string[11, 2]
        {

            { "", "" },
            { "", "" },
            { "", "" },
            { "", "" },
            { "", "" },
            { "", "" },
            { "", "" },
            { "", "" },
            { "", "" },
            { "", "" },
            { "", "" },

        };

        STAFF = new double[2, 2]
        {

            { 0, 0 },
            { 0, 0 },

        };

        STANDING = new double[3, 2]
        {

            { 0, 0 },
            { 0, 0 },
            { 0, 0 },

        };

        ADVERTISEMENT[0, 0] = 0;
        ADVERTISEMENT[0, 1] = 0;

        ADVERTISEMENT[1, 0] = 20;
        ADVERTISEMENT[1, 1] = 0.005;

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

        LOCATION_TEXT[0, 0] = "THE HOME";
        LOCATION_TEXT[0, 1] = "Your very own neighborhood!";
        LOCATION_TEXT[0, 2] = "Is there a better place to start your Mango Graham Shake empire than your very own neighborhood? Don't expect a lot of customers here, but the free rent and popularity bonus will help you test the ups and downs of the business without too much risk.";

        LOCATION_TEXT[1, 0] = "THE SUBDIVISION";
        LOCATION_TEXT[1, 1] = "";
        LOCATION_TEXT[1, 2] = "";

        LOCATION_TEXT[2, 0] = "THE PARK";
        LOCATION_TEXT[2, 1] = "";
        LOCATION_TEXT[2, 2] = "";

        LOCATION_TEXT[3, 0] = "THE TERMINAL";
        LOCATION_TEXT[3, 1] = "";
        LOCATION_TEXT[3, 2] = "";

        LOCATION_TEXT[4, 0] = "THE SCHOOL";
        LOCATION_TEXT[4, 1] = "";
        LOCATION_TEXT[4, 2] = "";

        LOCATION_TEXT[5, 0] = "THE MARKET";
        LOCATION_TEXT[5, 1] = "";
        LOCATION_TEXT[5, 2] = "";

        LOCATION_TEXT[6, 0] = "THE DOWNTOWN";
        LOCATION_TEXT[6, 1] = "";
        LOCATION_TEXT[6, 2] = "";

        LOCATION_TEXT[7, 0] = "THE BEACH";
        LOCATION_TEXT[7, 1] = "";
        LOCATION_TEXT[7, 2] = "";

        LOCATION_TEXT[8, 0] = "THE MALL";
        LOCATION_TEXT[8, 1] = "";
        LOCATION_TEXT[8, 2] = "";

        LOCATION_TEXT[9, 0] = "THE AMUSEMENT PARK";
        LOCATION_TEXT[9, 1] = "";
        LOCATION_TEXT[9, 2] = "";

        LOCATION_TEXT[10, 0] = "THE AIRPORT";
        LOCATION_TEXT[10, 1] = "";
        LOCATION_TEXT[10, 2] = "";

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

        SUPPLIES[3, 1, 0] = 50;
        SUPPLIES[3, 1, 1] = 120;
        SUPPLIES[3, 1, 2] = 300;

        SUPPLIES[4, 1, 0] = 75;
        SUPPLIES[4, 1, 1] = 225;
        SUPPLIES[4, 1, 2] = 500;

        DEFAULT_PRICE = 30;
        MAXIMUM_PRICE = 69;
        MINIMUM_CUPS = 10;

        DEFAULT_RECIPE = new int[]
        {

            12,
            37,
            12,
            10,

        };

        TEMPERATURE[0, 0] = 20;
        TEMPERATURE[0, 1] = 25;
        TEMPERATURE[1, 0] = 26;
        TEMPERATURE[1, 1] = 30;
        TEMPERATURE[2, 0] = 31;
        TEMPERATURE[2, 1] = 35;
        TEMPERATURE[3, 0] = 36;
        TEMPERATURE[3, 1] = 40;
        TEMPERATURE[4, 0] = 41;
        TEMPERATURE[4, 1] = 45;

        AVERAGE_SUPPLIES_COST = new double[]
        {

            20.90,
            1.66,
            24,
            0.5,
            0.89,

        };

        TARGET_CRITERIA = new int[]
        {

            4,
            30,
            2,
            10,
            49,

        };

        OVERPRICED = 59;
        INCREMENT_POPULARITY_PER_DAY = 2;

        STAFF[0, 0] = 200;
        STAFF[0, 1] = 0;

        STAFF[1, 0] = 500;
        STAFF[1, 1] = 0;

        STANDING[0, 0] = 0.95;
        STANDING[0, 1] = 1;

        STANDING[1, 0] = 0.85;
        STANDING[1, 1] = 0.94;

        STANDING[2, 0] = 0.75;
        STANDING[2, 1] = 0.84;

    }

    private void DontDestroy()
    {

        if (FindObjectsOfType(GetType()).Length > 1)

            Destroy(gameObject);

        else

            DontDestroyOnLoad(gameObject);

    }

    public double DEFAULT_PRICE { get; private set; }

    public double MAXIMUM_PRICE { get; private set; }

    public double OVERPRICED { get; private set; }

    public double[,,] SUPPLIES { get; private set; }

    public double[,] LOCATION { get; private set; }

    public double[,] STAFF { get; private set; }

    public double[,] TEMPERATURE { get; private set; }

    public double[] AVERAGE_SUPPLIES_COST { get; private set; }

    public int INCREMENT_POPULARITY_PER_DAY { get; private set; }

    public int MINIMUM_CUPS { get; private set; }

    public int[] DEFAULT_RECIPE { get; private set; }

    public int[] TARGET_CRITERIA { get; private set; }

    public string[,] LOCATION_TEXT { get; private set; }

    public double[,] ADVERTISEMENT { get; private set; }

    public double[,] STANDING { get; private set; }

}
