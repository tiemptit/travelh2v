/**
 * 
 */
package uit.is.thesis.travel.utilities;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import android.util.Log;
import uit.is.thesis.travel.models.PlaceCategoryModel;
import uit.is.thesis.travel.models.PlaceModel;

/**
 * @author LEHIEU
 * 
 */
public class SortListUtil {

	public static List<PlaceModel> SortListByDistance(List<PlaceModel> original) {
		List<PlaceModel> temp = new ArrayList<PlaceModel>();
		for (int i = 0; i < original.size(); i++) {
			PlaceModel p = new PlaceModel();
			p.setCity(original.get(i).getCity());
			p.setCountry(original.get(i).getCountry());
			p.setDetails(original.get(i).getDetails());
			p.setDistrict(original.get(i).getDistrict());
			p.setEmail(original.get(i).getEmail());
			p.setEstimate_rating(original.get(i).getEstimate_rating());
			p.setGeneral_count_rating(original.get(i).getGeneral_count_rating());
			p.setGeneral_rating(original.get(i).getGeneral_rating());
			p.setGeneral_sum_rating(original.get(i).getGeneral_sum_rating());
			p.setHistory(original.get(i).getHistory());
			p.setHouse_number(original.get(i).getHouse_number());
			p.setId(original.get(i).getId());
			p.setId_itemOnListView(i);
			p.setImgurl(original.get(i).getImgurl());
			p.setLat(original.get(i).getLat());
			p.setLng(original.get(i).getLng());
			p.setDistance(original.get(i).getDistance());
			p.setName(original.get(i).getName());
			p.setPhone_number(original.get(i).getPhone_number());
			p.place_category_obj = new PlaceCategoryModel();
			p.place_category_obj.setId(original.get(i).getPlace_category_obj().getId());
			p.place_category_obj.setPlace_category(original.get(i).getPlace_category_obj().getPlace_category());
			p.setProvince(original.get(i).getProvince());
			p.setSources(original.get(i).getSources());
			p.setStreet(original.get(i).getStreet());
			p.setWard(original.get(i).getWard());
			p.setWebsite(original.get(i).getWebsite());		
			temp.add(p);
		}
		Collections.sort(temp, new DistanceComparator());
		for (int i = 0; i < temp.size(); i++)
			temp.get(i).setId_itemOnListView(i);
		return temp;
	}

	public static List<PlaceModel> SortListByRating(List<PlaceModel> original) {
		List<PlaceModel> temp = new ArrayList<PlaceModel>();
		for (int i = 0; i < original.size(); i++) {
			PlaceModel p = new PlaceModel();
			p.setCity(original.get(i).getCity());
			p.setCountry(original.get(i).getCountry());
			p.setDetails(original.get(i).getDetails());
			p.setDistrict(original.get(i).getDistrict());
			p.setEmail(original.get(i).getEmail());
			p.setEstimate_rating(original.get(i).getEstimate_rating());
			p.setGeneral_count_rating(original.get(i).getGeneral_count_rating());
			p.setGeneral_rating(original.get(i).getGeneral_rating());
			p.setGeneral_sum_rating(original.get(i).getGeneral_sum_rating());
			p.setHistory(original.get(i).getHistory());
			p.setHouse_number(original.get(i).getHouse_number());
			p.setId(original.get(i).getId());
			p.setId_itemOnListView(i);
			p.setImgurl(original.get(i).getImgurl());
			p.setLat(original.get(i).getLat());
			p.setLng(original.get(i).getLng());
			p.setDistance(original.get(i).getDistance());
			p.setName(original.get(i).getName());
			p.setPhone_number(original.get(i).getPhone_number());
			p.place_category_obj = new PlaceCategoryModel();
			p.place_category_obj.setId(original.get(i).getPlace_category_obj().getId());
			p.place_category_obj.setPlace_category(original.get(i).getPlace_category_obj().getPlace_category());
			p.setProvince(original.get(i).getProvince());
			p.setSources(original.get(i).getSources());
			p.setStreet(original.get(i).getStreet());
			p.setWard(original.get(i).getWard());
			p.setWebsite(original.get(i).getWebsite());		
			temp.add(p);
		}
		Collections.sort(temp, new RatingComparator());
		for (int i = 0; i < temp.size(); i++)
			temp.get(i).setId_itemOnListView(i);
		return temp;
	}
	
	public static List<PlaceModel> SortListByRatingEstimate(List<PlaceModel> original) {
		List<PlaceModel> temp = new ArrayList<PlaceModel>();
		for (int i = 0; i < original.size(); i++) {
			PlaceModel p = new PlaceModel();
			p.setCity(original.get(i).getCity());
			p.setCountry(original.get(i).getCountry());
			p.setDetails(original.get(i).getDetails());
			p.setDistrict(original.get(i).getDistrict());
			p.setEmail(original.get(i).getEmail());
			p.setEstimate_rating(original.get(i).getEstimate_rating());
			p.setGeneral_count_rating(original.get(i).getGeneral_count_rating());
			p.setGeneral_rating(original.get(i).getGeneral_rating());
			p.setGeneral_sum_rating(original.get(i).getGeneral_sum_rating());
			p.setHistory(original.get(i).getHistory());
			p.setHouse_number(original.get(i).getHouse_number());
			p.setId(original.get(i).getId());
			p.setId_itemOnListView(i);
			p.setImgurl(original.get(i).getImgurl());
			p.setLat(original.get(i).getLat());
			p.setLng(original.get(i).getLng());
			p.setDistance(original.get(i).getDistance());
			p.setName(original.get(i).getName());
			p.setPhone_number(original.get(i).getPhone_number());
			p.place_category_obj = new PlaceCategoryModel();
			p.place_category_obj.setId(original.get(i).getPlace_category_obj().getId());
			p.place_category_obj.setPlace_category(original.get(i).getPlace_category_obj().getPlace_category());
			p.setProvince(original.get(i).getProvince());
			p.setSources(original.get(i).getSources());
			p.setStreet(original.get(i).getStreet());
			p.setWard(original.get(i).getWard());
			p.setWebsite(original.get(i).getWebsite());		
			temp.add(p);
		}
		Collections.sort(temp, new RatingEstimateComparator());
		Log.i("Rate", "Suggestions collections sort");
		for (int i = 0; i < temp.size(); i++)
			temp.get(i).setId_itemOnListView(i);
		return temp;
	}


	public static List<PlaceModel> SortListByType(List<PlaceModel> original,
			String place_cate) {
		List<PlaceModel> result = new ArrayList<PlaceModel>();
		int id_itemOnListView = 0;
		for (int i = 0; i < original.size(); i++) {
			PlaceModel p = new PlaceModel();
			p.setCity(original.get(i).getCity());
			p.setCountry(original.get(i).getCountry());
			p.setDetails(original.get(i).getDetails());
			p.setDistance(original.get(i).getDistance());
			p.setDistrict(original.get(i).getDistrict());
			p.setEmail(original.get(i).getEmail());
			p.setEstimate_rating(original.get(i).getEstimate_rating());
			p.setGeneral_count_rating(original.get(i).getGeneral_count_rating());
			p.setGeneral_rating(original.get(i).getGeneral_rating());
			p.setGeneral_sum_rating(original.get(i).getGeneral_sum_rating());
			p.setHistory(original.get(i).getHistory());
			p.setHouse_number(original.get(i).getHouse_number());
			p.setId(original.get(i).getId());
			p.setId_itemOnListView(original.get(i).getId_itemOnListView());
			p.setImgurl(original.get(i).getImgurl());
			p.setLat(original.get(i).getLat());
			p.setLng(original.get(i).getLng());
			p.setName(original.get(i).getName());
			p.setPhone_number(original.get(i).getPhone_number());
			p.place_category_obj = new PlaceCategoryModel();
			p.place_category_obj.setId(original.get(i).getPlace_category_obj().getId());
			p.place_category_obj.setPlace_category(original.get(i).getPlace_category_obj().getPlace_category());
			p.setProvince(original.get(i).getProvince());
			p.setSources(original.get(i).getSources());
			p.setStreet(original.get(i).getStreet());
			p.setWard(original.get(i).getWard());
			p.setWebsite(original.get(i).getWebsite());		
			if (p.place_category_obj.getPlace_category().compareTo(place_cate) == 0) {
				p.setId_itemOnListView(id_itemOnListView);
				result.add(p);
				id_itemOnListView++;
			}
		}
		return result;
	}
}

// Sort data by distance
class DistanceComparator implements Comparator<PlaceModel> {

	public int compare(PlaceModel object1, PlaceModel object2) {
		// TODO Auto-generated method stub
		double dis1 = object1.getDistance();
		double dis2 = object2.getDistance();
		if (dis1 > dis2)
			return 1;
		else {
			if (dis1 < dis2)
				return -1;
			else
				return 0;
		}
	}
}

// Sort data by Rating
class RatingComparator implements Comparator<PlaceModel> {

	public int compare(PlaceModel object1, PlaceModel object2) {
		// TODO Auto-generated method stub
		double rat1 = object1.getGeneral_rating();
		double rat2 = object2.getGeneral_rating();
		if (rat1 > rat2)
			return -1;
		else {
			if (rat1 < rat2)
				return 1;
			else
				return 0;
		}
	}
}

//Sort data by Rating
class RatingEstimateComparator implements Comparator<PlaceModel> {

	public int compare(PlaceModel object1, PlaceModel object2) {
		// TODO Auto-generated method stub
		double rat1 = object1.getEstimate_rating();
		double rat2 = object2.getEstimate_rating();
		if (rat1 > rat2)
			return -1;
		else {
			if (rat1 < rat2)
				return 1;
			else
				return 0;
		}
	}
}
