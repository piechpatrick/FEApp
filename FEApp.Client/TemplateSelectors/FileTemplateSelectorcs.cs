using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FEApp.Client.TemplateSelectors
{
    public class FileTemplateSelectorcs : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            Models.FileContent file = item as Models.FileContent;
            ContentPresenter pres = container as ContentPresenter;
            DataTemplate dataTemplate;

            if (file is Models.ImageFileContent)
                dataTemplate = pres.FindResource("ImageTemplate") as DataTemplate;
            else if (file is Models.TextFileContent)
                dataTemplate = pres.FindResource("TextTemplate") as DataTemplate;
            else
                dataTemplate = pres.FindResource("SimpleTemplate") as DataTemplate;

            return dataTemplate;
        }
    }
}
