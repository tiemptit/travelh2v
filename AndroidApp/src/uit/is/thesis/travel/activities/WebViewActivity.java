/**
 * 
 */
package uit.is.thesis.travel.activities;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.webkit.WebView;
import android.widget.Button;

/**
 * @author LEHIEU
 * 
 */
public class WebViewActivity extends Activity implements OnClickListener {
	WebView webView;
	String url;
	Button btnBackWeb;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.web_view);
		// get button view id
		btnBackWeb = (Button) findViewById(R.id.btnBackWeb);
		btnBackWeb.setOnClickListener(this);
		// get url from parent activity
		Intent intent = getIntent();
		Bundle bundle = intent.getExtras();
		url = bundle.getString("place_url");
		// load url on web view
		webView = (WebView) findViewById(R.id.webview);
		webView.getSettings().setJavaScriptEnabled(true);
		webView.loadUrl(url);
	}

	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		switch (v.getId()) {
		case R.id.btnBackWeb: {
			try {
				finish();
			} catch (Exception e) {
			}
		}
			break;
		}
	}
}