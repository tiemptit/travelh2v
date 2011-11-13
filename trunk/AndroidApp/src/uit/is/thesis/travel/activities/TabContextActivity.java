/**
 * 
 */
package uit.is.thesis.travel.activities;

/**
 * @author LEHIEU
 *
 */
import java.util.Date;
import uit.is.thesis.travel.SQLiteHelper.SQLiteCursorAdapter;
import uit.is.thesis.travel.SQLiteHelper.SQLiteDBAdapter;
import uit.is.thesis.travel.SQLiteHelper.SQLiteDBHelper;
import android.app.Activity;
import android.database.Cursor;
import android.os.Bundle;
import android.text.format.DateFormat;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;

public class TabContextActivity extends Activity implements OnClickListener {
	SQLiteDBAdapter mDBAdapter = null;
	SQLiteDBHelper mDBHelper = null;
	Spinner spinner_temperature, spinner_weather, spinner_companion,
			spinner_mood, spinner_familiarity, spinner_budget,
			spinner_travel_length;
	Button btnSaveC, btnResetC, btnPickDate, btnPickTime;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tab_context);

		// get Layout id
		spinner_temperature = (Spinner) findViewById(R.id.spinner_temperature);
		spinner_weather = (Spinner) findViewById(R.id.spinner_weather);
		spinner_companion = (Spinner) findViewById(R.id.spinner_companion);
		spinner_mood = (Spinner) findViewById(R.id.spinner_mood);
		spinner_familiarity = (Spinner) findViewById(R.id.spinner_familiarity);
		spinner_budget = (Spinner) findViewById(R.id.spinner_budget);
		spinner_travel_length = (Spinner) findViewById(R.id.spinner_travel_length);
		btnSaveC = (Button) findViewById(R.id.btnSaveC);
		btnSaveC.setOnClickListener(this);
		btnResetC = (Button) findViewById(R.id.btnResetC);
		btnResetC.setOnClickListener(this);
		btnPickDate = (Button) findViewById(R.id.btnPickDate);
		btnPickDate.setOnClickListener(this);
		btnPickTime = (Button) findViewById(R.id.btnPickTime);
		btnPickTime.setOnClickListener(this);
		
		if (this.mDBAdapter == null) {
			this.mDBAdapter = new SQLiteDBAdapter(this);
			mDBAdapter.open();
		}

		if (this.mDBHelper == null) {
			this.mDBHelper = mDBAdapter.getmDbHelper();
		}

		// set spinner adapter
		ArrayAdapter<CharSequence> adapter_temperature = ArrayAdapter
				.createFromResource(this, R.array.temperature_array,
						android.R.layout.simple_spinner_item);
		adapter_temperature
				.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner_temperature.setAdapter(adapter_temperature);
		ArrayAdapter<CharSequence> adapter_weather = ArrayAdapter
				.createFromResource(this, R.array.weather_array,
						android.R.layout.simple_spinner_item);
		adapter_weather
				.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner_weather.setAdapter(adapter_weather);
		ArrayAdapter<CharSequence> adapter_companion = ArrayAdapter
				.createFromResource(this, R.array.companion_array,
						android.R.layout.simple_spinner_item);
		adapter_companion
				.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner_companion.setAdapter(adapter_companion);
		ArrayAdapter<CharSequence> adapter_mood = ArrayAdapter
				.createFromResource(this, R.array.mood_array,
						android.R.layout.simple_spinner_item);
		adapter_mood
				.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner_mood.setAdapter(adapter_mood);
		ArrayAdapter<CharSequence> adapter_familiarity = ArrayAdapter
				.createFromResource(this, R.array.familiarity_array,
						android.R.layout.simple_spinner_item);
		adapter_familiarity
				.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner_familiarity.setAdapter(adapter_familiarity);
		ArrayAdapter<CharSequence> adapter_budget = ArrayAdapter
				.createFromResource(this, R.array.budget_array,
						android.R.layout.simple_spinner_item);
		adapter_budget
				.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner_budget.setAdapter(adapter_budget);
		ArrayAdapter<CharSequence> adapter_travel_length = ArrayAdapter
				.createFromResource(this, R.array.travel_length_array,
						android.R.layout.simple_spinner_item);
		adapter_travel_length
				.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner_travel_length.setAdapter(adapter_travel_length);

		// load current context config
		LoadCurrentContextConfig();

	}

	// button onclick handler
	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.btnResetC: {
			try {
				//get current date time
				Date d = new Date(); 
				String s_plit[] = DateFormat.format("yyyy-MM-dd hh:mm:ss", d.getTime()).toString().split(" ");
				mDBAdapter.updateContextConfig(0, 0, 0, 0, 0, 0, 0,
						s_plit[0]+"and"+s_plit[1]);
				LoadCurrentContextConfig();
			} catch (Exception e) {
			}
		}
			break;
		case R.id.btnSaveC: {
			try {
				// update current context config
				mDBAdapter.updateContextConfig(
						spinner_temperature.getSelectedItemPosition(),
						spinner_weather.getSelectedItemPosition(),
						spinner_companion.getSelectedItemPosition(),
						spinner_familiarity.getSelectedItemPosition(),
						spinner_mood.getSelectedItemPosition(),
						spinner_budget.getSelectedItemPosition(),
						spinner_travel_length.getSelectedItemPosition(),
						btnPickDate.getText() + "and" + btnPickTime.getText());
			} catch (Exception e) {
			}
		}
			break;
		}
	}
	
	public void LoadCurrentContextConfig(){
		Cursor c = mDBAdapter.getContextConfig();
		startManagingCursor(c);
		int value; // context value
		c.moveToPosition(0); // set current Temperature
		value = Integer.parseInt(c.getString(c.getColumnIndex("current_value")));
		spinner_temperature.setSelection(value);
		c.moveToPosition(1); // set current Weather
		value = Integer.parseInt(c.getString(c.getColumnIndex("current_value")));
		spinner_weather.setSelection(value);
		c.moveToPosition(2); // set current Companion
		value = Integer.parseInt(c.getString(c.getColumnIndex("current_value")));
		spinner_companion.setSelection(value);
		c.moveToPosition(3); // set current Mood
		value = Integer.parseInt(c.getString(c.getColumnIndex("current_value")));
		spinner_mood.setSelection(value);
		c.moveToPosition(4); // set current Familiarity
		value = Integer.parseInt(c.getString(c.getColumnIndex("current_value")));
		spinner_familiarity.setSelection(value);
		c.moveToPosition(5); // set current Budget
		value = Integer.parseInt(c.getString(c.getColumnIndex("current_value")));
		spinner_budget.setSelection(value);
		c.moveToPosition(6); // set current Travel length
		value = Integer.parseInt(c.getString(c.getColumnIndex("current_value")));
		spinner_travel_length.setSelection(value);
		c.moveToPosition(7); // set current Time
		String time = c.getString(c.getColumnIndex("current_value"));
		String[] time_plit = time.split("and");
		btnPickDate.setText(time_plit[0]);
		btnPickTime.setText(time_plit[1]);		
	}
}
