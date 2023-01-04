using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPrueba.Utilidades
{
    public class ClsUtilidades
    {
        private string _MensajeError;
        private List<TextBox> _LstTextBox;

        public string MensajeError { get => _MensajeError; set => _MensajeError = value; }
        public List<TextBox> LstTextBox { get => _LstTextBox; set => _LstTextBox = value; }

        public void FormatoDataGridWiew(ref DataGridView gv)
        {
            DataGridViewCellStyle estilo = gv.ColumnHeadersDefaultCellStyle;
            estilo.Alignment = DataGridViewContentAlignment.MiddleCenter;
            estilo.Font = new Font(gv.Font, FontStyle.Bold);
            // gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //gv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gv.AllowUserToAddRows = false;
            gv.AllowUserToDeleteRows = false;
            gv.ReadOnly = true;

        }

        public void ValidarTextBox(List<TextBox> LstTxtBox)
        {
            MensajeError = null;

            foreach (TextBox txt in LstTxtBox)
            {
                if (txt.Text.Equals(string.Empty))
                {
                    MensajeError = MensajeError + "\n" + txt.Name.Remove(0,2) + ", No puede estar en blanco.";
                }
            }
        }
    }
}
