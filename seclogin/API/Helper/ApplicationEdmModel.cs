using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace API.Helper
{
    public class ApplicationEdmModel
    {
        public static IEdmModel GetEdmModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EnableLowerCamelCase();

            modelBuilder.EntitySet<UserCredential>("UserCredential");
            return modelBuilder.GetEdmModel();
        }
    }
}
