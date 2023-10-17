using UnityEngine;
using PRJ;

public class UtilityManager : SingletonMonoBehaviour<UtilityManager>
{
    public Color HexColor(string hexCode)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            Debug.Log("This Call");
            return color;
        }

        Debug.Log("No Call");
        return Color.white;
    }
}
