using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Newtonsoft.Json;

namespace Template.Web.Infrastructure
{
    public class ModelStateToTempDataAttribute : ActionFilterAttribute
    {
        public static string MODELSTATE_KEY = "ModelStateModel";

        void OnActionExecutedModelstates(ActionExecutedContext context)
        {
            if (context.Result is ViewResult)
            {
                // SE SONO IN UNA VIEW PROVO A RICARICARE IL MODELSTATE SE PREPARATO DALLA RICHIESTA PRECEDENTE

                var controller = context.Controller as Controller;
                var serialisedModelState = controller?.TempData[MODELSTATE_KEY] as string;
                if (serialisedModelState != null)
                {
                    context.ModelState.Merge(DeserialiseModelState(serialisedModelState));
                }
            }
            else if (context.Result is RedirectResult
                || context.Result is RedirectToRouteResult
                || context.Result is RedirectToActionResult)
            {
                // SE SONO IN UN REDIRECT PRESERVO GLI ERRORI NEL MODELSTATE

                if (!context.ModelState.IsValid)
                {
                    var controller = context.Controller as Controller;
                    if (controller != null && context.ModelState != null)
                    {
                        controller.TempData[MODELSTATE_KEY] = SerialiseModelState(context.ModelState);
                    }
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            OnActionExecutedModelstates(context);
            base.OnActionExecuted(context);
        }

        string SerialiseModelState(ModelStateDictionary modelState)
        {
            var errorList = modelState
                .Select(x => new ModelStateTransferValue
                {
                    Key = x.Key,
                    AttemptedValue = x.Value.AttemptedValue,
                    RawValue = x.Value.RawValue,
                    ErrorMessages = x.Value.Errors.Select(err => err.ErrorMessage).ToArray(),
                }).ToList();

            return JsonConvert.SerializeObject(errorList);
        }

        public static ModelStateDictionary DeserialiseModelState(string serialisedErrorList)
        {
            var errorList = JsonConvert.DeserializeObject<List<ModelStateTransferValue>>(serialisedErrorList);
            var modelState = new ModelStateDictionary();

            foreach (var item in errorList)
            {
                if (item.RawValue != null &&
                    item.RawValue is Newtonsoft.Json.Linq.JContainer)
                {
                    var vettore = ((Newtonsoft.Json.Linq.JContainer)item.RawValue);
                    modelState.SetModelValue(item.Key, vettore.Values<string>().ToArray(), item.AttemptedValue);
                }
                else
                {
                    modelState.SetModelValue(item.Key, item.RawValue, item.AttemptedValue);
                }

                foreach (var error in item.ErrorMessages)
                {
                    modelState.AddModelError(item.Key, error);
                }
            }
            return modelState;
        }

        public class ModelStateTransferValue
        {
            public string Key { get; set; }
            public string AttemptedValue { get; set; }
            public object RawValue { get; set; }
            public string[] ErrorMessages { get; set; }
        }
    }


    public static class ModelStateExtensions
    {

        public static bool HasToRestoreValue(this ITempDataDictionary tempData, string name)
        {
            if (tempData.ContainsKey(ModelStateToTempDataAttribute.MODELSTATE_KEY))
            {
                var modelstateToRestore = ModelStateToTempDataAttribute.DeserialiseModelState(tempData[ModelStateToTempDataAttribute.MODELSTATE_KEY].ToString());
                if (modelstateToRestore is ModelStateDictionary)
                {
                    if (modelstateToRestore.ContainsKey(name))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static object TryRestoreValue(this ITempDataDictionary tempData, string name)
        {
            if (tempData.ContainsKey(ModelStateToTempDataAttribute.MODELSTATE_KEY))
            {
                var modelstateToRestore = ModelStateToTempDataAttribute.DeserialiseModelState(tempData[ModelStateToTempDataAttribute.MODELSTATE_KEY].ToString());
                if (modelstateToRestore is ModelStateDictionary)
                {
                    if (modelstateToRestore.ContainsKey(name))
                    {
                        if (modelstateToRestore[name].AttemptedValue != null)
                        {
                            return modelstateToRestore[name].AttemptedValue;
                        }
                    }
                }
            }

            return null;
        }
    }

    public static class ModelStateRazorPageExtensions
    {
        public static string GetModelStateDictionaryToJson(this Microsoft.AspNetCore.Mvc.Razor.RazorPageBase page)
        {
            var modelstate = new ModelStateDictionary();

            if (page.TempData.ContainsKey(ModelStateToTempDataAttribute.MODELSTATE_KEY))
            {
                var deserialisedModelState = ModelStateToTempDataAttribute.DeserialiseModelState(page.TempData[ModelStateToTempDataAttribute.MODELSTATE_KEY] as string);
                if (deserialisedModelState is ModelStateDictionary)
                {
                    modelstate = deserialisedModelState;
                }
            }

            return JsonSerializer.ToJsonCamelCase(modelstate);
        }
    }
}
