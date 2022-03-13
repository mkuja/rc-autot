using RadioControlledDrivingSites.Shared;
using System.Net.Http.Json;
using System.Web;

namespace RadioControlledDrivingSites.Client.Services;

public class SiteService : ISiteService
{
    private readonly HttpClient _httpClient;

    public SiteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ServiceResponse<List<SiteDto>>> GetAllSites()
    {
        var tmp = await _httpClient.GetFromJsonAsync<ServiceResponse<List<SiteDto>>>("api/Site");
        if (tmp != null && tmp.Success)
        {
            return tmp;
        }
        else
        {
            var errorMessage = "Something went wrong with getting the sites from the database :(";
            return new ServiceResponse<List<SiteDto>>() {Success = false, Message = errorMessage};
        }

    }
    

}