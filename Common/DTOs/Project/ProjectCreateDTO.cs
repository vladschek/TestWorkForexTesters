
namespace Common.DTOs
{
    public class ProjectCreateDTO
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public List<ChartDTO> Charts { get; set; }
    }
}
