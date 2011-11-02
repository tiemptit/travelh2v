/**
 * 
 *//*
package uit.is.thesis.travel.utilities;

import java.util.List;
import java.util.Vector;

import com.tma.ttc.androidK13.activities.DetailInfoActivity.RowData;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;
import uit.is.thesis.travel.activities.R;



*//**
 * @author LEHIEU
 *
 *//*

class ViewHolderOfArrayAdapter {
	private View mRow;
	private TextView title = null;
	private TextView info = null;
	private ImageView i11 = null;

	public ViewHolderOfArrayAdapter(View row) {
		mRow = row;
	}

	public TextView getTitle() {
		if (null == title) {
			title = (TextView) mRow.findViewById(R.id.txtTitle);
		}
		return title;
	}

	public TextView getInfo() {
		if (null == info) {
			info = (TextView) mRow.findViewById(R.id.txtInfo);
		}
		return info;
	}

	public ImageView getImage() {
		if (null == i11) {
			i11 = (ImageView) mRow.findViewById(R.id.imgIcon);
		}
		return i11;
	}
}

public class CustomArrayAdapter extends ArrayAdapter<RowData> {
	private LayoutInflater mInflater;
	private Vector<RowData> rowData;
	RowData rd;

	public CustomArrayAdapter(Context context, int resource,
			int textViewResourceId, List<RowData> objects) {
		super(context, resource, textViewResourceId, objects);
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		ViewHolder holder = null;
		TextView title = null;
		TextView detail = null;
		ImageView i11 = null;
		RowData rowData = getItem(position);
		if (null == convertView) {
			convertView = mInflater.inflate(R.layout.row_details, null);
			holder = new ViewHolder(convertView);
			convertView.setTag(holder);
		}
		holder = (ViewHolder) convertView.getTag();
		title = holder.getTitle();
		title.setText(rowData.mTitle);
		detail = holder.getInfo();
		detail.setText(rowData.mDetail);
		i11 = holder.getImage();
		i11.setImageResource(rowData.mIconResource);
		return convertView;
	}
}

*//** Class RowData *//*
class RowData {

	protected String mTitle;
	protected String mDetail;
	protected int mIconResource;

	RowData(String title, String detail, int iconResource) {
		mTitle = title;
		mDetail = detail;
		mIconResource = iconResource;
	}

	public String getmTitle() {
		return mTitle;
	}

	public String getmDetail() {
		return mDetail;
	}
	
	@Override
	public String toString() {
		return mTitle + " " + mDetail + " " + mIconResource;
	}

}


*/