using BD_Usuarios.Models;
using BD_Usuarios.Repositories;
using BD_Usuarios.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BD_Usuarios.ViewModels
{
    class UsuariosViewModels : INotifyPropertyChanged
    {

        UserRepository UsrRep = new UserRepository();
        UsrLoggedRepository UsrLogRep = new UsrLoggedRepository();

        public ICommand VerRegistrarCommand { get; set; }
        public ICommand RegistrarUsrCommand { get; set; }
        public ICommand VerLoggedCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand VerEditarCommand { get; set; }

        public UsuariosViewModels()
        {
            //Usr = new ObservableCollection<Usuario>(UsrRep.GetAll());
            Usuario = new Usuario();

            VerRegistrarCommand = new RelayCommand(VerRegist);
            VerLoggedCommand = new RelayCommand<Usuario>(Loggin);
            VerEditarCommand = new RelayCommand<Usuario>(VerEditar);
            GuardarCommand = new RelayCommand<Usuario>(Guardar);
            RegistrarUsrCommand = new RelayCommand<Usuario>(RegistUsr);

        }

        private ObservableCollection<Usuario> usr;
        public ObservableCollection<Usuario> Usr
        {
            get { return usr; }
            set { usr = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Usuarios")); }
        }


        //Views
        Registrar RegistView = new Registrar();
        Editar EditView = new Editar();
        Administrar AdministView = new Administrar();


        private Usuario usuario;
        public Usuario Usuario
        {
            get { return usuario; }

            set
            {
                usuario = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Usuario"));
            }
        }

        

        //ExecCommands
        void RegistUsr(Usuario Usr)
            {
            Error = "";
            try
            {
                if (UsrRep.Validate(usuario))
                {
                    UsrRep.insert(usuario);
                    RegistView.Close();
                }
            }

            catch (Exception ex)
            {
                Error = ex.Message;
            }

            Usuario = new Usuario();

        }

        private void Guardar(Usuario Usr)
        {
            UsrRep.Update(usuario);
            EditView.Close();
        }

        

        //Entrar Paginas
        void VerRegist()
        {
            RegistView = new Registrar();
            Usuario = new Usuario();
            RegistView.DataContext = this;
            RegistView.ShowDialog();
        }

        void Loggin(Usuario U)
        {

            Error = "";
            try
            {
                if(UsrLogRep.ValidarLogin(Usuario))
                {
                    UsrLogRep.Login(Usuario);
                    AdministView = new();
                    AdministView.DataContext = this;
                    AdministView.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

        }

        void VerEditar(Usuario U)
        {
            U = UsrRep.GetByEmail(U);
            Error = "";
            try
            {
                if (U != null)
                {

                    Usuario Copy = new Usuario()
                    {
                        Id = U.Id,
                        Nombre = U.Nombre,
                        EMail = U.EMail,
                        Direccion = U.Direccion,
                        Telefono = U.Telefono,
                        Contraseña = null
                    };
                    Usuario = Copy;

                    EditView = new Editar();
                    EditView.DataContext = this;
                    EditView.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                Error = ex.Message;
            }
        }


        //Error
        private string error;
        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Error"));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
