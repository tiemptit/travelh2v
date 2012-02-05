/**
 * 
 */
package uit.is.thesis.travel.activities;

/**
 * @author LEHIEU
 *
 */
import java.util.ArrayList;
import java.util.List;
import uit.is.thesis.travel.InternetHelper.MapService;
import uit.is.thesis.travel.models.PlaceModel;
import uit.is.thesis.travel.utilities.ConfigUtil;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.CornerPathEffect;
import android.graphics.Paint;
import android.graphics.Paint.Style;
import android.graphics.Path;
import android.graphics.PathEffect;
import android.graphics.Point;
import android.graphics.drawable.Drawable;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.os.Handler;
import android.os.Message;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;
import com.google.android.maps.GeoPoint;
import com.google.android.maps.ItemizedOverlay;
import com.google.android.maps.MapActivity;
import com.google.android.maps.MapController;
import com.google.android.maps.MapView;
import com.google.android.maps.MyLocationOverlay;
import com.google.android.maps.OverlayItem;
import com.google.android.maps.Projection;

public class MyMapActivity extends MapActivity implements Runnable {

	Button btnBackMap, btnDirection;
	// Prepare variable for map
	MapService ms = new MapService();
	MapController mc;
	MapView map = null;
	MyLocationOverlay me = null;
	SitesOverlay centerOverlay = null;
	SitesOverlay detailOverlay = null;
	double curLAT, curLNG, fromLat, fromLng;
	// LocationManager, location
	LocationManager locationManager;
	Location location;
	String provider;
	// Prepare variable for request direction
	PlaceModel respectLocation;
	PlaceModel fromLocation;
	String statusDirection, directionEN;
	// Prepare variable for draw direction
	private List<GeoPoint> listRoutePoint = null;

	ProgressDialog progressDialog;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.show_on_map);

		// Create and show ProgressDialog
		progressDialog = new ProgressDialog(this);
		progressDialog.setMessage("Loading ... Please wait!");
		progressDialog.show();

		// get current location of user (get from Details Activities)
		//getLatitudeLongitude();

		// get map controller
		map = (MapView) findViewById(R.id.map);
		mc = map.getController(); // get map controller to zoom to current place
		me = new MyLocationOverlay(this, map);

		// Create thread and start it
		Thread thread = new Thread(this);
		thread.start();
	}

	// methods of MapActivity
	@Override
	public void onResume() {
		super.onResume();
		me.enableCompass();
		/*// Provider is GPS
		provider = LocationManager.GPS_PROVIDER;
		if (locationManager != null) {
			locationManager.requestLocationUpdates(provider, 500, 10,
					locationListener);
			// stop update GPS after 6s
			CountDownTimer aCounter = new CountDownTimer(10000, 1000) {
				public void onTick(long millisUntilFinished) {
				}

				public void onFinish() {
					if (locationManager != null) {
						location = locationManager
								.getLastKnownLocation(provider);
						//locationManager.removeUpdates(locationListener);
					}
				}
			};
			aCounter.start();
		}
		// Initialize the location fields
		if (location != null) {
			this.curLAT = location.getLatitude();
			this.curLNG = location.getLongitude();
			// stop receive GPS signal
			// locationManager.removeUpdates(locationListener);
		} else {
			this.curLAT = ConfigUtil.LATITUDE;
			this.curLNG = ConfigUtil.LONGITUDE;
		}*/
	}

	@Override
	public void onPause() {
		super.onPause();
		me.disableCompass();
		/*if (locationManager != null) {
			locationManager.removeUpdates(locationListener);
		}*/
	}

	@Override
	protected boolean isRouteDisplayed() {
		return (true);
	}

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) { // click zoom key
		if (keyCode == KeyEvent.KEYCODE_S) {
			map.setSatellite(!map.isSatellite());
			return (true);
		} else if (keyCode == KeyEvent.KEYCODE_Z) {
			map.displayZoomControls(true);
			return (true);
		}
		return (super.onKeyDown(keyCode, event));
	}

	public void zoomToCurrentPlace() {
		// zoom to Place location
		GeoPoint point = getPoint(respectLocation.getLat(),
				respectLocation.getLng());
		mc.setZoom(16);
		mc.setCenter(point);
	}

	public void drawPlace() {
		// Prepare overlays: Center overlay and Details overlay
		Drawable marker = getResources().getDrawable(R.drawable.marker);
		Drawable centermarker = getResources().getDrawable(
				R.drawable.centermarker);
		centermarker.setBounds(0, 0, marker.getIntrinsicWidth(),
				marker.getIntrinsicHeight());
		centerOverlay = new SitesOverlay(centermarker);
		Drawable detailmarker = getResources().getDrawable(R.drawable.star);
		detailmarker.setBounds(0, 0, marker.getIntrinsicWidth(),
				marker.getIntrinsicHeight());
		detailOverlay = new SitesOverlay(detailmarker);

		// Add remain overlays into map
		centerOverlay.addItem(new OverlayItem(getPoint(curLAT, curLNG),
				"My Location", "My Location\n" + curLAT + " - " + curLNG));
		centerOverlay.completeToPopulate();

		// add user location
		map.getOverlays().add(centerOverlay);
		map.getOverlays().add(me);

		if (respectLocation != null) {
			// Draw position to map
			double detailLAT = respectLocation.getLat();
			double detailLNG = respectLocation.getLng();
			String address = "";
			address += respectLocation.getHouse_number() + ", "
					+ respectLocation.getStreet() + ", "
					+ respectLocation.getWard() + ", "
					+ respectLocation.getDistrict() + ", "
					+ respectLocation.getCity();
			String des = respectLocation.getName() + "\n" + address + "\n"
					+ detailLAT + " - " + detailLNG;
			detailOverlay.addItem(new OverlayItem(
					getPoint(detailLAT, detailLNG), respectLocation.getName(),
					des));
			detailOverlay.completeToPopulate();
			map.getOverlays().add(detailOverlay);

			// Get route direction and list route point to draw map
			fromLat = fromLocation.getLat();
			fromLng = fromLocation.getLng();
			try {
				ms.getRoute(respectLocation, fromLat, fromLng, "en");
				listRoutePoint = ms.getListPathPoint();
			} catch (Exception e) {
			}
			directionEN = ms.getDirection();
			statusDirection = ms.getStatusDirection();

			// can not find the direction, listRoutePoint == null
			if (!statusDirection.equals("OK")) {
				detailOverlay.addItem(new OverlayItem(getPoint(detailLAT,
						detailLNG), respectLocation.getName(), des));
				detailOverlay.completeToPopulate();
				map.getOverlays().add(detailOverlay);
			}
		}
	}

	public void setButtonClick() {
		btnBackMap.setOnClickListener(new android.view.View.OnClickListener() {
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				/*if (locationManager != null) {
					locationManager.removeUpdates(locationListener);
				}*/
				finish();
			}
		});

		btnDirection
				.setOnClickListener(new android.view.View.OnClickListener() {
					@Override
					public void onClick(View v) {
						// TODO Auto-generated method stub
						if (directionEN != null) { // show dialog box
							TextView myView = new TextView(
									getApplicationContext());
							myView.setText(directionEN);
							myView.setTextSize(15);
							AlertDialog.Builder alertDialog = new AlertDialog.Builder(
									MyMapActivity.this);
							alertDialog.setTitle("Driving directions");
							alertDialog.setView(myView);
							alertDialog.setPositiveButton("Ok", null);
							alertDialog.show();
						} else {
							Toast.makeText(getApplicationContext(),
									"There is no direction!",
									Toast.LENGTH_SHORT).show();
						}
					}
				});
	}

	// class overlay to show map and draw place
	public class SitesOverlay extends ItemizedOverlay<OverlayItem> {
		private List<OverlayItem> items = new ArrayList<OverlayItem>();
		private Drawable marker = null;
		private Paint paint;
		private int LINE_WIDTH = 2;

		public SitesOverlay(Drawable marker) {
			super(marker);
			this.marker = marker;
		}

		@Override
		protected OverlayItem createItem(int i) {
			return (items.get(i));
		}

		public void addItem(OverlayItem item) {
			items.add(item);
		}

		/*
		 * public void clear() { items.clear(); }
		 */
		public void completeToPopulate() {
			populate();
		}

		public void initPaint() {
			if (paint == null)
				paint = new Paint();
			paint.setColor(Color.RED);
			paint.setStyle(Style.STROKE);
			paint.setAntiAlias(true);
			paint.setStrokeWidth(LINE_WIDTH);
			PathEffect effect = new CornerPathEffect(LINE_WIDTH);
			paint.setPathEffect(effect);
		}

		@Override
		public void draw(Canvas canvas, MapView mapView, boolean shadow) {
			super.draw(canvas, mapView, shadow);

			int zoom = mapView.getZoomLevel();
			if (zoom <= 10) {
				LINE_WIDTH = 1;
			} else if (zoom < 15) {
				LINE_WIDTH = 2;
			} else {
				LINE_WIDTH = 3;
			}
			initPaint();

			try {
				boundCenterBottom(marker);
				if (listRoutePoint != null) {
					Projection projection = mapView.getProjection();
					canvas.drawPath(
							getPath(convertListGeoPointToPoint(listRoutePoint,
									projection)), paint);
				}
			} catch (Throwable e) {
			}
		}

		public List<Point> convertListGeoPointToPoint(
				List<GeoPoint> listGeoPoint, Projection project) {
			List<Point> listPoint = new ArrayList<Point>();
			if (listGeoPoint != null && listGeoPoint.size() > 0
					&& project != null) {
				listPoint.clear();
				for (GeoPoint geoPoint : listGeoPoint) {
					Point point = new Point();
					project.toPixels(geoPoint, point);
					listPoint.add(point);
				}
			}
			return listPoint;
		}

		public Path getPath(List<Point> list) {
			Path path = new Path();
			if (list != null && list.size() > 1) {
				path.reset();
				path.moveTo(list.get(0).x, list.get(0).y);
				int size = list.size();
				for (int i = 0; i < size; i++) {
					path.lineTo(list.get(i).x, list.get(i).y);
				}
				path.moveTo(list.get(list.size() - 1).x,
						list.get(list.size() - 1).y);
			}
			return path;
		}

		@Override
		protected boolean onTap(int i) {
			try {
				Toast.makeText(MyMapActivity.this, items.get(i).getSnippet(),
						Toast.LENGTH_SHORT).show();
				return (true);
			} catch (Exception e) {
				return true;
			}
		}

		@Override
		public int size() {
			return (items.size());
		}
	}

	public final LocationListener locationListener = new LocationListener() {
		public void onLocationChanged(Location location) {
			curLAT = location.getLatitude();
			curLAT = location.getLongitude();
		}

		@Override
		public void onProviderDisabled(String arg0) {
			// TODO Auto-generated method stub
		}

		@Override
		public void onProviderEnabled(String provider) {
			// TODO Auto-generated method stub
		}

		@Override
		public void onStatusChanged(String provider, int status, Bundle extras) {
			// TODO Auto-generated method stub
		}
	};

	// Get current location
	public void getLatitudeLongitude() {
		// Get the location manager
		locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
		// Provider is GPS
		provider = LocationManager.GPS_PROVIDER;
		if (locationManager != null) {
			locationManager.requestLocationUpdates(provider, 500, 10,
					locationListener);
			// stop update GPS after 6s
			CountDownTimer aCounter = new CountDownTimer(10000, 1000) {
				public void onTick(long millisUntilFinished) {
				}

				public void onFinish() {
					if (locationManager != null) {
						location = locationManager
								.getLastKnownLocation(provider);
						//locationManager.removeUpdates(locationListener);
					}
				}
			};
			aCounter.start();
		}
		// Initialize the location fields
		if (location != null) {
			this.curLAT = location.getLatitude();
			this.curLNG = location.getLongitude();
			// stop receive GPS signal
			// locationManager.removeUpdates(locationListener);
		} else {
			this.curLAT = ConfigUtil.LATITUDE;
			this.curLNG = ConfigUtil.LONGITUDE;
		}
	}

	// alert GPS signal
	public void alertGPSsignal() {
		if (location == null) {
			TextView myView = new TextView(getApplicationContext());
			myView.setText("Phone can't get the GPS signal! Your current location will be set to default value: at Reunification Place (Dinh Doc Lap), district 1, HCMC."
					+ "\n\n"
					+ "Please check the GPS service on your phone!"
					+ "\n\n"
					+ "(If you're inside your house, you can't get the GPS signal!)");
			myView.setTextSize(15);
			AlertDialog.Builder alertDialog = new AlertDialog.Builder(
					MyMapActivity.this);
			alertDialog.setTitle("Alert");
			alertDialog.setView(myView);
			alertDialog.setPositiveButton("OK", null);
			alertDialog.show();
		}
	}

	// convert Point to GeoPoint for drawing place on map
	public GeoPoint getPoint(double lat, double lon) {
		return (new GeoPoint((int) (lat * 1000000.0), (int) (lon * 1000000.0)));
	}

	@Override
	public void run() {
		// TODO Auto-generated method stub
		try {
			btnBackMap = (Button) findViewById(R.id.btnBackMap);
			btnDirection = (Button) findViewById(R.id.btnDirection);
			// Get intent and receive data from the parent activity
			Intent intent = getIntent();
			Bundle bundle = intent.getExtras();
			respectLocation = (PlaceModel) bundle
					.getSerializable("currentplace");
			curLAT = bundle.getDouble("currentLatitude");
			curLNG = bundle.getDouble("currentLongitude");

			// Set the coordinate for from-point
			if (fromLocation == null) {
				fromLocation = new PlaceModel();
				fromLocation.setLat(curLAT);
				fromLocation.setLng(curLNG);
			}

			// zoom to current place
			zoomToCurrentPlace();
			// draw place
			drawPlace();

			// buttonOnClick, button Zoom on map
			setButtonClick();
			map.setBuiltInZoomControls(true);
			map.invalidate();
		} catch (Exception e) {
		}
		handler.sendEmptyMessage(0);
	}

	/** Handler for handling message from method run() */
	public Handler handler = new Handler() {
		@Override
		public void handleMessage(Message message) {
			if (!statusDirection.equals("OK")) { // not find direction
				Toast.makeText(getApplicationContext(), statusDirection,
						Toast.LENGTH_SHORT).show();
			}
			// dismiss dialog
			progressDialog.dismiss();
			// alertGPSsignal();
		}
	};
}