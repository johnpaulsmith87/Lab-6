using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;

public partial class RestaurantReview : Page
{
    private const string RESTAURANTXMLPATH = "restaurant_reviews.xml";
    protected List<RestaurantsRestaurant> GetXMLToList(string path = RESTAURANTXMLPATH)
    {
        path = Server.MapPath(path);
        using (var fs = new FileStream(path, FileMode.Open))
        {
            var xmlSerializer = new XmlSerializer(typeof(Restaurants));
            return ((Restaurants)xmlSerializer.Deserialize(fs)).Restaurant.ToList();
        }
    }
    protected string SaveToXML(List<RestaurantsRestaurant> restos, string path = RESTAURANTXMLPATH)
    {
        path = Server.MapPath(path);
        string result = string.Empty;
        RestaurantsRestaurant[] restaurantsProperty = restos.ToArray();
        Restaurants restaurants = new Restaurants();
        restaurants.Restaurant = restaurantsProperty; 
        try
        {

            using (var fs = new FileStream(path, FileMode.Create))
            {
                var xmlSerializer = new XmlSerializer(typeof(Restaurants));
                xmlSerializer.Serialize(fs, restaurants);
                result = "Successfully saved to: " + path;
            }
        }
        catch
        {
            // to make sure it's caught is all
        }
            return result;

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlRestaurantDetails.Visible = false;

            var restaurants = GetXMLToList();
            SessionHelper.Set("Restaurants", restaurants);
            ddlSelectRestaurant.Items.AddRange(restaurants.Select(rest => new ListItem(rest.Name, rest.Name)).ToArray());
        }
    }

    protected void ddlSelectRestaurant_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelectRestaurant.SelectedValue != "-1")
        {
            var restaurants = SessionHelper.Get<List<RestaurantsRestaurant>>("Restaurants");
            var selectedRestaurantName = ddlSelectRestaurant.SelectedValue;
            var selectedRestaurant = restaurants.Single(rest => rest.Name == selectedRestaurantName);
            SessionHelper.Set("SelectedRestaurant", selectedRestaurant);
            pnlRestaurantDetails.Visible = true;
            pnlSave.Visible = false;
            txtAddress.Text = selectedRestaurant.RestaurantAddress.Address + Environment.NewLine
                + selectedRestaurant.RestaurantAddress.City + ", " + selectedRestaurant.RestaurantAddress.PostalCode;
            txtSummary.Text = selectedRestaurant.Summary;
            foreach (ListItem item in ddlRating.Items)
            {
                if (item.Text == selectedRestaurant.Rating.ToString())
                {
                    ddlRating.ClearSelection();
                    item.Selected = true;
                    break;
                }
            }
        }
    }
    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        pnlSave.Visible = true;
        var rest = SessionHelper.Get<RestaurantsRestaurant>("SelectedRestaurant");
        //now pull from the list of allrestaurants
        var allRestaurants = SessionHelper.Get<List<RestaurantsRestaurant>>("Restaurants");

        var selectedRestaurant = allRestaurants.Single(r => r.Name == rest.Name);
        selectedRestaurant.Summary = txtSummary.Text;
        selectedRestaurant.Rating = Convert.ToInt32(ddlRating.SelectedValue);
        //save to xml file
        var result = SaveToXML(allRestaurants);
        if(string.IsNullOrEmpty(result))
        {
            lblResult.Text = "Error saving XML";
            lblResult.CssClass = "alert alert-danger";
        }
        else
        {
            lblResult.Text = result;
            lblResult.CssClass = "alert alert-success";
        }
    }
}