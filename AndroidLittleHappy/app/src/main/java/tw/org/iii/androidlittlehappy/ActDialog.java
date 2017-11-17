package tw.org.iii.androidlittlehappy;

import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;

/**
 * Created by user on 2017/11/17.
 */

public class ActDialog implements DialogInterface.OnCancelListener,View.OnClickListener {

Context context;
    Dialog mDialog;
    int mResId;
    ImageView imgGreator;
    Button btnInterest;
    TextView lblcreator , lblTitle,lblContent;


    ActDialog(Context context){
        this.context = context;
    }
    public ActDialog imageRes(int resId){
        this.mResId = resId;
        return this;
    }

    public ActDialog show(){
        mDialog = new Dialog(context, R.style.Theme_AppCompat_Dialog_Alert);
        mDialog.setContentView(R.layout.actactivitydetail);
        mDialog.getWindow().setLayout(WindowManager.LayoutParams.MATCH_PARENT,
                WindowManager.LayoutParams.MATCH_PARENT);

        // 點邊取消
        mDialog.setCancelable(true);
        mDialog.setCanceledOnTouchOutside(true);

       imgGreator = (ImageView)mDialog.findViewById(R.id.imgGreator);
        btnInterest = (Button)mDialog.findViewById(R.id.btnInterest);
        lblcreator = (TextView)mDialog.findViewById(R.id.lblCreator);
        lblContent = (TextView)mDialog.findViewById(R.id.lblContent);
        lblTitle = (TextView)mDialog.findViewById(R.id.lblTitle);




       btnInterest.setOnClickListener(this);
        mDialog.setOnCancelListener(this);
        mDialog.show();

        return this;
    }




    @Override
    public void onCancel(DialogInterface dialogInterface) {
        mDialog.dismiss();

    }

    @Override
    public void onClick(View view) {
        mDialog.dismiss();


    }
}
