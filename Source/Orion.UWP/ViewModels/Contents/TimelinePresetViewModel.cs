using Orion.Shared.Models;
using Orion.UWP.Extensions;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class TimelinePresetViewModel : ViewModel
    {
        public TimelinePreset TimelinePreset { get; }
        public string Name => TimelinePreset.Name;
        public string Icon => TimelinePreset.ToIcon();
        public bool IsEditable => TimelinePreset.IsEditable;

        public TimelinePresetViewModel(TimelinePreset preset)
        {
            TimelinePreset = preset;
        }
    }
}