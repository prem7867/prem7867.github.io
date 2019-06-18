using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Dec_21_ASP_Bikes.Models
{
    [MetadataType(typeof(CycleDetailMetadata))]
    public partial class CycleDetail
    {
    }
    public class CycleDetailMetadata
    {
        
        [Required(ErrorMessage = "Please select Image", AllowEmptyStrings = false)]
        [DisplayAttribute(Name = "Cycle Image")]
        public byte[] CycleImage { get; set; }


        [Required(ErrorMessage = "Please Insert CycleType" , AllowEmptyStrings = false)]
        [DisplayAttribute(Name = "Cycle Type")]
        public string CycleType { get; set; }

        [Required(ErrorMessage = "Please Insert CycleAccessories" , AllowEmptyStrings = false)]
        [DisplayAttribute(Name = "Cycle Accessories")]
        public string CycleAccessories { get; set; }
    }
}
