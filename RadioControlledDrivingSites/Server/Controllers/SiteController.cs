using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadioControlledDrivingSites.Shared;
using RadioControlledDrivingSites.Server.DbContext;
using System.Web;
using RadioControlledDrivingSites.Server.Services;

namespace RadioControlledDrivingSites.Server.Controllers;



[Route("api/[controller]")]
[ApiController]
public class SiteController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ISiteService _siteService;

    public SiteController(DataContext dbContext, ISiteService siteService)
    {
        this._context = dbContext;
        _siteService = siteService;
    }

    /// <summary>
    /// Gets search suggestions by filtering Sites by their names.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("searchsuggestions/{str}")]
    public async Task<ActionResult<ServiceResponse<List<SiteDto>>>> GetSearchSuggestions(string str)
    {
        var results = await _context.Sites
            .Where(x => x.Name.ToLower().Contains(str.ToLower()))
            .ToListAsync();
        return Ok(new ServiceResponse<List<SiteDto>>() { Data = results });
    }

    /// <summary>
    /// Gets search results by filtering Sites by their names and description.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("search/{str}")]
    public async Task<ActionResult<ServiceResponse<List<SiteDto>>>> GetSearchResults(string str)
    {
        var results = await _context.Sites
            .Where(x => x.Name.ToLower().Contains(str.ToLower()) || x.Description.Contains(str.ToLower()))
            .ToListAsync();
        return Ok(new ServiceResponse<List<SiteDto>>() {Data = results});
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<SiteDto>>>> GetSites()
    {
        var returnee = new ServiceResponse<IEnumerable<SiteDto>>()
        {
            Data = _context.Sites.AsEnumerable()
        };
        return Ok(returnee);
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ServiceResponse<SiteDto>>> GetSite(int id)
    {
        var returnee = new ServiceResponse<SiteDto>()
        {
            Data = await _context.Sites
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync()
        };
        return Ok(returnee);
    }


    [HttpPost]
    [Route("")]
    public async Task<ActionResult<ServiceResponse<SiteDto>>> CreateSite(SiteDto site)
    {
        _context.Add(site);
        await _context.SaveChangesAsync();
        return Ok(site);
    }

    [HttpPut]
    [Route("")]
    public async Task<ActionResult<ServiceResponse<SiteDto>>> PutSite(SiteDto site)
    {
        try
        {
            var siteFromDb = await _context.Sites
                .Where(x => x.Id == site.Id)
                .FirstAsync();
            siteFromDb.Name = site.Name;
            siteFromDb.Description = site.Description;
            siteFromDb.Location = site.Location;
            siteFromDb.LongitudeLattitude = site.LongitudeLattitude;
            siteFromDb.Environment = site.Environment;
            _context.SaveChanges();
            return Ok(new ServiceResponse<SiteDto>() { Data = siteFromDb });
        }
        catch (InvalidOperationException e)
        {
            return this.BadRequest(new ServiceResponse<SiteDto>() { Success = false, Message = "Epäkelpo Id ajopaikalle." });
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteSite(int id)
    {
        var site = new SiteDto() { Id = id };
        _context.Sites.Attach(site);
        _context.Sites.Remove(site);
        _context.SaveChanges();
        return Ok(new ServiceResponse<bool>() { Success = true, Message = "Deleted." });
    }
}
