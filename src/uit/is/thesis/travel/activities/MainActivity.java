/**
 * 
 */
package uit.is.thesis.travel.activities;

/**
 * @author LEHIEU
 *
 */

import android.app.TabActivity;
import android.content.Context;
import android.content.Intent;
import android.content.res.Resources;
import android.os.Bundle;
import android.widget.RadioGroup;
import android.widget.TabHost;

public class MainActivity extends TabActivity {
	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.main); //set layout main.xml to this activity
		
		Context ctx = getApplicationContext(); //get app context
		Resources res = getResources(); // Resource object to get Drawable files
		TabHost tabHost = getTabHost(); // The activity TabHost
		TabHost.TabSpec spec = null; // Reusable TabSpec for each tab
		Intent intent = null; // Reusable Intent for each tab
		

		// Create an Intent to launch an Activity for the tab (to be reused)
		// Initialize a TabSpec for each tab and add it to the TabHost
		InitializeTab(res, tabHost, spec, intent, ctx, TabAllActivity.class, "all", "All", R.drawable.btn_selector_tab_all);
		InitializeTab(res, tabHost, spec, intent, ctx, TabSuggestionsActivity.class, "suggestions", "Suggestions", R.drawable.btn_selector_tab_suggestions);
		InitializeTab(res, tabHost, spec, intent, ctx, TabFavoritesActivity.class, "favorites", "Favorites", R.drawable.btn_selector_tab_favorites);
		InitializeTab(res, tabHost, spec, intent, ctx, TabContextActivity.class, "context", "Context", R.drawable.btn_selector_tab_context);
		InitializeTab(res, tabHost, spec, intent, ctx, TabProfileActivity.class, "profile", "Profile", R.drawable.btn_selector_tab_profile);
		
		// set the current Tab
		setCurrentTab(tabHost);
	}
	
	// Initialize a Tab
	public void InitializeTab(Resources res, TabHost tabHost, TabHost.TabSpec spec, Intent intent, Context ctx, Class<?> cls, String tabSpec, String indicator, int id_btn_selector ) {
		intent = new Intent().setClass(ctx, cls);
		spec = tabHost.newTabSpec(tabSpec)
					.setIndicator(indicator, res.getDrawable(id_btn_selector))
					.setContent(intent);
		tabHost.addTab(spec);
	}
	
	// set the current Tab
	public void setCurrentTab(final TabHost tabHost) {
		RadioGroup rg = (RadioGroup) findViewById(R.id.states);
		rg.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
			public void onCheckedChanged(RadioGroup group, int checkedId) {
				switch (checkedId) {
				case R.id.tab_all:
					tabHost.setCurrentTab(0);
					break;
				case R.id.tab_suggestions:
					tabHost.setCurrentTab(1);
					break;
				case R.id.tab_favorites:
					tabHost.setCurrentTab(2);
					break;
				case R.id.tab_context:
					tabHost.setCurrentTab(3);
					break;
				case R.id.tab_profile:
					tabHost.setCurrentTab(4);
					break;	
				}
			}
		});
	}
}