﻿using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_Allowance")]
    public class Allowance : Base
    {
        [Required]
        public string WorkType { get; set; }

        [Required]
        public long AllowanceFee { get; set; }
    }
}
