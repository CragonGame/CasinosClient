using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using io.openinstall.unity;

public class OpenInstallSample : MonoBehaviour {
    private OpenInstall openinstall;

    public Text installResult;
    public Text wakeupResult;

    // Use this for initialization
    void Start()
    {
        openinstall = GameObject.Find("OpenInstall").GetComponent<OpenInstall>();
        openinstall.RegisterWakeupHandler(getWakeupFinish);

        installResult = GameObject.Find("InstallText").GetComponent<Text>();
        wakeupResult = GameObject.Find("WakeupText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getInstallButtonClick()
    {
        Debug.Log("OpenInstallSample getInstall button click");
        openinstall.GetInstall(8, getInstallFinish);
    }

    public void reportRegisterButtonClick()
    {
        Debug.Log("OpenInstallSample reportRegister button click");
        openinstall.ReportRegister();
    }

    public void reportEffectPointButtonClick()
    {
        Debug.Log("OpenInstallSample reportEffectPoint button click");
        openinstall.ReportEffectPoint("effect_test", 1);
    }

    // callback
    public void getInstallFinish(OpenInstallData installData)
    {
        Debug.Log("OpenInstallSample getInstallFinish : 渠道编号=" + installData.channelCode + "，自定义数据=" + installData.bindData);
        installResult.text = "安装参数：" + JsonUtility.ToJson(installData);
    }

    public void getWakeupFinish(OpenInstallData wakeupData)
    {
        Debug.Log("OpenInstallSample getWakeupFinish : 渠道编号=" +wakeupData.channelCode + "， 自定义数据=" + wakeupData.bindData);
        wakeupResult.text = "拉起参数：" + JsonUtility.ToJson(wakeupData);
    }

}
