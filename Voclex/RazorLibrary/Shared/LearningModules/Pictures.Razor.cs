using SharedLibrary.DataTransferObjects;

namespace RazorLibrary.Shared.LearningModules
{
    public partial class Pictures : LearningModule<PictureDto>
    {
        private string pictureHost;

        protected override string ApiPath => "Pictures/GetList";

        protected override Task OnInitializedAsync()
        {
            pictureHost = AppHttpClient.ApiClient.BaseAddress.AbsoluteUri.TrimEnd('/');
            return base.OnInitializedAsync();
        }
    }
}
