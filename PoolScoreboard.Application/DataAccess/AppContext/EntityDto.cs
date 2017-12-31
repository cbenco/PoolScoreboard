using System.ComponentModel.DataAnnotations;

namespace PoolScoreboard.Application.DataAccess.AppContext
{
    public class EntityDto
    {
        [Key]
        public int? Id { get; set; }
    }
}