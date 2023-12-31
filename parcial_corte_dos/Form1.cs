﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static parcial_corte_dos.Form1;

namespace parcial_corte_dos
{
    public partial class Form1 : Form
    {
        // Definición de las constantes
        const double ValorPorHora = 5.0; // Cambia este valor según tus necesidades
        const double ValorPorFraccion = 2.0; // Cambia este valor según tus necesidades

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
            public string Tipo { get; set; }
            public string Marca { get; set; }
            public string Placa { get; set; }
            public DateTime HoraEntrada { get; set; }
            public DateTime HoraSalida { get; set; }
            public double Costo { get; set; }

            public Vehiculo(string tipo, string marca, string placa, DateTime horaEntrada, DateTime horaSalida, double costo)
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
            string tipo = txtTipo.Text;
            string marca = txtMarca.Text;
            string placa = txtPlaca.Text;
            DateTime horaEntrada = DateTime.Parse(txtHoraEntrada.Text);
            DateTime horaSalida = DateTime.Parse(txtHoraSalida.Text);

            TimeSpan tiempoEstacionado = horaSalida - horaEntrada;
            double costo = CalcularCosto(tiempoEstacionado, ValorPorHora, ValorPorFraccion);

            Vehiculo vehiculo = new Vehiculo(tipo, marca, placa, horaEntrada, horaSalida, costo);
            vehiculos.Add(vehiculo);

            // Puedes mostrar la información del vehículo registrado en una lista o en un DataGridView.
            // Por ejemplo, si tienes un DataGridView llamado dgvVehiculos:
            dgvVehiculos.Rows.Add(tipo, marca, placa, horaEntrada, horaSalida, costo);

            totalDineroRecaudado += vehiculo.Costo;
            totalCantidadVehiculos++;

            // Actualizar las etiquetas en el formulario con los valores calculados
            lblDineroRecaudado.Text = "Dinero recaudado: $" + totalDineroRecaudado.ToString("0.00");
            lblCantidadVehiculos.Text = "Cantidad de vehículos: " + totalCantidadVehiculos.ToString();
        }

        private double CalcularCosto(TimeSpan tiempoEstacionado, double valorPorHora, double valorPorFraccion)
        {
            double costo = 0;

            if (tiempoEstacionado.TotalHours >= 1)
            {
                int horas = (int)tiempoEstacionado.TotalHours;
                double minutos = tiempoEstacionado.Minutes;

                if (minutos > 0)
                {
                    // Si hay fracción de hora, se cuenta como una hora completa
                    horas += 1;
                }

                costo = horas * valorPorHora;
            }
            else
            {
                // Si el tiempo estacionado es menor a una hora, se aplica el valor por fracción
                costo = valorPorFraccion;
            }

            return costo;
        }


    }
}
