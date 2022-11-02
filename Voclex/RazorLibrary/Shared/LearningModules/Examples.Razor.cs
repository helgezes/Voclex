using SharedLibrary.DataTransferObjects;

namespace RazorLibrary.Shared.LearningModules
{
    public partial class Examples : LearningModule<ExampleDto>
    {
        protected override string ApiPath => "Examples/GetList";
    }
}
