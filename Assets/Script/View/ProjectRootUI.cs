using PRJ;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectRootUI : UIManager
{
    public static new ProjectRootUI Instance { get { return UIManager.Instance as ProjectRootUI; } }
    float fCheckTime = 0, wifiTime = 10f, gotoHomeTime = 40f;
    public bool isLoading = false;


    protected override void OnAwakeUI()
    {
        base.OnAwakeUI();
    }

    protected override void OnDestroyUI()
    {
        base.OnDestroyUI();
    }

    void Start()
    {

    }

    protected override void Update()
    {
        base.Update();
    }

    public void ApplicationQuit()
    {

    }

    void LogHandler(string condition, string stackTrace, LogType type)
    {

    }

    protected override void OnBeginLoading()
    {

    }

    protected override void OnEndLoading()
    {

    }

    protected override void OnShowLayer(UILayer layer)
    {
        base.OnShowLayer(layer);
    }

    protected override void OnHideLayer(UILayer layer)
    {
        base.OnHideLayer(layer);
    }

    public void Goto_IntroView()
    {

    }

    public void Goto_TitleView()
    {
        
    }
}
