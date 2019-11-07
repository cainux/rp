using System.Collections.Generic;

namespace RP.Core.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public IList<Photo> Photos { get; } = new List<Photo>();
    }
}
