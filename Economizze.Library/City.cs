namespace Economizze.Library
{
    public class City
    {
        public short CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string CityNameAscii { get; set; } = string.Empty;
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public short? StateId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
