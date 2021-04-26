using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

using System.Threading;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;

public class QrRender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RawImage>().texture = generateQR("Henlo world");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static Color32[] Encode(string textForEncoding,
  int width, int height)
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
        return writer.Write(textForEncoding);
    }

    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }
}
