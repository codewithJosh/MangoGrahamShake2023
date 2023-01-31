using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector] public int[] playerLeft;
    [HideInInspector] public int[] playerPerServe;
    [HideInInspector] public float playerCapital;
    [HideInInspector] public float playerPopularity;
    [HideInInspector] public float playerSatisfaction;
    [HideInInspector] public float playerPrice;
    [HideInInspector] public float playerAdvertisement;
    [HideInInspector] public int playerTemperature;

    public void NewPlayer()
    {

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
            0,
            0,
            0,
            0
        };

        playerCapital = 2000.00f;
        playerPopularity = 0.1f;
        playerSatisfaction = 1f;
        playerPrice = 1f;
        playerAdvertisement = 0f;
        playerTemperature = Random.Range(20, 45);

        SavePlayer();

    }

    public void SavePlayer()
    {

        Database.SavePlayer(this);

    }

    public void LoadPlayer()
    {

        PlayerModel player = Database.LoadPlayer();

        playerLeft = player.player_left;
        playerPerServe = player.player_per_serve;
        playerCapital = player.player_capital;
        playerPopularity = player.player_popularity;
        playerSatisfaction = player.player_satisfaction;
        playerPrice = player.player_price;
        playerAdvertisement = player.player_advertisement;
        playerTemperature = player.player_temperature;

    }

}
