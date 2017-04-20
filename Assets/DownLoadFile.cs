using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

public class DownLoadFile : MonoBehaviour
{
	//定义访问JSP登录表单的get方式访问路径
	private string url = "http://192.168.3.109:8080/MyUnityToJSPTest/DownloadMidi.do?Download=Midi";

	//当按钮点击
	public void OnPlayButtonClick()
	{
		//向服务器传递指令
		StartCoroutine(UpFileToJSP(url));
	}

	//访问JSP服务器
	private IEnumerator UpFileToJSP(string url)
	{
		WWW downLoad = new WWW(url);
		yield return downLoad;
		//如果失败
		if (!string.IsNullOrEmpty(downLoad.error) || downLoad.text.Equals("false"))
		{
			//在控制台输出错误信息
			print(downLoad.error);
		}
		else
		{
			//如果成功
			//定义一个字节数组保存文件
			byte[] myByte = downLoad.bytes;
			//新建一个文件接收字节流
			FileStream fs = new FileStream(Application.persistentDataPath + "/midi.wav",FileMode.Create, FileAccess.Write, FileShare.None);
			//开始转换
			fs.Write(myByte,0,myByte.Length);
			//刷新流
			fs.Flush();
			//关闭流
			fs.Close();
			//子啊控制台输出完成信息
			print("Finished Uploading Screenshot");
		}

		StartCoroutine(LoadXML());
	}

	IEnumerator LoadXML() {
		string sPath= Application.persistentDataPath + "/midi.wav";
		sPath = "file://" + sPath;

		Debug.Log("sPath===="+sPath);
		WWW www = new WWW(sPath);
		yield return www;
		//_result = www.text;

		AudioClip source = www.GetAudioClip(true,true);
		//Debug.Log(www.text);
		GetComponent<AudioSource>().clip = source;
		GetComponent<AudioSource>().Play();
	}
}