using System.Collections.Generic;

[System.Serializable]
public class PlayerModel
{

    #region DECLARATION

    public double player_capital;
    public double player_customer_satisfaction;
    public double player_equipments;
    public double player_price;
    public double player_profit_and_loss;
    public double player_reputation;
    public double player_temperature;
    public double player_top_earnings;
    public double[] player_earnings;
    public double[] player_expenses;
    public double[] player_gross_margin;
    public double[] player_gross_profit;
    public double[] player_marketing;
    public double[] player_popularity;
    public double[] player_rent;
    public double[] player_revenue;
    public double[] player_satisfaction;
    public double[] player_stock_lost;
    public double[] player_stock_used;
    public int player_advertisement;
    public int player_constant;
    public int player_cups_sold;
    public int player_days_without_advertisement;
    public int player_feedback;
    public int player_ice_cubes_melted;
    public int player_impatient_customers;
    public int player_location;
    public int player_over_priced_customers;
    public int player_satisfied_customers;
    public int player_tip;
    public int player_unsatisfied_customers;
    public int[] player_date;
    public int[] player_recipe;
    public int[] player_storage;
    public int[] player_supplies;
    public int[] player_target_criteria;
    public int[] player_upgrade;
    public List<int> player_staffs;
    public string player_email;
    public string player_id;
    public string player_image;
    public string player_name;

    #endregion

    #region PLAYER_MODEL_CONSTRUCTORS

    public PlayerModel(PlayerStruct _playerStruct)
    {

        player_advertisement = _playerStruct.player_advertisement;
        player_capital = _playerStruct.player_capital;
        player_constant = _playerStruct.player_constant;
        player_cups_sold = _playerStruct.player_cups_sold;
        player_customer_satisfaction = _playerStruct.player_customer_satisfaction;
        player_date = _playerStruct.player_date;
        player_days_without_advertisement = _playerStruct.player_days_without_advertisement;
        player_earnings = _playerStruct.player_earnings;
        player_email = _playerStruct.player_email;
        player_equipments = _playerStruct.player_equipments;
        player_expenses = _playerStruct.player_expenses;
        player_feedback = _playerStruct.player_feedback;
        player_gross_margin = _playerStruct.player_gross_margin;
        player_gross_profit = _playerStruct.player_gross_profit;
        player_ice_cubes_melted = _playerStruct.player_ice_cubes_melted;
        player_id = _playerStruct.player_id;
        player_image = _playerStruct.player_image;
        player_impatient_customers = _playerStruct.player_impatient_customers;
        player_location = _playerStruct.player_location;
        player_marketing = _playerStruct.player_marketing;
        player_name = _playerStruct.player_name;
        player_over_priced_customers = _playerStruct.player_over_priced_customers;
        player_popularity = _playerStruct.player_popularity;
        player_price = _playerStruct.player_price;
        player_profit_and_loss = _playerStruct.player_profit_and_loss;
        player_recipe = _playerStruct.player_recipe;
        player_rent = _playerStruct.player_rent;
        player_reputation = _playerStruct.player_reputation;
        player_revenue = _playerStruct.player_revenue;
        player_satisfaction = _playerStruct.player_satisfaction;
        player_satisfied_customers = _playerStruct.player_satisfied_customers;
        player_staffs = _playerStruct.player_staffs;
        player_stock_lost = _playerStruct.player_stock_lost;
        player_stock_used = _playerStruct.player_stock_used;
        player_storage = _playerStruct.player_storage;
        player_supplies = _playerStruct.player_supplies;
        player_target_criteria = _playerStruct.player_target_criteria;
        player_temperature = _playerStruct.player_temperature;
        player_tip = _playerStruct.player_tip;
        player_top_earnings = _playerStruct.player_top_earnings;
        player_unsatisfied_customers = _playerStruct.player_unsatisfied_customers;
        player_upgrade = _playerStruct.player_upgrade;

    }

    public PlayerModel(PLAYER _player)
    {

        player_advertisement = _player.PlayerAdvertisement;
        player_capital = _player.PlayerCapital;
        player_constant = _player.PlayerConstant;
        player_cups_sold = _player.PlayerCupsSold;
        player_customer_satisfaction = _player.PlayerCustomerSatisfaction;
        player_date = _player.PlayerDate;
        player_days_without_advertisement = _player.PlayerDaysWithoutAdvertisement;
        player_earnings = _player.PlayerEarnings;
        player_email = _player.PlayerEmail;
        player_equipments = _player.PlayerEquipments;
        player_expenses = _player.PlayerExpenses;
        player_feedback = _player.PlayerFeedback;
        player_gross_margin = _player.PlayerGrossMargin;
        player_gross_profit = _player.PlayerGrossProfit;
        player_ice_cubes_melted = _player.PlayerIceCubesMelted;
        player_id = _player.PlayerId;
        player_image = _player.PlayerImage;
        player_impatient_customers = _player.PlayerImpatientCustomers;
        player_location = _player.PlayerLocation;
        player_marketing = _player.PlayerMarketing;
        player_name = _player.PlayerName;
        player_over_priced_customers = _player.PlayerOverPricedCustomers;
        player_popularity = _player.PlayerPopularity;
        player_price = _player.PlayerPrice;
        player_profit_and_loss = _player.PlayerProfitAndLoss;
        player_recipe = _player.PlayerRecipe;
        player_rent = _player.PlayerRent;
        player_reputation = _player.PlayerReputation;
        player_revenue = _player.PlayerRevenue;
        player_satisfaction = _player.PlayerSatisfaction;
        player_satisfied_customers = _player.PlayerSatisfiedCustomers;
        player_staffs = _player.PlayerStaffs;
        player_stock_lost = _player.PlayerStockLost;
        player_stock_used = _player.PlayerStockUsed;
        player_storage = _player.PlayerStorage;
        player_supplies = _player.PlayerSupplies;
        player_target_criteria = _player.PlayerTargetCriteria;
        player_temperature = _player.PlayerTemperature;
        player_tip = _player.PlayerTip;
        player_top_earnings = _player.PlayerTopEarnings;
        player_unsatisfied_customers = _player.PlayerUnsatisfiedCustomers;
        player_upgrade = _player.PlayerUpgrade;

    }

    #endregion

}
