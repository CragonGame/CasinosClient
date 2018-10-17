/**
 * BillUtils.java
 * <p/>
 * Created by xuanzhui on 2015/10/31.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.demo.util;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class BillUtils {
    static SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyyMMddHHmmssSSS", Locale.CHINA);

    public static String genBillNum() {
        return simpleDateFormat.format(new Date());
    }
}
