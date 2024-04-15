using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> listServices = new Dictionary<Type, object>();
    public static void RegisterService<T>(T service)
    {
        listServices[typeof(T)] = service;
    }
    public static T GetService<T>()
    {
        Debug.Assert(listServices.ContainsKey(typeof(T)), "Ce service n'existe pas");
        if (listServices.ContainsKey(typeof(T)))
        {
            return (T)listServices[typeof(T)];
        }
        else
        {
            return default(T);
        }
    }
}
