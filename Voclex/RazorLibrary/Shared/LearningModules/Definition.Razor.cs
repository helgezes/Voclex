using Shared.DataTransferObjects;

namespace RazorLibrary.Shared.LearningModules
{
    public partial class Definition : LearningModule<DefinitionDto>
    {
        protected override string ApiPath => "Definitions/GetList";
    }
}
