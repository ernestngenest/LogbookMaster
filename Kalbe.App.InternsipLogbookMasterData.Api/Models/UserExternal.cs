using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_UserExternal")]
    public class UserExternal : Base
    {
        [Required]
        public string UserPrincipalName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long UniversityCode { get; set; }
        [Required]
        public string UniversityName { get; set; }
        [Required]
        public string Major { get; set; }
        [Required]
        public string Dept { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        public string Status { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public DateTime  JoinDate { get; set; }
        public DateTime EndDate { get; set; }

        //set relation

        public Claim[] GetClaims()
        {
            var claims = new List<Claim>();
            var properties = GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.PropertyType.Equals(typeof(string)) || prop.PropertyType.Equals(typeof(DateTime)))
                {
                    claims.Add(new Claim(prop.Name, GetPropertyValue(prop)));
                }
            }

            return claims.ToArray();
        }
        private string GetPropertyValue(System.Reflection.PropertyInfo prop)
        {
            var propValue = prop.GetValue(this);
            if (propValue != null)
            {
                if (prop.PropertyType.Equals(typeof(string)))
                {
                    return propValue.ToString();
                }
                else if (prop.PropertyType.Equals(typeof(DateTime)))
                {
                    return Convert.ToDateTime(propValue).ToString("yyyy-MM-dd");
                }
                else return "";
            }
            else return "";

        }


    }

    public class UserExternalLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
        
}
