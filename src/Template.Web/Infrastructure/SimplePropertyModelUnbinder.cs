using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using R4Mvc.ModelUnbinders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Template.Infrastructure.AspNetCore
{
    public class SimplePropertyModelUnbinder : IModelUnbinder
    {
        public virtual void UnbindModel(RouteValueDictionary routeValueDictionary, string routeName, object routeValue)
        {
            var dict = new RouteValueDictionary(routeValue);
            foreach (var entry in dict)
            {
                if (IgnoreProperty(routeValue, entry) == false)
                {
                    var name = entry.Key;

                    if (!(entry.Value is string) && (entry.Value is System.Collections.IEnumerable))
                    {
                        if (IncludeProperty(routeValue, entry))
                        {
                            var enumerableValue = (System.Collections.IEnumerable)entry.Value;
                            var i = 0;
                            foreach (var enumerableElement in enumerableValue)
                            {
                                var properties = (UnbindProperties)enumerableElement.GetType().GetCustomAttributes(typeof(UnbindProperties), true).FirstOrDefault();
                                if (properties != null)
                                {
                                    foreach (var property in properties.NomiProprieta)
                                    {
                                        var value = enumerableElement.GetType().GetProperty(property).GetValue(enumerableElement);
                                        ModelUnbinderHelpers.AddRouteValues(routeValueDictionary, string.Format($"{name}[{i}].{property}", name, i), value);
                                    }
                                }
                                else
                                {
                                    ModelUnbinderHelpers.AddRouteValues(routeValueDictionary, string.Format("{0}[{1}]", name, i), enumerableElement);
                                }
                                i++;
                            }
                        }
                    }
                    else if (entry.Value is DateTime)
                    {
                        var data = (DateTime)entry.Value;
                        if (IncludeParseTimeProperty(routeValue, entry))
                        {
                            ModelUnbinderHelpers.AddRouteValues(routeValueDictionary, name, data.ToTimeHtmlInput());
                        }
                        else if (IncludeParseDateProperty(routeValue, entry))
                        {
                            ModelUnbinderHelpers.AddRouteValues(routeValueDictionary, name, data.ToDateHtmlInput());
                        }
                        else if (IncludeParseDateTimeProperty(routeValue, entry))
                        {
                            ModelUnbinderHelpers.AddRouteValues(routeValueDictionary, name, data.ToDateTimeHtmlInput());
                        }
                    }
                    else
                    {
                        ModelUnbinderHelpers.AddRouteValues(routeValueDictionary, name, entry.Value);
                    }
                }
            }
        }
        bool IncludeProperty(object routeValue, KeyValuePair<string, object> entry)
        {
            var includeAttributes = routeValue.GetType().GetProperty(entry.Key).GetCustomAttributes(typeof(IncludeAttribute), true);
            return includeAttributes.Any();
        }

        bool IgnoreProperty(object routeValue, KeyValuePair<string, object> entry)
        {
            var includeAttributes = routeValue.GetType().GetProperty(entry.Key).GetCustomAttributes(typeof(IgnoreUnbindAttribute), true);
            return includeAttributes.Any();
        }
        bool IncludeParseTimeProperty(object routeValue, KeyValuePair<string, object> entry)
        {
            var includeAttributes = routeValue.GetType().GetProperty(entry.Key).GetCustomAttributes(typeof(ParseTimeAttribute), true);

            return includeAttributes.Any();
        }
        bool IncludeParseDateProperty(object routeValue, KeyValuePair<string, object> entry)
        {
            var includeAttributes = routeValue.GetType().GetProperty(entry.Key).GetCustomAttributes(typeof(ParseDateAttribute), true);

            return includeAttributes.Any();
        }
        bool IncludeParseDateTimeProperty(object routeValue, KeyValuePair<string, object> entry)
        {
            var includeAttributes = routeValue.GetType().GetProperty(entry.Key).GetCustomAttributes(typeof(ParseDateTimeAttribute), true);

            return includeAttributes.Any();
        }
    }

    public class IncludeAttribute : Attribute
    {

    }

    public class UnbindProperties : Attribute
    {
        public string[] NomiProprieta { get; set; }

        public UnbindProperties(string[] props)
        {
            NomiProprieta = props;
        }
    }

    public class IgnoreUnbindAttribute : Attribute
    {

    }

    [Obsolete("Use IgnoreUnbind", true)]
    public class ExcludeAttribute : Attribute
    {

    }

    public class ParseTimeAttribute : Attribute
    {

    }
    public class ParseDateAttribute : Attribute
    {

    }
    public class ParseDateTimeAttribute : Attribute
    {

    }
}
