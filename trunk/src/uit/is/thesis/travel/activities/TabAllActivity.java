/**
 * 
 */
package uit.is.thesis.travel.activities;

/**
 * @author LEHIEU
 *
 */

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;

public class TabAllActivity extends Activity implements OnClickListener {
	Button btnAz;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tab_all);

		btnAz = (Button)findViewById(R.id.btnAz);
		btnAz.setOnClickListener(this);
	}

	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.btnAz: {
			// call MapActivity class
			try {
			Intent intent = new Intent(TabAllActivity.this, MapActivity.class);
			startActivity(intent);
			} catch(Exception e){
				//Log.i("test", "ShowMap exception: " + e.toString());
			}

		}
		}
	}

}