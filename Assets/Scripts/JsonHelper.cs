using System;
using System.Text;
using UnityEngine;

public class JsonHelper
{
    public static T[] GetJsonArray<T>(string json)
    {
        var sb = new StringBuilder();

        sb.Append("{ \"array\": ");
        sb.Append(json);
        sb.Append("}");

        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (sb.ToString());
        
        return wrapper.array;
    }
 
    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}