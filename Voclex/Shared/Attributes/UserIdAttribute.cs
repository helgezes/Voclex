using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Binders;

namespace SharedLibrary.Attributes
{
    public class UserIdAttribute : ModelBinderAttribute
    {
        public UserIdAttribute() : base(typeof(UserIdBinder))
        { }
    }
}
