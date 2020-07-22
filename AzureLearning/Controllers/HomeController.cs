using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AzureLearning.Models;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Text;
using System.Web.Script.Serialization;

namespace AzureLearning.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            GetAllSubscriptionList();
            GetAllLocation();
            return View();
        }

        [HttpPost]
        public ActionResult RunScript(ResourceGroup getValues)
        {
            if (ModelState.IsValid)
            {
                Runspace runspace = RunspaceFactory.CreateRunspace();
                runspace.Open();
                Pipeline pipeline = runspace.CreatePipeline();
                pipeline.Commands.AddScript("Select-AzSubscription -Subscription " + getValues.SelectedSubscription.ToString());
                pipeline.Commands.AddScript("Get-AzResourceGroup -Location '" + getValues.Location.ToString() + "'");
                Collection<PSObject> result = pipeline.Invoke();
                runspace.Close();
                ViewBag.Result = result;
            }
            GetAllSubscriptionList();
            GetAllLocation();
            return View("Index");
        }

        [HttpGet]
        public ViewResult DataFactory()
        {
            GetAllSubscriptionList();
            return View();
        }

        [HttpPost]
        public ActionResult DataFactory(DataFactory dataFactory)
        {
            if(ModelState.IsValid)
            {
                Runspace runspace = RunspaceFactory.CreateRunspace();
                runspace.Open();
                Pipeline pipeline = runspace.CreatePipeline();
                pipeline.Commands.AddScript("Select-AzSubscription -Subscription " + dataFactory.SelectedSubscription.ToString());
                pipeline.Commands.AddScript("Get-AzDataFactoryV2 -ResourceGroupName " + dataFactory.RGValue.ToString());
                Collection<PSObject> result = pipeline.Invoke();
                runspace.Close();
                ViewBag.Result = result;
            }
            GetAllSubscriptionList();
            return View();
        }

        [HttpGet]
        protected void GetAllSubscriptionList()
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript("(Get-AzSubscription).Name");
            var res = pipeline.Invoke();
            ViewBag.SubLists = res;
            runspace.Close();
        }

        [HttpGet]
        public JsonResult GetAllResourceGroup(string subscription)
        {
            List<String> output = new List<string>();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript("Select-AzSubscription -Subscription "+ subscription);
            pipeline.Commands.AddScript("(Get-AzResourceGroup).ResourceGroupName");
            Collection<PSObject> res = pipeline.Invoke();
            runspace.Close();
            for (int i = 0; i < res.Count; i++)
            {
                output.Add(res[i].ToString());
            }
           
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        protected void GetAllLocation()
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript("(Get-AzLocation).DisplayName");
            var location = pipeline.Invoke();
            ViewBag.Location = location;
            runspace.Close();
        }
    }
}