namespace TrainRezervationProject.Model
{
    public class Train
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Vagon[] Vagons { get; set; }
    }
}
