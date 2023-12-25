

using LogbookManagementService.Models;

namespace LogbookManagementService.ViewModels
{
    public class WhyAnalysisDetail
    {
        public int Id { get; set; }

        public int? FkAnalysisId { get; set; }

        public object? FkTypeId { get; set; }

        public string? Hrs { get; set; }

        public string? Ai { get; set; }

        public string? Why1 { get; set; }
       
        public string? Why2 { get; set; }
        
        public string? Why3 { get; set; }
        public string? Why4 { get; set; }
        
        public string? Why5 { get; set; }
       
        public string? Why6 { get; set; }

       
        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

    }

    public class PostWhyAnalysisDetail
    {
        public int Id { get; set; }

        public int? FkAnalysisId { get; set; }

        public int? FkTypeId { get; set; }

        public string? Hrs { get; set; }

        public string? Ai { get; set; }

        public string? Why1 { get; set; }
        
        public string? Why2 { get; set; }
       
        public string? Why3 { get; set; }

       
        public string? Why4 { get; set; }
       
        public string? Why5 { get; set; }
       
        public string? Why6 { get; set; }

        public WhyReasonMaster? WhyDrop1 { get; set; }
        public WhyReasonMaster? WhyDrop2 { get; set; }
        public WhyReasonMaster? WhyDrop3 { get; set; }
        public WhyReasonMaster? WhyDrop4 { get; set; }
        public WhyReasonMaster? WhyDrop5 { get; set; }
        public WhyReasonMaster? WhyDrop6 { get; set; }


        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }

    public class ObjectWhyAnalysisDetail
    {
        public int Id { get; set; }

        public int? FkAnalysisId { get; set; }

        public object? FkTypeId { get; set; }

        public string? Hrs { get; set; }

        public string? Ai { get; set; }

        public WhyReasonMaster? whyDrop1 { get; set; }

        public WhyReasonMaster? whyDrop2 { get; set; }

        public WhyReasonMaster? whyDrop3 { get; set; }
        public WhyReasonMaster? whyDrop4 { get; set; }

        public WhyReasonMaster? whyDrop5 { get; set; }

        public WhyReasonMaster? whyDrop6 { get; set; }


        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

    }
}
