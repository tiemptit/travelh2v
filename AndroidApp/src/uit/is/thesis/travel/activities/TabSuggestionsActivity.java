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
import android.accounts.Account;
import android.accounts.AccountManager;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

public class TabSuggestionsActivity extends Activity implements
		OnClickListener, Runnable {
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
	Button btnAz, btnRating, btnDistance, btnRefresh;
	Spinner spinnerType;
	boolean loadSpinnerType = false;
	boolean btnAzFirstRun = true;
	// DB adapter
	SQLiteDBAdapter mDBAdapter = null;
	Cursor c = null;
	String place_cates[];
	ArrayAdapter<String> adapter_type;
	private ProgressDialog progressDialog;

	// /////////////////////////////METHODS///////////////////////////////
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tab_suggestions);
		
		if (!isConnectToInternet()) { // no Internet connection, quit
			TextView myView = new TextView(getApplicationContext());
			myView.setText("There is no Internet Connection!" + "\n\n"
					+ "Please check it and restart the application!");
			myView.setTextSize(15);
			AlertDialog.Builder alertDialog = new AlertDialog.Builder(
					TabSuggestionsActivity.this);
			alertDialog.setTitle("Alert");
			alertDialog.setView(myView);
			alertDialog.setPositiveButton("OK",
					new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface arg0, int arg1) {
							finish();
						}
					});
			alertDialog.show();
		} else {
			// get current location of user
			getLatitudeLongitude();
			// Create and show ProgressDialog
			progressDialog = new ProgressDialog(this);
			progressDialog.setMessage("Loading ... Please wait!");
			progressDialog.show();

			// Create thread and start it
			Thread thread = new Thread(this);
			thread.start();
		}
	}

	@Override
	public void onResume() {
		super.onResume();
		if (!isConnectToInternet()) { // no Internet connection, quit
			TextView myView = new TextView(getApplicationContext());
			myView.setText("There is no Internet Connection!" + "\n\n"
					+ "Please check it and restart the application!");
			myView.setTextSize(15);
			AlertDialog.Builder alertDialog = new AlertDialog.Builder(
					TabSuggestionsActivity.this);
			alertDialog.setTitle("Alert");
			alertDialog.setView(myView);
			alertDialog.setPositiveButton("OK",
					new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface arg0, int arg1) {
							finish();
						}
					});
			alertDialog.show();
		}
	}

	public boolean isConnectToInternet() { // check WIFI or MOBILE (3G ...)
		// connections
		boolean haveConnectedWifi = false;
		boolean haveConnectedMobile = false;
		try {
			ConnectivityManager cm = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
			NetworkInfo[] netInfo = cm.getAllNetworkInfo();
			for (NetworkInfo ni : netInfo) {
				if (ni.getTypeName().equalsIgnoreCase("WIFI"))
					if (ni.isConnected()) {
						haveConnectedWifi = true;
					}
				if (ni.getTypeName().equalsIgnoreCase("MOBILE"))
					if (ni.isConnected()) {
						haveConnectedMobile = true;
					}
			}
		} catch (Exception e) {
		}
		return haveConnectedWifi || haveConnectedMobile;
	}

	// button onclick handler
	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.btnAzS: {
			btnAz.setBackgroundResource(R.drawable.btn_col_az_focus);
			btnRating.setBackgroundResource(R.drawable.btn_col_rating);
			btnDistance.setBackgroundResource(R.drawable.btn_col_distance);
			spinnerType.setBackgroundResource(R.drawable.btn_col_type);
			displayListView(searchResultList_name);
			if (btnAzFirstRun == true) {
				// sort list by distance
				searchResultList_distance = SortListUtil
						.SortListByDistance(searchResultList_name);
				// sort list by rating
				searchResultList_rating = SortListUtil
						.SortListByRatingEstimate(searchResultList_name);
				// sort list by type
				searchResultArrayList_type = new ArrayList<List<PlaceModel>>();
				for (int i = 0; i < place_cates.length; i++) {
					List<PlaceModel> result = SortListUtil.SortListByType(
							searchResultList_distance, place_cates[i]);
					searchResultArrayList_type.add(result);
				}
			}
			btnAzFirstRun = false;
		}
			break;
		case R.id.btnRatingS: {
			btnAz.setBackgroundResource(R.drawable.btn_col_az);
			btnRating.setBackgroundResource(R.drawable.btn_col_rating_focus);
			btnDistance.setBackgroundResource(R.drawable.btn_col_distance);
			spinnerType.setBackgroundResource(R.drawable.btn_col_type);
			displayListView(searchResultList_rating);
		}
			break;
		case R.id.btnDistanceS: {
			btnAz.setBackgroundResource(R.drawable.btn_col_az);
			btnRating.setBackgroundResource(R.drawable.btn_col_rating);
			btnDistance
					.setBackgroundResource(R.drawable.btn_col_distance_focus);
			spinnerType.setBackgroundResource(R.drawable.btn_col_type);
			displayListView(searchResultList_distance);
		}
			break;
		case R.id.btnRefreshS: {
			// Create and show ProgressDialog
			progressDialog = new ProgressDialog(this);
			progressDialog.setMessage("Reloading ... Please wait!");
			progressDialog.show();
			getLatitudeLongitude();
			getSearchResultList();
			btnAzFirstRun = false;
			btnAz.performClick();
			progressDialog.dismiss();
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

	// Get list of SearchResults - Suggestions Places from JSON
	public void getSearchResultList() {
		try {
			// URL for web services
			String url_test = "http://"
					+ ConfigUtil.SERVER
					+ "/wcf4webservices/Service/Suggestions?username=10008&weather=1&companion=1&budget=1&time=2011-12-25and14:16";
			String url = "http://" + ConfigUtil.SERVER
					+ "/wcf4webservices/Service/Suggestions?";
			// get google account on the android phone
			String username = null;
			AccountManager manager = (AccountManager) getSystemService(ACCOUNT_SERVICE);
			Account[] list = manager.getAccounts();
			for (Account account : list) {
				if (account.type.equalsIgnoreCase("com.google")) {
					username = account.name;
					break;
				}
			}
			if (username != null) {
				//test user 10008
				//username="10008";
				url += "username=" + username;
				// get current context from SQLite
				Cursor c = mDBAdapter.getContextConfig();
				startManagingCursor(c);
				int value; // context value
				/*
				 * c.moveToPosition(0); // set current Temperature value =
				 * Integer.parseInt(c.getString(c
				 * .getColumnIndex("current_value"))); url += "&temperature=" +
				 * value;
				 */
				c.moveToPosition(0); // set current Weather
				value = Integer.parseInt(c.getString(c
						.getColumnIndex("current_value")));
				url += "&weather=" + value;
				c.moveToPosition(1); // set current Companion
				value = Integer.parseInt(c.getString(c
						.getColumnIndex("current_value")));
				url += "&companion=" + value;
				/*
				 * c.moveToPosition(3); // set current Mood value =
				 * Integer.parseInt(c.getString(c
				 * .getColumnIndex("current_value"))); url += "&mood=" + value;
				 * c.moveToPosition(4); // set current Familiarity value =
				 * Integer.parseInt(c.getString(c
				 * .getColumnIndex("current_value"))); url += "&familiarity=" +
				 * value;
				 */
				c.moveToPosition(2); // set current Budget
				value = Integer.parseInt(c.getString(c
						.getColumnIndex("current_value")));
				url += "&budget=" + value;
				/*
				 * c.moveToPosition(6); // set current Travel length value =
				 * Integer.parseInt(c.getString(c
				 * .getColumnIndex("current_value"))); url += "&travellength=" +
				 * value;
				 */
				c.moveToPosition(3); // set current Time
				String time = c.getString(c.getColumnIndex("current_value"));
				// String[] datetime_plit = time.split("and");
				url += "&time=" + time;
				// url += "&time=" +
				// datetime_plit[0].trim()+datetime_plit[1].trim();
				c.close();
				Log.i("Rate", "url suggestions= " + url);
				// Receive result from Internet (sorted by estimated rating)
				this.searchResultList_name = SearchService.SuggestionPlaces(
						url, latitude, longitude);
			} else { // have no google account
				TextView myView = new TextView(getApplicationContext());
				myView.setText("Please setup a Google account in your Android smartphone first!"
						+ "\n\n" + "(Home --> Settings --> Accounts and sync)");
				myView.setTextSize(15);
				AlertDialog.Builder alertDialog = new AlertDialog.Builder(
						TabSuggestionsActivity.this);
				alertDialog.setTitle("Alert");
				alertDialog.setView(myView);
				alertDialog.setPositiveButton("OK",null);
				alertDialog.show();
			}
		} catch (Exception e) {
			// Log.i("Rate", "getSearchResultList exception = " + e.toString());
		}
	}

	private final LocationListener locationListener = new LocationListener() {
		// Method of interface LocationListener
		@Override
		public void onLocationChanged(Location location) {
			btnRefresh.performClick();
		}

		// Method of interface LocationListener
		@Override
		public void onStatusChanged(String provider, int status, Bundle extras) {
		}

		// Method of interface LocationListener
		@Override
		public void onProviderDisabled(String provider) {
		}

		// Method of interface LocationListener
		@Override
		public void onProviderEnabled(String provider) {
		}
	};

	// Get current location
	public void getLatitudeLongitude() {
		try {
			// Get the location manager
			locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
			// Provider is GPS
			provider = LocationManager.GPS_PROVIDER;
			locationManager.requestLocationUpdates(provider, 0, 0,
					locationListener);
			Location location = locationManager.getLastKnownLocation(provider);
			// Initialize the location fields
			if (location != null) {
				this.latitude = location.getLatitude();
				this.longitude = location.getLongitude();
			} else {
				this.latitude = ConfigUtil.LATITUDE;
				this.longitude = ConfigUtil.LONGITUDE;
			}
			locationManager.removeUpdates(locationListener);
		} catch (Exception e) {
			// Log.i("Rate", "Sugesstions exception " + e.toString());
		}
	}

	// Display ListView with parameters
	public void displayListView(final List<PlaceModel> list) {
		try {
			listViewResult = (ListView) findViewById(R.id.listViewResultS);
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
				resultMap.put("viewCate",
						list.get(i).place_category_obj.getPlace_category());
				address = "";
				address += list.get(i).getHouse_number() + ", "
						+ list.get(i).getStreet() + ", P."
						+ list.get(i).getWard() + ", Q."
						+ list.get(i).getDistrict() + ", "
						+ list.get(i).getCity();
				if (address.charAt(0) == ',')
					address = address.substring(2);
				resultMap.put("viewAddress", address);
				resultMap.put("viewDistance",
						String.valueOf(list.get(i).getDistance()) + " km");
				resultMap.put("ratingBar", list.get(i).getEstimate_rating());
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
						bundle.putDouble("currentLatitude", latitude);
						bundle.putDouble("currentLongitude", longitude);
						bundle.putSerializable("currentplace", place);
						Intent intent = new Intent(TabSuggestionsActivity.this,
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

	@Override
	public void run() {
		// TODO Auto-generated method stub
		try {
			// button onclick listener
			btnAz = (Button) findViewById(R.id.btnAzS);
			btnAz.setOnClickListener(this);
			btnRating = (Button) findViewById(R.id.btnRatingS);
			btnRating.setOnClickListener(this);
			btnDistance = (Button) findViewById(R.id.btnDistanceS);
			btnDistance.setOnClickListener(this);
			btnRefresh = (Button) findViewById(R.id.btnRefreshS);
			btnRefresh.setOnClickListener(this);
			spinnerType = (Spinner) findViewById(R.id.spinner_typeS);
			spinnerType.setOnItemSelectedListener(spinnerTypeChange);

			// set adapter
			if (this.mDBAdapter == null) {
				this.mDBAdapter = new SQLiteDBAdapter(this);
				mDBAdapter.open();
			}
			c = mDBAdapter.getPlaceCategories();
			startManagingCursor(c);
			place_cates = new String[c.getCount()];
			for (int i = 0; i < c.getCount(); i++) {
				c.moveToPosition(i);
				place_cates[i] = c
						.getString(c.getColumnIndex("place_category"));
			}
			c.close();

			// get Result List of All Places (sorted by name)
			getSearchResultList();
		} catch (Exception e) {
			// Log.i("Rate", "run Exception" + e.toString());
		}
		handler.sendEmptyMessage(0);
	}

	/** Handler for handling message from method run() */
	private Handler handler = new Handler() {
		@Override
		public void handleMessage(Message message) {
			adapter_type = new ArrayAdapter<String>(getApplicationContext(),
					R.layout.my_spinner_item, place_cates);
			adapter_type
					.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
			spinnerType.setAdapter(adapter_type);
			btnAz.performClick();

			// dismiss the progress dialog
			progressDialog.dismiss();

			// have no suggestions, quit
			if (searchResultList_name.size() == 0) {
				TextView myView = new TextView(getApplicationContext());
				myView.setText("Please rate some places first! Then the recommender system can suggest you more!"
						+ "\n\n"
						+ "(Make sure that you've created an Google Account on your phone before rating!)");
				myView.setTextSize(15);
				AlertDialog.Builder alertDialog = new AlertDialog.Builder(
						TabSuggestionsActivity.this);
				alertDialog.setTitle("Alert");
				alertDialog.setView(myView);
				alertDialog.setPositiveButton("OK", null);
				alertDialog.show();
			}
			
			// if can't get GPS signal
			if(latitude == ConfigUtil.LATITUDE && longitude == ConfigUtil.LONGITUDE){
			TextView myView = new TextView(getApplicationContext());
			myView.setText("Phone can't get the GPS signal! Your current location will be set to the default: at Reunification Place (Dinh Doc Lap), district 1, HCMC."
					+ "\n\n" + "Please check the GPS service on your phone!"
					+ "\n\n" + "(If you're inside your house, you can't get the GPS signal!)");
			myView.setTextSize(15);
			AlertDialog.Builder alertDialog = new AlertDialog.Builder(
					TabSuggestionsActivity.this);
			alertDialog.setTitle("Alert");
			alertDialog.setView(myView);
			alertDialog.setPositiveButton("OK",null);
			alertDialog.show();
			}
		}
	};
}