namespace Turquoise.Models.Mongo
{
    public class Label
    {
        public Label()
        {

        }

        public Label(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}