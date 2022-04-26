using Supabase;
using Postgrest.Attributes;

namespace AvaloniaDatabase.Model
{
    public class Students : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("age")]
        public int Age { get; set; } = 0;
    }
}
