using RadioControlledDrivingSites.Shared;

namespace RadioControlledDrivingSites.Client;

public class MapInitializer
{
    public Coordinates center { get; set; }
    public int zoom { get; set; }

    public MapInitializer(decimal lat, decimal lng, int zoom = 6)
    {
        center = new Coordinates()
        {
            lat = lat,
            lng = lng
        };
        this.zoom = zoom;
    }
}
