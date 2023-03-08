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

        SUPPLIES = new double[5, 2, 3]
        {

            { { 0, 0, 0 }, { 0, 0, 0 }, },
            { { 0, 0, 0 }, { 0, 0, 0 }, },
            { { 0, 0, 0 }, { 0, 0, 0 }, },
            { { 0, 0, 0 }, { 0, 0, 0 }, },
            { { 0, 0, 0 }, { 0, 0, 0 }, },

        };

        TEMPERATURE = new double[5, 2]
        {

            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },

        };

        LOCATION_TEXT = new string[11, 3]
        {

            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },

        };

        STAFF = new double[3, 2]
        {

            { 0, 0 },
            { 0, 0 },
            { 0, 0 },

        };

        STANDING = new double[3, 2]
        {

            { 0, 0 },
            { 0, 0 },
            { 0, 0 },

        };

        UPGRADE_TEXT = new string[3, 2]
        {

            { "", "" },
            { "", "" },
            { "", "" },

        };

        UPGRADE = new double[3, 6, 2]
        {

            { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, },
            { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, },
            { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, },

        };

        STAFF_TEXT = new string[3, 2]
        {

            { "", "" },
            { "", "" },
            { "", "" },

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
        LOCATION_TEXT[1, 1] = "The rich people may go crazy with your Mango Graham Shake";
        LOCATION_TEXT[1, 2] = "There are rich and popular people in the near subdivision it may give you extra sales.";

        LOCATION_TEXT[2, 0] = "THE PARK";
        LOCATION_TEXT[2, 1] = "Take the opportunity of gwtting some tired customers";
        LOCATION_TEXT[2, 2] = "A lot of people rest in the park maybe some drinks may complete their day";

        LOCATION_TEXT[3, 0] = "THE TERMINAL";
        LOCATION_TEXT[3, 1] = "Travellers are thirsty after a long travel maybe they are looking for a good drink";
        LOCATION_TEXT[3, 2] = "Travelling may takes a lot of time. Having a good drink will make your travel experince a lot better.";

        LOCATION_TEXT[4, 0] = "THE SCHOOL";
        LOCATION_TEXT[4, 1] = "It may be your chance to be an internet superstar";
        LOCATION_TEXT[4, 2] = "Students are obsessed with social media. They will post anything new and interesting in the social media. It's an opportunity to start a business in a school.";

        LOCATION_TEXT[5, 0] = "THE MARKET";
        LOCATION_TEXT[5, 1] = "Someone is tired maybe they need some good drink to recover";
        LOCATION_TEXT[5, 2] = "A hot and bustling market is a gold pit of money. Vendors and customers need a good drink in the heat of the moment.";

        LOCATION_TEXT[6, 0] = "THE DOWNTOWN";
        LOCATION_TEXT[6, 1] = "A lot of people crave for a drink is there a good shop nearby?";
        LOCATION_TEXT[6, 2] = "It is the heart of the city a lot of people passes by. Someone might craved for a good drink. Is there something nearby?";

        LOCATION_TEXT[7, 0] = "THE BEACH";
        LOCATION_TEXT[7, 1] = "A hot environment is asking for a cold drink. Is there anything I can sell?";
        LOCATION_TEXT[7, 2] = "The waves are crashing, the sun is burning, and the swimsuits are dazzling the essence of ocean is present. Though one is missing, a refreshing drink after a good swim. Is there a refreshing drink I can buy?";

        LOCATION_TEXT[8, 0] = "THE MALL";
        LOCATION_TEXT[8, 1] = "Looks like some shoppers are tired maybe a drink may help";
        LOCATION_TEXT[8, 2] = "A lot of shoppers are walking and talking for a long time. They need some drink to alleviate some pain. Take the opportunity to make a lot of cash.";

        LOCATION_TEXT[9, 0] = "THE AMUSEMENT PARK";
        LOCATION_TEXT[9, 1] = "Do you hear something? They are screaming for a good drink. Is there anything you can do?";
        LOCATION_TEXT[9, 2] = "You can hear a screaming in the distance, a lot of people laughing, and a lot of people gossiping. But what is that you just heard? There is no good drink in the amusement park? Looks like a chance to satisfy a lot of customers while earning some money.";

        LOCATION_TEXT[10, 0] = "THE AIRPORT";
        LOCATION_TEXT[10, 1] = "Hmmm... is there a product that I can sell that represents the Philippines";
        LOCATION_TEXT[10, 2] = "A different types of passengers are leaving and arriving the airport but they looks discontented. There is no good drinks while waiting for their flight. Looks like the airport lacks something that represents the Philippines. Is there a product you can think of?";

        SUPPLIES[0, 0, 0] = 12;
        SUPPLIES[0, 0, 1] = 24;
        SUPPLIES[0, 0, 2] = 48;

        SUPPLIES[1, 0, 0] = 50;
        SUPPLIES[1, 0, 1] = 200;
        SUPPLIES[1, 0, 2] = 500;

        SUPPLIES[2, 0, 0] = 12;
        SUPPLIES[2, 0, 1] = 20;
        SUPPLIES[2, 0, 2] = 50;

        SUPPLIES[3, 0, 0] = 50;
        SUPPLIES[3, 0, 1] = 120;
        SUPPLIES[3, 0, 2] = 300;

        SUPPLIES[4, 0, 0] = 75;
        SUPPLIES[4, 0, 1] = 225;
        SUPPLIES[4, 0, 2] = 500;

        SUPPLIES[0, 1, 0] = 260;
        SUPPLIES[0, 1, 1] = 500;
        SUPPLIES[0, 1, 2] = 970;

        SUPPLIES[1, 1, 0] = 86;
        SUPPLIES[1, 1, 1] = 330;
        SUPPLIES[1, 1, 2] = 800;

        SUPPLIES[2, 1, 0] = 300;
        SUPPLIES[2, 1, 1] = 480;
        SUPPLIES[2, 1, 2] = 1150;

        SUPPLIES[3, 1, 0] = 30;
        SUPPLIES[3, 1, 1] = 60;
        SUPPLIES[3, 1, 2] = 120;

        SUPPLIES[4, 1, 0] = 70;
        SUPPLIES[4, 1, 1] = 200;
        SUPPLIES[4, 1, 2] = 420;

        DEFAULT_PRICE = 56;
        MAXIMUM_PRICE = 69;
        MINIMUM_CUPS = 10;

        DEFAULT_RECIPE = new int[]
        {

            2,
            20,
            3,
            5,

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

        STORAGE = new int[]
        {

            50,
            938,
            50,
            750,
            100,

        };

        OVERPRICED = 59;
        INCREMENT_POPULARITY_PER_DAY = 2;

        STAFF[0, 0] = 1000;
        STAFF[0, 1] = 0.01;

        STAFF[1, 0] = 5000;
        STAFF[1, 1] = 0.02;

        STAFF[2, 0] = 10000;
        STAFF[2, 1] = 0.02;

        STANDING[0, 0] = 0.95;
        STANDING[0, 1] = 1;

        STANDING[1, 0] = 0.85;
        STANDING[1, 1] = 0.94;

        STANDING[2, 0] = 0.75;
        STANDING[2, 1] = 0.84;

        UPGRADE_TEXT[0, 0] = "THE BLENDER 9000";
        UPGRADE_TEXT[0, 1] = "Are you tired of losing customer? Are you tired of waiting a long time to refill your pitcher? Introducing Blender 9000 it makes your refilling more faster than average household use. So what are you waiting for? Buy Now!";

        UPGRADE_TEXT[1, 0] = "THE FROZONE";
        UPGRADE_TEXT[1, 1] = "Are you tired of buying ice everyday? Are you done with losing money because of melting ice? Introducing Frozone an ice maker that will create ice for you. Convenient isn't it? Buy Now!";

        UPGRADE_TEXT[2, 0] = "THE STORE-AGE";
        UPGRADE_TEXT[2, 1] = "Are you tired of having fullspace everytime? Are you done with losing stocks? Introducing Store-age a storage that have infinite storage and makes the supplies age like fine wine. You need it right? Buy Now!";

        UPGRADE[0, 0, 0] = 0;
        UPGRADE[0, 1, 0] = 6000;
        UPGRADE[0, 2, 0] = 9000;
        UPGRADE[0, 3, 0] = 13000;
        UPGRADE[0, 4, 0] = 18000;
        UPGRADE[0, 5, 0] = 24000;

        UPGRADE[0, 0, 1] = 0.9;
        UPGRADE[0, 1, 1] = 0.91;
        UPGRADE[0, 2, 1] = 0.92;
        UPGRADE[0, 3, 1] = 0.93;
        UPGRADE[0, 4, 1] = 0.94;
        UPGRADE[0, 5, 1] = 0.95;

        UPGRADE[1, 0, 0] = 0;
        UPGRADE[1, 1, 0] = 20000;
        UPGRADE[1, 2, 0] = 40000;
        UPGRADE[1, 3, 0] = 60000;
        UPGRADE[1, 4, 0] = 80000;
        UPGRADE[1, 5, 0] = 100000;

        UPGRADE[1, 0, 1] = 0;
        UPGRADE[1, 1, 1] = 500;
        UPGRADE[1, 2, 1] = 1000;
        UPGRADE[1, 3, 1] = 2000;
        UPGRADE[1, 4, 1] = 5000;
        UPGRADE[1, 5, 1] = 10000;

        UPGRADE[2, 0, 0] = 10000;

        STAFF_TEXT[0, 0] = "THE CASHIER";
        STAFF_TEXT[0, 1] = "Look the line is long. It looks like you need help from someone. The Cashier is here to help you make the transactions faster.";

        STAFF_TEXT[1, 0] = "THE COOK";
        STAFF_TEXT[1, 1] = "Look the orders are piled up. It looks like you need help from someone. The Cook is here to help you make the refilling faster.";

        STAFF_TEXT[2, 0] = "THE ENTERTAINER";
        STAFF_TEXT[2, 1] = "Look the customers seems bored and about to leave. It looks like that you need help from someone. The Entertainer is here to help you entertain the customers in the waiting time.";

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

    public string[,] UPGRADE_TEXT { get; private set; }

    public double[,,] UPGRADE { get; private set; }

    public int[] STORAGE { get; private set; }

    public string[,] STAFF_TEXT { get; private set; }

}
