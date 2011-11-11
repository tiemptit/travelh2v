/**
 * 
 */
package uit.is.thesis.travel.activities;

/**
 * @author LEHIEU
 *
 */

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import uit.is.thesis.travel.InternetHelper.*;
import uit.is.thesis.travel.models.*;
import uit.is.thesis.travel.utilities.*;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

public class TabAllActivity extends Activity implements OnClickListener,
		LocationListener {
	// ListView on screen
	private ListView listViewResult;
	// List of All Places
	List<PlaceModel> searchResultList;
	// LocationManager
	LocationManager locationManager;
	// provider string (GPS provider)
	String provider;
	// current Latitude - Longitude of user
	public double latitude, longitude;
	// buttons on screen
	Button btnAz, btnRating, btnDistance, btnType;

	// /////////////////////////////METHODS///////////////////////////////
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tab_all);

		// button onclick listener
		btnAz = (Button) findViewById(R.id.btnAz);
		btnAz.setOnClickListener(this);
		btnRating = (Button) findViewById(R.id.btnRating);
		btnRating.setOnClickListener(this);
		btnDistance = (Button) findViewById(R.id.btnDistance);
		btnDistance.setOnClickListener(this);
		btnType = (Button) findViewById(R.id.btnType);
		btnType.setOnClickListener(this);

		// set default current location of user
		this.latitude = ConfigUtil.LATITUDE;
		this.longitude = ConfigUtil.LONGITUDE;

		// get current location of user
		getLatitudeLongitude();
		// get Result List of All Places
		getSearchResultList();
	}

	// button onclick handler
	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.btnAz: {
			searchResultList = SortListUtil.SortListByName(searchResultList);
			displayListView();
		}
			break;
		case R.id.btnRating: {
			searchResultList = SortListUtil.SortListByRating(searchResultList);
			displayListView();
		}
			break;
		case R.id.btnDistance: {
			searchResultList = SortListUtil.SortListByDistance(searchResultList);
			displayListView();
		}
			break;
		}
	}

	// Get list of SearchResults - All Places from JSON
	private void getSearchResultList() {
		// Receive result from Internet
		this.searchResultList = SearchService.SearchAllPlaces(
				"http://10.0.2.2:33638/Service/ListAllPlaces", latitude,
				longitude);
	}

	// Get current location
	private void getLatitudeLongitude() {
		// Get the location manager
		locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
		// Provider is GPS
		provider = LocationManager.GPS_PROVIDER;
		Location location = locationManager.getLastKnownLocation(provider);
		// Initialize the location fields
		if (location != null) {
			this.latitude = location.getLatitude();
			this.longitude = location.getLongitude();
		} else {
			this.latitude = ConfigUtil.LATITUDE;
			this.longitude = ConfigUtil.LONGITUDE;
		}
	}

	// Display ListView with parameters
	private void displayListView() {
		try {

			listViewResult = (ListView) findViewById(R.id.listViewResult);
			// Prepare for ListView
			ArrayList<HashMap<String, Object>> resultList = new ArrayList<HashMap<String, Object>>();
			Log.i("listViewAll", "create resultList");
			// Define a result
			HashMap<String, Object> resultMap;
			Log.i("listViewAll", "define hashMap");

			// Define address
			String address;
			// Put data to list of result
			for (int i = 0; i < searchResultList.size(); i++) {
				resultMap = new HashMap<String, Object>();
				Log.i("listViewAll", i + "new resultMap");
				resultMap.put("viewName", searchResultList.get(i).getName());
				Log.i("listViewAll", i + "putName");
				address = "";
				address += searchResultList.get(i).getHouse_number() + ", "
						+ searchResultList.get(i).getStreet() + ", "
						+ searchResultList.get(i).getWard() + ", "
						+ searchResultList.get(i).getDistrict() + ", "
						+ searchResultList.get(i).getCity();
				resultMap.put("viewAddress", address);
				Log.i("listViewAll", i + "putAddress");
				resultMap.put("viewDistance",
						String.valueOf(searchResultList.get(i).getDistance())
								+ " km");
				Log.i("listViewAll", i + "putDistance");
				resultMap.put("ratingBar", searchResultList.get(i)
						.getGeneral_rating());
				Log.i("listViewAll", i + "putRating");
				resultMap.put("id_itemOnListView",
						String.valueOf(searchResultList.get(i)
								.getId_itemOnListView()));
				Log.i("listViewAll", i + "putId_itemOnListView");
				resultList.add(resultMap);
				Log.i("listViewAll", i + "add result Map to result List");
			}
			Log.i("listViewAll", "result List size = " + resultList.size());

			Log.i("listViewAll", "before setAdapter");
			CustomBaseAdapter myCustomListViewAdapter = new CustomBaseAdapter(
					resultList, this.getApplicationContext());

			listViewResult.setAdapter(myCustomListViewAdapter);
			Log.i("listViewAll", "after setAdapter");

			// Handle click event of item on ListView
			listViewResult.setOnItemClickListener(new OnItemClickListener() {
				@SuppressWarnings("unchecked")
				@Override
				public void onItemClick(AdapterView<?> arg0, View arg1,
						int arg2, long arg3) {
					try {
						HashMap<String, Object> item = (HashMap<String, Object>) listViewResult
								.getItemAtPosition(arg2);
						Log.i("startDetails", "getItemAtPosition");
						Bundle bundle = new Bundle();
						int id_itemOnListView = Integer.parseInt(item.get(
								"id_itemOnListView").toString());
						Log.i("startDetails", "id_itemOnListView = "
								+ id_itemOnListView);
						PlaceModel place = searchResultList
								.get(id_itemOnListView);
						Log.i("startDetails", "place id_listview = "
								+ place.getId_itemOnListView());
						Log.i("startDetails",
								"searchResult id = " + place.getId());

						// bundle.putInt(CommonConfiguration.IQUERY,iquery);
						bundle.putDouble("currentLatitude", latitude);
						bundle.putDouble("currentLongitude", longitude);
						bundle.putSerializable("currentplace", place);
						Log.i("startDetails", "putSerializable");
						Intent intent = new Intent(TabAllActivity.this,
								DetailsActivity.class);
						intent.putExtras(bundle);
						Log.i("startDetails", "startActivity");
						startActivity(intent);
					} catch (Exception e) {
						Log.i("startDetails exception", e.toString());
					}
				}
			});
		} catch (Exception e) {
			Log.i("listViewAll", "Exception: " + e.toString());
		}
	}

	// Method of interface LocationListener
	@Override
	public void onLocationChanged(Location location) {
		getLatitudeLongitude();
		getSearchResultList();
		displayListView();
	}

	// Method of interface LocationListener
	@Override
	public void onStatusChanged(String provider, int status, Bundle extras) {
	}

	// Method of interface LocationListener
	@Override
	public void onProviderDisabled(String provider) {
		Toast.makeText(this, "Enabled new provider " + provider,
				Toast.LENGTH_SHORT).show();
	}

	// Method of interface LocationListener
	@Override
	public void onProviderEnabled(String provider) {
		Toast.makeText(this, "Disabled provider " + provider,
				Toast.LENGTH_SHORT).show();
	}

}