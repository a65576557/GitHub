package tw.org.iii.androidlittlehappy;

import android.app.Activity;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.model.Marker;

/**
 * Created by user on 2017/11/6.
 */

public class CustomInfoWindowAdapter implements GoogleMap.InfoWindowAdapter {

    private Activity context;
    public CustomInfoWindowAdapter(Activity context){		        this.context = context;		    }

    @Override
    public View getInfoWindow(Marker marker) {
        return null;
    }

    @Override
    public View getInfoContents(Marker marker) {
       View view = context.getLayoutInflater().inflate(R.layout.activityinfo,null);


        TextView lblCreateActivity = (TextView)view.findViewById(R.id.lblCreateActivity);

        TextView lblMemberName = (TextView)view.findViewById(R.id.lblMemberName);
        ImageView imgType = (ImageView)view.findViewById(R.id.imgType);



     lblCreateActivity.setText(marker.getTitle());


        return  view;
    }



}
