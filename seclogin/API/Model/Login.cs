using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace API.Model
{
    public class Login
    {
        public string Benutzername { get; set; }
        public string Passwort { get; set; }
    }
}