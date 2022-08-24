using SQLite;

namespace AssignmentApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }
        public string Sex { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string ProfilePic { get; set; }
        public string ImagePublicUrl { get; set; }
        public System.DateTime DateCreted { get; set; }
        public System.DateTime DateUpdated { get; set; }
    }
}
