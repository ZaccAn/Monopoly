﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Monopoly.Classi;

namespace Monopoly
{
    /// <summary>
    /// Logica di interazione per WindowIpoteca.xaml
    /// </summary>
    public partial class WindowIpoteca : Window
    {
        Giocatore Corrente;
        int GuadagnoTotale;
        MainWindow Principale;

        public WindowIpoteca(Giocatore G, MainWindow Pr)
        {
            InitializeComponent();
            Corrente = G;
            Principale = Pr;
            foreach (Proprieta P in Corrente.Proprieta)
            {
                CheckBox C = new CheckBox();
                C.HorizontalAlignment = HorizontalAlignment.Left;
                C.VerticalAlignment = VerticalAlignment.Top;
                C.Content = P.Nome;
                if (P.Speciale)
                    C.Background = Brushes.DarkGray;
                else
                    C.Background = P.Colore;
                C.Width = 200;
                C.Height = 20;
                C.Checked += CambiaSelezione;
                Stack_Proprieta.Children.Add(C);
            }
        }

        private void CambiaSelezione(object sender, RoutedEventArgs e)
        {
            CheckBox C = (CheckBox)sender;
            Proprieta P = (Proprieta)Corrente.Proprieta[Stack_Proprieta.Children.IndexOf(C)];
            if ((bool)C.IsChecked)
                GuadagnoTotale += P.Costo / 2;
            else
                GuadagnoTotale += P.Costo / 2;

            TextBox_Guadagno.Text = GuadagnoTotale.ToString();
        }

        private void Chiudi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfermaIpoteca(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Risposta = MessageBox.Show("Sei sicuro di voler ipotecare queste proprietà per L." + GuadagnoTotale+ "?", "Conferma", MessageBoxButton.OKCancel);

            if (Risposta.HasFlag(MessageBoxResult.OK))
            {
                foreach (object O in Stack_Proprieta.Children)
                {
                    CheckBox C = (CheckBox)O;
                    Proprieta P = (Proprieta)Corrente.Proprieta[Stack_Proprieta.Children.IndexOf(C)];
                    if ((bool)C.IsChecked)
                        Corrente.Vendi(P);
                }
                this.Close();
            }
        }
    }
}
