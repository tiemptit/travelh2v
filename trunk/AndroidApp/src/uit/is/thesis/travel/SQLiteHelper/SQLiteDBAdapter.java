/**
 * 
 */
package uit.is.thesis.travel.SQLiteHelper;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.util.Log;

/**
 * @author LEHIEU
 * 
 */

public class SQLiteDBAdapter {

	/*
	 * public static final String DATABASE_TABLE = "Favourite"; // columns are
	 * the same to SearchResult properties public static final String KEY_ROWID
	 * = "_id"; public static final String KEY_NAME = "name"; public static
	 * final String KEY_ADDRESS = "address"; public static final String
	 * KEY_PHONE = "phone"; public static final String KEY_LAT = "lat"; public
	 * static final String KEY_LNG = "lng"; public static final String
	 * KEY_DISTANCE = "distance"; public static final String KEY_RATING =
	 * "rating"; public static final String KEY_URL = "url"; public static final
	 * String KEY_IDC = "idc"; //id of Catefory (iquery)
	 */
	// new String[] {
	// "_id","id","id_place_category","name","imgurl","lat","lng","house_number","street","ward","district","city","province","country","phone_number","email","website","history","details","sources","general_rating","general_count_rating","general_sum_rating"}
	public SQLiteDBHelper mDbHelper;
	public SQLiteDatabase mDb;
	public final Context mCtx;

	// Constructor, set the variable context
	public SQLiteDBAdapter(Context context) {
		mCtx = context;
	}

	// get the mDbHelper
	public SQLiteDBHelper getmDbHelper() {
		return mDbHelper;
	}

	// Open DB connection
	public SQLiteDBAdapter open() throws SQLException {
		mDbHelper = new SQLiteDBHelper(mCtx);
		mDb = mDbHelper.getWritableDatabase();
		return this;
	}

	// close DB connection
	public void close() {
		mDbHelper.close();
	}

	// delete DB
	public void deleteDatabase() {
		mDbHelper.close();
		try {
			mCtx.deleteDatabase("TravleH2VDB");
		} catch (Exception e) {
		}
	}

	// get All places from DB
	public Cursor getAllItems() {
		/*
		 * return mDb.query("PLACES", new String[] { "_id", "id", ""
		 * KEY_ADDRESS, KEY_PHONE, KEY_LAT, KEY_LNG, KEY_RATING, KEY_DISTANCE,
		 * KEY_URL, KEY_IDC }, null, null, null, null, KEY_NAME);
		 */
		return mDb
				.rawQuery(
						"SELECT * FROM PLACES,PLACE_CATEGORIES WHERE PLACES.id_place_category=PLACE_CATEGORIES.id",
						null);
	}

	// get 1 place with given ID
	public Cursor getItem(long itemId) {
		/*
		 * return mDb.query(DATABASE_TABLE, new String[] { KEY_ROWID, KEY_NAME,
		 * KEY_ADDRESS, KEY_PHONE, KEY_LAT, KEY_LNG, KEY_RATING, KEY_DISTANCE,
		 * KEY_URL, KEY_IDC }, KEY_ROWID + "=" + itemId, null, null, null,
		 * null);
		 */
		return mDb
				.rawQuery(
						"SELECT * FROM PLACES,PLACE_CATEGORIES WHERE PLACES.id_place_category=PLACE_CATEGORIES.id and PLACES._id="
								+ itemId, null);
	}

	// get places that have name like the keyword
	public Cursor getItemsLikeThis(String keyWord) {
		/*
		 * return mDb.query(DATABASE_TABLE, new String[] { KEY_ROWID, KEY_NAME,
		 * KEY_ADDRESS, KEY_PHONE, KEY_LAT, KEY_LNG, KEY_RATING, KEY_DISTANCE,
		 * KEY_URL, KEY_IDC }, KEY_NAME + " like " + "'%" + keyWord + "%'",
		 * null, null, null, KEY_NAME);
		 */
		return mDb
				.rawQuery(
						"SELECT * FROM PLACES,PLACE_CATEGORIES WHERE PLACES.id_place_category=PLACE_CATEGORIES.id and PLACES.name like '%"
								+ keyWord + "%'", null);
	}

	// check if the place existed or not
	public boolean checkIfExist(String lat, String lng, String name) {
		Log.i("checkIfExist", " DBAdapter - checkIfExist start");
		Cursor c = mDb.query("PLACES", new String[] { "_id" }, "lat" + "="
				+ lat + " and " + "lng" + "=" + lng + " and " + "name"
				+ " like " + "'%" + name + "%'", null, null, null, null);
		Log.i("checkIfExist", " DBAdapter - checkIfExist after Cursor");
		if (c.moveToFirst() == false) {
			Log.i("checkIfExist", " DBAdapter - checkIfExist false");
			return false;
		} else {
			Log.i("checkIfExist", " DBAdapter - checkIfExist true");
			return true;
		}
	}

	// insert a new place
	public long insertItem(String id, String id_place_category, String name,
			String imgurl, String lat, String lng, String house_number,
			String street, String ward, String district, String city,
			String province, String country, String phone_number, String email,
			String website, String history, String details, String sources,
			String general_rating, String general_count_rating,
			String general_sum_rating) {
		ContentValues insertedValue = new ContentValues();
		insertedValue.put("id", id);
		insertedValue.put("id_place_category", id_place_category);
		insertedValue.put("name", name);
		insertedValue.put("imgurl", imgurl);
		insertedValue.put("lat", lat);
		insertedValue.put("lng", lng);
		insertedValue.put("house_number", house_number);
		insertedValue.put("street", street);
		insertedValue.put("ward", ward);
		insertedValue.put("district", district);
		insertedValue.put("city", city);
		insertedValue.put("province", province);
		insertedValue.put("country", country);
		insertedValue.put("phone_number", phone_number);
		insertedValue.put("email", email);
		insertedValue.put("website", website);
		insertedValue.put("history", history);
		insertedValue.put("details", details);
		insertedValue.put("sources", sources);
		insertedValue.put("general_rating", general_rating);
		insertedValue.put("general_count_rating", general_count_rating);
		insertedValue.put("general_sum_rating", general_sum_rating);

		long kq = mDb.insert("PLACES", null, insertedValue);
		Log.i("InsertFavorites", "insertItem - kq =" + kq);
		return kq;
	}

	// delete a place with given ID
	public long deleteItem(long rowId) {
		return mDb.delete("PLACES", "_id" + "=" + rowId, null);
	}

	// get 'count' places from row 'from'
	public Cursor getItemsFromTo(int from, int count) {
		Log.i("TabFavorites", "DBAdapter - getItemsFromTo = " + from + "and"
				+ count);
		return mDb
				.rawQuery(
						"SELECT * FROM PLACES,PLACE_CATEGORIES WHERE PLACES.id_place_category = PLACE_CATEGORIES.id limit "
								+ from + "," + count, null);
	}

	// get 'count' places that have name like the key word from row 'from'
	public Cursor getItemsLikeThisFromTo(String keyWord, int from, int count) {
		Log.i("TabFavorites", "DBAdapter - getItemsLikeThisFromTo = " + from + "and"
				+ count);
		return mDb
				.rawQuery(
						"SELECT * FROM PLACES,PLACE_CATEGORIES WHERE PLACES.id_place_category = PLACE_CATEGORIES.id and PLACES.name like '%"
								+ keyWord + "%' limit " + from + "," + count,
						null);
	}

	// get total row number
	public int getRowReturnCount(String keyWord) {
		return mDb.query("PLACES", new String[] { "_id" },
				"name" + " like " + "'%" + keyWord + "%'", null, null, null,
				null).getCount();
	}

	// test get places_categories
	public int getAllPlacesCategories() {
		return mDb.query("PLACE_CATEGORIES", new String[] { "_id" }, null, null, null,
				null, null).getCount();
	}
	// test get places
	public int getAllPlaces() {
		return mDb.query("PLACES", new String[] { "_id" }, null, null, null,
				null, null).getCount();
	}

	// test get places_categories
	public int getAllCONTEXT_CONFIG() {
		return mDb.query("CONTEXT_CONFIG", new String[] { "_id" }, null, null, null,
				null, null).getCount();
	}


}
