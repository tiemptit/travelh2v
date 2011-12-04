/**
 * 
 */
package uit.is.thesis.travel.models;
import java.io.Serializable;

/**
 * @author LEHIEU
 *
 */

@SuppressWarnings("serial")
public class PlaceModel implements Serializable{
	//Properties from Database table
	public int id;
	public PlaceCategoryModel place_category_obj;
    public String name;
    public String imgurl;
    public double lat; 
    public double lng; 
    public String house_number;
    public String street;
    public String ward;
    public String district;
    public String city;
    public String province;
    public String country;
    public String phone_number;
    public String email;
    public String website;
    public String history;
    public String details;
    public String sources;
    public double general_rating;
    public double general_count_rating;
    public double general_sum_rating;
    public double estimate_rating;
    
    // more Properties
    public double distance; // distance between current location of user and location of place
    private int id_itemOnListView; // id of item on listView
	
    /**
	 * @return the id
	 */
	public int getId() {
		return id;
	}
	/**
	 * @param id the id to set
	 */
	public void setId(int id) {
		this.id = id;
	}
	/**
	 * @return the place_category_obj
	 */
	public PlaceCategoryModel getPlace_category_obj() {
		return place_category_obj;
	}
	/**
	 * @param place_category_obj the place_category_obj to set
	 */
	public void setPlace_category_obj(PlaceCategoryModel place_category_obj) {
		this.place_category_obj = place_category_obj;
	}
	/**
	 * @return the name
	 */
	public String getName() {
		return name;
	}
	/**
	 * @param name the name to set
	 */
	public void setName(String name) {
		this.name = name;
	}
	/**
	 * @return the imgurl
	 */
	public String getImgurl() {
		return imgurl;
	}
	/**
	 * @param imgurl the imgurl to set
	 */
	public void setImgurl(String imgurl) {
		this.imgurl = imgurl;
	}
	/**
	 * @return the lat
	 */
	public double getLat() {
		return lat;
	}
	/**
	 * @param lat the lat to set
	 */
	public void setLat(double lat) {
		this.lat = lat;
	}
	/**
	 * @return the lng
	 */
	public double getLng() {
		return lng;
	}
	/**
	 * @param lng the lng to set
	 */
	public void setLng(double lng) {
		this.lng = lng;
	}
	/**
	 * @return the house_number
	 */
	public String getHouse_number() {
		return house_number;
	}
	/**
	 * @param house_number the house_number to set
	 */
	public void setHouse_number(String house_number) {
		this.house_number = house_number;
	}
	/**
	 * @return the street
	 */
	public String getStreet() {
		return street;
	}
	/**
	 * @param street the street to set
	 */
	public void setStreet(String street) {
		this.street = street;
	}
	/**
	 * @return the ward
	 */
	public String getWard() {
		return ward;
	}
	/**
	 * @param ward the ward to set
	 */
	public void setWard(String ward) {
		this.ward = ward;
	}
	/**
	 * @return the district
	 */
	public String getDistrict() {
		return district;
	}
	/**
	 * @param district the district to set
	 */
	public void setDistrict(String district) {
		this.district = district;
	}
	/**
	 * @return the city
	 */
	public String getCity() {
		return city;
	}
	/**
	 * @param city the city to set
	 */
	public void setCity(String city) {
		this.city = city;
	}
	/**
	 * @return the province
	 */
	public String getProvince() {
		return province;
	}
	/**
	 * @param province the province to set
	 */
	public void setProvince(String province) {
		this.province = province;
	}
	/**
	 * @return the country
	 */
	public String getCountry() {
		return country;
	}
	/**
	 * @param country the country to set
	 */
	public void setCountry(String country) {
		this.country = country;
	}
	/**
	 * @return the phone_number
	 */
	public String getPhone_number() {
		return phone_number;
	}
	/**
	 * @param phone_number the phone_number to set
	 */
	public void setPhone_number(String phone_number) {
		this.phone_number = phone_number;
	}
	/**
	 * @return the email
	 */
	public String getEmail() {
		return email;
	}
	/**
	 * @param email the email to set
	 */
	public void setEmail(String email) {
		this.email = email;
	}
	/**
	 * @return the website
	 */
	public String getWebsite() {
		return website;
	}
	/**
	 * @param website the website to set
	 */
	public void setWebsite(String website) {
		this.website = website;
	}
	/**
	 * @return the history
	 */
	public String getHistory() {
		return history;
	}
	/**
	 * @param history the history to set
	 */
	public void setHistory(String history) {
		this.history = history;
	}
	/**
	 * @return the details
	 */
	public String getDetails() {
		return details;
	}
	/**
	 * @param details the details to set
	 */
	public void setDetails(String details) {
		this.details = details;
	}
	/**
	 * @return the sources
	 */
	public String getSources() {
		return sources;
	}
	/**
	 * @param sources the sources to set
	 */
	public void setSources(String sources) {
		this.sources = sources;
	}
	/**
	 * @return the general_rating
	 */
	public double getGeneral_rating() {
		return general_rating;
	}
	/**
	 * @param general_rating the general_rating to set
	 */
	public void setGeneral_rating(double general_rating) {
		this.general_rating = general_rating;
	}
	/**
	 * @return the general_count_rating
	 */
	public double getGeneral_count_rating() {
		return general_count_rating;
	}
	/**
	 * @param general_count_rating the general_count_rating to set
	 */
	public void setGeneral_count_rating(double general_count_rating) {
		this.general_count_rating = general_count_rating;
	}
	
	/**
	 * @return the general_sum_rating
	 */
	public double getGeneral_sum_rating() {
		return general_sum_rating;
	}
	/**
	 * @param general_sum_rating the general_sum_rating to set
	 */
	public void setGeneral_sum_rating(double general_sum_rating) {
		this.general_sum_rating = general_sum_rating;
	}
	
	/**
	 * @return the estimate_rating
	 */
	public double getEstimate_rating() {
		return estimate_rating;
	}
	/**
	 * @param estimate_rating the estimate_rating to set
	 */
	public void setEstimate_rating(double estimate_rating) {
		this.estimate_rating = estimate_rating;
	}
	/** Return round distance */
	public double getDistance() {
		double d = Math.pow(10, 3) ;
		return Math.round( this.distance * d ) / d;
	}
	/** Compute distance from current location of user to location of place use Haverine formula */
	public void setDistance(double latitude, double longitude) {
		// Haversine formula
		double deltaLatitude = Math.toRadians(Math.abs(this.lat - latitude));
		double deltaLongitude = Math.toRadians(Math.abs(this.lng - longitude));
		double a = Math.sin(deltaLatitude / 2) * Math.sin(deltaLatitude / 2) + 
				   Math.cos(Math.toRadians(this.lat)) * Math.cos(Math.toRadians(latitude)) * 
				   Math.sin(deltaLongitude / 2) * Math.sin(deltaLongitude / 2);
		double c =  2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
		this.distance = 6371 * c;
	}
	/**
	 * @return the id_listView
	 */
	public int getId_itemOnListView() {
		return id_itemOnListView;
	}
	/**
	 * @param id the id to set
	 */
	public void setId_itemOnListView(int id) {
		this.id_itemOnListView = id;
	}
	
}
