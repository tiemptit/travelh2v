/**
 * 
 */
package uit.is.thesis.travel.activities;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import android.widget.ToggleButton;

/**
 * @author LEHIEU
 * 
 */
public class DetailsActivity extends Activity implements OnClickListener {
	// buttons on screen
	ToggleButton ToggleBtnBasicInfo;
	TextView txtViewRequire2;
	int show = 1;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.details);
		// Get intent and receive data from the parent activity (TabAllActivity
		// ...)
		Intent intent = getIntent();

		//
		ToggleBtnBasicInfo = (ToggleButton) findViewById(R.id.ToggleBtnBasicInfo);
		ToggleBtnBasicInfo.setOnClickListener(this);
		txtViewRequire2 = (TextView) findViewById(R.id.txtViewRequire2);
	}

	// button onclick handler
	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.ToggleBtnBasicInfo: {
			try {
				if (!((ToggleButton) v).isChecked()) {
					txtViewRequire2.setVisibility(View.GONE);
				} else {
					txtViewRequire2.setVisibility(View.VISIBLE);
				}
			} catch (Exception e) {

			}

		}
		}
	}
	
	
}