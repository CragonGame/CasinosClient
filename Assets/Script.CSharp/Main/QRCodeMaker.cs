using System;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class QRCodeMaker
{
    //---------------------------------------------------------------------
    public static Color32[] createQRCode(string encoding_text, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(encoding_text);
    }
}