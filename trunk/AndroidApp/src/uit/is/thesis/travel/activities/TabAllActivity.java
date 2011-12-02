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
import uit.is.thesis.travel.InternetHelper.SearchService;
import uit.is.thesis.travel.SQLiteHelper.SQLiteDBAdapter;
import uit.is.thesis.travel.models.PlaceModel;
import uit.is.thesis.travel.utilities.ConfigUtil;
import uit.is.thesis.travel.utilities.CustomBaseAdapter;
import uit.is.thesis.travel.utilities.SortListUtil;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Toast;

public class TabAllActivity extends Activity implements OnClickListener,
		LocationListener {
	// ListView on screen
	ListView listViewResult;
	// List of All Places sorted by name, rating, distance
	List<PlaceModel> searchResultList_name, searchResultList_rating,
			searchResultList_distance, searchResultList_type;
	List<List<PlaceModel>> searchResultArrayList_type;
	// LocationManager
	LocationManager locationManager;
	// provider string (GPS provider)
	String provider;
	// current Latitude - Longitude of user
	double latitude, longitude;
	// buttons on screen
	Button btnAz, btnRating, btnDistance;
	Spinner spinnerType;
	boolean loadSpinnerType = false;
	// DB adapter
	SQLiteDBAdapter mDBAdapter = null;
	Cursor c = null;
	String place_cates[];

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
		spinnerType = (Spinner) findViewById(R.id.spinner_type);
		spinnerType.setOnItemSelectedListener(spinnerTypeChange);

		// set spinner adapter
		if (this.mDBAdapter == null) {
			this.mDBAdapter = new SQLiteDBAdapter(this);
			mDBAdapter.open();
		}
		c = mDBAdapter.getPlaceCategories();
		startManagingCursor(c);
		place_cates = new String[c.getCount()];
		for (int i = 0; i < c.getCount(); i++) {
			c.moveToPosition(i);
			place_cates[i] = c.getString(c.getColumnIndex("place_category"));
		}
		c.close();
		mDBAdapter.close();

		ArrayAdapter<String> adapter_type = new ArrayAdapter<String>(this,
				R.layout.my_spinner_item, place_cates);
		adapter_type
				.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinnerType.setAdapter(adapter_type);

		// get current location of user
		getLatitudeLongitude();
		// get Result List of All Places (sorted by name)
		getSearchResultList();
		// click on btnAZ
		btnAz.performClick();
	}

	// button onclick handler
	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.btnAz: {
			btnAz.setBackgroundResource(R.drawable.btn_col_az_focus);
			btnRating.setBackgroundResource(R.drawable.btn_col_rating);
			btnDistance.setBackgroundResource(R.drawable.btn_col_distance);
			spinnerType.setBackgroundResource(R.drawable.btn_col_type);
			displayListView(searchResultList_name);
			// sort list by rating
			searchResultList_rating = SortListUtil
					.SortListByRating(searchResultList_name);
			// sort list by distance
			searchResultList_distance = SortListUtil
					.SortListByDistance(searchResultList_name);
			// sort list by type
			searchResultArrayList_type = new ArrayList<List<PlaceModel>>();
			for (int i = 0; i < place_cates.length; i++) {
				List<PlaceModel> result = SortListUtil.SortListByType(
						searchResultList_distance, place_cates[i]);
				searchResultArrayList_type.add(result);
			}
		}
			break;
		case R.id.btnRating: {
			btnAz.setBackgroundResource(R.drawable.btn_col_az);
			btnRating.setBackgroundResource(R.drawable.btn_col_rating_focus);
			btnDistance.setBackgroundResource(R.drawable.btn_col_distance);
			spinnerType.setBackgroundResource(R.drawable.btn_col_type);
			displayListView(searchResultList_rating);
		}
			break;
		case R.id.btnDistance: {
			btnAz.setBackgroundResource(R.drawable.btn_col_az);
			btnRating.setBackgroundResource(R.drawable.btn_col_rating);
			btnDistance
					.setBackgroundResource(R.drawable.btn_col_distance_focus);
			spinnerType.setBackgroundResource(R.drawable.btn_col_type);
			displayListView(searchResultList_distance);
		}
			break;
		}
	}

	OnItemSelectedListener spinnerTypeChange = new OnItemSelectedListener() {
		@Override
		public void onItemSelected(AdapterView<?> parentView,
				View selectedItemView, int position, long id) {
			if (loadSpinnerType == true) {
				btnAz.setBackgroundResource(R.drawable.btn_col_az);
				btnRating.setBackgroundResource(R.drawable.btn_col_rating);
				btnDistance.setBackgroundResource(R.drawable.btn_col_distance);
				spinnerType
						.setBackgroundResource(R.drawable.btn_col_type_focus);
				displayListView(searchResultArrayList_type.get(position));
			} else {
				loadSpinnerType = true;
			}
		}

		@Override
		public void onNothingSelected(AdapterView<?> arg0) {
			// TODO Auto-generated method stub
		}
	};

	// Get list of SearchResults - All Places from JSON
	public void getSearchResultList() {
		// Receive result from Internet (select and sorted by name on server)
		this.searchResultList_name = SearchService.SearchAllPlaces(
				"http://10.0.2.2:33638/Service/GetAllPlaces", latitude,
				longitude);
	}

	// Get current location
	public void getLatitudeLongitude() {
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
	public void displayListView(final List<PlaceModel> list) {
		try {
			listViewResult = (ListView) findViewById(R.id.listViewResult);
			// Prepare for ListView
			ArrayList<HashMap<String, Object>> resultList = new ArrayList<HashMap<String, Object>>();
			// Define a result
			HashMap<String, Object> resultMap;
			// Define address
			String address;
			// Put data to list of result
			for (int i = 0; i < list.size(); i++) {
				resultMap = new HashMap<String, Object>();
				resultMap.put("viewName", list.get(i).getName());
				resultMap.put("viewCate", list.get(i).place_category_obj.getPlace_category());
				address = "";
				address += list.get(i).getHouse_number() + ", "
						+ list.get(i).getStreet() + ", "
						+ list.get(i).getWard() + ", "
						+ list.get(i).getDistrict() + ", "
						+ list.get(i).getCity();
				resultMap.put("viewAddress", address);
				resultMap.put("viewDistance",
						String.valueOf(list.get(i).getDistance()) + " km");
				resultMap.put("ratingBar", list.get(i).getGeneral_rating());
				resultMap.put("id_itemOnListView",
						String.valueOf(list.get(i).getId_itemOnListView()));
				resultList.add(resultMap);
			}
			CustomBaseAdapter myCustomListViewAdapter = new CustomBaseAdapter(
					resultList, this.getApplicationContext());
			listViewResult.setAdapter(myCustomListViewAdapter);
			// Handle click event of item on ListView
			listViewResult.setOnItemClickListener(new OnItemClickListener() {
				@SuppressWarnings("unchecked")
				@Override
				public void onItemClick(AdapterView<?> arg0, View arg1,
						int arg2, long arg3) {
					try {
						HashMap<String, Object> item = (HashMap<String, Object>) listViewResult
								.getItemAtPosition(arg2);
						Bundle bundle = new Bundle();
						int id_itemOnListView = Integer.parseInt(item.get(
								"id_itemOnListView").toString());
						PlaceModel place = list.get(id_itemOnListView);
						// bundle.putInt(CommonConfiguration.IQUERY,iquery);
						bundle.putDouble("currentLatitude", latitude);
						bundle.putDouble("currentLongitude", longitude);
						bundle.putSerializable("currentplace", place);
						Intent intent = new Intent(TabAllActivity.this,
								DetailsActivity.class);
						intent.putExtras(bundle);
						startActivity(intent);
					} catch (Exception e) {
					}
				}
			});
		} catch (Exception e) {
		}
	}

	// Method of interface LocationListener
	@Override
	public void onLocationChanged(Location location) {
		getLatitudeLongitude();
		getSearchResultList();
		btnAz.performClick();
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