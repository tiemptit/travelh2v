/**
 * 
 */
package uit.is.thesis.travel.utilities;

import java.util.Collections;
import java.util.Comparator;
import java.util.List;
import uit.is.thesis.travel.models.PlaceModel;

/**
 * @author LEHIEU
 *
 */
public class SortListUtil {
	
	public static List<PlaceModel> SortListByName(List<PlaceModel> original)
	{	
		Collections.sort(original, new NameComparator());
		for (int i =0; i< original.size();i++)
			original.get(i).setId_itemOnListView(i);
		return original;	
	}
	
	public static List<PlaceModel> SortListByDistance(List<PlaceModel> original)
	{
		Collections.sort(original, new DistanceComparator());
		for (int i =0; i< original.size();i++)
			original.get(i).setId_itemOnListView(i);
		return original;			
	}
	
	public static List<PlaceModel> SortListByRating(List<PlaceModel> original)
	{
		Collections.sort(original, new RatingComparator());
		for (int i =0; i< original.size();i++)
			original.get(i).setId_itemOnListView(i);
		return original;
	}

}

//Sort data by name
class NameComparator implements Comparator<PlaceModel> {

	public int compare(PlaceModel object1, PlaceModel object2) {
		// TODO Auto-generated method stub
		String name1 = object1.getName();
		String name2 = object2.getName();		
		return name1.compareTo(name2);
	}
}

// Sort data by distance
class DistanceComparator implements Comparator<PlaceModel> {

	public int compare(PlaceModel object1, PlaceModel object2) {
		// TODO Auto-generated method stub
		double dis1 = object1.getDistance();
		double dis2 = object2.getDistance();	
		if(dis1 > dis2)
			return 1;
		else {
			if(dis1 < dis2)
				return -1;
			else
				return 0;
		}		
	}	
}

//Sort data by Rating
class RatingComparator implements Comparator<PlaceModel> {

	public int compare(PlaceModel object1, PlaceModel object2) {
		// TODO Auto-generated method stub
		double rat1 = object1.getGeneral_rating();
		double rat2 = object2.getGeneral_rating();	
		if(rat1 > rat2)
			return -1;
		else {
			if(rat1 < rat2)
				return 1;
			else
				return 0;
		}		
	}	
}
