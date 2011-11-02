/**
 * 
 */
package uit.is.thesis.travel.InternetHelper;

import java.util.List;
import uit.is.thesis.travel.models.*;
import uit.is.thesis.travel.utilities.*;

/**
 * @author LEHIEU
 *
 */
public class SearchService {
	
	// search All Places
	public static List<PlaceModel> SearchAllPlaces(String url, double current_lat, double current_lng) {
		return JsonUtil.GetAllPlaces(url, current_lat, current_lng);
	}	
	
}
