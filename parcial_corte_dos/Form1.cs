using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static parcial_corte_dos.Form1;

namespace parcial_corte_dos
{
    public partial class Form1 : Form
    {
        // Definición de las constantes
        const double ValorPorHoraAuto = 2000;
        const double ValorPorHoraMoto = 1200;

        // Definición de la lista de vehículos como una variable miembro de la clase
        List<Vehiculo> vehiculos = new List<Vehiculo>();

        // Variables de clase para llevar un seguimiento de los totales
        double totalDineroRecaudado = 0.0;
        int totalCantidadVehiculos = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // Definición de la clase Vehiculo
        public class Vehiculo
        {
            public int Tipo { get; set; }
            public string Marca { get; set; }
            public string Placa { get; set; }
            public DateTime HoraEntrada { get; set; }
            public DateTime HoraSalida { get; set; }
            public double Costo { get; set; }

            public Vehiculo(int tipo, string marca, string placa, DateTime horaEntrada, DateTime horaSalida, double costo)
            {
                Tipo = tipo;
                Marca = marca;
                Placa = placa;
                HoraEntrada = horaEntrada;
                HoraSalida = horaSalida;
                Costo = costo;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            int tipo = txtTipo.SelectedIndex;
            string marca = txtMarca.Text;
            string placa = txtPlaca.Text;
            DateTime horaEntrada = DateTime.Parse(txtHoraEntrada.Text);
            DateTime horaSalida = DateTime.Parse(txtHoraSalida.Text);

            TimeSpan tiempoEstacionado = horaSalida - horaEntrada;
            double costo = CalcularCosto(tiempoEstacionado, CalcularValor(tipo));

            Vehiculo vehiculo = new Vehiculo(tipo, marca, placa, horaEntrada, horaSalida, costo);
            vehiculos.Add(vehiculo);

            //Se Agrega informacion a la tabla
            dgvVehiculos.Rows.Add(tipo, marca, placa, horaEntrada, horaSalida, costo);

            totalDineroRecaudado += vehiculo.Costo;
            totalCantidadVehiculos++;

            // Actualizar las etiquetas en el formulario con los valores calculados
            lblDineroRecaudado.Text = "Dinero recaudado: $" + totalDineroRecaudado.ToString("0.00");
            lblCantidadVehiculos.Text = "Cantidad de vehículos: " + totalCantidadVehiculos.ToString();
        }

        private double CalcularValor(int tipo)
        {
            double valor = 0;

            if (tipo == 0)
            {
                valor = ValorPorHoraAuto;
            }
            else
            {
          
                valor = ValorPorHoraMoto;
            }

            return valor;
        }

        private double CalcularCosto(TimeSpan tiempoEstacionado, double valorPorHora)
        {
            double costo = 0;

            // Calcula el tiempo en minutos redondeando hacia arriba
            int minutosTotales = (int)Math.Ceiling(tiempoEstacionado.TotalMinutes);
            label12.Text = minutosTotales.ToString();
            if (minutosTotales >= 60)
            {
                int horasCompletas = minutosTotales / 60;
                int minutosRestantes = minutosTotales % 60;
                label7.Text = horasCompletas.ToString();
                label8.Text = minutosRestantes.ToString();

                costo = horasCompletas * valorPorHora;

                label9.Text = costo.ToString();

                // Calcula el costo adicional por fracción cada 15 minutos
                if (minutosRestantes > 0)
                {
                    int fracciones = minutosRestantes / 15;
                    //costo += fracciones * valorPorFraccion;
                    costo += fracciones * (valorPorHora / 4);
                    label10.Text = fracciones.ToString();

                }
            }
            else
            {
                // Si el tiempo estacionado es menor a una hora, se aplica el valor por fracción
                label11.Text = "else";
                costo = valorPorHora / 4;
            }

            return costo;
        }


    }
}
