using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Management.Automation;
using System.Web;

namespace AzureLearning.Models
{
    public class ResourceGroup
    {
        [Required(ErrorMessage = "Please Select the location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please Select the Subscription")]
        public string SelectedSubscription { get; set; }

        public string RGValue { get; set; }

    }
}