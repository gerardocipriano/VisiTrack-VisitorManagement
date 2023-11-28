// Versione 1.0.0

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Template.Web.Infrastructure
{
    public static class Alerts
    {
        public const string ALERTS_KEY = "AlertsModel";

        public static void AddInfo(Controller controller, string value)
        {
            AddAlert(controller.ViewData, AlertsDto.AlertLevelEnum.info, value, false, 0);
        }

        public static void AddInfo(Controller controller, string value, int millisecondsBeforeAutoDismiss)
        {
            AddAlert(controller.ViewData, AlertsDto.AlertLevelEnum.info, value, true, millisecondsBeforeAutoDismiss);
        }

        public static void AddSuccess(Controller controller, string value)
        {
            AddAlert(controller.ViewData, AlertsDto.AlertLevelEnum.success, value, false, 0);
        }

        public static void AddSuccess(Controller controller, string value, int millisecondsBeforeAutoDismiss)
        {
            AddAlert(controller.ViewData, AlertsDto.AlertLevelEnum.success, value, true, millisecondsBeforeAutoDismiss);
        }

        public static void AddWarning(Controller controller, string value)
        {
            AddAlert(controller.ViewData, AlertsDto.AlertLevelEnum.warning, value, false, 0);
        }

        public static void AddWarning(Controller controller, string value, int millisecondsBeforeAutoDismiss)
        {
            AddAlert(controller.ViewData, AlertsDto.AlertLevelEnum.warning, value, true, millisecondsBeforeAutoDismiss);
        }

        public static void AddError(Controller controller, string value)
        {
            AddAlert(controller.ViewData, AlertsDto.AlertLevelEnum.error, value, false, 0);
        }

        public static void AddError(Controller controller, string value, int millisecondsBeforeAutoDismiss)
        {
            AddAlert(controller.ViewData, AlertsDto.AlertLevelEnum.error, value, true, millisecondsBeforeAutoDismiss);
        }



        static void AddAlert(ViewDataDictionary viewData, AlertsDto.AlertLevelEnum level, string value, bool autoDismiss, int milliseconds)
        {
            var alerts = viewData[ALERTS_KEY] as AlertsDto;
            if (alerts == null)
            {
                viewData[ALERTS_KEY] = alerts = new AlertsDto();
            }

            alerts.List.Add(new AlertsDto.Alert
            {
                Level = level,
                Value = value,
                AutoDismiss = autoDismiss,
                MillisecondsAutoDismiss = milliseconds,
            });
        }
    }

    public class AlertsDto
    {
        public bool HasAlerts => List.Count > 0;
        public List<Alert> List { get; set; }

        public AlertsDto()
        {
            List = new List<Alert>();
        }

        public void AddAlert(AlertLevelEnum level, string value, bool autoDismiss)
        {
            List.Add(new Alert()
            {
                Value = value,
                AutoDismiss = autoDismiss
            });
        }

        public class Alert
        {
            public AlertLevelEnum Level { get; set; }
            public string Value { get; set; }
            public bool AutoDismiss { get; set; }
            public int MillisecondsAutoDismiss { get; set; }
        }

        public enum AlertLevelEnum
        {
            info,
            success,
            warning,
            error,
        }
    }

    public class AlertsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            OnActionExecutingAlert(context);
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            OnActionExecutedAlert(context);
            base.OnActionExecuted(context);
        }

        void OnActionExecutingAlert(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            var tempDataAlerts = controller.TempData[Alerts.ALERTS_KEY] as string;

            if (tempDataAlerts != null)
                controller.ViewData[Alerts.ALERTS_KEY] = JsonConvert.DeserializeObject<AlertsDto>(tempDataAlerts);
        }

        void OnActionExecutedAlert(ActionExecutedContext context)
        {
            var isHistoryBack = context.Result != null && context.Result.GetType().Name == "HistoryContentResult"; // fatto in questo modo per non prendere una dipendenza con codice della funzionalità History
            // SE SONO IN UN REDIRECT PRESERVO I ViewData
            if (context.Result is RedirectResult
                || context.Result is RedirectToRouteResult
                || context.Result is RedirectToActionResult
                || isHistoryBack)
            {
                var controller = (Controller)context.Controller;
                var viewDataAlerts = controller.ViewData[Alerts.ALERTS_KEY];

                if (viewDataAlerts != null)
                    controller.TempData[Alerts.ALERTS_KEY] = JsonConvert.SerializeObject(viewDataAlerts);
            }
        }

    }


    public static class AlertsRazorPageExtensions
    {
        /// <summary>
        /// Render Alerts with Toastify : https://github.com/apvarun/toastify-js/blob/master/README.md
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static HtmlString RenderAlertsWithToastify(this Microsoft.AspNetCore.Mvc.Razor.RazorPageBase page)
        {
            var alerts = GetAlerts(page.ViewContext.ViewData);

            if (alerts.Any() == false)
            {
                return HtmlString.Empty;
            }
            else
            {
                var s = new System.Text.StringBuilder();

                foreach (var a in alerts)
                {
                    var t = $@"Toastify({{close: true,gravity:'bottom',position:'left', className:'onit-toastify onit-toastify-{a.Level}',text:'{a.Value}',duration:{a.MillisecondsAutoDismiss}}}).showToast();";
                    s.AppendLine(t);
                }

                return new HtmlString(s.ToString());
            }
        }

        static IEnumerable<AlertsDto.Alert> GetAlerts(ViewDataDictionary viewData)
        {
            var alerts = viewData[Alerts.ALERTS_KEY] as AlertsDto;
            if (alerts == null)
            {
                return System.Array.Empty<AlertsDto.Alert>();
            }
            else
            {
                return alerts.List.ToArray();
            }
        }
    }
}