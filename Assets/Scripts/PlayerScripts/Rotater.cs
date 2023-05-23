using System;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    private object blockFlipObject;
    private bool currentFlipValue;

    public void Rotate(bool value, object sender)
    {
        if (blockFlipObject == null || sender == blockFlipObject)
        {
            if (currentFlipValue != value)
            {
                Quaternion angle = Quaternion.Euler(new Vector3(transform.rotation.x, (Convert.ToInt32(value) * 180), transform.rotation.z));
                transform.rotation = angle;
            }
            currentFlipValue = value;
        }
    }
    public void BlockFlip(object sender)
    {
        blockFlipObject = sender;
    }
    public void UnblockFlip(object sender)
    {
        if (sender == blockFlipObject)
        {
            blockFlipObject = null;
        }
    }
}
