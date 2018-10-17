package com.getui.demo;

import android.os.AsyncTask;

import org.json.simple.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.security.NoSuchAlgorithmException;
import java.util.Map;
import java.util.Map.Entry;
import java.util.SortedMap;
import java.util.TreeMap;

public class GetuiSdkHttpPost {

    private static final String SERVICEURL = "http://sdk.open.api.igexin.com/service";
    private static final int CONNECTION_TIMEOUT_INT = 10000;
    private static final int READ_TIMEOUT_INT = 10000;

    public static void httpPost(final Map<String, Object> map) {
        new AsyncTask<Void, Integer, Void>() {

            @Override
            protected Void doInBackground(Void... params) {
                String param = JSONObject.toJSONString(map);

                if (param != null) {
                    BufferedReader bufferReader = null;
                    HttpURLConnection urlConn = null;

                    try {
                        URL url = new URL(SERVICEURL);
                        urlConn = (HttpURLConnection) url.openConnection();
                        urlConn.setDoInput(true);
                        urlConn.setDoOutput(true);
                        urlConn.setRequestMethod("POST");
                        urlConn.setUseCaches(false);
                        urlConn.setRequestProperty("Charset", "utf-8");
                        urlConn.setConnectTimeout(CONNECTION_TIMEOUT_INT);
                        urlConn.setReadTimeout(READ_TIMEOUT_INT);

                        urlConn.connect();

                        DataOutputStream dop = new DataOutputStream(urlConn.getOutputStream());
                        dop.write(param.getBytes("utf-8"));
                        dop.flush();
                        dop.close();

                        bufferReader = new BufferedReader(new InputStreamReader(urlConn.getInputStream()));
                        String result = "";
                        String readLine;

                        while ((readLine = bufferReader.readLine()) != null) {
                            result += readLine;
                        }
                        System.out.println("result： " + result);
                    } catch (Exception e) {
                        e.printStackTrace();
                    } finally {
                        if (urlConn != null) {
                            urlConn.disconnect();
                        }

                        if (bufferReader != null) {
                            try {
                                bufferReader.close();
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }
                } else {
                    System.out.println("param is null");
                }
                return null;
            }
        }.execute();
    }

    public static String makeSign(String masterSecret, Map<String, Object> params) throws IllegalArgumentException {
        if (masterSecret == null || params == null) {
            throw new IllegalArgumentException("masterSecret and params can not be null.");
        }

        if (!(params instanceof SortedMap)) {
            params = new TreeMap<String, Object>(params);
        }

        StringBuilder input = new StringBuilder(masterSecret);
        for (Entry<String, Object> entry : params.entrySet()) {
            Object value = entry.getValue();
            if (value instanceof String || value instanceof Integer || value instanceof Long) {
                input.append(entry.getKey());
                input.append(entry.getValue());
            }
        }

        return getMD5Str(input.toString());
    }

    private static String getMD5Str(String sourceStr) {
        byte[] source = sourceStr.getBytes();
        char hexDigits[] = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};
        java.security.MessageDigest md = null;

        try {
            md = java.security.MessageDigest.getInstance("MD5");
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }

        if (md == null) {
            return null;
        }

        md.update(source);
        byte tmp[] = md.digest();
        char str[] = new char[16 * 2];
        int k = 0;
        for (int i = 0; i < 16; i++) {
            byte byte0 = tmp[i];
            str[k++] = hexDigits[byte0 >>> 4 & 0xf];
            str[k++] = hexDigits[byte0 & 0xf];
        }

        return new String(str);
    }

}
