// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace AutomatedCar
{
    using System;
    using Avalonia.Controls;
    using Avalonia.Controls.Templates;
    using ViewModels;

    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }

        Control ITemplate<object, Control>.Build(object param)
        {
            var name = param.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type);
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }
    }
}