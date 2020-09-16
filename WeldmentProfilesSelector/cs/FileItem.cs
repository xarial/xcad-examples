namespace CascadingComboBox
{
    public class FileItem
    {
        public string Path { get; }

        public FileItem(string path)
        {
            Path = path;
        }

        public override string ToString()
        {
            return System.IO.Path.GetFileNameWithoutExtension(Path);
        }
    }
}
