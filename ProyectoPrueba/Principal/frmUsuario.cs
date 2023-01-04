using Entidades.Usuarios;
using LogicaNegocio.Usuarios;
using ProyectoPrueba.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPrueba.Principal
{
    public partial class frmUsuario : Form
    {
        private ClsUsuario objUsuario = null;
        private readonly ClsUsuarioLN objUsuarioLn = new ClsUsuarioLN();
        private ClsUtilidades objUtilidades = new ClsUtilidades();

        public frmUsuario()
        {
            InitializeComponent();
            CargasListaUsuarios();
            BlanquearCampos();
        }
        private void ValidarCampos()
        {
            objUtilidades = new ClsUtilidades()
            {
               LstTextBox = new System.Collections.Generic.List<TextBox>() 
            };

            objUtilidades.LstTextBox.Add(txtNombre);
            objUtilidades.LstTextBox.Add(txtApellido1);

            objUtilidades.ValidarTextBox(objUtilidades.LstTextBox);

        }

        private void BlanquearCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido1.Text = string.Empty;
            txtApellido2.Text = string.Empty;
           // dtpFechaNacimiento.Value = DateTime.MinValue;
            chkEstado.Checked = true;

            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnAgregar.Enabled = true;

        }

        private void CargasListaUsuarios ()
        {
            objUsuario = new ClsUsuario();
            objUsuarioLn.Index(ref objUsuario);

            if (objUsuario.MensajeError == null)
            {
                gvUsuarios.DataSource = objUsuario.DtResultados;
                objUtilidades.FormatoDataGridWiew(ref gvUsuarios);
                gvUsuarios.Columns[0].DisplayIndex = gvUsuarios.ColumnCount - 1;
            }
            else
            {
                MessageBox.Show( objUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ValidarCampos();

            if (objUtilidades.MensajeError == null)
            {
                objUsuario = new ClsUsuario()
                {
                    Nombre = txtNombre.Text,
                    Apellido1 = txtApellido1.Text,
                    Apellido2 = txtApellido2.Text,
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Estado = chkEstado.Checked
                };

                objUsuarioLn.Create(ref objUsuario);

                if (objUsuario.MensajeError == null)
                {
                    // MessageBox.Show("El Id: "+objUsuario.ValorScalar+ ",fue agregado correctamente");
                    MessageBox.Show("El Usuario: " + objUsuario.Nombre + ",fue agregado correctamente");
                    CargasListaUsuarios();
                    BlanquearCampos();
                }
                else
                {
                    MessageBox.Show(objUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(objUtilidades.MensajeError.ToString(), "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Esta seguro que desea actualizar el registro de: " + objUsuario.Nombre + "?", "Mensaje del sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (respuesta == DialogResult.OK)
            {
                objUsuario = new ClsUsuario()
                {
                    IdUsuario = Convert.ToByte(lblIdUsuario.Text),
                    Nombre = txtNombre.Text,
                    Apellido1 = txtApellido1.Text,
                    Apellido2 = txtApellido2.Text,
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Estado = chkEstado.Checked
                };

                objUsuarioLn.Update(ref objUsuario);

                if (objUsuario.MensajeError == null)
                {
                    // MessageBox.Show("El Id: "+objUsuario.ValorScalar+ ",fue agregado correctamente");
                    MessageBox.Show("El Usuario: " + objUsuario.Nombre + ", fue actualizado correctamente.");
                    CargasListaUsuarios();
                    BlanquearCampos();
                }
                else
                {
                    MessageBox.Show(objUsuario.MensajeError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void gvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvUsuarios.Columns[e.ColumnIndex].Name == "Editar")
                {
                    objUsuario = new ClsUsuario()
                    {
                        IdUsuario = Convert.ToByte(gvUsuarios.Rows[e.RowIndex].Cells["IdUsuario"].Value.ToString())
                    };

                    lblIdUsuario.Text = objUsuario.IdUsuario.ToString(); 

                    objUsuarioLn.Read(ref objUsuario);

                    txtNombre.Text = objUsuario.Nombre;
                    txtApellido1.Text = objUsuario.Apellido1;
                    txtApellido2.Text = objUsuario.Apellido2;
                    dtpFechaNacimiento.Value = objUsuario.FechaNacimiento;
                    chkEstado.Checked = objUsuario.Estado;

                    btnActualizar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnAgregar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Esta seguro que desea eliminar el registro de: "+ objUsuario.Nombre+"?", "Mensaje del sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (respuesta == DialogResult.OK)
            {
                objUsuario = new ClsUsuario()
                {
                    IdUsuario = Convert.ToByte(lblIdUsuario.Text)
                };

                objUsuarioLn.Delete(ref objUsuario);
                CargasListaUsuarios();
                BlanquearCampos();
            }         

        }
    }
}
