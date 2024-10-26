namespace Economizze.Library
{
    public class Neighborhood
    {
        public int NeighborhoodId { get; set; }
        public string NeighborhoodName { get; set; } = string.Empty;
        public string NeighborhoodNameAscii { get; set; } = string.Empty;
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public short? CityId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
