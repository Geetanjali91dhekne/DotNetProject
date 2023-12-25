using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class VScCustSubGroupMstOmsPbi
{
    public string? GroupCode { get; set; }

    public string? MainCustomerCode { get; set; }

    public string CustomerCode { get; set; } = null!;

    public string? CustomerName { get; set; }

    public string? CustAdd1 { get; set; }

    public string? CustAdd2 { get; set; }

    public string? CustAdd3 { get; set; }

    public string? CustCity { get; set; }

    public string? CustState { get; set; }

    public string? CustPin { get; set; }

    public string? CustCountry { get; set; }

    public string? ContactPerson { get; set; }

    public string? CustStNo { get; set; }

    public DateTime? CustStDate { get; set; }

    public string? CustCstNo { get; set; }

    public DateTime? CustCstDate { get; set; }

    public string? CustPanNo { get; set; }

    public string? CustPhone { get; set; }

    public string? CustFax { get; set; }

    public string? CustEmail { get; set; }

    public string? GroupName { get; set; }

    public string? CompletionFlag { get; set; }

    public string? CustDocGroup { get; set; }

    public string? CustDocSname { get; set; }

    public double? SapExpFlag { get; set; }

    public DateTime? SapExpDate { get; set; }

    public string? SapModiFlag { get; set; }

    public DateTime? SapModiDate { get; set; }

    public string? CountryCode { get; set; }

    public string? CustWebSite { get; set; }

    public string? CustTinNo { get; set; }

    public string? Enteredby { get; set; }

    public DateTime? Enteredon { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedon { get; set; }

    public decimal Isactive { get; set; }

    public string? SapCustomerCode { get; set; }

    public string? OmsInvToMailid { get; set; }

    public string? OmsInvCcMailid { get; set; }

    public string? OmsInvBccMailid { get; set; }

    public string? MktGroupCode { get; set; }

    public decimal? Industrysegmentid { get; set; }

    public decimal? Isaddressfrommktgroup { get; set; }

    public decimal? Isaddressfromcustomer { get; set; }

    public string? CrmsAccMgr { get; set; }

    public string? MktEmailAdd { get; set; }

    public bool DeliverReport { get; set; }
}
