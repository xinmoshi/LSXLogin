using System;
using System.IO;
using UnityEngine;
using System.Collections;

public class UpFile : MonoBehaviour
{
	//持有三个状态面板的对象
	public GameObject upFileing;
	public GameObject successPanel;
	public GameObject failPanel;

	//定义访问JSP登录表单的post方式访问路径
	private string Url = "http://192.168.3.109:8080/MyUnityToJSPTest/ByteFileContentServlet.do";

	//点击上传按钮
	public void OnUpFileButtonClick()
	{
		//设置上传文件中面板为显示状态
		upFileing.SetActive(true);
		//上传本地文件
		Debug.Log("path="+Application.dataPath);
		StartCoroutine(UpFileToJSP(Url, Application.dataPath + "/midi.txt"));
	}



	//访问JSP服务器
	private IEnumerator UpFileToJSP(string url, string filePath)
	{
		WWWForm form=new WWWForm();
		form.AddBinaryData("midiFile",FileContent(filePath),"midi.txt");


		WWW upLoad=new WWW(url,form);
		yield return upLoad;


		//如果失败
		if (/*!string.IsNullOrEmpty(upLoad.error)||*/upLoad.text.Equals("false"))
		{
			//在控制台输出错误信息
			print(upLoad.error);
			//将失败面板显示  上传中不显示
			upFileing.SetActive(false);
			failPanel.SetActive(true);
		}
		else
		{
			//如果成功
			print("Finished Uploading Screenshot");
			//将成功面板显示  上传中不显示
			upFileing.SetActive(false);
			successPanel.SetActive(true);
		}


	}



	//将文件转换为字节流
	private byte[] FileContent(string filePath)
	{
		FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		try
		{
			byte[] buffur = new byte[fs.Length];
			fs.Read(buffur, 0, (int)fs.Length);

			return buffur;
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
			return null;
		}
		finally
		{
			if (fs != null)
			{

				//关闭资源  
				fs.Close();
			}
		}
	}  
}