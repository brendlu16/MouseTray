using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MouseTray
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Vychozi.vychoziSens = Vychozi.ZjistitSens();
            SensText.Text = Vychozi.ZjistitSens().ToString();
            Vychozi.vychoziScroll = Vychozi.ZjistitScroll();
            DoubleClickText.Text = Vychozi.ZjistitDoubleClick().ToString();
            Vychozi.vychoziDoubleClick = Vychozi.ZjistitDoubleClick();
            ScrollText.Text = Vychozi.ZjistitScroll().ToString();
            SensSlider.Value = Vychozi.vychoziSens;
            ScrollSlider.Value = Vychozi.vychoziScroll;
            DoubleClickSlider.Value = Vychozi.vychoziDoubleClick;
            SensSlider.ValueChanged += SensSlider_ValueChanged;
            ScrollSlider.ValueChanged += ScrollSlider_ValueChanged;
            DoubleClickSlider.ValueChanged += DoubleClickSlider_ValueChanged;
            Vypsat();
        }

        public void Vypsat()
        {
            ProfilyList.Items.Clear();
            //List<Profil> profily = FileManager.NacistProfily();
            List<Profil> profily = HttpManager.NacistVse();
            foreach (var item in profily)
            {
                ListViewItem listViewItem = new ListViewItem { Width = 275 };
                Button button = new Button { Width = 265, Content = item.Nazev };
                button.Click += Button_Click2;
                listViewItem.Content = button;
                ProfilyList.Items.Add(listViewItem);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Zmena.ZmenitSens(Vychozi.vychoziSens);
            Zmena.ZmenitScroll(Vychozi.vychoziScroll);
            Zmena.ZmenitDoubleClick(Vychozi.vychoziDoubleClick);
            System.Windows.Application.Current.Shutdown();
        }

        private void SensSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            Zmena.ZmenitSens((uint)slider.Value);
            SensText.Text = Vychozi.ZjistitSens().ToString();
            ProfilSelector.Text = "Žádný profil";
        }
        private void ScrollSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            Zmena.ZmenitScroll((uint)slider.Value);
            ScrollText.Text = Vychozi.ZjistitScroll().ToString();
            ProfilSelector.Text = "Žádný profil";
        }
        private void DoubleClickSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            Zmena.ZmenitDoubleClick((uint)slider.Value);
            DoubleClickText.Text = Vychozi.ZjistitDoubleClick().ToString();
            ProfilSelector.Text = "Žádný profil";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Profil profil = new Profil { Nazev = NazevInput.Text, DoubleClick = Vychozi.ZjistitDoubleClick(), Scroll = Vychozi.ZjistitScroll(), Sens = Vychozi.ZjistitSens() };
            FileManager.UlozitProfil(profil);
            NazevInput.Text = "";
            Vypsat();
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Profil profil = FileManager.NacistProfil(button.Content.ToString());
            ProfilSelector.Text = profil.Nazev;
            Zmena.ZmenitDoubleClick(profil.DoubleClick);
            Zmena.ZmenitScroll(profil.Scroll);
            Zmena.ZmenitSens(profil.Sens);

            SensText.Text = Vychozi.ZjistitSens().ToString();
            DoubleClickText.Text = Vychozi.ZjistitDoubleClick().ToString();
            ScrollText.Text = Vychozi.ZjistitScroll().ToString();
            SensSlider.Value = Vychozi.ZjistitSens();
            ScrollSlider.Value = Vychozi.ZjistitScroll();
            DoubleClickSlider.Value = Vychozi.ZjistitDoubleClick();
        }
    }
}
