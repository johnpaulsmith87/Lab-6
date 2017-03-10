using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionHelper
/// </summary>
public static class SessionHelper
{
    public static TypeOfSessionObject Get<TypeOfSessionObject>(string key)
    {
        var variableFromSession = HttpContext.Current.Session[key];
        if (variableFromSession is TypeOfSessionObject)
            return (TypeOfSessionObject)variableFromSession;

        return default(TypeOfSessionObject);
    }
    public static void Set(string key, object value)
    {
        HttpContext.Current.Session[key] = value;
    }
    public static void Remove(string key)
    {
        HttpContext.Current.Session.Remove(key);
    }

}
