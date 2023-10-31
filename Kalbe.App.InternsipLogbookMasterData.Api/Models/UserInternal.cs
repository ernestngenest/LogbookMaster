using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text.Json;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_UserExternal")]
    public class UserInternal
    {
        public string UserPrincipalName { get; set; }
        public string NIK { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string DeptName { get; set; }
        public string CompCode { get; set; }
        public string CompName { get; set; }
        public List<UserRole> Roles { get; set; } = new List<UserRole>();
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

                else if (prop.PropertyType.Equals(typeof(List<UserRole>)))
                {
                    claims.Add(new Claim(prop.Name, JsonSerializer.Serialize(prop.GetValue(this))));
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

        public class UserLogin
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
            
            public bool GetProfile { get; set; }
        }
        public class UserReturn
        {
            public string Upn { get; set; }
            public string Name { get; set; }
            public string AccessToken { get; set; }
        }

    }
}
