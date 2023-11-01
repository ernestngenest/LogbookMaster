using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_Faculty")]
    public class Faculty : Base
    {
        [Required]
        public string FacultyCode { get; set; }
        [Required]
        public string FacultyName { get; set; }
    }
}
