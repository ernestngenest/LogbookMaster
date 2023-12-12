using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_Education")]
    public class Education : Base
     {
        public string EducationCode { get; set; }
        public string EducationName { get; set; }

        public List<Allowance> Allowances { get; set; } = new List<Allowance>();
    }
}
