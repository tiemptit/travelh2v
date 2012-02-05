/**
 * 
 */
package uit.is.thesis.travel.activities;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;

import uit.is.thesis.travel.SQLiteHelper.SQLiteDBAdapter;
import uit.is.thesis.travel.models.PlaceModel;
import uit.is.thesis.travel.utilities.ConfigUtil;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ToggleButton;

/**
 * @author LEHIEU
 * 
 */
public class DetailsActivity extends Activity implements OnClickListener, Runnable {
	PlaceModel place;
	double currentLat, currentLng;
	SQLiteDBAdapter mDbAdapter = null;
	// buttons on screen
	ToggleButton ToggleBtnBasicInfo, ToggleBtnHistory, ToggleBtnMoreDetails,
			ToggleBtnSource;
	Button btnRateDetails, btnShowOnMap, btnAddFavorite, btnBackDetails;
	TextView txtViewPlaceName, txtViewPlaceNameContent, txtViewPhone,
			txtViewPhoneContent, txtViewAddress, txtViewAddressContent,
			txtViewEmail, txtViewEmailContent, txtViewWebsite,
			txtViewWebsiteContent, txtViewHistoryContent,
			txtViewMoreDetailsContent, txtViewSourceContent;
	ImageView imview;
	Bitmap bmImg;

	private ProgressDialog progressDialog;
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.details);

		// Create and show ProgressDialog
		progressDialog = new ProgressDialog(this);
		progressDialog.setMessage("Loading ... Please wait!");
		progressDialog.show();
		
		// Create thread and start it
		Thread thread = new Thread(this);
		thread.start();
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
				Bundle bundle = new Bundle();
				bundle.putInt("id_place", place.getId());
				Intent intent = new Intent(DetailsActivity.this,
						RateActivity.class);
				intent.putExtras(bundle);
				startActivity(intent);
			} catch (Exception e) {
			}
		}
			break;
		case R.id.btnShowOnMap: {
			try {
				Bundle bundle = new Bundle();
				bundle.putSerializable("currentplace", place);
				bundle.putDouble("currentLatitude", currentLat);
				bundle.putDouble("currentLongitude", currentLng);
				Intent intent = new Intent(DetailsActivity.this,
						MyMapActivity.class);
				intent.putExtras(bundle);
				startActivity(intent);
			} catch (Exception e) {
			}
		}
			break;
		case R.id.btnAddFavorite: { // Click on button Favourite
			if (place != null) {
				try {
					String id_place = String.valueOf(place.getId());
					String id_place_category = String.valueOf(place
							.getPlace_category_obj().getId());
					String name = place.getName();
					String imgurl = place.getImgurl();
					String lat = Double.toString(place.getLat());
					String lng = Double.toString(place.getLng());
					String house_number = place.getHouse_number();
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
						mDbAdapter.insertItem(id_place, id_place_category,
								name, imgurl, lat, lng, house_number, street,
								ward, district, city, province, country,
								phone_number, email, website, history, details,
								sources, general_rating, general_count_rating,
								general_sum_rating);
						// show Toast successful
						Toast.makeText(getApplicationContext(),
								"Add successfully!", Toast.LENGTH_SHORT).show();
					} else {
						// show Toast place existed
						Toast.makeText(getApplicationContext(),
								"This place existed!", Toast.LENGTH_SHORT)
								.show();
					}
				} catch (Exception e) {
				}
			}
		}
			break;
		case R.id.btnBackDetails: {
			try {
				finish();
			} catch (Exception e) {
			}
		}
			break;
		case R.id.txtViewPhoneContent: {
			try {
				String phoneNum = txtViewPhoneContent.getText().toString();
				if (phoneNum != "") {
					try {
						Intent intent = new Intent(Intent.ACTION_CALL);
						intent.setData(Uri.parse("tel:" + phoneNum));
						startActivity(intent);
					} catch (Exception e) {
						Toast.makeText(getApplicationContext(),
								"Can't make call!", Toast.LENGTH_SHORT).show();
					}
				} else {
					// do nothing
				}
			} catch (Exception e) {
			}
		}
			break;
		case R.id.txtViewWebsiteContent: {
			try {
				String url = txtViewWebsiteContent.getText().toString();
				if (url != "") {
					try {
						Bundle bundle = new Bundle();
						bundle.putString("place_url", url);
						Intent intent = new Intent(DetailsActivity.this,
								WebViewActivity.class);
						intent.putExtras(bundle);
						startActivity(intent);
					} catch (Exception e) {
						Toast.makeText(getApplicationContext(),
								"Can't show webpage!", Toast.LENGTH_SHORT)
								.show();
					}
				} else {
					// do nothing
				}
			} catch (Exception e) {
			}
		}
			break;
		}
	}

	// download image from server
	Bitmap downloadFile(String fileUrl) {
		Bitmap bmImg = null;
		URL myFileUrl = null;
		try {
			myFileUrl = new URL(fileUrl);
			HttpURLConnection conn = (HttpURLConnection) myFileUrl
					.openConnection();
			conn.setDoInput(true);
			conn.connect();
			InputStream is = conn.getInputStream();
			bmImg = BitmapFactory.decodeStream(is);
		} catch (IOException e) {
		}
		return bmImg;
	}

	@Override
	public void run() {
		// TODO Auto-generated method stub
		try{
		// Get intent and receive data from the parent activity
		Intent intent = getIntent();
		Bundle bundle = intent.getExtras();
		place = (PlaceModel) bundle.getSerializable("currentplace");
		currentLat = bundle.getDouble("currentLatitude");
		currentLng = bundle.getDouble("currentLongitude");
		// get layouts id
		ToggleBtnBasicInfo = (ToggleButton) findViewById(R.id.ToggleBtnBasicInfo);
		ToggleBtnBasicInfo.setOnClickListener(this);
		ToggleBtnHistory = (ToggleButton) findViewById(R.id.ToggleBtnHistory);
		ToggleBtnHistory.setOnClickListener(this);
		ToggleBtnMoreDetails = (ToggleButton) findViewById(R.id.ToggleBtnMoreDetails);
		ToggleBtnMoreDetails.setOnClickListener(this);
		ToggleBtnSource = (ToggleButton) findViewById(R.id.ToggleBtnSource);
		ToggleBtnSource.setOnClickListener(this);	
		btnRateDetails = (Button) findViewById(R.id.btnRateDetails);
		btnRateDetails.setOnClickListener(this);
		btnShowOnMap = (Button) findViewById(R.id.btnShowOnMap);
		btnShowOnMap.setOnClickListener(this);
		btnAddFavorite = (Button) findViewById(R.id.btnAddFavorite);
		btnAddFavorite.setOnClickListener(this);
		btnBackDetails = (Button) findViewById(R.id.btnBackDetails);
		btnBackDetails.setOnClickListener(this);
		txtViewPlaceName = (TextView) findViewById(R.id.txtViewPlaceName);
		txtViewPlaceNameContent = (TextView) findViewById(R.id.txtViewPlaceNameContent);	
		txtViewPhone = (TextView) findViewById(R.id.txtViewPhone);
		txtViewPhoneContent = (TextView) findViewById(R.id.txtViewPhoneContent);
		txtViewPhoneContent.setOnClickListener(this);
		txtViewAddress = (TextView) findViewById(R.id.txtViewAddress);
		txtViewAddressContent = (TextView) findViewById(R.id.txtViewAddressContent);
			
		txtViewEmail = (TextView) findViewById(R.id.txtViewEmail);
		txtViewEmailContent = (TextView) findViewById(R.id.txtViewEmailContent);		
		txtViewWebsite = (TextView) findViewById(R.id.txtViewWebsite);
		txtViewWebsiteContent = (TextView) findViewById(R.id.txtViewWebsiteContent);	
		txtViewWebsiteContent.setOnClickListener(this);
		txtViewHistoryContent = (TextView) findViewById(R.id.txtViewHistoryContent);	
		txtViewMoreDetailsContent = (TextView) findViewById(R.id.txtViewMoreDetailsContent);
		txtViewSourceContent = (TextView) findViewById(R.id.txtViewSourceContent);	
		imview = (ImageView) findViewById(R.id.imview);
		// load image
		String fileUrl = "http://" + ConfigUtil.SERVER
				+ "/wcf4webservices/imgPlacesResize/" + place.imgurl;
		bmImg = downloadFile(fileUrl);
		// open SQLite DB connection
		if (this.mDbAdapter == null) {
			this.mDbAdapter = new SQLiteDBAdapter(this);
			mDbAdapter.open();
		}
		handler.sendEmptyMessage(0);	
		
		}catch(Exception e){
		}
	}
	
	/** Handler for handling message from method run() */
	private Handler handler = new Handler() {
		@Override
		public void handleMessage(Message message) {			
			ToggleBtnBasicInfo.setChecked(true);
			ToggleBtnHistory.setChecked(true);
			ToggleBtnMoreDetails.setChecked(true);
			ToggleBtnSource.setChecked(true);
			txtViewPlaceNameContent.setText(place.getName());
			txtViewPhoneContent.setText(place.getPhone_number());
			String address = "";
			address += place.getHouse_number() + ", " + place.getStreet() + ", P."
					+ place.getWard() + ", Q." + place.getDistrict() + ", "
					+ place.getCity();
			txtViewAddressContent.setText(address);
			txtViewEmailContent.setText(place.getEmail());
			txtViewWebsiteContent.setText(place.getWebsite());
			txtViewHistoryContent.setText(place.getHistory());
			txtViewMoreDetailsContent.setText(place.getDetails());
			txtViewSourceContent.setText(place.getSources());
			imview.setImageBitmap(bmImg);
			// dismiss dialog
			progressDialog.dismiss();
		}
	};
}