using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
	//持有用户名和密码这两个输入框的对象
	public InputField Username;
	public InputField Password;

	//定义访问JSP登录表单的get方式访问路径
	private string Url = "http://192.168.3.109:8080/MyUnityToJSPTest/StringContentServlet.do?";

	//当按钮被点击
	public void LoginButtonOnClick()
	{
		//向服务器传递的参数
		string parameter = "";
		parameter += "UserName=" + Username.text + "&";
		parameter += "PassWord=" + Password.text;

		//开始传递
		StartCoroutine(login(Url + parameter));

	}

	//访问JSP服务器
	IEnumerator login(string path)
	{
		WWW www = new WWW(path);
		yield return www;
		//如果发生错误，打印这个错误
		if (www.error != null)
		{
			Debug.Log(www.error);
		}
		else
		{
			//如果服务器返回的是true
			if (www.text.Equals("true"))
			{
				//登陆成功
				print("Login Success!!!");
				Application.LoadLevel("UpLoadFile");
			}
			else
			{
				//否则登录失败
				print("Login Fail...");
			}
		}
	}


}