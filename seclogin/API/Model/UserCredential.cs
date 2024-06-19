using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Model
{
    [Table("tbl_UserCredentials")]
    public class UserCredential
    {
        [ReadOnly(true)]
        public Guid? pk_UserCredential {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
    }
}
