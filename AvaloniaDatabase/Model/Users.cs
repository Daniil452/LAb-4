using Supabase;
using Postgrest.Attributes;
namespace AvaloniaDatabase.Model
{
    public class Users : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("name")]
        public string name { get; set; } = string.Empty;

        [Column("text")]
        public string text { get; set; } = string.Empty;
    }
}
