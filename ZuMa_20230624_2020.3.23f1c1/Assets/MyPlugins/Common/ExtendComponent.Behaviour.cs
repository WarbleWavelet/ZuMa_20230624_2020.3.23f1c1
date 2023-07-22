/****************************************************
    文件：ExtendComponent.Behaviour.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/4 15:57:15
	功能：
*****************************************************/

using UnityEngine;

public static partial class ExtendBehaviour
{

    public static void Example()
    {
        var gameObject = new GameObject();
        var monoBehaviour = gameObject.GetComponent<MonoBehaviour>();

        monoBehaviour.Enable();  // component.enabled = true
        monoBehaviour.Disable(); // component.enabled = false
    }


    //public static void Enabled(this Behaviour behaviour)
    //{
    //    behaviour.enabled = true;
    //}

    //public static void Disabled(this Behaviour behaviour)
    //{
    //    behaviour.enabled = false;
    //}
    public static T Enable<T>(this T behaviour) where T : Behaviour
    {
        behaviour.enabled = true;
        return behaviour;
    }

    public static T Disable<T>(this T behaviour) where T : Behaviour
    {
        behaviour.enabled = false;
        return behaviour;
    }

}