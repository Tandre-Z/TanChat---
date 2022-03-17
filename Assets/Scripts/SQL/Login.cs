using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
   public  SQLiteController sqliteController;
    public Image BgImg;

    public GameObject loginPanel;
    public InputField userName;
    public InputField password;

    public GameObject registPanel;
    public InputField userNameRegist;
    public InputField passwordRegist;

    public GameObject mainPanel;
    public Button loginBtn;
    public Button registBtn;
    public Text errorText;

    public GameObject personPanel;

    Vector3 loginPos;
    Vector3 registPos;
    Vector3 personPos;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor ||
                        Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        mainPanel.SetActive(true);
        loginPanel.SetActive(false);
        registPanel.SetActive(false);
        personPanel.SetActive(false);

        errorText.gameObject.SetActive(false);

        sqliteController.OpenSqlite();
        sqliteController.CreatTable();

        loginPos=loginPanel.transform.position;
        registPos=registPanel.transform.position;
        personPos=personPanel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LogBtn()//登录
    {
        if( sqliteController.Select() !=null )
        {
            var users=sqliteController.Select();
            foreach (var item in users)
            {
                if(item.UserName==userName.text)
                {
                    if (item.Password!=password.text)
                    {
                        errorText.gameObject.SetActive(true);
                        errorText.text = "密码错误，请检查大小写或再次核对后重新输入！";
                        StartCoroutine(CloseErrorText());
                    }
                    else
                    {
                        StartCoroutine(LogSuccess());
                    }
                    return;
                }
            }
            errorText.gameObject.SetActive(true);
            errorText.text = "该账号不存在，请点击注册！";
            StartCoroutine(CloseErrorText());

        }
    }

    public void RegistBtn()//注册
    {
        if (sqliteController.Select() != null)
        {
            var users = sqliteController.Select();
            foreach (var item in users)
            {
                if (item.UserName == userNameRegist.text)//用户名已存在
                {
                    if (item.Password != passwordRegist.text)//密码不同
                    {
                        errorText.gameObject.SetActive(true);
                        errorText.text = "该用户名已存在，请重新注册其它用户名";
                        StartCoroutine(CloseErrorText());
                    }
                    else
                    {
                        errorText.gameObject.SetActive(true);//密码相同
                        errorText.text = "该用户名已存在，请直接登录";
                        StartCoroutine(CloseErrorText());
                    }
                    return;
                }
            }
            User user=new User();
            user.UserName= userNameRegist.text;
            user.Password= passwordRegist.text;
            sqliteController.Insert(user);
            errorText.gameObject.SetActive(true);
            errorText.text = "注册成功！";
            Open_Close_RegistBtn();
            StartCoroutine(CloseErrorText());
        }
        
    }

    public void Open_Close_RegistBtn()//开关注册面板
    {
        //获取与按钮之间的距离并分段200
        Vector3 destion = new Vector3((registPos.x - registBtn.transform.position.x) / 10,
            (registPos.y - registBtn.transform.position.y) / 10,
            (registPos.z - registBtn.transform.position.z) / 10);

        //停止背景渐变的协程
        StopCoroutine(BGColorGradient(true | false));

        //背景渐变效果和界面弹出
        if (!registPanel.activeSelf)
        {
            //打开界面时缩小并将其位置放置在按钮上
            registPanel.transform.position = registBtn.transform.position;
            registPanel.transform.localScale = Vector3.zero;

            StartCoroutine(BGColorGradient(true));
            StartCoroutine(PanelBonuce(registPanel, destion, true));
            //弹出时激活面板
            registPanel.SetActive(!registPanel.activeSelf);
        }
        else
        {
            StartCoroutine(BGColorGradient(false));
            StartCoroutine(PanelBonuce(registPanel, destion, false));
        }
        mainPanel.SetActive(!mainPanel.activeSelf);
        //loginPanel.SetActive(!loginPanel.activeSelf);
    }

    public void Open_Close_LogBtn()//开关登录面板
    {
        //获取与按钮之间的距离并分段200
        Vector3 destion = new Vector3((loginPos.x - loginBtn.transform.position.x) / 10,
            (loginPos.y - loginBtn.transform.position.y) / 10,
            (loginPos.z - loginBtn.transform.position.z) / 10);

        //停止背景渐变的协程
        StopCoroutine(BGColorGradient(true | false));

        //背景渐变效果和界面弹出
        if (!loginPanel.activeSelf)
        {
            //打开界面时缩小并将其位置放置在按钮上
            loginPanel.transform.position = loginBtn.transform.position;
            loginPanel.transform.localScale = Vector3.zero;

            StartCoroutine(BGColorGradient(true));
            StartCoroutine(PanelBonuce(loginPanel,destion, true));
            //弹出时激活面板
            loginPanel.SetActive(!loginPanel.activeSelf);
        }
        else
        {
            StartCoroutine(BGColorGradient(false));
            StartCoroutine(PanelBonuce(loginPanel,destion, false));
        }
       
        mainPanel.SetActive(!mainPanel.activeSelf);
        // registPanel.SetActive(!registPanel.activeSelf);


    }
    
    public void Open_Close_PerBtn()//开关个人信息面板
    {
        //获取与按钮之间的距离并分段200
        Vector3 destion = Vector3.zero;

        //停止背景渐变的协程
        StopCoroutine(BGColorGradient(true | false));

        //背景渐变效果和界面弹出
        if (!personPanel.activeSelf)
        {
            //打开界面时缩小并将其位置放置在按钮上
           //personPanel.transform.position = loginBtn.transform.position;
            personPanel.transform.localScale = Vector3.zero;

            StartCoroutine(BGColorGradient(false));
            StartCoroutine(PanelBonuce(personPanel, destion, true));
            //弹出时激活面板
            personPanel.SetActive(!personPanel.activeSelf);
        }
        else
        {
            Open_Close_LogBtn();
            StartCoroutine(BGColorGradient(true));
            StartCoroutine(PanelBonuce(personPanel, destion, false));
        }
       
        mainPanel.SetActive(false);
        // registPanel.SetActive(!registPanel.activeSelf);


    }

    

    //面板弹出的协程
    public IEnumerator PanelBonuce(GameObject gameObject, Vector3 destion,bool open)
    {
        if (open)
        {
            if (gameObject.transform.localScale.x < 1f)
            {
                
                    gameObject.transform.position += destion;
                    gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

                yield return new WaitForSeconds(0.005f);
                StartCoroutine(PanelBonuce(gameObject, destion,open));
            }
        }
        else
        {
            if (gameObject.transform.localScale.x > 0f)
            {
                gameObject.transform.position -= destion;
                gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

                yield return new WaitForSeconds(0.005f);
                StartCoroutine(PanelBonuce(gameObject, destion, open));
            }
            else
            {
                gameObject.SetActive(!gameObject.activeSelf);//开关控制
            }
                
        }
        
        yield return null;
    }

    public IEnumerator BGColorGradient(bool open)//背景颜色渐变效果
    {
        if (open)
        {
            if (BgImg.color.r >= 0.3f)
            {
                BgImg.color -= new Color(0.05f, 0.05f, 0.05f, 0);
                yield return new WaitForSeconds(0.005f);
                StartCoroutine(BGColorGradient(open));
            }
        }
        else
        {
            if (BgImg.color.r <= 0.8f)
            {
                BgImg.color += new Color(0.05f, 0.05f, 0.05f, 0);
                yield return new WaitForSeconds(0.005f);
                StartCoroutine(BGColorGradient(open));
            }

        }
        yield return null;
    }

    public IEnumerator LogSuccess()
    {
        errorText.gameObject.SetActive(true);
        errorText.text = "登陆成功！";
        //loginPanel.SetActive(!loginPanel.activeSelf);
        Open_Close_LogBtn();
        BgImg.color = new Color(0.2f, 0.2f, 0.2f);
        StartCoroutine(CloseErrorText());

        // yield return new WaitForSeconds(2);

        Open_Close_PerBtn();//开关个人信息面板
        
        yield return null;
        //personPanel.SetActive(!personPanel.activeSelf);
        //loginPanel.SetActive(!loginPanel.activeSelf);
        //SceneManager.LoadScene(1);
    }

    public  IEnumerator CloseErrorText()
    {
        yield return new WaitForSeconds(2);
        errorText.gameObject.SetActive(false);

    }
}
