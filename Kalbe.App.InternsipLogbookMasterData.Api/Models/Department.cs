using Kalbe.Library.Common.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_Department")]
    public class Department : Base
    {
        [Required]
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
    }
}
