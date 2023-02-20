namespace TheMaxt.MapData
{
    internal class Column
    {
        public string Ctype { get; set; }
        public string Name { get; set; }

        public Column(string ctype, string name)
        {
            Ctype = ctype;
            Name = name;
        }
    }
}
