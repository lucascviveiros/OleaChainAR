using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CopyCache : MonoBehaviour
{
    //[SerializeField]
    //private string FILE_NAME;
    public Cache mapCache;
    public Text tDebug;
    private string rootPath; //root path video folder in oculus gallery
    private string path; //actual path with root path and video's name
    private string[] videoList = { "boreal_texto_fixed_NOAUDIO.mp4", "Douro360_h265_8K_OculusQuest2_v3.mp4" };

    HashSet<string> allowedExtesions = new HashSet<string>() { ".db" };

    NetworkStream myNetworkStream;
    NetworkStream NetworkStream_ASync_Send_Receive;

    IAsyncResult ar;



    byte[] myReadBuffer = new byte[1024];


    /* VER DEPOIS
     * byte[] buffer = new byte[1024];
    int numberOfBytesRead = 0;

    FileStream fs = new FileStream(@"C:\file.png", FileMode.Create, FileAccess.Write);
    do
    {
        numberOfBytesRead = serverStream.Read(buffer, 0, buffer.Length); //Read from network stream
        fs.Write(buffer, 0, numberOfBytesRead);
    } while (serverStream.DataAvailable);
    fs.Close()
     
     */

    static void CopySomething(string cachePath1, string cachePath2)
    {
        //FileUtil.CopyFileOrDirectory(cachePath, targetCachePath);

        Debug.Log("CachePath 1 :: " + cachePath1);

        Debug.Log("CachePath 2 :: " + cachePath2);
#if UNITY_EDITOR
        FileUtil.CopyFileOrDirectory(cachePath1, cachePath2);
        //FileUtil.ReplaceFile(cachePath1, cachePath2); 
#endif
    }
    // Example of EndRead, DataAvailable and BeginRead.
    public static void myReadCallBack(IAsyncResult ar)
    {

        NetworkStream myNetworkStream = (NetworkStream)ar.AsyncState;
        byte[] myReadBuffer = new byte[1024];
        String myCompleteMessage = "";
        int numberOfBytesRead;

        numberOfBytesRead = myNetworkStream.EndRead(ar);
        myCompleteMessage =
            String.Concat(myCompleteMessage, Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));

        // message received may be larger than buffer size so loop through until you have it all.
        while (myNetworkStream.DataAvailable)
        {

           // myNetworkStream.BeginRead(myReadBuffer, 0, myReadBuffer.Length,
                                               //        new AsyncCallback(NetworkStream_ASync_Send_Receive.myReadCallBack),
             //                                          myNetworkStream);
        }

        // Print out the received message to the console.
        Console.WriteLine("You received the following message : " +
                                    myCompleteMessage);
    }

/*    public static void myReadCallBack(IAsyncResult ar)
    {
        byte[] myReadBuffer = new byte[1024];

        myNetworkStream = (NetworkStream)ar.AsyncState;
        String myCompleteMessage = "";
        int numberOfBytesRead;

        numberOfBytesRead = myNetworkStream.EndRead(ar);
        myCompleteMessage =
            String.Concat(myCompleteMessage, Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));

        // message received may be larger than buffer size so loop through until you have it all.
        while (myNetworkStream.DataAvailable)
        {

            myNetworkStream.BeginRead(myReadBuffer, 0, myReadBuffer.Length,
                                                       new AsyncCallback(NetworkStream_ASync_Send_Receive.myReadCallBack),
                                                       myNetworkStream);
        }

        // Print out the received message to the console.
        Console.WriteLine("You received the following message : " +
                                    myCompleteMessage);
    }*/

    public static void Leia(FileStream inputStream)
    {
        if (inputStream.CanRead)
        {

            //byte[] myReadBuffer = new byte[1024];
            //inputStream.BeginRead(myReadBuffer, 0, myReadBuffer.Length,
            //   new AsyncCallback(NetworkStream_ASync_Send_Receive.myReadCallBack),
            //inputStream);

            //allDone.WaitOne();
        }
    } 

    private void CopySomething(string cachePath1, string cachePath2, Cache cache)
    {
        //FileUtil.CopyFileOrDirectory(cachePath, targetCachePath);

        Debug.Log("CachePath 1 :: " + cachePath1);

        Debug.Log("CachePath 2 :: " + cachePath2);

        File.Copy(cachePath1, cachePath2);



        //     FileStream stream;
        // stream.BeginRead(myReadBuffer, 0, myReadBuffer.Length,
        // new AsyncCallback(NetworkStream_ASync_Send_Receive.myReadCallBack),
        //   inputStream);

        //stream.BeginRead();
#if UNITY_EDITOR
        FileUtil.CopyFileOrDirectory(cachePath1, cachePath2);
#endif
        //FileUtil.ReplaceFile(cachePath1, cachePath2); 
    }

    void Awake()
    {
        string pathSource = StreamingCache();
        Debug.Log("STREAMING:: " + pathSource + " \n");

        //string pathDestiny = @"storage/emulated/0/Android/data/com.DefaultCompany.MapBoxNav/files/cache";
        //string pathDestiny = @"C:\Users\vivei\AppData\LocalLow\DefaultCompany\MapboxNav\cache_teste\"; //Barra Invertida
        string pathDestiny = @"C:/Users/vivei/AppData/LocalLow/DefaultCompany/MapboxNav/cache_teste/"; //Barra Direta

        mapCache = GetComponent<Cache>();
        mapCache = Caching.AddCache(pathSource);


        Debug.Log("Source  :: " + pathSource);

        Debug.Log("Destiny  :: " + pathDestiny);

        CopySomething(pathSource, pathDestiny, mapCache);
    }

    public string StreamingCache()
    {
        //string fileName = videoList[clipIndex];
        string cacheName = "cache.db";
        rootPath = Application.persistentDataPath;
        Debug.Log("rootPath: " + rootPath.ToString());

        rootPath = rootPath + "/cache/";
        path = Path.Combine(rootPath, cacheName);

        Debug.Log("rootPath: " + path.ToString());
        //tDebug.text = path.ToString();
        //\Divisão interna de armazenamento\Android\data\com.DefaultCompany.MapboxNav\files\cache
        //DESTINO storage/emulated/0/Android/data/com.DefaultCompany.MapBoxNav/files/cache
        return path;
        //videoPlayer.url = path;
        //videoPlayer.Play();
    }

    /*
private void Search()
{
    string fileName = "";
//#if UNITY_IPHONE
string fileNameBase = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'));
fileName = fileNameBase.Substring(0, fileNameBase.LastIndexOf('/')) + "/Documents/" + FILE_NAME;
//#elif UNITY_ANDROID
    fileName = Application.persistentDataPath + "/" + FILE_NAME;
//#else
fileName = Application.dataPath + "/" + FILE_NAME;
//#endif


    //fileWriter = File.CreateText(fileName); //abre arquivo
    //fileWriter.WriteLine("Hello world"); //escreve linha
    //fileWriter.Close(); //fecha arquivo
}*/

}
