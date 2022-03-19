namespace TrainRezervationProject.Model
{
    public class Rezervation
    {
        public Train Train { get; set; }
        public int RezervationLimit { get; set; }
        public bool DifferentVagonAssignment { get; set; }
    }
}
