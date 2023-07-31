using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DEV2023
{
    public partial class Form1 : Form
    {
        clsPaises objPaises;
        clsLenguajes objLenguajes;
        clsTotales objTotales;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            try
            {
                objPaises = new clsPaises();
                objLenguajes = new clsLenguajes();
                objTotales = new clsTotales();


            }
            catch (Exception error)
            {
                MessageBox.Show("No se pudo cargar la base de datoss", "Error", MessageBoxButtons.OK);
                Application.Exit();
            }

            objPaises.CargarLista(listView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            DataTable tablaLenguajes = objLenguajes.GetTablaLenguajes();
            DataTable tablaPaises = objPaises.GetTabla();
            DataTable tablaTotales = objTotales.GetTablaTotales();
            
            Int32 Totales = 0;

            
            foreach (ListViewItem item in listView1.CheckedItems)
            {
                chart1.Series.Clear();
                foreach (DataRow filaLenguaje in tablaLenguajes.Rows)
                {
                    Series serie = chart1.Series.Add(filaLenguaje["nombre"].ToString());

                    foreach (DataRow filaTotales in tablaTotales.Rows)
                    {
                        if (filaTotales["pais"].ToString() == item.Tag.ToString())
                        {
                            Totales = Totales + (int)filaTotales["cantidad"];
                            
                        }
                        serie.Points.AddXY(item.Text, filaTotales["cantidad"]);
                        lblStrip.Text = "Total Programadores =" + Totales.ToString();
                    }
                    Totales = 0;
                }
           
            }
            

        }
    }
}
