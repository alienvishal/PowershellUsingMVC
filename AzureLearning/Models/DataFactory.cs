using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AzureLearning.Models
{
    public class DataFactory
    {
        [Required(ErrorMessage = "Please Select the Subscription")]
        public string SelectedSubscription { get; set; }

        [Required(ErrorMessage = "Please Enter Resource Group")]
        public string RGValue { get; set; }
    }
}