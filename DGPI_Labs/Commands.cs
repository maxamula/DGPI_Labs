using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace DGPI_Labs
{

    public class OpenCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
    {
            return true;
        }

        public void Execute(object parameter)
    {
            if (parameter is System.Windows.Controls.TextBox textBox)
        {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
            {
                    textBox.Text = System.IO.File.ReadAllText(openFileDialog.FileName);
                }
            }
        }
    }

    public class CleanCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is System.Windows.Controls.TextBox textBox)
            {
                textBox.Text = "";
            }
        }
    }

    public class CopyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string text = parameter as string;
            if (!string.IsNullOrEmpty(text))
            {
                System.Windows.Clipboard.SetText(text);
            }
        }
    }

    public class PasteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (System.Windows.Clipboard.ContainsText())
            {
                string text = System.Windows.Clipboard.GetText();

                if (parameter is System.Windows.Controls.TextBox textBox)
                {
                    textBox.Text += text;
                }
            }
        }
    }
}
