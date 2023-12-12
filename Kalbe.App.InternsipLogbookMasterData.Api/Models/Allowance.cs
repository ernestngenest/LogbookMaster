using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_Allowance")]
    public class Allowance : Base
    {
        [Required]
        public string WorkType { get; set; }

        [Required]
        public long AllowanceFee { get; set; }

        public long? EducationId { get; set; }
        [ForeignKey("EducationId")]
        [JsonIgnore]
        public Education Education { get; set; }
    }
}
