[System.Serializable]
public class PlayerModel
{

    public double player_advertisement;
    public double player_capital;
    public double player_popularity;
    public double player_price;
    public double player_satisfaction;
    public int player_temperature;
    public int[] player_left;
    public int[] player_per_serve;

    public PlayerModel(FirebasePlayerModel _firebasePlayerModel)
    {

        player_advertisement = _firebasePlayerModel.player_advertisement;
        player_capital = _firebasePlayerModel.player_capital;
        player_popularity = _firebasePlayerModel.player_popularity;
        player_price = _firebasePlayerModel.player_price;
        player_satisfaction = _firebasePlayerModel.player_satisfaction;
        player_temperature = _firebasePlayerModel.player_temperature;
        player_left = _firebasePlayerModel.player_left;
        player_per_serve = _firebasePlayerModel.player_per_serve;

    }

    public PlayerModel(Player _player)
    {

        player_advertisement = _player.PlayerAdvertisement;
        player_capital = _player.PlayerCapital;
        player_popularity = _player.PlayerPopularity;
        player_price = _player.PlayerPrice;
        player_satisfaction = _player.PlayerSatisfaction;
        player_temperature = _player.PlayerTemperature;
        player_left = _player.PlayerLeft;
        player_per_serve = _player.PlayerPerServe;

    }

}
