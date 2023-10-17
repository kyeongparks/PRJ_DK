using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

public static class Util
{
    public static List<DateTime> MapToDateTimes(List<string> dateTimeStrings)
    {
        string[] formats = { "yyyyMMdd", "yyyy-MM-dd" };
        List<DateTime> values = new List<DateTime>();

        for (int i = 0; i < dateTimeStrings.Count; i++)
        {
            DateTime dat;
            if (DateTime.TryParseExact(dateTimeStrings[i], formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dat))
            {
                values.Add(dat);
            }
            else
            {
                Debug.LogError($"DateTime.Parse 실패({dateTimeStrings[i]})");
            }
        }

        return values;
    }

    public static T CheckOrCreateComponent<T>(this T targetT, Transform parentTr, string findName) where T : Component
    {
        if (targetT == null)
        {
            targetT = parentTr.FindOrCreateComponent<T>(findName);
            return targetT;
        }

        return targetT;
    }

    public static T FindOrCreateComponent<T>(this Transform parentTr, string findName) where T : Component
    {
        GameObject findObj = GameObject.Find(findName);
        if (findObj != null)
        {
            T findT = findObj.GetComponent<T>();
            if (findT != null)
                return findT;
            else
            {
                //Create
                GameObject go = new GameObject { name = findName };
                go.transform.SetParent(parentTr);
                return go.AddComponent<T>();
            }
        }
        else
        {
            //Create
            GameObject go = new GameObject { name = findName };
            go.transform.SetParent(parentTr);
            return go.AddComponent<T>();
        }
    }

    public static void StrechRectTransformToFullScreen(this RectTransform _mRect)
    {
        _mRect.anchoredPosition3D = Vector3.zero;
        _mRect.anchorMin = Vector2.zero;
        _mRect.anchorMax = Vector2.one;
        _mRect.pivot = new Vector2(0.5f, 0.5f);
        _mRect.sizeDelta = Vector2.zero;
    }

    // 바이트 배열을 String으로 변환 
    public static string ByteToString(byte[] strByte)
    {
        string str = Encoding.Default.GetString(strByte);
        return str;
    }

    // String을 바이트 배열로 변환 
    public static byte[] StringToByte(string str)
    {
        byte[] StrByte = Encoding.UTF8.GetBytes(str);
        return StrByte;
    }

    public static float AngleReapply(float angle)
    {
        /*Function : AngleReapply()
         * return type float;
        * 각도 재조정 함수
        * 각이 180도를 넘을 경우, 360도를 빼서 180도 미만의 각으로 변환함
        * 각이 -180도일 경우, 360을 더해서 양의 실수값을 가지는 각도로 변환함
        * 각이 90도를 넘을 경우, 180도를 빼서 값을 조정함
        * 각이 -90도일 경우, 180도를 더해서 값을 조정함
        * 위의 과정을 통해 사용자가 이해 가능한 범위의 각도로 값을 출력한다.
        * ex) 350도의 데이터 값을 얻었을 경우
        *      360도를 빼서 -10도로 만듬. SVV 검사의 경우 2도의 편차를 정상 범주로 판단하고 있기 때문에 데이터 값을 최소화 하여 보는것이다.
        */

        float ang = angle;

        if (ang > 180)
            ang -= 360f;

        if (ang < -180)
            ang += 360f;

        if (ang > 90)
            ang -= 180f;

        if (ang < -90)
            ang += 180f;

        return ang;
    }

    public static float AngleHeadReapply(float angle)
    {
        /*Function : AngleReapply()
         * return type float;
        * 각도 재조정 함수
        * 각이 180도를 넘을 경우, 360도를 빼서 180도 미만의 각으로 변환함
        * 각이 -180도일 경우, 360을 더해서 양의 실수값을 가지는 각도로 변환함
        * 위의 과정을 통해 사용자가 이해 가능한 범위의 각도로 값을 출력한다.
        * ex) 350도의 데이터 값을 얻었을 경우
        *      360도를 빼서 -10도로 만듬. SVV 검사의 경우 2도의 편차를 정상 범주로 판단하고 있기 때문에 데이터 값을 최소화 하여 보는것이다.
        */

        float ang = angle;

        if (ang > 180)
            ang -= 360f;

        if (ang < -180)
            ang += 360f;

        return ang;
    }

    public static void SetLayerMask(GameObject go, int layerMask)
    {
        go.layer = layerMask;
        foreach (Transform t in go.GetComponentsInChildren<Transform>(true))
        {
            t.gameObject.layer = layerMask;
        }
    }

    public static int[,] ConvertTextAssetArray(TextAsset textAsset, int row, int col)
    {
        int[,] result = new int[row, col];
        string[] dataLines = textAsset.text.Split(new char[] { '\n' });
        for (int i = 0; i < dataLines.Length; i++)
        {
            var data = dataLines[i].Split(',');
            for (int j = 0; j < data.Length; j++)
            {
                result[i, j] = int.Parse(data[j]);
            }
        }

        return result;
    }
}
