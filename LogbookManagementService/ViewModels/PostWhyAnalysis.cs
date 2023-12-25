using LogbookManagementService.Models;

namespace LogbookManagementService.ViewModels
{
    public class PostWhyAnalysis
    {
     public  TWhyAnalysis? TWhyAnalysisList { get; set; }
     public List<PostWhyAnalysisDetail>? whyAnalysisDetailList { get; set; }
    }
}
