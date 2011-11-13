/**
 * 
 */
package uit.is.thesis.travel.models;
import java.io.Serializable;

/**
 * @author LEHIEU
 *
 */
@SuppressWarnings("serial")
public class PlaceCategoryModel implements Serializable{

	public int id;
	public String place_category;
	
    /**
	 * @return the id
	 */
	public int getId() {
		return id;
	}
	/**
	 * @param id the id to set
	 */
	public void setId(int id) {
		this.id = id;
	}
	/**
	 * @return the place_category
	 */
	public String getPlace_category() {
		return place_category;
	}
	/**
	 * @param place_category the place_category to set
	 */
	public void setPlace_category(String place_category) {
		this.place_category = place_category;
	}
	
}
