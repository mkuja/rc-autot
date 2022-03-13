using System.Globalization;
using System.Web;
using System.Xml.Serialization;
using RadioControlledDrivingSites.Client.Services;
using RadioControlledDrivingSites.Shared;

namespace RadioControlledDrivingSites.Client;

public class SiteDtoToMapMarkersConverter
{
    private readonly SiteDto _site;

    private decimal lng
    {
        get
        {
            var tmp = _site.LongitudeLattitude;
            tmp = tmp.Split(",")[1].Trim();
            Console.WriteLine($"lng: {tmp}");
            var culture = new CultureInfo("fi-FI");
            culture.NumberFormat.NumberDecimalSeparator = ".";
            return Convert.ToDecimal(tmp, culture);
        }
    }

    private decimal lat
    {
        get
        {
            var tmp = _site.LongitudeLattitude;
            tmp = tmp.Split(",")[0].Trim();
            Console.WriteLine($"lat: {tmp}");
            var culture = new CultureInfo("fi-FI");
            culture.NumberFormat.NumberDecimalSeparator = ".";
            return Convert.ToDecimal(tmp, culture);
        }
    }

    public string infoWindowContent { get; set; }

    public Coordinates position
    {
        get
        {
            var retVal = new Coordinates() {lng = lng, lat = lat};
            return retVal;
        }
    }

    public string title
    {
        get
        {
            return _site.Name;
        }
    }

    public SiteDtoToMapMarkersConverter(SiteDto site)
    {
        _site = site;
        site.Description = HttpUtility.HtmlEncode(site.Description);
        site.Name = HttpUtility.HtmlEncode(site.Name);
        site.Environment = HttpUtility.HtmlEncode(site.Environment);
        site.Location = HttpUtility.HtmlEncode(site.Location);
        infoWindowContent = $"<h5>{site.Name}</h5><p>{site.Description}</p>";
    }

}

public class Coordinates
{
    public decimal lat { get; set; }
    public decimal lng { get; set; }
}
