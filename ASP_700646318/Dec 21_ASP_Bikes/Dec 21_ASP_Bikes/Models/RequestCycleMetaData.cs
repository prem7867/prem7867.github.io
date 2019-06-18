using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Models
{
    [MetadataType(typeof(RequestCycleMetaData))]
    public partial class RequestCycle
    {
    }

   // [Table("RequestCycle")]
    public class RequestCycleMetaData
    {
       
        [HiddenInput(DisplayValue = false)]
        public int RequestID { get; set; }

       // [HiddenInput(DisplayValue = false)]
     //   public int CycleID { get; set; }

        [Required]
        [Display(Name = "Start Date")]

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime FromDate { get; set; }

        [Required]
        [DisplayAttribute(Name = "To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime ToDate { get; set; }  // public virtual CycleDetail CycleDetail { get; set; }

      // public List<CycleDetail> CycleDetails { get; set; }
    }
}


