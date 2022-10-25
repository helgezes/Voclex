using Application.Models;
using Application.Services;
using Shared.DataTransferObjects;

namespace WebApi.Controllers
{
    public class PicturesController : GenericTermsRelatedController<Picture, PictureDto>
    {
        public PicturesController(TermRelatedService<Picture, PictureDto> service) : 
            base(service)
        { }
    }
}
