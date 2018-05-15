using System.Web.Script.Serialization;
using System.Data;
using System.Collections.Generic;
using System;
using Entites.Request;

namespace WebApi.Helpers
{
    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                return serializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}