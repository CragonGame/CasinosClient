/**
 * DisplayUtils.java
 * <p/>
 * Created by xuanzhui on 2015/9/10.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.demo.util;

import android.app.Activity;
import android.view.View;
import android.widget.LinearLayout;

import cn.beecloud.demo.R;

public class DisplayUtils {

    public static void initBack(final Activity activity) {
        LinearLayout clickArea = (LinearLayout) activity.findViewById(R.id.backClickArea);

        clickArea.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    activity.finish();
                }
            });
    }
}
