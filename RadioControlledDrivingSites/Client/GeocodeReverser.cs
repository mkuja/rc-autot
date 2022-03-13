using System.Globalization;
using System.Net.Http.Json;
using Geocoding;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Geocoding.Google;

namespace RadioControlledDrivingSites.Client;


public class GeocodeReverser
{
    private IGeocoder _gls;

    public GeocodeReverser()
    { 
        _gls = new GoogleGeocoder() { ApiKey = "AIzaSyBS_mhlJqhGADHqNgpaRkpYrxIXKfq8c8k" };
    }

    public async Task<IEnumerable<Address>> GetAddressData(double lattitude, double longitude)
    {
        return await _gls.ReverseGeocodeAsync(lattitude, longitude);
    }

}
