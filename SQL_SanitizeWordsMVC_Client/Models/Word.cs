using System.ComponentModel.DataAnnotations;

namespace SQL_SanitizeWordsMVC_Client.Models
{
    public class Word
    {
        [Key]
        public int id { get; set; }
        public string value { get; set; }
    }
}