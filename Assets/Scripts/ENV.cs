using UnityEngine;

public class ENV : MonoBehaviour
{

    #region AWAKE_METHOD

    void Awake()
    {

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

        TUTORIAL_TEXT = new string[]
        {

            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",

        };

        DEFAULT_RECIPE = new int[]
        {

            2,
            20,
            3,
            5,

        };

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

        };

        STORAGE = new int[]
        {

            50,
            938,
            50,
            750,
            100,

        };

        STARTING_RECIPE = new int[]
        {

            12,
            37,
            12,
            10,

        };

        STARTING_SUPPLIES = new int[]
        {

            0,
            0,
            0,
            0,
            0,

        };

        CUSTOMER_BUDGET = new double[5, 2]
        {

            { 0, 0, },
            { 0, 0, },
            { 0, 0, },
            { 0, 0, },
            { 0, 0, },

        };

        LOCATION_CLASSES = new double[11, 5]
        {

            { 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, },
            { 0, 0, 0, 0, 0, },

        };

        SUPPLIES_MANGO_PRICES = new double[12, 3] 
        {

            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },

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
        LOCATION[1, 1] = 161;
        LOCATION[1, 2] = 0.0011;

        LOCATION[2, 0] = 500;
        LOCATION[2, 1] = 323;
        LOCATION[2, 2] = 0.0013;

        LOCATION[3, 0] = 1000;
        LOCATION[3, 1] = 645;
        LOCATION[3, 2] = 0.0016;

        LOCATION[4, 0] = 3000;
        LOCATION[4, 1] = 1613;
        LOCATION[4, 2] = 0.002;

        LOCATION[5, 0] = 5000;
        LOCATION[5, 1] = 3226;
        LOCATION[5, 2] = 0.0025;

        LOCATION[6, 0] = 10000;
        LOCATION[6, 1] = 9032;
        LOCATION[6, 2] = 0.003;

        LOCATION[7, 0] = 15000;
        LOCATION[7, 1] = 14839;
        LOCATION[7, 2] = 0.0035;

        LOCATION[8, 0] = 20000;
        LOCATION[8, 1] = 20645;
        LOCATION[8, 2] = 0.004;

        LOCATION[9, 0] = 25000;
        LOCATION[9, 1] = 26452;
        LOCATION[9, 2] = 0.0045;

        LOCATION[10, 0] = 30000;
        LOCATION[10, 1] = 32258;
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

        SUPPLIES_MANGO_PRICES[0, 0] = 362;
        SUPPLIES_MANGO_PRICES[0, 1] = 704;
        SUPPLIES_MANGO_PRICES[0, 2] = 1388;

        SUPPLIES_MANGO_PRICES[1, 0] = 352;
        SUPPLIES_MANGO_PRICES[1, 1] = 684;
        SUPPLIES_MANGO_PRICES[1, 2] = 1338;

        SUPPLIES_MANGO_PRICES[2, 0] = 296;
        SUPPLIES_MANGO_PRICES[2, 1] = 572;
        SUPPLIES_MANGO_PRICES[2, 2] = 1104;

        SUPPLIES_MANGO_PRICES[3, 0] = 266;
        SUPPLIES_MANGO_PRICES[3, 1] = 512;
        SUPPLIES_MANGO_PRICES[3, 2] = 1004;

        SUPPLIES_MANGO_PRICES[4, 0] = 244;
        SUPPLIES_MANGO_PRICES[4, 1] = 468;
        SUPPLIES_MANGO_PRICES[4, 2] = 916;

        SUPPLIES_MANGO_PRICES[5, 0] = 332;
        SUPPLIES_MANGO_PRICES[5, 1] = 644;
        SUPPLIES_MANGO_PRICES[5, 2] = 1258;

        SUPPLIES_MANGO_PRICES[6, 0] = 398;
        SUPPLIES_MANGO_PRICES[6, 1] = 776;
        SUPPLIES_MANGO_PRICES[6, 2] = 1532;

        SUPPLIES_MANGO_PRICES[7, 0] = 420;
        SUPPLIES_MANGO_PRICES[7, 1] = 820;
        SUPPLIES_MANGO_PRICES[7, 2] = 1610;

        SUPPLIES_MANGO_PRICES[8, 0] = 396;
        SUPPLIES_MANGO_PRICES[8, 1] = 772;
        SUPPLIES_MANGO_PRICES[8, 2] = 1524;

        SUPPLIES_MANGO_PRICES[9, 0] = 346;
        SUPPLIES_MANGO_PRICES[9, 1] = 672;
        SUPPLIES_MANGO_PRICES[9, 2] = 1324;

        SUPPLIES_MANGO_PRICES[10, 0] = 346;
        SUPPLIES_MANGO_PRICES[10, 1] = 636;
        SUPPLIES_MANGO_PRICES[10, 2] = 1252;

        SUPPLIES_MANGO_PRICES[11, 0] = 344;
        SUPPLIES_MANGO_PRICES[11, 1] = 668;
        SUPPLIES_MANGO_PRICES[11, 2] = 1306;

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

        TUTORIAL_TEXT[0] = "Welcome! Young Entrepreneur.\nTo get started, kindly pressed on Start Day button.";
        TUTORIAL_TEXT[1] = "OOPS! Keep it mind to be able to start a business.\nYou must prepare all the necessary ingredients and recipe.";
        TUTORIAL_TEXT[2] = "In order to buy ingredients,\nkindly pressed on Supplies navigation button.";
        TUTORIAL_TEXT[3] = "The Supplies Section will help you buy all the necessary ingredients you need. Let's buy for each ingredient.";
        TUTORIAL_TEXT[4] = "Once you're done,\nyou can now proceed on purchasing all the items.";
        TUTORIAL_TEXT[5] = "Confirm your purchase.";
        TUTORIAL_TEXT[6] = "Congratulations! You've successfully purchased your first supplies.";
        TUTORIAL_TEXT[7] = "Now to go back to your store, kindly pressed on Close button or Supplies navigation button again.";
        TUTORIAL_TEXT[8] = "It's a best practice to always check your Supplies HUD before you start your day.";
        TUTORIAL_TEXT[9] = "The Supplies HUD will help you count the number you can only store and check how many left ingredients on your stock.";
        TUTORIAL_TEXT[10] = "Now in order to tweak your recipe,\nkindly pressed on Recipe navigation button.";
        TUTORIAL_TEXT[11] = "As a Young Entrepreneur. Being resourceful is a must. Resourceful enough to effectively and efficiently utilize the resources available to achieve desired goals.";
        TUTORIAL_TEXT[12] = $"Now let's make a recipe. Let's try ({DEFAULT_RECIPE[0]}) mangoes, ({DEFAULT_RECIPE[1]}) pieces of graham, ({DEFAULT_RECIPE[2]}) cans of milk, and ({DEFAULT_RECIPE[3]}) ice cubes.";
        TUTORIAL_TEXT[13] = "If you ever run out of funds and can't purchase goods. Reducing the ingredients to be used will help.";
        TUTORIAL_TEXT[14] = "Ofcourse in business there's no such thing as free. A business cannot live without profit.";
        TUTORIAL_TEXT[15] = "Therefore, in order to produce some profit,\nkindly pressed on Marketing navigation button.";
        TUTORIAL_TEXT[16] = "Under the Price Panel, let's name your price around ₱ 50.00 and ₱ 60.00 per cup.";
        TUTORIAL_TEXT[17] = "Once you're done, kindly pressed on Close button or Marketing navigation button again.";
        TUTORIAL_TEXT[18] = "Finally we're almost done! Goodluck to your journey Young Entrepreneur. Until we meet again, kindly pressed on Start Day.";

        CUSTOMER_BUDGET[0, 0] = 200; 
        CUSTOMER_BUDGET[0, 1] = 500; 

        CUSTOMER_BUDGET[1, 0] = 160; 
        CUSTOMER_BUDGET[1, 1] = 280; 

        CUSTOMER_BUDGET[2, 0] = 120; 
        CUSTOMER_BUDGET[2, 1] = 210; 

        CUSTOMER_BUDGET[3, 0] = 80; 
        CUSTOMER_BUDGET[3, 1] = 140; 

        CUSTOMER_BUDGET[4, 0] = 40; 
        CUSTOMER_BUDGET[4, 1] = 70;

        LOCATION_CLASSES[0, 0] = 0.01;
        LOCATION_CLASSES[0, 1] = 0.02;
        LOCATION_CLASSES[0, 2] = 0.07;
        LOCATION_CLASSES[0, 3] = 0.2;
        LOCATION_CLASSES[0, 4] = 0.7;

        LOCATION_CLASSES[1, 0] = 0.01;
        LOCATION_CLASSES[1, 1] = 0.02;
        LOCATION_CLASSES[1, 2] = 0.07;
        LOCATION_CLASSES[1, 3] = 0.3;
        LOCATION_CLASSES[1, 4] = 0.6;

        LOCATION_CLASSES[2, 0] = 0.02;
        LOCATION_CLASSES[2, 1] = 0.03;
        LOCATION_CLASSES[2, 2] = 0.15;
        LOCATION_CLASSES[2, 3] = 0.3;
        LOCATION_CLASSES[2, 4] = 0.5;

        LOCATION_CLASSES[3, 0] = 0.05;
        LOCATION_CLASSES[3, 1] = 0.05;
        LOCATION_CLASSES[3, 2] = 0.2;
        LOCATION_CLASSES[3, 3] = 0.3;
        LOCATION_CLASSES[3, 4] = 0.4;

        LOCATION_CLASSES[4, 0] = 0.1;
        LOCATION_CLASSES[4, 1] = 0.1;
        LOCATION_CLASSES[4, 2] = 0.2;
        LOCATION_CLASSES[4, 3] = 0.3;
        LOCATION_CLASSES[4, 4] = 0.2;

        LOCATION_CLASSES[5, 0] = 0.15;
        LOCATION_CLASSES[5, 1] = 0.25;
        LOCATION_CLASSES[5, 2] = 0.3;
        LOCATION_CLASSES[5, 3] = 0.2;
        LOCATION_CLASSES[5, 4] = 0.1;

        LOCATION_CLASSES[6, 0] = 0.2;
        LOCATION_CLASSES[6, 1] = 0.3;
        LOCATION_CLASSES[6, 2] = 0.35;
        LOCATION_CLASSES[6, 3] = 0.1;
        LOCATION_CLASSES[6, 4] = 0.05;

        LOCATION_CLASSES[7, 0] = 0.3;
        LOCATION_CLASSES[7, 1] = 0.35;
        LOCATION_CLASSES[7, 2] = 0.3;
        LOCATION_CLASSES[7, 3] = 0.03;
        LOCATION_CLASSES[7, 4] = 0.02;

        LOCATION_CLASSES[8, 0] = 0.35;
        LOCATION_CLASSES[8, 1] = 0.4;
        LOCATION_CLASSES[8, 2] = 0.2;
        LOCATION_CLASSES[8, 3] = 0.05;
        LOCATION_CLASSES[8, 4] = 0;

        LOCATION_CLASSES[9, 0] = 0.5;
        LOCATION_CLASSES[9, 1] = 0.4;
        LOCATION_CLASSES[9, 2] = 0.05;
        LOCATION_CLASSES[9, 3] = 0.05;
        LOCATION_CLASSES[9, 4] = 0;

        LOCATION_CLASSES[10, 0] = 0.6;
        LOCATION_CLASSES[10, 1] = 0.35;
        LOCATION_CLASSES[10, 2] = 0.05;
        LOCATION_CLASSES[10, 3] = 0;
        LOCATION_CLASSES[10, 4] = 0;

        DEFAULT_PRICE = 56;
        MINIMUM_CUPS = 10;
        INCREMENT_POPULARITY_PER_DAY = 2;
        STARTING_CAPITAL = 1000;
        UPGRADE_BOOST = 1;
        STARTING_PRICE = 30;

        IS_LOADING = "isLoading";
        NOW_INFORMING = "nowInforming";
        NEXT = "next";
        NOW_PLAYING = "nowPlaying";
        DIALOG_TUTORIAL = "dialogTutorial";
        DIALOG = "dialog";
        OPTION_PANE = "optionPane";
        INPUT_PANE = "inputPane";
        INPUT_PANE_TO_DIALOG = "inputPaneToDialog";
        DIALOG_TO_INPUT_PANE = "dialogToInputPane";
        OK = "ok";
        OK_TUTORIAL = "okTutorial";

        PLAYLIST_TEXT = new string[]
        {

            "ASHAMALUEV MUSIC\n\"Cooking\"\nHappy Music",
            "ASHAMALUEV MUSIC\n\"Funny\"\nHappy Music",
            "ASHAMALUEV MUSIC\n\"Nature\"\nAcoustic Music",
            "ASHAMALUEV MUSIC\n\"Quirky\"\nHappy Music",
            "ASHAMALUEV MUSIC\n\"Upbeat Acoustic\"\nAcoustic Music",

        };

    }

    #endregion

    #region AUTOMATED_PROPERTIES

    public static double DEFAULT_PRICE { get; private set; }

    public static double[,,] SUPPLIES { get; private set; }

    public static double[,] LOCATION { get; private set; }

    public static double[,] STAFF { get; private set; }

    public static double[,] TEMPERATURE { get; private set; }

    public static double[] AVERAGE_SUPPLIES_COST { get; private set; }

    public static int INCREMENT_POPULARITY_PER_DAY { get; private set; }

    public static int MINIMUM_CUPS { get; private set; }

    public static int[] DEFAULT_RECIPE { get; private set; }

    public static int[] TARGET_CRITERIA { get; private set; }

    public static string[,] LOCATION_TEXT { get; private set; }

    public static double[,] ADVERTISEMENT { get; private set; }

    public static double[,] STANDING { get; private set; }

    public static string[,] UPGRADE_TEXT { get; private set; }

    public static double[,,] UPGRADE { get; private set; }

    public static int[] STORAGE { get; private set; }

    public static string[,] STAFF_TEXT { get; private set; }

    public static string[] TUTORIAL_TEXT { get; private set; }

    public static double STARTING_CAPITAL { get; private set; }

    public static int[] STARTING_RECIPE { get; private set; }

    public static int[] STARTING_SUPPLIES { get; private set; }

    public static double UPGRADE_BOOST { get; private set; }

    public static double STARTING_PRICE { get; private set; }

    public static string IS_LOADING { get; private set; }

    public static string NOW_INFORMING { get; private set; }

    public static string NEXT { get; private set; }

    public static string[] PLAYLIST_TEXT { get; private set; }

    public static string NOW_PLAYING { get; private set; }

    public static string DIALOG_TUTORIAL { get; private set; }

    public static string DIALOG { get; private set; }

    public static string OPTION_PANE { get; private set; }

    public static string INPUT_PANE { get; private set; }

    public static string INPUT_PANE_TO_DIALOG { get; private set; }

    public static string DIALOG_TO_INPUT_PANE { get; private set; }

    public static string OK { get; private set; }

    public static string OK_TUTORIAL { get; private set; }

    public static double[,] SUPPLIES_MANGO_PRICES { get; private set; }

    public static double[,] CUSTOMER_BUDGET { get; private set; }

    public static double[,] LOCATION_CLASSES { get; private set; }

    #endregion

}
