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
import uit.is.thesis.travel.utilities.*;

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
			/*try {
			Intent intent = new Intent(TabAllActivity.this, MapActivity.class);
			startActivity(intent);
			} catch(Exception e){
				//Log.i("test", "ShowMap exception: " + e.toString());
			}*/
			
			//JsonUtil.GetAllPlaces("http://routes.cloudmade.com/8ee2a50541944fb9bcedded5165f09d9/api/0.3/11.956372,108.4444,11.947283,108.443080/car.js?lang=en");
			//JsonUtil.GetAllPlaces("https://ajax.googleapis.com/ajax/services/search/local?v=1.0&rsz=2&sll=10,106&q=hotel&start=0");
			JsonUtil.GetAllPlaces("http://10.0.2.2:33638/Service/ListAllPlaces");

		}
		}
	}

}