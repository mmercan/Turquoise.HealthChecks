namespace Turquoise.Models.RabbitMQ
{
    public class DeploymentScalerMessager
    {
        public int? ReplicaNumber { get; set; }
        public string Schedule { get; set; }
        public string Timezone { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string nameSpace { get; set; }
        public ScaleUpDown ScaleUpDown { get; set; }

        public string Status { get; set; }
    }

    public enum ScaleUpDown
    {
        ScaleUp,
        ScaleDown
    }
}