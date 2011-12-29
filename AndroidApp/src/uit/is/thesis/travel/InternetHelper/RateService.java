/**
 * 
 */
package uit.is.thesis.travel.InternetHelper;

import java.io.InputStream;
import java.net.URL;

import android.util.Log;

import uit.is.thesis.travel.utilities.ConfigUtil;
import uit.is.thesis.travel.utilities.JsonUtil;

/**
 * @author LEHIEU
 * 
 */
public class RateService {
	String username, time;
	int id_place, id_temperature, id_companion, id_farmiliarity, id_mood,
			id_budget, id_weather, id_travel_length;
	float rating;

	/**
	 * @return the username
	 */
	public String getUsername() {
		return username;
	}

	/**
	 * @param username
	 *            the username to set
	 */
	public void setUsername(String username) {
		this.username = username;
	}

	/**
	 * @return the time
	 */
	public String getTime() {
		return time;
	}

	/**
	 * @param time
	 *            the time to set
	 */
	public void setTime(String time) {
		this.time = time;
	}

	/**
	 * @return the id_place
	 */
	public int getId_place() {
		return id_place;
	}

	/**
	 * @param id_place
	 *            the id_place to set
	 */
	public void setId_place(int id_place) {
		this.id_place = id_place;
	}

	/**
	 * @return the id_temperature
	 */
	public int getId_temperature() {
		return id_temperature;
	}

	/**
	 * @param id_temperature
	 *            the id_temperature to set
	 */
	public void setId_temperature(int id_temperature) {
		this.id_temperature = id_temperature;
	}

	/**
	 * @return the id_companion
	 */
	public int getId_companion() {
		return id_companion;
	}

	/**
	 * @param id_companion
	 *            the id_companion to set
	 */
	public void setId_companion(int id_companion) {
		this.id_companion = id_companion;
	}

	/**
	 * @return the id_farmiliarity
	 */
	public int getId_farmiliarity() {
		return id_farmiliarity;
	}

	/**
	 * @param id_farmiliarity
	 *            the id_farmiliarity to set
	 */
	public void setId_farmiliarity(int id_farmiliarity) {
		this.id_farmiliarity = id_farmiliarity;
	}

	/**
	 * @return the id_mood
	 */
	public int getId_mood() {
		return id_mood;
	}

	/**
	 * @param id_mood
	 *            the id_mood to set
	 */
	public void setId_mood(int id_mood) {
		this.id_mood = id_mood;
	}

	/**
	 * @return the id_budget
	 */
	public int getId_budget() {
		return id_budget;
	}

	/**
	 * @param id_budget
	 *            the id_budget to set
	 */
	public void setId_budget(int id_budget) {
		this.id_budget = id_budget;
	}

	/**
	 * @return the id_weather
	 */
	public int getId_weather() {
		return id_weather;
	}

	/**
	 * @param id_weather
	 *            the id_weather to set
	 */
	public void setId_weather(int id_weather) {
		this.id_weather = id_weather;
	}

	/**
	 * @return the id_travel_length
	 */
	public int getId_travel_length() {
		return id_travel_length;
	}

	/**
	 * @param id_travel_length
	 *            the id_travel_length to set
	 */
	public void setId_travel_length(int id_travel_length) {
		this.id_travel_length = id_travel_length;
	}

	/**
	 * @return the rating
	 */
	public float getRating() {
		return rating;
	}

	/**
	 * @param rating
	 *            the rating to set
	 */
	public void setRating(float rating) {
		this.rating = rating;
	}

	/**
	 * @return url string that is used to rate a place
	 */
	public String buildURL() {
		/*
		 * return "http://10.0.2.2:33638/Service/Rate?username=" + username +
		 * "&place=" + id_place + "&temperature=" + id_temperature + "&weather="
		 * + id_weather + "&companion=" + id_companion + "&familiarity=" +
		 * id_farmiliarity + "&mood=" + id_mood + "&budget=" + id_budget +
		 * "&travellength=" + id_travel_length + "&time=" + time + "&rating=" +
		 * rating;
		 */
		return "http://" + ConfigUtil.SERVER +"/wcf4webservices/Service/Rate?username="
				+ username + "&place=" + id_place + "&weather=" + id_weather
				+ "&companion=" + id_companion + "&budget=" + id_budget
				+ "&time=" + time + "&rating=" + rating;
	}

	/**
	 * @return the string result from server after rating a place result =
	 *         "true" if successful, otherwise return "false"
	 */
	public String ratePlace() {
		InputStream inputStream = null;
		String rs = null;
		try {
			String url = buildURL();
			Log.i("Rate", "url rate = " + url);
			URL feedUrl = new URL(url);
			inputStream = feedUrl.openConnection().getInputStream();
			rs = JsonUtil.convertStreamToString(inputStream);
			if (rs.contains("true"))
				rs = "true";
			else
				rs = "false";
		} catch (Exception e) {
		}
		return rs;
	}
}
