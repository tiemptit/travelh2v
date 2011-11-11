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
		return mDb
				.rawQuery(
						"SELECT * FROM PLACES,PLACE_CATEGORIES WHERE PLACES.id_place_category=PLACE_CATEGORIES.id_cate",
						null);
	}

	// get 1 place with given ID
	public Cursor getItem(long itemId) {
		return mDb
				.rawQuery(
						"SELECT * FROM PLACES,PLACE_CATEGORIES WHERE PLACES.id_place_category=PLACE_CATEGORIES.id_cate and PLACES.id_place="
								+ itemId, null);
	}

	// check if the place existed or not
	public boolean checkIfExist(String lat, String lng, String name) {
		Cursor c = mDb.query("PLACES", new String[] { "_id" }, "lat" + "="
				+ lat + " and " + "lng" + "=" + lng + " and " + "name"
				+ " like " + "'%" + name + "%'", null, null, null, null);
		if (c.moveToFirst() == false) {
			return false;
		} else {
			return true;
		}
	}

	// insert a new place
	public long insertItem(String id_place, String id_place_category,
			String name, String imgurl, String lat, String lng,
			String house_number, String street, String ward, String district,
			String city, String province, String country, String phone_number,
			String email, String website, String history, String details,
			String sources, String general_rating, String general_count_rating,
			String general_sum_rating) {
		ContentValues insertedValue = new ContentValues();
		insertedValue.put("id_place", id_place);
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
		return kq;
	}

	// delete a place with given ID
	public long deleteItem(long id_place) {
		return mDb.delete("PLACES", "id_place" + "=" + id_place, null);
	}

	// get 'count' places that have name like the key word from row 'from'
	public Cursor getItemsLikeThisFromTo(String keyWord, int from, int count) {
		return mDb
				.rawQuery(
						"SELECT * FROM PLACES,PLACE_CATEGORIES WHERE PLACES.id_place_category = PLACE_CATEGORIES.id_cate and PLACES.name like '%"
								+ keyWord + "%' limit " + from + "," + count,
						null);
	}

	// get total row number
	public int getRowReturnCount(String keyWord) {
		return mDb.query("PLACES", new String[] { "_id" },
				"name" + " like " + "'%" + keyWord + "%'", null, null, null,
				null).getCount();
	}

	// get current context config from DB
	public Cursor getContextConfig() {
		return mDb.rawQuery("SELECT * FROM CONTEXT_CONFIG", null);
	}

	// update current context config
	public void updateContextConfig(int Temperature, int Weather,
			int Companion, int Familiarity, int Mood, int Budget,
			int Travel_length, String Time) {
		Log.i("Context", "update context start");
		mDb.execSQL("UPDATE CONTEXT_CONFIG set current_value = '" + Temperature
				+ "' where CONTEXT_CONFIG._id = 1");
		mDb.execSQL("UPDATE CONTEXT_CONFIG set  current_value= '" + Weather
				+ "' where CONTEXT_CONFIG._id = 2");
		mDb.execSQL("UPDATE CONTEXT_CONFIG set current_value = '" + Companion
				+ "' where CONTEXT_CONFIG._id = 3");
		mDb.execSQL("UPDATE CONTEXT_CONFIG set current_value = '" + Familiarity
				+ "' where CONTEXT_CONFIG._id = 4");
		mDb.execSQL("UPDATE CONTEXT_CONFIG set current_value = '" + Mood
				+ "' where CONTEXT_CONFIG._id = 5");
		mDb.execSQL("UPDATE CONTEXT_CONFIG set current_value = '" + Budget
				+ "' where CONTEXT_CONFIG._id = 6");
		mDb.execSQL("UPDATE CONTEXT_CONFIG set current_value = '" + Travel_length
				+ "' where CONTEXT_CONFIG._id = 7");
		mDb.execSQL("UPDATE CONTEXT_CONFIG set current_value = '" + Time
				+ "' where CONTEXT_CONFIG._id = 8");
		Log.i("Context", "update context end");
	}
}
