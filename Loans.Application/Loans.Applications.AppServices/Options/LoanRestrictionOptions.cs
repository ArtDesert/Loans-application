
namespace Loans.Application.AppServices.Options;

public class LoanRestrictionOptions
{
	public int MinimumSalary { get; set; }
    public int MaximumSalary { get; set; }
    public int MinimumAmount { get; set; }
    public int MaximumAmount { get; set; }
    public int MinimumPeriod { get; set; }
    public int MaximumPeriod { get; set; }
}
