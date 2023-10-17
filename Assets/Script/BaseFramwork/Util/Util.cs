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
                Debug.LogError($"DateTime.Parse ����({dateTimeStrings[i]})");
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

    // ����Ʈ �迭�� String���� ��ȯ 
    public static string ByteToString(byte[] strByte)
    {
        string str = Encoding.Default.GetString(strByte);
        return str;
    }

    // String�� ����Ʈ �迭�� ��ȯ 
    public static byte[] StringToByte(string str)
    {
        byte[] StrByte = Encoding.UTF8.GetBytes(str);
        return StrByte;
    }

    public static float AngleReapply(float angle)
    {
        /*Function : AngleReapply()
         * return type float;
        * ���� ������ �Լ�
        * ���� 180���� ���� ���, 360���� ���� 180�� �̸��� ������ ��ȯ��
        * ���� -180���� ���, 360�� ���ؼ� ���� �Ǽ����� ������ ������ ��ȯ��
        * ���� 90���� ���� ���, 180���� ���� ���� ������
        * ���� -90���� ���, 180���� ���ؼ� ���� ������
        * ���� ������ ���� ����ڰ� ���� ������ ������ ������ ���� ����Ѵ�.
        * ex) 350���� ������ ���� ����� ���
        *      360���� ���� -10���� ����. SVV �˻��� ��� 2���� ������ ���� ���ַ� �Ǵ��ϰ� �ֱ� ������ ������ ���� �ּ�ȭ �Ͽ� ���°��̴�.
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
        * ���� ������ �Լ�
        * ���� 180���� ���� ���, 360���� ���� 180�� �̸��� ������ ��ȯ��
        * ���� -180���� ���, 360�� ���ؼ� ���� �Ǽ����� ������ ������ ��ȯ��
        * ���� ������ ���� ����ڰ� ���� ������ ������ ������ ���� ����Ѵ�.
        * ex) 350���� ������ ���� ����� ���
        *      360���� ���� -10���� ����. SVV �˻��� ��� 2���� ������ ���� ���ַ� �Ǵ��ϰ� �ֱ� ������ ������ ���� �ּ�ȭ �Ͽ� ���°��̴�.
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
