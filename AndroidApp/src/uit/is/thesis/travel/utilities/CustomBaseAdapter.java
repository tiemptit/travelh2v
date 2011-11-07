/**
 * 
 */
package uit.is.thesis.travel.utilities;

import java.util.ArrayList;
import java.util.HashMap;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.RatingBar;
import android.widget.TextView;
import uit.is.thesis.travel.activities.R;

/**
 * @author LEHIEU
 *
 */
class ViewHolderOfBaseAdapter {
    TextView txtViewName;
    TextView txtViewAddress;
    TextView txtViewDistance;
    RatingBar ratingBar;
}

public class CustomBaseAdapter extends BaseAdapter{
	private ArrayList<HashMap<String, Object>> Places;
	private LayoutInflater mInflater;  
	
	public CustomBaseAdapter(ArrayList<HashMap<String, Object>> places, Context context){
			Places = places;
			mInflater = LayoutInflater.from(context);
	}
		         
    @Override
    public int getCount() {
        // TODO Auto-generated method stub
        return Places.size();
    }
	         
    @Override
    public Object getItem(int position) {
        // TODO Auto-generated method stub
        return Places.get(position);
    }
		        
    @Override
    public long getItemId(int position) {
        // TODO Auto-generated method stub
        return position;
    }	 
		       
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        // TODO Auto-generated method stub
        // A ViewHolder keeps references to children views to avoid unneccessary calls
        // to findViewById() on each row.
    	try{
    	ViewHolderOfBaseAdapter holder;
         
        // When convertView is not null, we can reuse it directly, there is no need
        // to reinflate it. We only inflate a new View when the convertView supplied
        // by ListView is null
         if (convertView == null) {
             convertView = mInflater.inflate(R.layout.row_place, null);
             // Creates a ViewHolder and store references to the two children views
             // we want to bind data to.
             holder = new ViewHolderOfBaseAdapter();
             holder.txtViewName = (TextView) convertView.findViewById(R.id.txtViewName);
             holder.txtViewAddress = (TextView) convertView.findViewById(R.id.txtViewAddress);
             holder.txtViewDistance = (TextView) convertView.findViewById(R.id.txtViewDistance);
             holder.ratingBar = (RatingBar)convertView.findViewById(R.id.ratingBar);
             convertView.setTag(holder);             
         }else {
             // Get the ViewHolder back to get fast access to the TextView and RatingBar.
             holder = (ViewHolderOfBaseAdapter) convertView.getTag();
         }
            // Bind the data with the holder.
            holder.txtViewName.setText(Places.get(position).get("viewName").toString());          
            holder.txtViewAddress.setText(Places.get(position).get("viewAddress").toString());        
            holder.txtViewDistance.setText(Places.get(position).get("viewDistance").toString());
            holder.ratingBar.setRating(Float.parseFloat(Places.get(position).get("ratingBar").toString()));
    	}catch(Exception e){
    	}
            return convertView;
    }	 
}	       
		         

