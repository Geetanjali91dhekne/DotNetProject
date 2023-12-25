namespace LogbookManagementService.ViewModels
{
    public class ResponseTurbineStatus
    {
        public TurbineStatus[] TurbineStatuses { get; set; }
        public TurbineName[] TurbineNames { get; set; }
        public PlantName[] PlantNames { get; set; }
    }
    public class TurbineStatus
    {
        public string StatusCode { get; set; }
        public int Value { get; set; }
    }

    public class PlantName
    {
        public string Name { get; set; }
    }
    public class TurbineName
    {
        public string Name { get; set; }
        public string StatusName { get; set; }
        public string Turbine { get; set; }
        public string ErrorDescription { get; set; }
        public string WindSpeed { get; set; }
        public string CurrentActivePower { get; set; }
        public string DownTimeCount { get; set; }
        public string BreakdownCategory { get; set; }

    }


    public class CustomerFilter
    {
        public string CustomerName { get; set; }
        public string OwnerId { get; set; }
    }
}
