using Shared.DataTransferObjects;

namespace RazorLibrary.Shared.LearningModules
{
    public partial class Examples : LearningModule<ExampleDto>
    {
        protected override string ApiPath => "Examples/GetList";

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }
    }
}
