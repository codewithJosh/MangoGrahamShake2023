using Firebase.Firestore;
using System.Collections.Generic;

[FirestoreData]
public struct PlayerStruct
{

    #region AUTOMATED_PROPERTIES

    [FirestoreProperty]
    public double player_capital { get; set; }

    [FirestoreProperty]
    public double player_price { get; set; }

    [FirestoreProperty]
    public double player_reputation { get; set; }

    [FirestoreProperty]
    public double player_temperature { get; set; }

    [FirestoreProperty]
    public double[] player_earnings { get; set; }

    [FirestoreProperty]
    public double[] player_expenses { get; set; }

    [FirestoreProperty]
    public double[] player_gross_margin { get; set; }

    [FirestoreProperty]
    public double[] player_gross_profit { get; set; }

    [FirestoreProperty]
    public double[] player_marketing { get; set; }

    [FirestoreProperty]
    public double[] player_popularity { get; set; }

    [FirestoreProperty]
    public double[] player_rent { get; set; }

    [FirestoreProperty]
    public double[] player_revenue { get; set; }

    [FirestoreProperty]
    public double[] player_satisfaction { get; set; }

    [FirestoreProperty]
    public double[] player_stock_lost { get; set; }

    [FirestoreProperty]
    public double[] player_stock_used { get; set; }

    [FirestoreProperty]
    public int player_advertisement { get; set; }

    [FirestoreProperty]
    public int player_constant { get; set; }

    [FirestoreProperty]
    public int player_cups_sold { get; set; }

    [FirestoreProperty]
    public int player_impatient_customers { get; set; }

    [FirestoreProperty]
    public int player_location { get; set; }

    [FirestoreProperty]
    public int player_over_priced_customers { get; set; }

    [FirestoreProperty]
    public int player_satisfied_customers { get; set; }

    [FirestoreProperty]
    public int player_unsatisfied_customers { get; set; }

    [FirestoreProperty]
    public int[] player_date { get; set; }

    [FirestoreProperty]
    public int[] player_recipe { get; set; }

    [FirestoreProperty]
    public int[] player_supplies { get; set; }

    [FirestoreProperty]
    public int[] player_target_criteria { get; set; }

    [FirestoreProperty]
    public List<int> player_staffs { get; set; }

    [FirestoreProperty]
    public string player_id { get; set; }

    [FirestoreProperty]
    public string player_image { get; set; }

    [FirestoreProperty]
    public string player_name { get; set; }

    [FirestoreProperty]
    public int[] player_storage { get; set; }

    [FirestoreProperty]
    public double player_customer_satisfaction { get; set; }

    [FirestoreProperty]
    public int player_ice_cubes_melted { get; set; }

    [FirestoreProperty]
    public double player_top_earnings { get; set; }

    [FirestoreProperty]
    public int player_days_without_advertisement { get; set; }

    [FirestoreProperty]
    public int player_feedback { get; set; }

    [FirestoreProperty]
    public double player_equipments { get; set; }

    [FirestoreProperty]
    public double player_profit_and_loss { get; set; }

    [FirestoreProperty]
    public int[] player_upgrade { get; set; }

    #endregion

}
