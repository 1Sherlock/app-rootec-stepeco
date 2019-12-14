namespace Stepeco.Controllers.DAL.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public double Temperature { get; set; }
        public double Quality { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
    }
}