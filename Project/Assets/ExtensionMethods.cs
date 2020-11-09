﻿using UnityEngine;

public static class ExtensionMethods
{
    public static void Reset(this Transform trans, Space space = default(Space))
    {
        switch (space)
        {
            case Space.World:
                trans.position = Vector3.zero;
                trans.rotation = Quaternion.identity;
                break;
            case Space.Self:
                trans.localPosition = Vector3.zero;
                trans.localRotation = Quaternion.identity;
                break;
            default:
                trans.position = Vector3.zero;
                trans.rotation = Quaternion.identity;
                break;
        }
        trans.localScale = Vector3.one;
    }

    public static void Lerp(this Transform t1, Transform t2, float time1, float time2)
    {
        t1.position = Vector3.Lerp(t1.position, t2.position, time1);
        t1.rotation = Quaternion.Lerp(t1.rotation, t2.rotation, time2);
    }

    public static int ToLayer(this LayerMask layerMask)
    {
        int layerNumber = 0;
        int layer = layerMask.value;
        while (layer > 0)
        {
            layer = layer >> 1;
            layerNumber++;
        }
        return layerNumber - 1;
    }
}