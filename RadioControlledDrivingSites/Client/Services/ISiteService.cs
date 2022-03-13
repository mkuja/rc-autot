using RadioControlledDrivingSites.Shared;

namespace RadioControlledDrivingSites.Client.Services;

public interface ISiteService
{
    Task<ServiceResponse<List<SiteDto>>> GetAllSites();
}