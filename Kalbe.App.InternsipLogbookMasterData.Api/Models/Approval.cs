using Kalbe.Library.Common.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Models
{
    [Table("m_Approval")]
    public class Approval : Base
    {
        [Required]
        public string ApplicationCode { get; set; }

        public string ModuleCode { get; set; }

        public string Role { get; set; }
        public int ApprovalLevel { get; set; }
        public bool isAllMustApprove { get; set; }
        public string Status { get; set; }
    }

    [Table("t_Approval)")]

    public class ApprovalDetail : Base
    {
        public long ApprovalId { get; set; }
        public string DocumentNumber { get; set; }
        public string EmailPIC { get; set; }
        public string NamePIC { get; set; }
        public string PIC { get; set; }
        public int ApprovalLine { get; set; }
        public bool NeedApprove { get; set; }
        public string Notes { get; set; }
        public string DueDate { get; set; }

        [ForeignKey("ApprovalId")]
        [JsonIgnore]
        public Approval Approval { get; set; }
    }
    public class ApprovalLogModel
    {
        [Required]
        public string SystemCode { get; set; }

        [Required]
        public string ModuleCode { get; set; }

        [Required]
        public string DocNo { get; set; }
        public string Role { get; set; }
        public string ApproverFrom { get; set; }
        public string ApproverFromName { get; set; }
        public string ApproverFromEmail { get; set; }
        public string ApproverTo { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public Email EmailData { get; set; } = new Email();
    }
    public class ApprovalTransactionData : ApprovalLogModel
    {
        public List<ApprovalTransactionDataModel> ApprovalTransactionDataModel { get; set; } = new List<ApprovalTransactionDataModel>();
    }

    public class ApprovalTransactionDataModel
    {
        public string SystemCode { get; set; }
        public string ModuleCode { get; set; }
        [Required]
        public int ApprovalLevel { get; set; }
        public string ID { get; set; }
        public long ApprovalID { get; set; }
        public string DocNo { get; set; }
        public string Role { get; set; }
        public string EmailPIC { get; set; }
        public string NamePIC { get; set; }
        [Required]
        public string PIC { get; set; }
        public int ApprovalLine { get; set; }
        public string Notes { get; set; }
        public bool NeedApprove { get; set; }
        public string ApproveDate { get; set; }
        public string Status { get; set; }
        public string DueDate { get; set; }
        public string CreatedByUpn { get; set; } = "";
        public string CreatedByEmail { get; set; } = "";
        public string CreatedByName { get; set; } = "";
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByUpn { get; set; } = "";
        public string UpdatedByEmail { get; set; } = "";
        public string UpdatedByName { get; set; } = "";

        public DateTime? UpdatedDate { get; set; }
    }
}
