using AccesoDatos.DataBase;
using Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Usuarios
{
    public class ClsUsuarioLN
    {
        #region VariablesPrivadas
        private clsDataBase objDataBase = null;
        #endregion

        #region MetodoIndex
        //busca informacion y la trae para mostrar en pantalla
        public void Index(ref ClsUsuario objUsuario)
        {
            objDataBase = new clsDataBase() 
            { 
                NombreTable = "Usuarios",
                NombreSP = "[SCH_GENERAL].[SP_USUARIOS_Index]",
                Scalar = false
            };
            Ejecutar(ref objUsuario);
        }

        #endregion

        #region CrudUsuario
        public void Create(ref ClsUsuario objUsuario)
        {
            objDataBase = new clsDataBase()
            {
                NombreTable = "Usuarios",
                NombreSP = "[SCH_GENERAL].[SP_USUARIOS_Create]",
                Scalar = true
            };
            objDataBase.DtParametros.Rows.Add(@"@Nombre", "17", objUsuario.Nombre);
            objDataBase.DtParametros.Rows.Add(@"@Apellido1", "17", objUsuario.Apellido1);
            objDataBase.DtParametros.Rows.Add(@"@Apellido2", "17", objUsuario.Apellido2);
            objDataBase.DtParametros.Rows.Add(@"@FechaNacimiento", "13", objUsuario.FechaNacimiento);
            objDataBase.DtParametros.Rows.Add(@"@Estado", "1", objUsuario.Estado);

            Ejecutar(ref objUsuario);
        }
        public void Read(ref ClsUsuario objUsuario)
        {
            objDataBase = new clsDataBase()
            {
                NombreTable = "Usuarios",
                NombreSP = "[SCH_GENERAL].[SP_USUARIOS_Read]",
                Scalar = false
            };
            objDataBase.DtParametros.Rows.Add(@"@IdUsuario", "2", objUsuario.IdUsuario);

            Ejecutar(ref objUsuario);
        }
        public void Update(ref ClsUsuario objUsuario)
        {
            objDataBase = new clsDataBase()
            {
                NombreTable = "Usuarios",
                NombreSP = "[SCH_GENERAL].[SP_USUARIOS_Update]",
                Scalar = true
            };

            objDataBase.DtParametros.Rows.Add(@"@IdUsuario", "2", objUsuario.IdUsuario);
            objDataBase.DtParametros.Rows.Add(@"@Nombre", "17", objUsuario.Nombre);
            objDataBase.DtParametros.Rows.Add(@"@Apellido1", "17", objUsuario.Apellido1);
            objDataBase.DtParametros.Rows.Add(@"@Apellido2", "17", objUsuario.Apellido2);
            objDataBase.DtParametros.Rows.Add(@"@FechaNacimiento", "13", objUsuario.FechaNacimiento);
            objDataBase.DtParametros.Rows.Add(@"@Estado", "1", objUsuario.Estado);


            Ejecutar(ref objUsuario);
        }
        public void Delete(ref ClsUsuario objUsuario)
        {
            objDataBase = new clsDataBase()
            {
                NombreTable = "Usuarios",
                NombreSP = "[SCH_GENERAL].[SP_USUARIOS_Delete]",
                Scalar = true
            };
            objDataBase.DtParametros.Rows.Add(@"@IdUsuario", "2", objUsuario.IdUsuario);

            Ejecutar(ref objUsuario);
        }
        #endregion

        #region MetodosPrivados
        private void Ejecutar(ref ClsUsuario objUsuario)
        {
            objDataBase.Crud(ref objDataBase);

            if (objDataBase.MensajeErrorDB == null)
            {
                if (objDataBase.Scalar)
                {
                    objUsuario.ValorScalar = objDataBase.ValorScalar;
                }
                else
                {
                    objUsuario.DtResultados = objDataBase.DsResultados.Tables[0];
                    if (objUsuario.DtResultados.Rows.Count == 1)
                    {
                        foreach (DataRow item in objUsuario.DtResultados.Rows)
                        {
                            objUsuario.IdUsuario = Convert.ToByte(item["IdUsuario"].ToString());
                            objUsuario.Nombre = item["Nombre"].ToString();
                            objUsuario.Apellido1 = item["Apellido1"].ToString();
                            objUsuario.Apellido2 = item["Apellido2"].ToString();
                            objUsuario.FechaNacimiento = Convert.ToDateTime(item["FechaNacimiento"].ToString());
                            objUsuario.Estado = Convert.ToBoolean(item["Estado"].ToString());
                        }
                    }
                }
            }
            else
            {
                objUsuario.MensajeError = objDataBase.MensajeErrorDB;
            }
        }
        #endregion


    }
}
