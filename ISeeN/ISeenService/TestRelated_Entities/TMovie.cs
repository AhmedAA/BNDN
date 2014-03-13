using System;

namespace ISeeN.TestRelated_Entities
{
    public class TMovie : IMedia
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string SomeMovieField { get; set; }
        public int SomethingElse { get; set; }
        public void Do()
        {
            throw new NotImplementedException();
        }
    }
}