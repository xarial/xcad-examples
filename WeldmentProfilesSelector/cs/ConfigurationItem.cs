namespace CascadingComboBox
{
    public class ConfigurationItem
    {
        public string Name { get; }

        public ConfigurationItem(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
