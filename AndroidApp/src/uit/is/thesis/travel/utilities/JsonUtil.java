/**
 * 
 */
package uit.is.thesis.travel.utilities;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.io.StringWriter;
import java.io.Writer;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONObject;

import android.util.Log;
import uit.is.thesis.travel.models.*;


/**
 * @author LEHIEU
 * 
 */
public class JsonUtil {

	public static String convertStreamToString(InputStream is)
			throws IOException {
		if (is != null) {
			Writer writer = new StringWriter();
			char[] buffer = new char[1024];
			try {
				Reader reader = new BufferedReader(new InputStreamReader(is,
						"UTF-8"));
				int n;
				while ((n = reader.read(buffer)) != -1) {
					writer.write(buffer, 0, n);
				}
			} finally {
				is.close();
			}
			return writer.toString();
		} else {
			return "";
		}
	}

	public static List<PlaceModel> GetAllPlaces(String url, double current_lat, double current_lng) {
		List<PlaceModel> ListAllPlaces = new ArrayList<PlaceModel>();
		InputStream inputStream = null;
		String strResults = null;
		Log.i("test", "start ----------");
		try {
			// Get data from web service
			/*URL feedUrl = new URL(url);
			Log.i("test", "feedUrl ----------");
			inputStream = feedUrl.openConnection().getInputStream();
			Log.i("test", "inputStream: " + inputStream);*/
			
			//strResults = JsonUtil.convertStreamToString(inputStream);
			strResults ="{\"responseData\":[{\"id\":1,\"place_category_obj\":{\"id\":1,\"place_category\":\"Market\"},\"name\":\"C Da Lat Market 1\",\"imgurl\":\"http://www.google.com/maps/place?source=uds&q=hotel&cid=6565492984366607731\",\"lat\":10.760134,\"lng\":106.661582,\"house_number\":201,\"street\":\"Nguyen Thi Minh Khai\",\"ward\":\"6\",\"district\":\"10\",\"city\":\"Da Lat\",\"province\":\"Lam Dong\",\"country\":\"Viet Nam\",\"phone_number\":\"0909090909\",\"email\":\"abc@yahoo.com\",\"website\":\"www.google.com\",\"history\":\"history ...\",\"details\":\"details ...\",\"sources\":\"www.google.com.vn\",\"general_rating\":3,\"general_count_rating\":100,\"general_sum_rating\":500},{\"id\":1,\"place_category_obj\":{\"id\":1,\"place_category\":\"Market\"},\"name\":\"A Da Lat Market 2\",\"imgurl\":\"http://www.google.com/maps/place?source=uds&q=hotel&cid=6565492984366607731\",\"lat\":10.760110,\"lng\":106.661572,\"house_number\":201,\"street\":\"Nguyen Thi Minh Khai\",\"ward\":\"6\",\"district\":\"10\",\"city\":\"Da Lat\",\"province\":\"Lam Dong\",\"country\":\"Viet Nam\",\"phone_number\":\"0909090909\",\"email\":\"abc@yahoo.com\",\"website\":\"www.google.com\",\"history\":\"history ...\",\"details\":\"details ...\",\"sources\":\"www.google.com.vn\",\"general_rating\":4,\"general_count_rating\":100,\"general_sum_rating\":500},{\"id\":1,\"place_category_obj\":{\"id\":1,\"place_category\":\"Market\"},\"name\":\"B Da Lat Market 3\",\"imgurl\":\"http://www.google.com/maps/place?source=uds&q=hotel&cid=6565492984366607731\",\"lat\":10.760120,\"lng\":106.661542,\"house_number\":201,\"street\":\"Nguyen Thi Minh Khai\",\"ward\":\"6\",\"district\":\"10\",\"city\":\"Da Lat\",\"province\":\"Lam Dong\",\"country\":\"Viet Nam\",\"phone_number\":\"0909090909\",\"email\":\"abc@yahoo.com\",\"website\":\"www.google.com\",\"history\":\"history ...\",\"details\":\"details ...\",\"sources\":\"www.google.com.vn\",\"general_rating\":5,\"general_count_rating\":100,\"general_sum_rating\":500}]}";
			Log.i("test", "JSON string: " + strResults);
			
			// Parse JSON string
			// Create new JSON object from JSON string
			JSONObject objJSON = new JSONObject(strResults);

			// Create array of JSON objects with key "responseData" from
			// responseData JSON object
			
			JSONArray arrayPlaces = objJSON.getJSONArray("responseData");
			
			
			// Define a PlaceModel instance
			PlaceModel place_obj;

			// Iteritor array, parse each obj and create ListPlaces
			for (int i = 0; i < arrayPlaces.length(); i++) {
				JSONObject obj = arrayPlaces.getJSONObject(i);
				
				place_obj = new PlaceModel();
				place_obj.id = obj.getInt("id");
				Log.i("test", "place: " + i + "id = " +place_obj.id);

				// obj Place Category
				JSONObject objPlaceCate = obj.getJSONObject("place_category_obj");
				Log.i("test", "place: " + i + "place_category_obj = " + objPlaceCate);
				place_obj.place_category_obj= new PlaceCategoryModel();
				place_obj.place_category_obj.id = objPlaceCate.getInt("id");
				place_obj.place_category_obj.place_category = objPlaceCate
						.getString("place_category");
				Log.i("test", "place: " + i +"placecate id =" + place_obj.place_category_obj.id);
				Log.i("test", "place: " + i +"placecate name =" + place_obj.place_category_obj.place_category);
				
				place_obj.name = obj.getString("name");
				Log.i("test", "place: " + i + "name = " +place_obj.name);
				place_obj.imgurl = obj.getString("imgurl");
				Log.i("test", "place: " + i + "imgurl = " +place_obj.imgurl);
				place_obj.lat = obj.getDouble("lat");
				Log.i("test", "place: " + i + "lat = " +place_obj.lat);
				place_obj.lng = obj.getDouble("lng");
				Log.i("test", "place: " + i + "lng = " +place_obj.lng);
				place_obj.house_number = obj.getInt("house_number");
				Log.i("test", "place: " + i + "house_number = " +place_obj.house_number);
				place_obj.street = obj.getString("street");
				Log.i("test", "place: " + i + "street = " +place_obj.street);
				place_obj.ward = obj.getString("ward");
				Log.i("test", "place: " + i + "ward = " +place_obj.ward);
				place_obj.district = obj.getString("district");
				Log.i("test", "place: " + i + "district = " +place_obj.district);
				place_obj.city = obj.getString("city");
				Log.i("test", "place: " + i + "city = " +place_obj.city);
				place_obj.province = obj.getString("province");
				Log.i("test", "place: " + i + "province = " +place_obj.province);
				place_obj.country = obj.getString("country");
				Log.i("test", "place: " + i + "country = " +place_obj.country);
				place_obj.phone_number = obj.getString("phone_number");
				Log.i("test", "place: " + i + "phone_number = " +place_obj.phone_number);
				place_obj.email = obj.getString("email");
				Log.i("test", "place: " + i + "email = " +place_obj.email);
				place_obj.website = obj.getString("website");
				Log.i("test", "place: " + i + "website = " +place_obj.website);
				place_obj.history = obj.getString("history");
				Log.i("test", "place: " + i + "history = " +place_obj.history);
				place_obj.details = obj.getString("details");
				Log.i("test", "place: " + i + "details = " +place_obj.details);
				place_obj.sources = obj.getString("sources");
				Log.i("test", "place: " + i + "sources = " +place_obj.sources);
				place_obj.general_rating = obj.getDouble("general_rating");
				Log.i("test", "place: " + i + "general_rating = " +place_obj.general_rating);
				place_obj.general_count_rating = obj.getDouble("general_count_rating");
				Log.i("test", "place: " + i + "general_count_rating = " +place_obj.general_count_rating);
				place_obj.general_sum_rating = obj.getDouble("general_sum_rating");
				Log.i("test", "place: " + i + "general_sum_rating = " +place_obj.general_sum_rating);
				place_obj.setDistance(current_lat, current_lng);
				Log.i("test", "place: " + i + "distance = " +place_obj.getDistance());
				place_obj.setId_itemOnListView(i);
				Log.i("test", "place: " + i + "Id_itemOnListView = " +place_obj.getId_itemOnListView());
				//add to Array List Places
				ListAllPlaces.add(place_obj);
			}
		} catch (Exception e) {
			Log.i("test", "Exception: " + e.toString());
		}

		return ListAllPlaces;

	}

}
