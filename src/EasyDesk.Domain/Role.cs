using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyDesk.Domain;

public class Role : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<UserRole> Users { get; set; } = new List<UserRole>();
}