using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    public class Allowance : Base
    {
        [Required]
        public string WorkType { get; set; }

        [Required]
        public long AllowanceFee { get; set; }
    }
}
