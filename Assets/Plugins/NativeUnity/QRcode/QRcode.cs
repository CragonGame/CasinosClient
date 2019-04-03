using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using ZXing;
using ZXing.QrCode;

public class QRCode
{
    public QRCode()
    {
    }

    //---------------------------------------------------------------------
    // 创建二维码
    public Color32[] CreateQRCode(string encoding_text, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                CharacterSet = "UTF-8",
                Height = height,
                Width = width,
                Margin = 1// 设置二维码的边距,单位不是固定像素
            }
        };
        return writer.Write(encoding_text);
    }

    public void SaveTextureToFile( Texture2D texture, string file_name )
    {
        // 方法1
        //var bytes = texture.EncodeToPNG();
        //File.WriteAllBytes(file_name, bytes);

        // 方法2
        var bytes = texture.EncodeToPNG();
        using (var fs = File.Create(file_name))
        {
            var binary = new BinaryWriter(fs);
            binary.Write(bytes);
        }
    }
}
