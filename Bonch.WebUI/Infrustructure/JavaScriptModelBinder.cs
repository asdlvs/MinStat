using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace Bonch.WebUI.Infrustructure
{
    public class JavaScriptModelBinder<T> : IModelBinder
    {
        private string _jsVariableName = String.Empty;
        private string _defaultProperty;
        public JavaScriptModelBinder(string jsVariableName, string defaultProperty = "Id")
        {
            _jsVariableName = jsVariableName;
            _defaultProperty = defaultProperty;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            if (request.Params.AllKeys.Contains(String.Format("{0}[{1}]", _jsVariableName, _defaultProperty)))
            {

                T element = (T)typeof(T).GetConstructor(new Type[] { }).Invoke(null);
                foreach (PropertyInfo pi in typeof(T).GetProperties())
                {
                    string key = String.Format("{0}[{1}]", _jsVariableName, pi.Name);
                    if (request.Params.AllKeys.Contains(key))
                    {
                        if (pi.PropertyType == typeof(string))
                        {
                            pi.SetValue(element, request.Params[key], null);
                        }
                    }
                }
                return element;
            }
            return null;
        }
    }
}