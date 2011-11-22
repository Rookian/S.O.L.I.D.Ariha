namespace UserInterface.Models
{
    public class EmptyDropdownItem : IDropdownList
    {
        public int Id
        {
            get { return 0; }
            set { }
        }

        public string Text
        {
            get { return string.Empty; }
            set { }
        }
    }
}