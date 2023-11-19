using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("t_UserRole")]
    public class UserRole :Base
    {

        public string UserPrincipalName { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }

        [Required]
        public string RoleCode { get; set; }
        [Required]
        public string RoleName { get; set; }

        public long? UserInternalId { get; set; }
        public long? UserExternalId { get; set; }

        [ForeignKey("UserInternalId")]
        [JsonIgnore]
        public UserInternal UserInternal { get; set; }

        [ForeignKey("UserExternalId")]
        [JsonIgnore]
        public UserExternal UserExternal { get; set;}
    }
}
