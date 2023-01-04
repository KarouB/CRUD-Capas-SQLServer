using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Usuarios
{
    public class ClsUsuario
    {
        #region AtributosPrivados
        private byte _idUsuario;
        private string _Nombre, _Apellido1, _Apellido2;
        private DateTime _FechaNacimiento;
        private bool _Estado;

        //atributos de manejo de la base de datos
        private string _MensajeError, _ValorScalar;
        private DataTable _dtResultados;
        #endregion

        #region AtributosPublicos
        public byte IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Apellido1 { get => _Apellido1; set => _Apellido1 = value; }
        public string Apellido2 { get => _Apellido2; set => _Apellido2 = value; }
        public DateTime FechaNacimiento { get => _FechaNacimiento; set => _FechaNacimiento = value; }
        public bool Estado { get => _Estado; set => _Estado = value; }
        public string MensajeError { get => _MensajeError; set => _MensajeError = value; }
        public string ValorScalar { get => _ValorScalar; set => _ValorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }

        #endregion
    }
}
