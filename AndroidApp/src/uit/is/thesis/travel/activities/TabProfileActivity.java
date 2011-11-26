/**
 * 
 */
package uit.is.thesis.travel.activities;

/**
 * @author LEHIEU
 *
 */

import uit.is.thesis.travel.SQLiteHelper.SQLiteDBAdapter;
import android.app.Activity;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.database.Cursor;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.Spinner;

public class TabProfileActivity extends Activity implements OnClickListener {
	SQLiteDBAdapter mDBAdapter = null;
	Spinner spinner_gender;
	Button btnSaveP, btnBirthday;
	int mYear, mMonth, mDay;
	static final int DATE_DIALOG_ID = 0;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tab_profile);

		// get Layout id
		btnSaveP = (Button) findViewById(R.id.btnSaveP);
		btnSaveP.setOnClickListener(this);
		btnBirthday = (Button) findViewById(R.id.btnBirthday);
		btnBirthday.setOnClickListener(this);
		spinner_gender = (Spinner) findViewById(R.id.spinner_gender);

		// set spinner adapter
		ArrayAdapter<CharSequence> adapter_gender = ArrayAdapter
				.createFromResource(this, R.array.gender_array,
						android.R.layout.simple_spinner_item);
		adapter_gender
				.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner_gender.setAdapter(adapter_gender);

		if (this.mDBAdapter == null) {
			this.mDBAdapter = new SQLiteDBAdapter(this);
			mDBAdapter.open();
		}
		//mDBAdapter.deleteDatabase();
		// get the current profile info
		Cursor c = mDBAdapter.getProfile();
		startManagingCursor(c);
		String value; // context value
		c.moveToPosition(0); // set current Gender
		value = c.getString(c.getColumnIndex("current_value"));
		spinner_gender.setSelection(Integer.parseInt(value));
		c.moveToPosition(1); // set current Birthday
		value = c.getString(c.getColumnIndex("current_value"));
		String s_plit[]  = value.split("-");		
		mYear = Integer.parseInt(s_plit[0].trim());
		mMonth = Integer.parseInt(s_plit[1].trim());
		mDay = Integer.parseInt(s_plit[2].trim());
		
		// display the current date (this method is below)
		updateDisplay();
	}

	// button onclick handler
	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.btnSaveP: {
			try {
				// update profile info into SQLite
				String gender = spinner_gender.getSelectedItem().toString();
				String birthday = btnBirthday.getText().toString();
				String gender1;
				if(gender.compareTo("Male")==0)
					gender1 = "0";
				else
					gender1 = "1";
				mDBAdapter.updateProfile(gender1, birthday);
			} catch (Exception e) {
			}
		}
			break;
		case R.id.btnBirthday: {
			try {
				showDialog(DATE_DIALOG_ID);
			} catch (Exception e) {
			}
		}
			break;
		}
	}

	// the callback received when the user set the date in the dialog
	private DatePickerDialog.OnDateSetListener mDateSetListener = new DatePickerDialog.OnDateSetListener() {

		public void onDateSet(DatePicker view, int year, int monthOfYear,
				int dayOfMonth) {
			mYear = year;
			mMonth = monthOfYear + 1;
			mDay = dayOfMonth;
			updateDisplay();
		}
	};

	@Override
	protected Dialog onCreateDialog(int id) {
		switch (id) {
		case DATE_DIALOG_ID:
			return new DatePickerDialog(this, mDateSetListener, mYear, mMonth - 1,
					mDay);
		}
		return null;
	}

	// updates the date in the button Date
	private void updateDisplay() {
		btnBirthday.setText(new StringBuilder()
				// Month is 0 based so add 1
				.append(mYear).append("-").append(mMonth).append("-")
				.append(mDay).append(" "));
	}

}
