using UnityEngine;

[ExecuteInEditMode]
public class FullScreenWidget : MonoBehaviour
{

    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.StrechRectTransformToFullScreen();
    }
}
