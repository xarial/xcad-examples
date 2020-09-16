namespace CascadingComboBox
{
    public class FolderItem
    {
        public string Path { get; }

        public FolderItem(string path)
        {
            Path = path;
        }

        public override string ToString()
        {
            return System.IO.Path.GetFileName(Path);
        }
    }
}
