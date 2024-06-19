using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Model
{
    [Table("tbl_UserCredentials")]
    public class UserCredential
    {
        [Key]
        [ReadOnly(true)]
        public Guid? pk_UserCredential {  get; set; }
        public string Email { get; set; }
        public string Passwort { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
    }
}
