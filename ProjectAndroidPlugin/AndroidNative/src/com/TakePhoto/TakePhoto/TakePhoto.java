package com.TakePhoto.TakePhoto;

import java.io.ByteArrayOutputStream;
import java.io.FileNotFoundException;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Bitmap.Config;
import android.net.Uri;
import android.util.Base64;
import android.util.Log;

import com.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge;

public class TakePhoto {
	// -------------------------------------------------------------------------
	public static TakePhoto mTakePhoto;
	private static AndroidToUnityMsgBridge mAndroidToUnityMsgBridge;	
	private Activity mUnityActivity;
	public static String mResultReceiver = "";
	public int mPhotoWidth = 150;
	public int mPhotoHeight = 150;
	public static String mSuccessMethodName = "";
	public static String mFailMethodName = "";
	public String mPhotoFinalPath="";
	public String mPhotoName = "";

	// public int QUALITY = 60;

	// -------------------------------------------------------------------------
	public static TakePhoto Instantce(int photo_width, int photo_height,
			String success_methodname, String fail_methodname,
			String photo_name,String photo_final_path, String resultreceiver_name) {
		mResultReceiver = resultreceiver_name;
		mSuccessMethodName = success_methodname;
		mFailMethodName = fail_methodname;		
		if (mTakePhoto == null) {
			mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge
					.Instance();
			mTakePhoto = new TakePhoto(photo_width, photo_height,
					success_methodname, fail_methodname, photo_name,
					photo_final_path,resultreceiver_name);
		}

		return mTakePhoto;
	}

	// -------------------------------------------------------------------------
	private TakePhoto(int photo_width, int photo_height,
			String success_methodname, String fail_methodname,
			String photo_name, String photo_final_path,String pay_resultreceiver) {
		this.mPhotoWidth = photo_width;
		this.mPhotoHeight = photo_height;
		this.mPhotoName = photo_name;
		this.mPhotoFinalPath = photo_final_path;
		mUnityActivity = mAndroidToUnityMsgBridge.getActivity();
	}

	// -------------------------------------------------------------------------
	public void takeExistPhoto() {	
		_startTakePhotoActivity(_ERESULT.getPicFromPicture);		
	}

	// -------------------------------------------------------------------------
	public void takeNewPhoto() {	
		_startTakePhotoActivity(_ERESULT.getPicFromCamera);
	}

	// -------------------------------------------------------------------------
	public static void sendToUnity(Boolean is_success, String info) {		
		if (mAndroidToUnityMsgBridge == null) {			
			mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge
					.Instance();
		}
		if (is_success) {
			Log.e("TakePhoto", "sendToUnity::is_success::" + info+
					"  mSuccessMethodName::"+mSuccessMethodName+"  mResultReceiver:"+mResultReceiver);
			mAndroidToUnityMsgBridge.sendMsgToUnity(mResultReceiver,mSuccessMethodName, info);
		} else {
			Log.e("TakePhoto", "sendToUnity::is_fail::"+info+"  mFailMethodName::"+mFailMethodName
					+"  mResultReceiver:"+mResultReceiver);
			mAndroidToUnityMsgBridge.sendMsgToUnity(mResultReceiver,mFailMethodName, info);
		}
	}

	// -------------------------------------------------------------------------
	public static void sendToUnityPic(Uri pic_uri) {		
		getPicStringThread(pic_uri);
		// _decodeUriAsBitmap(pic_uri);
	}

	// -------------------------------------------------------------------------
	private static void _decodeUriAsBitmap(Uri uri) {

		Bitmap bitmap = null;

		try {
			Log.e("TakePhotoActivity", "_decodeUriAsBitmap");
			BitmapFactory.Options options = new BitmapFactory.Options();
			options.inPreferredConfig = Config.RGB_565;
			bitmap = BitmapFactory.decodeStream(
					mAndroidToUnityMsgBridge.mUnityPlayerActivity
							.getContentResolver().openInputStream(uri), null,
					options);
		} catch (Exception e) {
			e.printStackTrace();
			return;
		}

		_picToString(bitmap);
	}

	// -------------------------------------------------------------------------
	private static void _picToString(Bitmap photo) {
		String icon = "";
		try {
			// File file = new File(photo.);

			// 创建一个字节数组输出流,流的大小为size
			ByteArrayOutputStream baos = new ByteArrayOutputStream();
			// 设置位图的压缩格式，质量为100%，并放入字节数组输出流中
			photo.compress(Bitmap.CompressFormat.PNG, 100, baos);
			// 将字节数组输出流转化为字节数组byte[]
			byte[] imagedata = baos.toByteArray();
			Log.e("TakePhotoActivity", "imagedata：：Length::" + imagedata.length);
			icon = Base64.encodeToString(imagedata, Base64.DEFAULT);
		} catch (Exception e) {
			e.printStackTrace();
			return;
		}

		sendToUnity(true, icon);
		Log.e("TakePhotoActivity", "icon:: " + icon);
	}

	// -------------------------------------------------------------------------
	private static void getPicStringThread(final Uri image_uri) {
		(new Thread(new Runnable() {
			public void run() {
				_decodeUriAsBitmap(image_uri);
			}
		})).start();
	}
	
	// -------------------------------------------------------------------------
	void _startTakePhotoActivity(_ERESULT takephoto_type)
	{
		Intent intent = new Intent(mUnityActivity, TakePhotoActivity.class);
		intent.putExtra("PhotoWidth", mPhotoWidth);
		intent.putExtra("PhotoHeight", mPhotoHeight);
		intent.putExtra("PhotoName", mPhotoName);
		intent.putExtra("PhotoFinalPath", mPhotoFinalPath);
		intent.putExtra("TakePhotoType", takephoto_type.toString());
		mUnityActivity.startActivity(intent);
	}

	// ----------------------------------------------------------------------
	// 返回消息类型
	public enum _ERESULT {
		getPicFailed, getPicFromCamera, getPicFromPicture, getPicFromCameraSuccess, getPicFromPictureSuccess
	}
}
