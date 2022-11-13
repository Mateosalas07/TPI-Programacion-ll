using AutomotrizApp.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomotrizFront
{
    public partial class frmAltaVehiculo : Form
    {
        public frmAltaVehiculo()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Debe ingresar una fecha menor a la actual","Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtColor.Text == "")
            {
                MessageBox.Show("Debe ingresar un color!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (cboModelo.Text.Equals(String.Empty))
            {
                MessageBox.Show("Debe seleccionar un Modelo!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtPrecio.Text=="")
            {
                MessageBox.Show("Debe ingresar un precio!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dateTimePicker1.Value.Equals(String.Empty))
            {
                MessageBox.Show("Debe seleccionar una fecha", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
          //  Vehiculo v = new Vehiculo();
            //v.Año = dateTimePicker1.Value;
            //v.Color = txtColor.Text;
            //v.Precio = Convert.ToInt32(txtPrecio.Text);
            //v.Modelo = Convert.ToInt32(cboModelo.SelectedIndex);
        }

        private void cboModelo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
