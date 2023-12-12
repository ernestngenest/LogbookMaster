using Kalbe.Library.Common.EntityFramework.Models;
using MassTransit.Saga;
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
        public string SchoolCode { get; set; }
        [Required]
        public string SchoolName { get; set; }
        public string FacultyCode { get; set; }
        [Required]
        public string Faculty { get; set; }
        public string DeptCode { get; set; }
        [Required]
        public string Dept { get; set; }
        public string EducationCode { get; set; }
        [Required]
        public string Education { get; set;}

        [MaxLength(100)]
        public string Password { get; set; }

        public string Status { get; set; }
        //public string RoleCode { get; set; }
        //public string RoleName { get; set; }
        public DateTime  JoinDate { get; set; }
        public DateTime EndDate { get; set; }

        public string SupervisorUpn { get; set; }
        public string SupervisorName { get; set; }
        public  string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public int InternshipPeriodMonth { get; set; }

        //set relation
        public UserRole UserRole { get; set; }
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

    public class Mentor
    {
        public string MentorUPN { get; set; }
        public string MentorName { get; set; }
        public string MentorEmail { get; set; }
    }
        
}
