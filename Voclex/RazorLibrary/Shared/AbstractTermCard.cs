using Microsoft.AspNetCore.Components;
using RazorLibrary.Shared.LearningModules;

namespace RazorLibrary.Shared
{
    public abstract class AbstractTermCard : ComponentBase
    {
        private Dictionary<Type, bool>? loadedModules;

        [Parameter, EditorRequired]
        public string CurrentTerm { get; set; } = null!;

        [Parameter, EditorRequired]
        public IDictionary<string, object> LearningModuleParameters { get; set; } = null!;

        [Parameter]
        public EventCallback<OnTermCardLoadingEventArgs> OnLoading { get; set; }

        protected bool AllModulesLoaded { get; private set; } = false;

        protected abstract Type[] LoadableModules { get; }

        private Dictionary<Type, bool> LoadedModules
        {
            get
            {
                return loadedModules ??= LoadableModules.ToDictionary(l => l, _ => false); //todo concurrent dict?
            }
        }

        protected IDictionary<string, object> GetLearningModuleParameters()
        {
            LearningModuleParameters.TryAdd("OnLoading", EventCallback.Factory.Create<OnLearningModuleLoadingEventArgs>(this, OnModuleLoading));
            return LearningModuleParameters;
        }

        private void OnModuleLoading(OnLearningModuleLoadingEventArgs args)
        {
            LoadedModules[args.Type] = args.IsLoadingFinished;

            bool allModulesLoaded;

            if (args.IsLoadingStarted)
            {
                allModulesLoaded = false;
            }
            else
            {
                allModulesLoaded = LoadedModules.All(m => m.Value);
            }

            if (AllModulesLoaded != allModulesLoaded) // if the state had changed
            {
                OnLoading.InvokeAsync(new OnTermCardLoadingEventArgs(allModulesLoaded));
            }

            AllModulesLoaded = allModulesLoaded;
        }
    }


    public sealed class OnTermCardLoadingEventArgs
    {
        public OnTermCardLoadingEventArgs(bool isLoadingFinished)
        {
            IsLoadingFinished = isLoadingFinished;
        }
        
        public bool IsLoadingFinished { get; init; }
    }
}
