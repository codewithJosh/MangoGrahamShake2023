using System.Collections.Generic;

[System.Serializable]
public class PlayerModel
{

    public bool player_is_student;
    public double player_capital;
    public double player_price;
    public double[] player_popularity;
    public double[] player_satisfaction;
    public int player_advertisement;
    public int player_location;
    public double player_temperature;
    public int[] player_recipe;
    public int[] player_supplies;
    public int[] player_date;
    public string player_first_name;
    public string player_id;
    public string player_image;
    public string player_last_name;
    public string player_student_id;
    public string room_id;
    public int player_constant;
    public int[] player_target_criteria;
    public double[] player_revenue;
    public double[] player_stock_used;
    public double[] player_stock_lost;
    public double[] player_gross_profit;
    public double[] player_gross_margin;
    public double[] player_rent;
    public double[] player_marketing;
    public double[] player_expenses;
    public double[] player_earnings;
    public List<int> player_staffs;
    public double player_reputation;

    public PlayerModel(PlayerStruct _playerStruct)
    {

        player_is_student = _playerStruct.player_is_student;
        player_advertisement = _playerStruct.player_advertisement;
        player_capital = _playerStruct.player_capital;
        player_first_name = _playerStruct.player_first_name;
        player_id = _playerStruct.player_id;
        player_image = _playerStruct.player_image;
        player_last_name = _playerStruct.player_last_name;
        player_location = _playerStruct.player_location;
        player_popularity = _playerStruct.player_popularity;
        player_price = _playerStruct.player_price;
        player_recipe = _playerStruct.player_recipe;
        player_satisfaction = _playerStruct.player_satisfaction;
        player_student_id = _playerStruct.player_student_id;
        player_supplies = _playerStruct.player_supplies;
        player_temperature = _playerStruct.player_temperature;
        room_id = _playerStruct.room_id;
        player_date = _playerStruct.player_date;
        player_constant = _playerStruct.player_constant;
        player_target_criteria = _playerStruct.player_target_criteria;
        player_revenue = _playerStruct.player_revenue;
        player_stock_used = _playerStruct.player_stock_used;
        player_stock_lost = _playerStruct.player_stock_lost;
        player_gross_profit = _playerStruct.player_gross_profit;
        player_gross_margin = _playerStruct.player_gross_margin;
        player_rent = _playerStruct.player_rent;
        player_marketing = _playerStruct.player_marketing;
        player_expenses = _playerStruct.player_expenses;
        player_earnings = _playerStruct.player_earnings;
        player_staffs = _playerStruct.player_staffs;
        player_reputation = _playerStruct.player_reputation;

    }

    public PlayerModel(Player _player)
    {

        player_is_student = _player.PlayerIsStudent;
        player_advertisement = _player.PlayerAdvertisement;
        player_capital = _player.PlayerCapital;
        player_first_name = _player.PlayerFirstName;
        player_id = _player.PlayerId;
        player_image = _player.PlayerImage;
        player_last_name = _player.PlayerLastName;
        player_location = _player.PlayerLocation;
        player_popularity = _player.PlayerPopularity;
        player_price = _player.PlayerPrice;
        player_recipe = _player.PlayerRecipe;
        player_satisfaction = _player.PlayerSatisfaction;
        player_student_id = _player.PlayerStudentId;
        player_supplies = _player.PlayerSupplies;
        player_temperature = _player.PlayerTemperature;
        room_id = _player.RoomId;
        player_date = _player.PlayerDate;
        player_constant = _player.PlayerConstant;
        player_target_criteria = _player.PlayerTargetCriteria;
        player_revenue = _player.PlayerRevenue;
        player_stock_used = _player.PlayerStockUsed;
        player_stock_lost = _player.PlayerStockLost;
        player_gross_profit = _player.PlayerGrossProfit;
        player_gross_margin = _player.PlayerGrossMargin;
        player_rent = _player.PlayerRent;
        player_marketing = _player.PlayerMarketing;
        player_expenses = _player.PlayerExpenses;
        player_earnings = _player.PlayerEarnings;
        player_staffs = _player.PlayerStaffs;
        player_reputation = _player.PlayerReputation;

    }

}
