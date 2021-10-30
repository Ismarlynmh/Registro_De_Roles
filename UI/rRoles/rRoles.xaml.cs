using Registro_De_Roles.BLL;
using Registro_De_Roles.Entidades;
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
using System.Windows.Shapes;

namespace Registro_De_Roles.UI.rRoles
{
    /// <summary>
    /// Lógica de interacción para rRoles.xaml
    /// </summary>
    public partial class rRoles : Window


    {

        private Roles Rol;
        private Permisos permiso;

        public rRoles()
        {
            InitializeComponent();
            Rol = new Roles();
            this.DataContext = Rol;
            PermisoIdComboBox.ItemsSource = PermisosBLL.GetList(c => true);
            PermisoIdComboBox.DisplayMemberPath = "Nombre";
            PermisoIdComboBox.SelectedValuePath = "PermisoId";
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var registro = RolesBLL.Buscar(Rol.RolId);
            if (registro != null)
            {
                Rol = registro;
                this.DataContext = Rol;
            }
            else
            {
                MessageBox.Show("No se encontro el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAgregar_Click(object sender, RoutedEventArgs e)
        {
            var detalle = new DetalleRoles
            {
                PermisoId = int.Parse(PermisoIdComboBox.SelectedValue.ToString())
            };

            detalle.Permisos = permiso;
            Rol.Detalle.Add(detalle);
            PermisosBLL.Incrementar(permiso.PermisoId);
            Cargar();
            PermisoIdComboBox.SelectedIndex = -1;
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.SelectedIndex != -1)
            {
                Rol.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                DetalleRoles aux = (DetalleRoles)DetalleDataGrid.SelectedItem;
                PermisosBLL.Decrementar(aux.Permisos.PermisoId);
                Cargar();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (RolesBLL.Guardar(Rol))
            {
                MessageBox.Show("Guardado", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se logro guardar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (RolesBLL.Eliminar(Rol.RolId))
            {
                MessageBox.Show("Elimando", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se logro eliminar", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PermisoIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PermisoIdComboBox.SelectedIndex != -1)
            {
                permiso = (Permisos)PermisoIdComboBox.SelectedItem;
            }
        }

        private void PermisoIdComboBox_DropDownOpened(object sender, EventArgs e)
        {
            PermisoIdComboBox.ItemsSource = PermisosBLL.GetList(c => true);
        }
        private void Limpiar()
        {
            Rol = new Roles();
            this.DataContext = Rol;
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = Rol;
        }
    }

}
