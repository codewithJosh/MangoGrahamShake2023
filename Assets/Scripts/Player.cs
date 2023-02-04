using UnityEngine;

public class Player : MonoBehaviour
{

    private double playerAdvertisement;
    private double playerCapital;
    private double playerPopularity;
    private double playerPrice;
    private double playerSatisfaction;
    private int playerTemperature;
    private int[] playerLeft;
    private int[] playerPerServe;

    private void Save()
    {

        Database.SavePlayer(this);

    }

    private void Load()
    {

        PlayerModel playerModel = Database.LoadPlayer();

        playerAdvertisement = playerModel.player_advertisement;
        playerCapital = playerModel.player_capital;
        playerPopularity = playerModel.player_popularity;
        playerPrice = playerModel.player_price;
        playerSatisfaction = playerModel.player_satisfaction;
        playerTemperature = playerModel.player_temperature;
        playerLeft = playerModel.player_left;
        playerPerServe = playerModel.player_per_serve;

    }

    private void Create()
    {

        playerAdvertisement = 0;
        playerCapital = 2000.00;
        playerPopularity = 0.01;
        playerPrice = 65f;
        playerSatisfaction = 1;
        playerTemperature = Random.Range(20, 45);
        playerLeft = new int[]
        {
            0,
            0,
            0,
            0,
            0
        };
        playerPerServe = new int[]
        {
            4,
            2,
            2,
            2
        };

        Save();

    }

    public double PlayerAdvertisement
    {

        get { return playerAdvertisement; }
        set { playerAdvertisement = value; }

    }

    public double PlayerCapital 
    { 

        get { return playerCapital; } 
        set { playerCapital = value; } 

    }
    
    public double PlayerPopularity
    { 

        get { return playerPopularity; } 
        set { playerPopularity = value; } 

    }
    
    public double PlayerPrice
    { 

        get { return playerPrice; } 
        set { playerPrice = value; } 

    }
    
    public double PlayerSatisfaction
    { 

        get { return playerSatisfaction; } 
        set { playerSatisfaction = value; } 

    }
    
    public int PlayerTemperature
    { 

        get { return playerTemperature; } 
        set { playerTemperature = value; }

    }

    public int[] PlayerLeft
    {

        get { return playerLeft; }
        set { playerLeft = value; }

    }

    public int[] PlayerPerServe
{ 

        get { return playerPerServe; } 
        set { playerPerServe = value; } 

    }

    public void OnSave() { Save(); }

    public void OnLoad() { Load(); }

    public void OnCreate() { Create(); }

}
