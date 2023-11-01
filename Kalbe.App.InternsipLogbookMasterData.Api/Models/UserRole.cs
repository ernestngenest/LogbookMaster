﻿using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_UserRole")]
    public class UserRole : Base
    {
        [Required]
        public string RoleCode { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
