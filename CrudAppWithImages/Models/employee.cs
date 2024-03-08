//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrudAppWithImages.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class employee
    {
        public int id { get; set; }
        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "Employee Name is Required")]
        public string name { get; set; }
        [DisplayName("Employee Designation")]
        [Required(ErrorMessage = "Designation Is Required")]
        public string designation { get; set; }
        [DisplayName("Employee Image")]
        public string image_path { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
