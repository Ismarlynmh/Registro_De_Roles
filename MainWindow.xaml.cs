using Registro_De_Roles.UI.rPermisos;
using Registro_De_Roles.UI.rRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Registro_De_Roles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //new rPermisos().Show(); ESTA ES OTRA FORMA DE COLOCARLO   :v :)
               rPermisos rPermisos = new rPermisos();    
               rPermisos.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            rRoles rRoles = new rRoles();
           rRoles.Show();
        }
    }
}
