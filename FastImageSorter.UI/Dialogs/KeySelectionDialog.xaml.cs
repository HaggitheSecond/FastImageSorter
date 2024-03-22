using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace FastImageSorter.UI.UI.KeySelection
{

    public partial class KeySelectionView : Window
    {
        public Key? Key { get; set; }

        public DelegateCommand AcceptCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public KeySelectionView()
        {
            this.InitializeComponent();

            this.AcceptCommand = new DelegateCommand(this.Accept, this.CanAccept);
            this.CancelCommand = new DelegateCommand(this.Cancel);

            this.DataContext = this;
        }

        private void Cancel()
        {
            this.DialogResult = false;
            this.Close();
        }

        private bool CanAccept()
        {
            return this.Key != null;
        }

        private void Accept()
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.Key = e.Key;
            this.KeyDisplayRun.Text = e.Key.ToString();
        }
    }
}
