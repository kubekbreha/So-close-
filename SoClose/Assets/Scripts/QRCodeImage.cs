//using System;
//using System.IO;
//using QRCoder;
//using UnityEngine;
//using UnityEngine.UI;

//public class QRCodeImage : MonoBehaviour
//{
//    public string path;

//    private Image image;

//    // Use this for initialization
//    void Start()
//    {
//        image = GetComponent<Image>();

//        var texture = GenerateQRCode("http://game.openlab.kpi.fei.tuke.sk/?id=" + GameController.Instance.GameId);
//        if (texture == null)
//            texture = LoadTexture(path);
//        var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 100f);
//        image.sprite = sprite;
//    }

//    //Load texture from file
//    private Texture2D LoadTexture(string filePath)
//    {
//        if (File.Exists(filePath))
//        {
//            var fileData = File.ReadAllBytes(filePath);
//            var tex2D = new Texture2D(2, 2);
//            if (tex2D.LoadImage(fileData))
//                return tex2D;
//        }

//        return null;
//    }

//    //generate qr code
//    private Texture2D GenerateQRCode(string content)
//    {
//        var qrGenerator = new QRCodeGenerator();
//        var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
//        var qrCode = new QRCoder.UnityQRCode(qrCodeData);
//        return qrCode.GetGraphic(20);
//    }
//}