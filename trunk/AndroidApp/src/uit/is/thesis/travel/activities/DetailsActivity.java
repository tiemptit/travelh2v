/**
 * 
 */
package uit.is.thesis.travel.activities;

import uit.is.thesis.travel.SQLiteHelper.SQLiteDBAdapter;
import uit.is.thesis.travel.models.PlaceModel;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ToggleButton;

/**
 * @author LEHIEU
 * 
 */
public class DetailsActivity extends Activity implements OnClickListener {
	PlaceModel place;
	SQLiteDBAdapter mDbAdapter = null;
	// buttons on screen
	ToggleButton ToggleBtnBasicInfo, ToggleBtnHistory, ToggleBtnMoreDetails,
			ToggleBtnSource;
	Button btnRateDetails, btnShowOnMap, btnAddFavorite;
	TextView txtViewPlaceName, txtViewPlaceNameContent, txtViewPhone,
			txtViewPhoneContent, txtViewAddress, txtViewAddressContent,
			txtViewEmail, txtViewEmailContent, txtViewWebsite,
			txtViewWebsiteContent, txtViewHistoryContent,
			txtViewMoreDetailsContent, txtViewSourceContent;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.details);

		// Get intent and receive data from the parent activity
		Log.i("startDetails", "before get intent");
		Intent intent = getIntent();
		Log.i("startDetails", "before get bundle");
		Bundle bundle = intent.getExtras();
		Log.i("startDetails", "before  getSerializable");
		place = (PlaceModel) bundle.getSerializable("currentplace");

		// get layouts id
		ToggleBtnBasicInfo = (ToggleButton) findViewById(R.id.ToggleBtnBasicInfo);
		ToggleBtnBasicInfo.setOnClickListener(this);
		ToggleBtnBasicInfo.setChecked(true);
		ToggleBtnHistory = (ToggleButton) findViewById(R.id.ToggleBtnHistory);
		ToggleBtnHistory.setOnClickListener(this);
		ToggleBtnHistory.setChecked(true);
		ToggleBtnMoreDetails = (ToggleButton) findViewById(R.id.ToggleBtnMoreDetails);
		ToggleBtnMoreDetails.setOnClickListener(this);
		ToggleBtnMoreDetails.setChecked(true);
		ToggleBtnSource = (ToggleButton) findViewById(R.id.ToggleBtnSource);
		ToggleBtnSource.setOnClickListener(this);
		ToggleBtnSource.setChecked(true);
		btnRateDetails = (Button) findViewById(R.id.btnRateDetails);
		btnRateDetails.setOnClickListener(this);
		btnShowOnMap = (Button) findViewById(R.id.btnShowOnMap);
		btnShowOnMap.setOnClickListener(this);
		btnAddFavorite = (Button) findViewById(R.id.btnAddFavorite);
		btnAddFavorite.setOnClickListener(this);

		txtViewPlaceName = (TextView) findViewById(R.id.txtViewPlaceName);
		txtViewPlaceNameContent = (TextView) findViewById(R.id.txtViewPlaceNameContent);
		txtViewPhone = (TextView) findViewById(R.id.txtViewPhone);
		txtViewPhoneContent = (TextView) findViewById(R.id.txtViewPhoneContent);
		txtViewAddress = (TextView) findViewById(R.id.txtViewAddress);
		txtViewAddressContent = (TextView) findViewById(R.id.txtViewAddressContent);
		txtViewEmail = (TextView) findViewById(R.id.txtViewEmail);
		txtViewEmailContent = (TextView) findViewById(R.id.txtViewEmailContent);
		txtViewWebsite = (TextView) findViewById(R.id.txtViewWebsite);
		txtViewWebsiteContent = (TextView) findViewById(R.id.txtViewWebsiteContent);
		txtViewHistoryContent = (TextView) findViewById(R.id.txtViewHistoryContent);
		txtViewMoreDetailsContent = (TextView) findViewById(R.id.txtViewMoreDetailsContent);
		txtViewSourceContent = (TextView) findViewById(R.id.txtViewSourceContent);

		// open SQLite DB connection
		if (this.mDbAdapter == null) {
			this.mDbAdapter = new SQLiteDBAdapter(this);
			mDbAdapter.open();
			Log.i("InsertFavorites", "DetailsActivity - mDbAdapter.open()");
		}
	}

	// button onclick handler
	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.ToggleBtnBasicInfo: {
			try {
				if (!((ToggleButton) v).isChecked()) {
					txtViewPlaceName.setVisibility(View.GONE);
					txtViewPlaceNameContent.setVisibility(View.GONE);
					txtViewPhone.setVisibility(View.GONE);
					txtViewPhoneContent.setVisibility(View.GONE);
					txtViewAddress.setVisibility(View.GONE);
					txtViewAddressContent.setVisibility(View.GONE);
					txtViewEmail.setVisibility(View.GONE);
					txtViewEmailContent.setVisibility(View.GONE);
					txtViewWebsite.setVisibility(View.GONE);
					txtViewWebsiteContent.setVisibility(View.GONE);
				} else {
					txtViewPlaceName.setVisibility(View.VISIBLE);
					txtViewPlaceNameContent.setVisibility(View.VISIBLE);
					txtViewPhone.setVisibility(View.VISIBLE);
					txtViewPhoneContent.setVisibility(View.VISIBLE);
					txtViewAddress.setVisibility(View.VISIBLE);
					txtViewAddressContent.setVisibility(View.VISIBLE);
					txtViewEmail.setVisibility(View.VISIBLE);
					txtViewEmailContent.setVisibility(View.VISIBLE);
					txtViewWebsite.setVisibility(View.VISIBLE);
					txtViewWebsiteContent.setVisibility(View.VISIBLE);
				}
			} catch (Exception e) {
			}
		}
			break;
		case R.id.ToggleBtnHistory: {
			try {
				if (!((ToggleButton) v).isChecked()) {
					txtViewHistoryContent.setVisibility(View.GONE);
				} else {
					txtViewHistoryContent.setVisibility(View.VISIBLE);
				}
			} catch (Exception e) {
			}
		}
			break;
		case R.id.ToggleBtnMoreDetails: {
			try {
				if (!((ToggleButton) v).isChecked()) {
					txtViewMoreDetailsContent.setVisibility(View.GONE);
				} else {
					txtViewMoreDetailsContent.setVisibility(View.VISIBLE);
				}
			} catch (Exception e) {
			}
		}
			break;
		case R.id.ToggleBtnSource: {
			try {
				if (!((ToggleButton) v).isChecked()) {
					txtViewSourceContent.setVisibility(View.GONE);
				} else {
					txtViewSourceContent.setVisibility(View.VISIBLE);
				}
			} catch (Exception e) {
			}
		}
			break;
		case R.id.btnRateDetails: {
			try {

			} catch (Exception e) {
			}
		}
			break;
		case R.id.btnShowOnMap: {
			try {
				Log.i("ShowOnMap", "click btnShowOnMap");
				Bundle bundle = new Bundle();
				bundle.putSerializable("currentplace", place);
				Log.i("ShowOnMap", "after putSerializable ");
				Intent intent = new Intent(DetailsActivity.this,
						MyMapActivity.class);
				Log.i("ShowOnMap", "after create intent ");
				intent.putExtras(bundle);
				Log.i("ShowOnMap", "before start activity");
				startActivity(intent);
			} catch (Exception e) {
				Log.i("ShowOnMap",
						"click btnShowOnMap exception" + e.toString());
			}
		}
			break;
		case R.id.btnAddFavorite: { // Click on button Favourite
			if (place != null) {
				try {
					Log.i("InsertFavorites", " DetailsActivity - click btnAddFavorites");
					String id = Double.toString(place.getId());
					String id_place_category = Double.toString(place
							.getPlace_category_obj().getId());
					String name = place.getName();
					String imgurl = place.getImgurl();
					String lat = Double.toString(place.getLat());
					String lng = Double.toString(place.getLng());
					String house_number = Double.toString(place
							.getHouse_number());
					String street = place.getStreet();
					String ward = place.getWard();
					String district = place.getDistrict();
					String city = place.getCity();
					String province = place.getProvince();
					String country = place.getCountry();
					String phone_number = place.getPhone_number();
					String email = place.getEmail();
					String website = place.getWebsite();
					String history = place.getHistory();
					String details = place.getDetails();
					String sources = place.getSources();
					String general_rating = Double.toString(place
							.getGeneral_rating());
					String general_count_rating = Double.toString(place
							.getGeneral_count_rating());
					String general_sum_rating = Double.toString(place
							.getGeneral_sum_rating());

					// check if it exists in db, if not then insert, else do
					// nothing
					if (mDbAdapter.checkIfExist(lat, lng, name) == false) {
						Log.i("InsertFavorites", " DetailsActivity - checkIfExist false");
						mDbAdapter.insertItem(id, id_place_category, name,
								imgurl, lat, lng, house_number, street, ward,
								district, city, province, country,
								phone_number, email, website, history, details,
								sources, general_rating, general_count_rating,
								general_sum_rating);
						// show Toast successful
						Toast.makeText(getApplicationContext(),
								"Add successfully!", Toast.LENGTH_SHORT).show();

					} else {
						// show Toast place existed
						Log.i("InsertFavorites", " Details - checkIfExist true");
						Toast.makeText(getApplicationContext(),
								"This place existed!", Toast.LENGTH_SHORT).show();
					}
				} catch (Exception e) {
					Log.i("InsertFavorites",
							" DetailActivity - mClickbtnFavourite - exception"
									+ e.toString());
				}
			}
		}
			break;
		}
	}

}