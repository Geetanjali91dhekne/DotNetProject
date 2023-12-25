using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class VScCustMktGroupMstOmsPbi
{
    public string? MktGroupCode { get; set; }

    public string? MktGroupName { get; set; }

    public string? Remarks { get; set; }

    public string? Add1 { get; set; }

    public string? Add2 { get; set; }

    public string? Add3 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PinCode { get; set; }

    public string? StdCode { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? WebSite { get; set; }

    public string? Enteredby { get; set; }

    public DateTime? Enteredon { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedon { get; set; }

    public decimal? Isactive { get; set; }

    public string? CustomerSegment { get; set; }

    public string? Emailid { get; set; }

    public bool DeliverReport { get; set; }
}
