/**
 * 
 */
package uit.is.thesis.travel.SQLiteHelper;

import uit.is.thesis.travel.activities.R;
import android.content.Context;
import android.database.Cursor;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.RatingBar;
import android.widget.SimpleCursorAdapter;
import android.widget.TextView;

/**
 * @author LEHIEU
 *
 */
public class SQLiteCursorAdapter extends SimpleCursorAdapter {
	Cursor currentCursor;
	final LayoutInflater mInflater;
	int[] mFrom; // int array contain column-index of column in DB
	int[] mTo;
	double currentLat,currentLng;
	
	public SQLiteCursorAdapter(Context context, int layout, Cursor c,
			String[] from, int[] to, SQLiteDBHelper dbHelper, double currentLatitude, double currentLongitude) {
		super(context, layout, c, from, to);
		this.currentCursor = c;
		mInflater = LayoutInflater.from(context);
		mTo = to;
		findColumns(from);
		currentLat = currentLatitude;
		currentLng = currentLongitude;
	}

	/**
	 * Create a map from an array of strings to an array of column-id (column index) integers
	 * in mCursor. If mCursor is null, the array will be discarded.
	 * 
	 * @param from
	 *            the Strings naming the columns of interest
	 */
	private void findColumns(String[] from) {
		if (currentCursor != null) {
			int i;
			int count = from.length;
			if (mFrom == null || mFrom.length != count) {
				mFrom = new int[count];
			}
			for (i = 0; i < count; i++) {
				mFrom[i] = currentCursor.getColumnIndexOrThrow(from[i]);
			}
		} else {
			mFrom = null;
		}
	}

	// Makes a new view to hold the data pointed to by cursor.
	@Override
	public View newView(Context context, Cursor cursor, ViewGroup parent) {
		final View view = mInflater.inflate(R.layout.row_favorite, parent,
				false);
		return view;
	}

	// Bind an existing view to the data pointed to by cursor
	@Override
	public void bindView(View v, Context context, Cursor c) {
		final int[] to = mTo;
		String id = c.getString(mFrom[0]);
		String name = c.getString(mFrom[1]);
		String address = c.getString(mFrom[2]) + " " + c.getString(mFrom[3]) + " " + c.getString(mFrom[4]) + " " + c.getString(mFrom[5]) + " " + c.getString(mFrom[6]);
		double lat = Double.parseDouble(c.getString(mFrom[7]));
		double lng = Double.parseDouble(c.getString(mFrom[8]));
		float rating =  Float.parseFloat(c.getString(mFrom[9]));		
		// Haversine formula
		double deltaLatitude = Math.toRadians(Math.abs(lat - currentLat));
		double deltaLongitude = Math.toRadians(Math.abs(lng - currentLng));
		double a = Math.sin(deltaLatitude / 2) * Math.sin(deltaLatitude / 2) + 
				   Math.cos(Math.toRadians(lat)) * Math.cos(Math.toRadians(currentLat)) * 
				   Math.sin(deltaLongitude / 2) * Math.sin(deltaLongitude / 2);
		double temp =  2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
		double distance = 6371 * temp;
			
		CheckBox checkbox = (CheckBox) v.findViewById(to[0]);
		checkbox.setText(id);
		TextView txtViewName = (TextView) v.findViewById(to[1]);
		txtViewName.setText(name);
		TextView txtViewAddress = (TextView) v.findViewById(to[2]);
		txtViewAddress.setText(address);
		TextView txtViewDistance = (TextView) v.findViewById(to[3]);
		txtViewDistance.setText(String.valueOf(distance));
		RatingBar ratingBar = (RatingBar) v.findViewById(to[4]);
		ratingBar.setRating(rating);
	}
}
