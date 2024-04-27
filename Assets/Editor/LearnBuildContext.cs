using System;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;

public class LearnBuildContext
{
    internal Dictionary<Type, IContextObject> m_ContextObjects;

    public LearnBuildContext()
    {
        m_ContextObjects = new Dictionary<Type, IContextObject>();
    }

    public void Test()
    {
        Debug.Log("add ContextObject1---");
        SetContextObject(new ContextObject1());
        Debug.Log("add ContextObject2---");
        SetContextObject(new ContextObject2());
        Debug.Log("add ContextObject3---");
        SetContextObject(new ContextObject3());
    }

    private IEnumerable<Type> WalkAssignableTypes(IContextObject contextObject)
    {
        Debug.Log("===start WalkAssignableTypes");
        
        var iCType = typeof(IContextObject);
        foreach (Type t in contextObject.GetType().GetInterfaces())
        {
            if (iCType.IsAssignableFrom(t) && t != iCType)
                yield return t;
        }

        for (var current = contextObject.GetType(); current != null; current = current.BaseType)
            if (iCType.IsAssignableFrom(current) && current != iCType)
                yield return current;

        Debug.Log("===end WalkAssignableTypes");
    }

    public void SetContextObject(IContextObject contextObject)
    {
        if (contextObject == null)
            throw new ArgumentNullException("contextObject");

        List<Type> types = new List<Type>(WalkAssignableTypes(contextObject));

        foreach (var type in types)
        {
            Debug.Log($"Adding context object of type {type.FullName}");
        }

        if (types.Count == 0)
            throw new Exception($"Could not determine context object type for object of type {contextObject.GetType().FullName}");
        types.ForEach(x => m_ContextObjects[x] = contextObject);
    }
}

public interface IContextObject1 : IContextObject
{
}

public class ContextObject1:IContextObject1
{

}

public interface IContextObject2 : IContextObject1
{
}

public class ContextObject2:ContextObject1,IContextObject2
{
    
}

public interface IContextObject3 : IContextObject2
{
}

public class ContextObject3:ContextObject2,IContextObject3
{
    
}