using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_School")]
    public class School : Base
    {
        [Required]
        public string SchoolCode { get; set; }
        [Required]
        public string SchoolName { get; set; }
    }
}
