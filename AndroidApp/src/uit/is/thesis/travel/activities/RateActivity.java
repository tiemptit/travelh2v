/**
 * 
 */
package uit.is.thesis.travel.activities;

import uit.is.thesis.travel.InternetHelper.RateService;
import uit.is.thesis.travel.SQLiteHelper.SQLiteDBAdapter;
import android.accounts.Account;
import android.accounts.AccountManager;
import android.app.Activity;
import android.content.Intent;
import android.database.Cursor;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.RatingBar;
import android.widget.Toast;

/**
 * @author LEHIEU
 * 
 */
public class RateActivity extends Activity implements OnClickListener {
	Button btnRate, btnBackRate;
	SQLiteDBAdapter mDBAdapter = null;
	RatingBar ratingBar;
	int id_place;
	// username and pass of Google Account on the phone
	String username;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.rate);

		// Get intent and receive data from the parent activity
		Intent intent = getIntent();
		Bundle bundle = intent.getExtras();
		id_place = bundle.getInt("id_place");
		
		btnRate = (Button) findViewById(R.id.btnRateR);
		btnRate.setOnClickListener(this);
		btnBackRate = (Button) findViewById(R.id.btnBackRate);
		btnBackRate.setOnClickListener(this);
		ratingBar = (RatingBar) findViewById(R.id.ratingBarR);

		if (this.mDBAdapter == null) {
			this.mDBAdapter = new SQLiteDBAdapter(this);
			mDBAdapter.open();
		}	
	}

	// button onclick handler
	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.btnRateR: {
			try {
				if (checkAccount() == true) { // login successful
					RateService r = new RateService();
					// get context config from DB
					Cursor c = mDBAdapter.getContextConfig();
					startManagingCursor(c);
					int value; // context value
					r.setUsername(username);
					r.setId_place(id_place);
					/*c.moveToPosition(0); // set current Temperature
					value = Integer.parseInt(c.getString(c
							.getColumnIndex("current_value")));
					Log.i("Rate", "Temperature = " + value);
					r.setId_temperature(value);*/
					c.moveToPosition(0); // set current Weather
					value = Integer.parseInt(c.getString(c
							.getColumnIndex("current_value")));
					r.setId_weather(value);
					c.moveToPosition(1); // set current Companion
					value = Integer.parseInt(c.getString(c
							.getColumnIndex("current_value")));
					r.setId_companion(value);
					/*c.moveToPosition(3); // set current Mood
					value = Integer.parseInt(c.getString(c
							.getColumnIndex("current_value")));
					Log.i("Rate", "Mood = " + value);
					r.setId_mood(value);
					c.moveToPosition(4); // set current Familiarity
					value = Integer.parseInt(c.getString(c
							.getColumnIndex("current_value")));
					Log.i("Rate", "Familiarity = " + value);
					r.setId_farmiliarity(value);*/
					c.moveToPosition(2); // set current Budget
					value = Integer.parseInt(c.getString(c
							.getColumnIndex("current_value")));
					r.setId_budget(value);
					/*c.moveToPosition(6); // set current Travel length
					value = Integer.parseInt(c.getString(c
							.getColumnIndex("current_value")));
					Log.i("Rate", "Travel length = " + value);
					r.setId_travel_length(value);*/
					c.moveToPosition(3); // set current Time
					String time = c
							.getString(c.getColumnIndex("current_value"));
					Log.i("Rate", "Time = " + time);
					r.setTime(time);
					Log.i("Rate", "Rating = " + ratingBar.getRating());
					r.setRating(ratingBar.getRating());
					// result of rating
					if (r.ratePlace() == "true") {
						Toast.makeText(getApplicationContext(),
								"Rate successfully! Thank you very much!", Toast.LENGTH_SHORT)
								.show();
					} else {
						Toast.makeText(getApplicationContext(), "Rate fail!",
								Toast.LENGTH_SHORT).show();
					}
				} else { // not login
					Toast.makeText(getApplicationContext(),
							"Please setup a Google account in your Android smartphone first! (Home --> Settings --> Accounts and sync)", Toast.LENGTH_LONG).show();
				}
			} catch (Exception e) {
			}
		}
			break;
		case R.id.btnBackRate: {
			try {
				finish();		
			} catch (Exception e) {
			}
		}
			break;
		}
	}

	public boolean checkAccount() {
		// get google account on the android phone (Settings --> Accounts and sync)
		AccountManager manager = (AccountManager) getSystemService(ACCOUNT_SERVICE);
		Account[] list = manager.getAccounts();
		for (Account account : list) {
			if (account.type.equalsIgnoreCase("com.google")) {
				username = account.name;
				break;
			}
		}
		if (username != null)
			return true;
		else
			return false;
	}
}
