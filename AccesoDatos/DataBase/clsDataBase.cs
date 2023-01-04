using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.DataBase
{
    public class clsDataBase
    {

        #region VariablesPrivadas
        private SqlConnection _objSqlConnection;
        private SqlDataAdapter _objSqlDataAdapter;
        private SqlCommand _objSqlCommand;
        private DataSet _dsResultados;
        private DataTable _dtParametros;
        private string _NombreTable, _NombreSP, _MensajeErrorDB, _ValorScalar, _NombreDB;
        private bool _scalar;
        #endregion

        #region VariablesPublicas
        public SqlConnection ObjSqlConnection { get => _objSqlConnection; set => _objSqlConnection = value; }
        public SqlDataAdapter ObjSqlDataAdapter { get => _objSqlDataAdapter; set => _objSqlDataAdapter = value; }
        public SqlCommand ObjSqlCommand { get => _objSqlCommand; set => _objSqlCommand = value; }
        public DataSet DsResultados { get => _dsResultados; set => _dsResultados = value; }
        public DataTable DtParametros { get => _dtParametros; set => _dtParametros = value; }
        public string NombreTable { get => _NombreTable; set => _NombreTable = value; }
        public string NombreSP { get => _NombreSP; set => _NombreSP = value; }
        public string MensajeErrorDB { get => _MensajeErrorDB; set => _MensajeErrorDB = value; }
        public string ValorScalar { get => _ValorScalar; set => _ValorScalar = value; }
        public string NombreDB { get => _NombreDB; set => _NombreDB = value; }
        public bool Scalar { get => _scalar; set => _scalar = value; }
        #endregion

        #region Constructores
        public clsDataBase()
        {
            DtParametros = new DataTable("spParametros");
            DtParametros.Columns.Add("Nombre");
            DtParametros.Columns.Add("TipoDato");
            DtParametros.Columns.Add("Valor");

            NombreDB = "Practica";
        }
        #endregion

        #region MetodosPrivados
        private void CrearConexionDB(ref clsDataBase objDataBase)
        {
            switch (objDataBase.NombreDB)
            {
                case "Practica":
                    objDataBase.ObjSqlConnection = new SqlConnection(Properties.Settings.Default.CadenaConexion_Practicas);
                    break;

                default:
                    break;
            }
        }

        private void ValidarConexionDB(ref clsDataBase objDataBase)
        {
            if (objDataBase.ObjSqlConnection.State == ConnectionState.Closed)
            {
                objDataBase.ObjSqlConnection.Open();
            } else
            {
                objDataBase.ObjSqlConnection.Close();
                objDataBase.ObjSqlConnection.Dispose();
            }
        }

        private void AgregarParametros(ref clsDataBase objDataBase)
        {
            if (objDataBase.DtParametros != null)
            {
                SqlDbType TipoDatoSQL = new SqlDbType();

                foreach (DataRow item in objDataBase.DtParametros.Rows)
                {
                    switch (item[1])
                    {
                        case "1":
                            TipoDatoSQL = SqlDbType.Bit;
                            break;
                        case "2":
                            TipoDatoSQL = SqlDbType.TinyInt;
                            break;
                        case "3":
                            TipoDatoSQL = SqlDbType.SmallInt;
                            break;
                        case "4":
                            TipoDatoSQL = SqlDbType.Int;
                            break;
                        case "5":
                            TipoDatoSQL = SqlDbType.BigInt;
                            break;
                        case "6":
                            TipoDatoSQL = SqlDbType.Decimal;
                            break;
                        case "7":
                            TipoDatoSQL = SqlDbType.SmallMoney;
                            break;
                        case "8":
                            TipoDatoSQL = SqlDbType.Money;
                            break;
                        case "9":
                            TipoDatoSQL = SqlDbType.Float;
                            break;
                        case "10":
                            TipoDatoSQL = SqlDbType.Real;
                            break;
                        case "11":
                            TipoDatoSQL = SqlDbType.Date;
                            break;
                        case "12":
                            TipoDatoSQL = SqlDbType.Time;
                            break;
                        case "13":
                            TipoDatoSQL = SqlDbType.SmallDateTime;
                            break;
                        case "14":
                            TipoDatoSQL = SqlDbType.DateTime;
                            break;
                        case "15":
                            TipoDatoSQL = SqlDbType.Char;
                            break;
                        case "16":
                            TipoDatoSQL = SqlDbType.NChar;
                            break;
                        case "17":
                            TipoDatoSQL = SqlDbType.VarChar;
                            break;
                        case "18":
                            TipoDatoSQL = SqlDbType.NVarChar;
                            break;
                        default:
                            break;
                    }

                    if (objDataBase.Scalar)
                    {
                        if (item[2].ToString().Equals(string.Empty))
                        {
                            objDataBase.ObjSqlCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = DBNull.Value;

                        } else
                        {
                            objDataBase.ObjSqlCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = item[2].ToString();
                        }
                    } else
                    {
                        if (item[2].ToString().Equals(string.Empty))
                        {
                            objDataBase.ObjSqlDataAdapter.SelectCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = DBNull.Value;

                        }
                        else
                        {
                            objDataBase.ObjSqlDataAdapter.SelectCommand.Parameters.Add(item[0].ToString(), TipoDatoSQL).Value = item[2].ToString();
                        }
                    }

                }
            }
            

            
        }

        private void PrepararConexionDB(ref clsDataBase objDataBase)
        {
            CrearConexionDB(ref objDataBase);
            ValidarConexionDB(ref objDataBase);
        }

        private void EjecutarDA(ref clsDataBase objDataBase)
        {
            try
            {
                PrepararConexionDB(ref objDataBase);

                objDataBase.ObjSqlDataAdapter = new SqlDataAdapter(objDataBase.NombreSP, ObjSqlConnection);
                objDataBase.ObjSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                AgregarParametros(ref objDataBase);
                objDataBase.DsResultados = new DataSet();
                objDataBase.ObjSqlDataAdapter.Fill(objDataBase.DsResultados, objDataBase.NombreTable);
            }
            catch (Exception ex)
            {
                objDataBase.MensajeErrorDB = ex.Message.ToString();

            } 
            finally
            {
                if (objDataBase.ObjSqlConnection.State == ConnectionState.Open)
                {
                    ValidarConexionDB(ref objDataBase);
                }
            }
        }

        private void EjecutarCommand(ref clsDataBase objDataBase)
        {
            try
            {
                PrepararConexionDB(ref objDataBase);
                objDataBase.ObjSqlCommand = new SqlCommand(objDataBase.NombreSP, ObjSqlConnection);
                objDataBase.ObjSqlCommand.CommandType = CommandType.StoredProcedure;
                AgregarParametros(ref objDataBase);

                if (objDataBase.Scalar)
                {
                    
                    objDataBase.ValorScalar = objDataBase.ObjSqlCommand.ExecuteScalar().ToString().Trim();

                } else
                {
                    objDataBase.ObjSqlCommand.ExecuteNonQuery();
                }


            }
            catch (Exception exc)
            {
                objDataBase.MensajeErrorDB = exc.Message.ToString();
            } 
            finally
            {
                if (objDataBase.ObjSqlConnection.State == ConnectionState.Open)
                {
                    ValidarConexionDB(ref objDataBase);
                }
            }
        }
        #endregion

        #region MetodosPublicos

        //este es el unico metodo que va a tener negocios para insertar, eliminar datos
        public void Crud(ref clsDataBase objDataBase)
        {
            if (objDataBase.Scalar)
            {
                EjecutarCommand(ref objDataBase);
            } 
            else
            {
                EjecutarDA(ref objDataBase);
            }
        }

        #endregion

    }
}
