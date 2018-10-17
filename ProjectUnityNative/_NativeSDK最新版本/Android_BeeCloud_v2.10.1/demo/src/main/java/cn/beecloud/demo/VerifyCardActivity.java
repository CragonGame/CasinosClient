package cn.beecloud.demo;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import cn.beecloud.BCValidationUtil;
import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.demo.util.DisplayUtils;
import cn.beecloud.entity.BCCardVerifyResult;

public class VerifyCardActivity extends Activity {

    EditText idNameView;
    EditText idNumView;

    String toastMsg;

    private Handler mHandler = new Handler(new Handler.Callback() {
        @Override
        public boolean handleMessage(Message msg) {
            Toast.makeText(VerifyCardActivity.this, toastMsg, Toast.LENGTH_SHORT).show();
            return true;
        }
    });

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_verify_card);
        DisplayUtils.initBack(this);
        idNameView = (EditText) findViewById(R.id.id_name_view);
        idNumView = (EditText) findViewById(R.id.id_no_view);
    }

    public void verifyCard(View view) {
        String idName = idNameView.getText().toString();
        String idNum = idNumView.getText().toString();

        BCValidationUtil.verifyCardFactors(idName, idNum, new BCCallback() {
            @Override
            public void done(BCResult result) {
                Message msg = mHandler.obtainMessage();

                BCCardVerifyResult verifyResult = (BCCardVerifyResult) result;
                if (verifyResult.getAuthResult())
                    toastMsg = "校验匹配";
                else {
                    toastMsg = verifyResult.getAuthMsg() + " | " + verifyResult.getErrDetail();
                }

                mHandler.sendMessage(msg);
            }
        });
    }
}
