/**
 * 
 */
package uit.is.thesis.travel.SQLiteHelper;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

/**
 * @author LEHIEU
 * 
 */

public class SQLiteDBHelper extends SQLiteOpenHelper {
	// context of the app
	private Context mCtx;
	// DB info
	private static String DATABASE_PATH = "/data/data/uit.is.thesis.travel.activities/databases/";
	private static final String DATABASE_NAME = "TravleH2VDB";
	private static final int DATABASE_VERSION = 1;
	private static final String CREATE_TABLE_PLACE_CATEGORIES = "CREATE TABLE PLACE_CATEGORIES("
			+ "_id 				 INTEGER primary key autoincrement,"
			+ "id_cate                   INTEGER, "
			+ "place_category	     TEXT "
			+ ");";

	private static final String CREATE_TABLE_PLACES = "CREATE TABLE PLACES("
			+ "_id 				 INTEGER primary key autoincrement,"
			+ "id_place                   INTEGER,"
			+ "id_place_category    INTEGER,"
			+ "name                 TEXT,"
			+ "imgurl               TEXT,"
			+ "lat                  TEXT,"
			+ "lng                  TEXT,"
			+ "house_number         TEXT,"
			+ "street               TEXT,"
			+ "ward                 TEXT,"
			+ "district             TEXT,"
			+ "city                 TEXT,"
			+ "province             TEXT,"
			+ "country              TEXT,"
			+ "phone_number         TEXT,"
			+ "email                TEXT,"
			+ "website              TEXT,"
			+ "history              TEXT,"
			+ "details              TEXT,"
			+ "sources              TEXT,"
			+ "general_rating       TEXT,"
			+ "general_count_rating TEXT,"
			+ "general_sum_rating   TEXT,"
			+ "FOREIGN KEY(id_place_category) REFERENCES PLACE_CATEGORIES(id) );";

	private static final String CREATE_TABLE_CONTEXT_CONFIG = "CREATE TABLE CONTEXT_CONFIG("
			+ "_id 				 INTEGER primary key autoincrement,"
			+ "type                 TEXT," + "current_value			     TEXT" + ");";

	// constructor
	public SQLiteDBHelper(Context context) {
		super(context, DATABASE_NAME, null, DATABASE_VERSION);
		mCtx = context;
	}

	@Override
	public void onCreate(SQLiteDatabase db) {
		// TODO Auto-generated method stub
		boolean dbExist = checkDataBase(); // check if DB existed
		if (dbExist) {
			// do nothing - database already exist
			this.getWritableDatabase(); // use this to call onUpgrade function
		} else {
			db.execSQL("drop table if exists PLACE_CATEGORIES");
			db.execSQL("drop table if exists PLACES");
			db.execSQL("drop table if exists CONTEXT_CONFIG");
			db.execSQL(CREATE_TABLE_PLACE_CATEGORIES);
			db.execSQL(CREATE_TABLE_PLACES);
			db.execSQL(CREATE_TABLE_CONTEXT_CONFIG);
			
			db.execSQL("insert into PLACE_CATEGORIES values (1,1,'Seeing');");
			db.execSQL("insert into PLACE_CATEGORIES values (2,2,'Entertainment');");
			db.execSQL("insert into PLACE_CATEGORIES values (3,3,'Hotel');");
			db.execSQL("insert into PLACE_CATEGORIES values (4,4,'Restaurant');");
			
			db.execSQL("insert into CONTEXT_CONFIG values (1,'Temperature','0');");
			db.execSQL("insert into CONTEXT_CONFIG values (2,'Weather','1');");
			db.execSQL("insert into CONTEXT_CONFIG values (3,'Companion','2');");
			db.execSQL("insert into CONTEXT_CONFIG values (4,'Familiarity','0');");
			db.execSQL("insert into CONTEXT_CONFIG values (5,'Mood','1');");
			db.execSQL("insert into CONTEXT_CONFIG values (6,'Budget','2');");
			db.execSQL("insert into CONTEXT_CONFIG values (7,'Travel length','0');");
			db.execSQL("insert into CONTEXT_CONFIG values (8,'Time','2011-11-04and09:09:09');");
		}
	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		// TODO Auto-generated method stub
		if (newVersion != oldVersion) {
			mCtx.deleteDatabase(DATABASE_NAME);
			onCreate(db);
		}
	}

	// Check if the database already exist
	private boolean checkDataBase() {
		SQLiteDatabase checkDB = null;
		try {
			String myPath = DATABASE_PATH + DATABASE_NAME;
			checkDB = SQLiteDatabase.openDatabase(myPath, null,
					SQLiteDatabase.OPEN_READONLY);
		} catch (Exception e) {
			// database does't exist yet.
		}
		if (checkDB != null) {
			checkDB.close();
		}
		return checkDB != null ? true : false;
	}
}
