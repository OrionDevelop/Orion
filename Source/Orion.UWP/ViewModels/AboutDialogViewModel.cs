using System.Collections.Generic;

using Microsoft.Toolkit.Uwp.Helpers;

using Orion.UWP.Models;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels
{
    public class AboutDialogViewModel : ViewModel
    {
        public string Name { get; }
        public string Version { get; }

        public List<Software> Softwares => new List<Software>
        {
            new Software {Name = "CoreTweet", Author = "CoreTweet Development Team", Url = "https://github.com/CoreTweet/CoreTweet"},
            new Software {Name = "CoreTweetSupplement", Author = "CoreTweet Development Team", Url = "https://github.com/CoreTweet/CoreTweetSupplement"},
            new Software {Name = "Emoji One", Author = "ranks.com", Url = "https://github.com/Ranks/emojione"},
            new Software {Name = "Html Agility Pack", Author = "Simon Mourrier, Jeff Klawiter, Stephan Grell", Url = "http://htmlagilitypack.codeplex.com/"},
            new Software {Name = "Newtonsoft.Json", Author = "James Newton-King", Url = "http://www.newtonsoft.com/json"},
            new Software {Name = "Prism", Author = "Brian Lagunas, Brian Noyes", Url = "https://github.com/PrismLibrary/Prism"},
            new Software {Name = "Reactive Extensions", Author = ".NET Foundation and Contributors", Url = "https://github.com/Reactive-Extensions/Rx.NET"},
            new Software {Name = "ReactiveProperty", Author = "neuecc xin9le okazuki", Url = "https://github.com/runceel/ReactiveProperty"},
            new Software {Name = "ToriatamaText", Author = "azyobuzin", Url = "https://github.com/azyobuzin/ToriatamaText"},
            new Software {Name = "UWP Community Toolkit", Author = "Microsoft", Url = "https://github.com/Microsoft/UWPCommunityToolkit"},
            new Software
            {
                Name = "WinRT XAML Toolkit for Windows 10",
                Author = "Filip Skakun, Thomas Martinsen, Mahmoud Moussa, Joost van Schaik",
                Url = "https://github.com/xyzzer/WinRTXamlToolkit"
            }
        };

        public AboutDialogViewModel()
        {
            Name = SystemInformation.ApplicationName;
            var version = SystemInformation.ApplicationVersion;
            Version = $"Version {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}