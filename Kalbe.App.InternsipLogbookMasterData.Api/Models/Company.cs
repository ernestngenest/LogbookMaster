using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_Company")]
    public class Company : Base
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
