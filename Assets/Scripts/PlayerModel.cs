[System.Serializable]
public class PlayerModel
{

    public int[] player_left;
    public int[] player_per_serve;
    public float player_popularity;
    public float player_satisfaction;
    public float player_capital;
    public float player_price;
    public float player_advertisement;
    public int player_temperature;

    public PlayerModel(Player _player)
    {

        player_left = _player.playerLeft;
        player_per_serve = _player.playerPerServe;
        player_capital = _player.playerCapital;
        player_popularity = _player.playerPopularity;
        player_satisfaction = _player.playerSatisfaction;
        player_price = _player.playerPrice;
        player_advertisement = _player.playerAdvertisement;
        player_temperature = _player.playerTemperature;

    }

}
