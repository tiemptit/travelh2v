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
import android.location.LocationManager;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;
import com.google.android.maps.GeoPoint;
import com.google.android.maps.ItemizedOverlay;
import com.google.android.maps.MapActivity;
import com.google.android.maps.MapView;
import com.google.android.maps.MyLocationOverlay;
import com.google.android.maps.OverlayItem;
import com.google.android.maps.Projection;


public class MyMapActivity extends MapActivity {

	Button btnBackMap;
	// Prepare variable for map
	MapService ms = new MapService();
	private MapView map = null;
	private MyLocationOverlay me = null;
	private SitesOverlay centerOverlay = null;
	private SitesOverlay detailOverlay = null;
	double curLAT, curLNG, fromLat, fromLng;

	// Prepare variable for request direction
	private PlaceModel respectLocation;
	private PlaceModel fromLocation;

	// Prepare variable for draw direction
	private List<GeoPoint> listRoutePoint = null;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.show_on_map);
		try {
			btnBackMap = (Button) findViewById(R.id.btnBackMap);
			// Get intent and receive data from the parent activity
			Intent intent = getIntent();
			Bundle bundle = intent.getExtras();
			respectLocation = (PlaceModel) bundle.getSerializable("currentplace");

			// get current location of user
			getLatitudeLongitude();
			// Set the coordinate for from-point
			if (fromLocation == null) {
				fromLocation = new PlaceModel();
				fromLocation.setLat(curLAT);
				fromLocation.setLng(curLNG);
			}

			// draw place
			drawPlace();

			// buttonOnClick
			setButtonClick();
		} catch (Exception e) {
		}
	}

	private void drawPlace() {
		// Prepare map and overlays
		map = (MapView) findViewById(R.id.map);
		map.getController().setZoom(16);
		map.setBuiltInZoomControls(true);
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
			ms.getRoute(respectLocation, fromLat, fromLng, "vi");
			listRoutePoint = ms.getListPathPoint();
			// directionVI = ms.getDirection();
			String statusDirection = ms.getStatusDirection();
			if (!statusDirection.equals("OK")) // can not find the direction
				Toast.makeText(getApplicationContext(), statusDirection,
						Toast.LENGTH_SHORT).show();
		}

		// Add remain overlays into map
		centerOverlay.addItem(new OverlayItem(getPoint(curLAT, curLNG),
				"My Location", "My Location\n" + curLAT + " - " + curLNG));
		centerOverlay.completeToPopulate();
		// overlay.completeToPopulate();
		map.getOverlays().add(centerOverlay);
		me = new MyLocationOverlay(this, map); // user location
		map.getOverlays().add(me);
	}

	private void setButtonClick() {
		btnBackMap.setOnClickListener(new android.view.View.OnClickListener() {	
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				finish();
			}
		});
	}

	// methods of MapActivity
	@Override
	public void onResume() {
		super.onResume();
		me.enableCompass();
	}

	@Override
	public void onPause() {
		super.onPause();
		me.disableCompass();
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

	// class overlay to show map and draw place
	private class SitesOverlay extends ItemizedOverlay<OverlayItem> {
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

	// Get current location
	private void getLatitudeLongitude() {
		// Get the location manager
		LocationManager locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
		// Provider is GPS
		String provider = LocationManager.GPS_PROVIDER;
		Location location = locationManager.getLastKnownLocation(provider);
		// Initialize the location fields
		if (location != null) {
			this.curLAT = location.getLatitude();
			this.curLNG = location.getLongitude();
		} else {
			this.curLAT = ConfigUtil.LATITUDE;
			this.curLNG = ConfigUtil.LONGITUDE;
		}
	}

	// convert Point to GeoPoint for drawing place on map
	private GeoPoint getPoint(double lat, double lon) {
		return (new GeoPoint((int) (lat * 1000000.0), (int) (lon * 1000000.0)));
	}

}