using System.Collections.Generic;

namespace Application.ApplicationMenus
{
    public record ApplicationMenuDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<ApplicationMenuDto> ChildMenus { get; set; }
    }
}
