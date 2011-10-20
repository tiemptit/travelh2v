/**
 * 
 */
package uit.is.thesis.travel.activities;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

/**
 * @author LEHIEU
 *
 */
public class MapActivity extends Activity {
	
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.map);
        
     // Get intent and receive data from the parent activity (...)
		Intent intent = getIntent();
    }
}