/**
 * 
 */
package uit.is.thesis.travel.activities;

/**
 * @author LEHIEU
 *
 */
import java.util.ArrayList;
import uit.is.thesis.travel.SQLiteHelper.SQLiteCursorAdapter;
import uit.is.thesis.travel.SQLiteHelper.SQLiteDBAdapter;
import uit.is.thesis.travel.SQLiteHelper.SQLiteDBHelper;
import uit.is.thesis.travel.models.PlaceCategoryModel;
import uit.is.thesis.travel.models.PlaceModel;
import uit.is.thesis.travel.utilities.ConfigUtil;
import android.app.AlertDialog;
import android.app.ListActivity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.location.Location;
import android.location.LocationManager;
import android.os.Bundle;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class TabFavoritesActivity extends ListActivity {
	SQLiteCursorAdapter mSQLiteCursorAdapter = null;
	SQLiteDBAdapter mDBAdapter = null;
	SQLiteDBHelper mDBHelper = null;
	Cursor currentCursor = null;
	ListView listView = null;

	// current location of user
	double currentLatitude, currentLongitude;

	/*
	 * parameter for Select Limit (class SQLiteDBAdapter - function
	 * getItemsFromTo)
	 */
	int limit_from = 0;
	int limit_count = 5;
	int rowReturnsCount = 0;
	int sumPage = 0;
	int curPage = 1;
	String keyWord = "";

	Button btnDelete, btnSearch, btnPrev, btnNext, btnCheckAll;
	EditText txtSearch;
	Boolean isCheckAll = false;
	TextView txtViewPage;

	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tab_favorites);
		// get current location of user
		getLatitudeLongitude();
		// get layout id
		txtSearch = (EditText) findViewById(R.id.txtSearch);
		btnSearch = (Button) findViewById(R.id.btnSearch);
		btnSearch.setOnClickListener(mSearchListener);
		btnDelete = (Button) findViewById(R.id.btnDelete);
		btnDelete.setOnClickListener(mDeleteListener);	
		btnPrev = (Button) findViewById(R.id.btnPrev);
		btnPrev.setOnClickListener(mPrevListener);
		btnNext = (Button) findViewById(R.id.btnNext);
		btnNext.setOnClickListener(mNextListener);
		btnCheckAll = (Button) findViewById(R.id.btnCheckAll);	
		btnCheckAll.setOnClickListener(mCheckAllListener);
		txtViewPage = (TextView) findViewById(R.id.txtViewPage);
		
		
		if (this.mDBAdapter == null) {
			this.mDBAdapter = new SQLiteDBAdapter(this);
			mDBAdapter.open();		
		}
	
		if (this.mDBHelper == null) {
			this.mDBHelper = mDBAdapter.getmDbHelper();
		}
		
		if (listView == null) {
			listView = getListView();
			listView.setOnItemClickListener(listViewItemListener);
		}
		try {
			Search(keyWord, limit_from, limit_count);
			txtViewPage.setText("Page" + curPage + "/" + sumPage);
		} catch (Exception e) {
		}
	}

	private AdapterView.OnItemClickListener listViewItemListener = new OnItemClickListener() {
		@Override
		public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
				long arg3) {
			try {
				ViewGroup row = (ViewGroup) listView.getChildAt(arg2);
				CheckBox check = (CheckBox) row.findViewById(R.id.checkboxItem);
				long itemId = Long.parseLong(check.getText().toString());
				Cursor c = mDBAdapter.getItem(itemId);
				startManagingCursor(c);
				c.moveToFirst(); // move to the 1st row
				// get data
				int id_place = Integer.parseInt(c.getString(c
						.getColumnIndex("id_place")));
				int id_place_category = Integer.parseInt(c.getString(c
						.getColumnIndex("id_place_category")));
				String place_category = c.getString(c
						.getColumnIndex("place_category"));
				String name = c.getString(c.getColumnIndex("name"));
				String imgurl = c.getString(c.getColumnIndex("imgurl"));
				double lat = Double.parseDouble(c.getString(c
						.getColumnIndex("lat")));
				double lng = Double.parseDouble(c.getString(c
						.getColumnIndex("lng")));
				String house_number = c.getString(c
						.getColumnIndex("house_number"));
				String street = c.getString(c.getColumnIndex("street"));
				String ward = c.getString(c.getColumnIndex("ward"));
				String district = c.getString(c.getColumnIndex("district"));
				String city = c.getString(c.getColumnIndex("city"));
				String province = c.getString(c.getColumnIndex("province"));
				String country = c.getString(c.getColumnIndex("country"));
				String phone_number = c.getString(c
						.getColumnIndex("phone_number"));
				String email = c.getString(c.getColumnIndex("email"));
				String website = c.getString(c.getColumnIndex("website"));
				String history = c.getString(c.getColumnIndex("history"));
				String details = c.getString(c.getColumnIndex("details"));
				String sources = c.getString(c.getColumnIndex("sources"));
				double general_rating = c.getDouble(c
						.getColumnIndex("general_rating"));
				double general_count_rating = c.getDouble(c
						.getColumnIndex("general_count_rating"));
				double general_sum_rating = c.getDouble(c
						.getColumnIndex("general_sum_rating"));
				// create PlaceModel obj
				PlaceModel place = new PlaceModel();
				place.setId(id_place);
				place.place_category_obj = new PlaceCategoryModel();
				place.place_category_obj.setId(id_place_category);
				place.place_category_obj.setPlace_category(place_category);
				place.setName(name);
				place.setImgurl(imgurl);
				place.setLat(lat);
				place.setLng(lng);
				place.setHouse_number(house_number);
				place.setStreet(street);
				place.setWard(ward);
				place.setDistrict(district);
				place.setCity(city);
				place.setProvince(province);
				place.setCountry(country);
				place.setPhone_number(phone_number);
				place.setEmail(email);
				place.setWebsite(website);
				place.setHistory(history);
				place.setDetails(details);
				place.setSources(sources);
				place.setGeneral_rating(general_rating);
				place.setGeneral_count_rating(general_count_rating);
				place.setGeneral_sum_rating(general_sum_rating);
				place.setId_itemOnListView(-1);
				place.setDistance(currentLatitude, currentLongitude);
				
				// send them to Details Activity
				Bundle bundle = new Bundle();
				bundle.putDouble("currentLatitude", currentLatitude);
				bundle.putDouble("currentLongitude", currentLongitude);
				bundle.putSerializable("currentplace", place);
				Intent intent = new Intent(TabFavoritesActivity.this,
						DetailsActivity.class);
				intent.putExtras(bundle);
				startActivity(intent);
			} catch (Exception e) {
			}
		}
	};

	private View.OnClickListener mCheckAllListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {
			try {
				if (isCheckAll == false) { // if want to check all
					for (int i = 0; i < listView.getCount(); ++i) {
						ViewGroup row = (ViewGroup) listView.getChildAt(i);
						CheckBox check = (CheckBox) row
								.findViewById(R.id.checkboxItem);
						check.setChecked(true);
					}
					isCheckAll = true;
				} else { // if want to uncheck all
					for (int i = 0; i < listView.getCount(); ++i) {
						ViewGroup row = (ViewGroup) listView.getChildAt(i);
						CheckBox check = (CheckBox) row
								.findViewById(R.id.checkboxItem);
						check.setChecked(false);
					}
					isCheckAll = false;
				}
			} catch (Exception e) {
			}
		}
	};

	private View.OnClickListener mDeleteListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {
			final ArrayList<Integer> a = new ArrayList<Integer>();
			for (int i = 0; i < listView.getCount(); ++i) {
				ViewGroup row = (ViewGroup) listView.getChildAt(i);
				CheckBox check = (CheckBox) row.findViewById(R.id.checkboxItem);
				// hidden_id is text value of the checkbox
				// check
				int hidden_id = Integer.parseInt(check.getText().toString());
				if (check.isChecked()) {
					a.add(hidden_id);
				}
			}
			if (!a.isEmpty()) {
				// confirm delete if some items are checked
				AlertDialog.Builder alertbox = new AlertDialog.Builder(
						TabFavoritesActivity.this);
				alertbox.setMessage("Are you sure want to delete?");
				alertbox.setPositiveButton("Yes",
						new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface arg0, int arg1) {
								try {
									// delete all checked item
									for (int i = 0; i < a.size(); i++) {
										mDBAdapter.deleteItem(a.get(i));
									}
									isCheckAll = false;
									// refresh listview
									keyWord = txtSearch.getText().toString();
									limit_from = 0;
									curPage = 1;
									Search(keyWord, limit_from, limit_count);
									txtViewPage.setText("Page" + curPage + "/"
											+ sumPage);
								} catch (Exception e) {
								}
							}
						});

				// Set a negative/no button and create a listener
				alertbox.setNegativeButton("No",
						new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface arg0, int arg1) {
								// do nothing
							}
						});

				alertbox.show();
			} else { // no item is checked
				Toast.makeText(getApplicationContext(),
						"Please choose items you want to delete first!",
						Toast.LENGTH_SHORT).show();
			}
		}
	};

	private View.OnClickListener mPrevListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {
			try {
				if (limit_from - 5 >= 0) {
					limit_from = limit_from - 5;
					Search(keyWord, limit_from, limit_count);
					curPage -= 1;
					txtViewPage.setText("Page" + curPage + "/" + sumPage);
					isCheckAll = false;
				}
			} catch (Exception e) {
			}
		}
	};

	private View.OnClickListener mNextListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {
			try {
				if (limit_from + 5 < rowReturnsCount) {
					limit_from = limit_from + 5;
					Search(keyWord, limit_from, limit_count);
					curPage += 1;
					txtViewPage.setText("Page" + curPage + "/" + sumPage);
					isCheckAll = false;
				}
			} catch (Exception e) {
			}
		}
	};

	private View.OnClickListener mSearchListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {
			try {
				keyWord = txtSearch.getText().toString();
				limit_from = 0;
				curPage = 1;
				Search(keyWord, limit_from, limit_count);
				txtViewPage.setText("Page" + curPage + "/" + sumPage);
			} catch (Exception e) {
			}
		}
	};

	private void Search(String keyWord, int lm_from, int lm_count) {
		try {
			//mDBAdapter.deleteDatabase();
			this.currentCursor = mDBAdapter.getItemsLikeThisFromTo(keyWord,
					lm_from, lm_count);
			rowReturnsCount = mDBAdapter.getRowReturnCount(keyWord);
			if (rowReturnsCount % limit_count > 0) {
				sumPage = rowReturnsCount / limit_count + 1;
			} else {
				sumPage = rowReturnsCount / limit_count;
			}
			startManagingCursor(currentCursor);
			String[] from = new String[] { "id_place", "name", "place_category","house_number",
					"street", "ward", "district", "city", "lat", "lng", "general_rating"};
			int[] to = new int[] { R.id.checkboxItem, R.id.txtViewNameF, R.id.txtViewCateF,
					R.id.txtViewAddressF, R.id.txtViewDistanceF, R.id.ratingBarF };
			// create an array adapter and set it to display using our row
			this.mSQLiteCursorAdapter = new SQLiteCursorAdapter(this,
					R.layout.row_favorite, currentCursor, from, to, mDBHelper, currentLatitude, currentLongitude);
			setListAdapter(mSQLiteCursorAdapter);
		} catch (Exception e) {
		}
	}

	// Get current location
	private void getLatitudeLongitude() {
		// Get the location manager
		LocationManager locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
		// Provider is GPS
		String provider = LocationManager.GPS_PROVIDER;
		Location location = locationManager.getLastKnownLocation(provider);
		// Initialize the location fields
		if (location != null) {
			this.currentLatitude = location.getLatitude();
			this.currentLongitude = location.getLongitude();
		} else {
			this.currentLatitude = ConfigUtil.LATITUDE;
			this.currentLongitude = ConfigUtil.LONGITUDE;
		}
	}
}