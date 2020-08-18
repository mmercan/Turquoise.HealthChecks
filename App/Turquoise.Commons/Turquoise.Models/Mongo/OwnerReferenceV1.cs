namespace Turquoise.Models.Mongo
{
    public class OwnerReferenceV1
    {
        public bool BlockOwnerDeletion { get; set; }
        public bool Controller { get; set; }
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Uid { get; set; }
    }
}